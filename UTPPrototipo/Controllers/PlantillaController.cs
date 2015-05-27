using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Common;
using UTPPrototipo.Utiles;

using Novacode;
using Mustache;

namespace UTPPrototipo.Controllers
{
    class Mustache
    {
        private string template;
        private string pattern = @"{{([\s\S]+?)}}";
        private string filter = String.Empty;
        private dynamic DELIMITER = new
        {
            Expression = new 
            { 
                Open = "{{", 
                Close = "}}" 
            },
            Filter = new {
                Pointer = "||",
                Value = ":"
            }
        };

        public Mustache Compile(string template)
        {
            string[] source = Regex.Replace(template, this.pattern, "$1").Split(
                new string[] { this.DELIMITER.Filter.Pointer },
                StringSplitOptions.None
            );

            this.template = this.DELIMITER.Expression.Open + source[0].Trim() + this.DELIMITER.Expression.Close;
            this.filter = String.Empty;

            if (source.Count() > 1)
            {
                this.filter = source[1];
            }

            return this;
        }

        public string Render(object context)
        {
            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(this.template);
            string result;
            try
            {
                result = generator.Render(context).Trim();
            }
            catch (Exception e)
            {
                result = this.template;
            }

            if (this.filter != String.Empty)
            {
                result = this.FilterCompile(result, this.filter);
            }

            return result;
        }

        private string FilterCompile(string text, string filter)
        {
            string output = String.Empty;
            string[] filterProperties = filter.Split(
                new string[] { this.DELIMITER.Filter.Value },
                StringSplitOptions.None
            );
            string filterName = filterProperties[0].Trim();
            string filterValue = filterProperties[1];

            switch (filterName)
            {
                case "prefix":
                    output = (text != String.Empty ? Regex.Unescape(filterValue) + text : String.Empty);
                    break;
                case "suffix":
                    output = (text != String.Empty ? text + Regex.Unescape(filterValue) : String.Empty);
                    break;
                case "default":
                    output = (text == String.Empty ? Regex.Unescape(filterValue) : text);
                    break;
            }

            return output;
        }
    }

    class Template
    {
        private string Source;
        private string Pattern;
        private string ModelPattern;
        private string PointerPattern;
        private System.Text.RegularExpressions.RegexOptions REGEX = System.Text.RegularExpressions.RegexOptions.ECMAScript;
        private MemoryStream Stream;

        public Template(string Source, string Pattern = @"{{<regex>}}")
        {
            this.Source = Source;
            this.Pattern = Pattern;
            this.ModelPattern = Pattern
                .Replace("<regex>", @"<model>\.([\s\S]+?)");
            this.PointerPattern = Pattern
                .Replace("<regex>", @"><model>");
        }

        public Template Build()
        {
            this.Stream = new MemoryStream();

            using (FileStream Document = File.OpenRead(this.Source))
            {
                this.Stream.SetLength(Document.Length);
                Document.Read(this.Stream.GetBuffer(), 0, (int)Document.Length);
            }

            return this;
        }

        public object Compile()
        {
            object person;
            object education;
            object experience;
            object aditionalInformation;

            using (DocX word = DocX.Load(this.Stream))
            {
                person = Tagify(word, "person");
                education = Tagify(word, "education");
                experience = Tagify(word, "experience");
                aditionalInformation = Tagify(word, "aditionalInformation");

                word.Save();
            }

            return new
            {
                person = person,
                education = education,
                experience = experience,
                aditionalInformation = aditionalInformation
            };
        }

        public object Tagify(DocX word, string model)
        {
            // Patterns
            string inlinePattern = this.ModelPattern.Replace("<model>", model);
            string blockPattern = this.PointerPattern.Replace("<model>", model);

            // Get inline templates 
            List<string> inlineTemplates = word.FindUniqueByPattern(inlinePattern, this.REGEX);

            // Get block templates
            List<Table> blockTemplates;

            try
            {
                Table blockTemplatesWrapper = word
                    .Tables
                    .Where(e => e.Paragraphs[0].Text.Contains(blockPattern))
                    .ToList()[0];

                blockTemplates = this.CascadeBlock(blockTemplatesWrapper, new List<Table>());
            }
            catch (Exception e)
            {
                blockTemplates = new List<Table>();
            }

            return new
            {
                name = model,
                inlines = inlineTemplates,
                blocks = blockTemplates
            };
        }

        public List<Table> CascadeBlock(Table block, List<Table> blocks)
        {
            blocks.Add(block);

            try
            {
                block = block.Rows[1].Tables[0];

                return this.CascadeBlock(block, blocks);
            }
            catch (Exception e)
            {
                return blocks;
            }
        }
    }

    [LogPortal]
    public class PlantillaController : Controller
    {
        // GET: Plantilla
        public ActionResult Index()
        {
            return View();
        }

        public FileResult GenerarCV(int idCV)
        {
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();
            DataSet Result = lnPlantilla.ObtenerDatosParaPlantilla(idCV);
            DataTable Person = Result.Tables[0];
            DataTable Education = Result.Tables[1];
            DataTable Experience = Result.Tables[2];
            DataTable AditionalInformation = Result.Tables[3];

            MemoryStream stream = new MemoryStream();

            string filePath = Server.MapPath("~/Plantillas/template.docx");
            Mustache mustache = new Mustache();
            string blockPattern = @"{{><model>}}";

            using (FileStream fileStream = System.IO.File.OpenRead(filePath))
            {
                stream.SetLength(fileStream.Length);
                fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
            }

            using (DocX doc = DocX.Load(stream))
            {
                dynamic template = new Template(filePath)
                    .Build()
                    .Compile();

                #region person

                object person = new
                {
                    // Informacion basica
                    firstname = Convert.ToString(Person.Rows[0]["Nombres"]).ToUpper(),
                    lastname = Convert.ToString(Person.Rows[0]["Apellidos"]).ToUpper(),
                    document = Convert.ToString(Person.Rows[0]["NumeroDocumento"]),
                    documentType = Convert.ToString(Person.Rows[0]["TipoDocumento"]),
                    address = Convert.ToString(Person.Rows[0]["Direccion"]),
                    district = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Person.Rows[0]["DireccionDistrito"])),
                    celphone = Convert.ToString(Person.Rows[0]["TelefonoCelular"]),
                    email = Convert.ToString(Person.Rows[0]["CorreoElectronico"]),
                    emailAlternative = Convert.ToString(Person.Rows[0]["CorreoElectronico2"]),
                    profile = Convert.ToString(Person.Rows[0]["Perfil"]),
                    birthdate = Convert.ToString(Person.Rows[0]["FechaNacimiento"])
                };

                if (Convert.ToBoolean(template.person.inlines.Count))
                {
                    foreach (string i in template.person.inlines)
                    {
                        doc.ReplaceText(i, mustache.Compile(i).Render(new { person = person }));
                    }
                }

                #endregion

                #region education
                
                 object education;
                 string educationPattern = blockPattern.Replace("<model>", "education");
                 Table educationWrapper;

                try
                {
                    educationWrapper = doc
                        .Tables
                        .Where(e => e.Paragraphs[0].Text.Contains(educationPattern))
                        .ToList()[0];

                    // Clean reference template
                    educationWrapper.Rows[1].Remove();

                    foreach (DataRow data in Education.Rows) 
                    {
                        educationWrapper.InsertTableAfterSelf(template.education.blocks[1]);

                        education = new
                        {
                            institute = Convert.ToString(data["Institucion"]),
                            study = Convert.ToString(data["Estudio"]),
                            period = ConvertirMes(Convert.ToInt32(data["FechaInicioMes"])) + Convert.ToString(data["FechaInicioAno"]).Substring(2, 2) + 
                                "-" + (data["FechaFinMes"] == DBNull.Value && data["FechaFinAno"] == DBNull.Value 
                                    ? "Cont"
                                    : ConvertirMes(Convert.ToInt32(data["FechaFinMes"])) + Convert.ToString(data["FechaFinAno"]).Substring(2, 2)) 
                        };

                        foreach (string i in template.education.inlines)
                        {
                            doc.ReplaceText(i, mustache.Compile(i).Render(new { education = education }));
                        }
                    }

                    // Clean reference pointer
                    educationWrapper.Rows[0].Remove();
                }
                catch (Exception e) { }

                #endregion

                #region experience

                string experiencePattern = blockPattern.Replace("<model>", "experience");
                Table experienceWrapper;

                Dictionary<string, dynamic> enterprises = new Dictionary<string, dynamic>();
                Dictionary<string, List<object>> experiences = new Dictionary<string, List<object>>();
                string[] enterpriseFields = new string[] { "Ciudad", "DescripcionEmpresa", "PaisDescripcion", "Empresa" };

                DataTable Enterprises = Experience.DefaultView.ToTable(true, enterpriseFields);
                foreach (DataRow enterprise in Enterprises.Rows)
                {
                    List<object> aux = new List<object>();
                    int timeOfExperience = 0;

                    DataTable experiencesOfEnterprise = Experience.Select("Empresa = '" + Convert.ToString(enterprise["Empresa"]) + "'").CopyToDataTable();
                    foreach (DataRow data in experiencesOfEnterprise.Rows)
                    {
                        string period = String.Empty;
                        string periodStart = String.Empty;
                        string periodEnd = String.Empty;

                        periodStart = ConvertirMes(Convert.ToInt32(data["FechaInicioCargoMes"])) + 
                            Convert.ToString(data["FechaInicioCargoAno"]).Substring(2, 2);

                        periodEnd = (data["FechaFinCargoMes"] == DBNull.Value && data["FechaFinCargoAno"] == DBNull.Value)
                            ? "Cont"
                            : ConvertirMes(Convert.ToInt32(data["FechaFinCargoMes"])) + 
                                Convert.ToString(data["FechaFinCargoAno"]).Substring(2, 2);

                        period = String.Format("{0}-{1}", periodStart, periodEnd);

                        aux.Add(new
                        {
                            period =  period,
                            office = Convert.ToString(data["NombreCargo"]),
                            officeDescription = Convert.ToString(data["DescripcionCargo"])
                        });

                        int yearStart = Convert.ToInt32(data["FechaInicioCargoAno"]);
                        int monthStart = Convert.ToInt32(data["FechaInicioCargoMes"]);
                        int yearEnd = (data["FechaFinCargoAno"] == DBNull.Value)
                            ? DateTime.Now.Year
                            : Convert.ToInt32(data["FechaFinCargoAno"]);
                        int monthEnd = (data["FechaFinCargoMes"] == DBNull.Value)
                            ? DateTime.Now.Month
                            : Convert.ToInt32(data["FechaFinCargoMes"]);

                        timeOfExperience += (yearEnd - yearStart) * 12 + monthEnd - monthStart;
                     }

                    enterprises.Add(Convert.ToString(enterprise["Empresa"]), new 
                    {
                        enterprise = Convert.ToString(enterprise["Empresa"]),
                        enterpriseDescription = Convert.ToString(enterprise["DescripcionEmpresa"]),
                        enterpriseTimeOfExperience = String.Format("({0} años, {1} meses)", Math.Truncate(timeOfExperience / 12.0), timeOfExperience % 12),
                        enterpriseCountry = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(enterprise["PaisDescripcion"])),
                        enterpriseCity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(enterprise["Ciudad"]))
                    });
                    experiences.Add(Convert.ToString(enterprise["Empresa"]), aux);
                }

                try
                {
                    experienceWrapper = doc
                        .Tables
                        .Where(e => e.Paragraphs[0].Text.Contains(experiencePattern))
                        .ToList()[0];

                    // Clean reference template
                    experienceWrapper.Rows[1].Remove();

                    foreach (var data in enterprises)
                    {
                        Table enterpriseWrapper = experienceWrapper.InsertTableAfterSelf(template.experience.blocks[1]);

                        // Render enterprise info
                        for (int i = 0; i < enterpriseFields.Count() + 1; i++)
                        {
                            doc.ReplaceText(
                                template.experience.inlines[i],
                                mustache
                                    .Compile(template.experience.inlines[i])
                                    .Render(new
                                    {
                                        experience = enterprises[data.Key]
                                    })
                            );
                        }

                        Table enterpriseExperienceWrapper = enterpriseWrapper.Rows[1].Tables[0];
                        foreach (object experience in experiences[data.Key])
                        {
                            enterpriseExperienceWrapper.InsertTableAfterSelf(template.experience.blocks[2]);

                            foreach (string i in template.experience.inlines)
                            {
                                doc.ReplaceText(i, mustache.Compile(i).Render(new { experience = experience }));
                            }
                        }

                        enterpriseExperienceWrapper.Rows[0].Remove();
                    }

                    // Clean reference pointer
                    experienceWrapper.Rows[0].Remove();
                }
                catch (Exception e) {}

                #endregion

                #region aditional information

                object aditionalInformation;
                string aditionalInformationPattern = blockPattern.Replace("<model>", "aditionalInformation");
                Table aditionalInformationWrapper;

                try
                {
                    aditionalInformationWrapper = doc
                        .Tables
                        .Where(e => e.Paragraphs[0].Text.Contains(aditionalInformationPattern))
                        .ToList()[0];

                    // Clean reference template
                    aditionalInformationWrapper.Rows[1].Remove();

                    foreach (DataRow data in AditionalInformation.Rows)
                    {
                        aditionalInformationWrapper.InsertTableAfterSelf(template.aditionalInformation.blocks[1]);

                        aditionalInformation = new
                        {
                            knowledge = Convert.ToString(data["Conocimiento"]),
                            level = Convert.ToString(data["NivelConocimientoDescripcion"]),
                            institute = Convert.ToString(data["InstituciónDeEstudio"]),
                            date = Convert.ToString(data["FechaConocimientoHastaAno"])
                        };

                        foreach (string i in template.aditionalInformation.inlines)
                        {
                            doc.ReplaceText(i, mustache.Compile(i).Render(new { aditionalInformation = aditionalInformation }));
                        }
                    }

                    // Clean reference pointer
                    aditionalInformationWrapper.Rows[0].Remove();
                }
                catch (Exception e) { }

                #endregion
                                            
               doc.Save();               
            
            }
      
            string codigoAlumno = Convert.ToString(Person.Rows[0]["CodAlumnoUtp"]);

            return File(stream.ToArray(), "application/octet-stream", codigoAlumno + ".docx");            
        }

        /// <summary>
        /// Se utiliza para crear el CV que se carga a la BD.
        /// </summary>
        /// <param name="idCV"></param>
        /// <param name="rutaPlantilla"></param>
        /// <returns></returns>
        public MemoryStream CrearCurriculum(int idCV, string rutaPlantilla)
        {
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();
            DataSet Result = lnPlantilla.ObtenerDatosParaPlantilla(idCV);
            DataTable Person = Result.Tables[0];
            DataTable Education = Result.Tables[1];
            DataTable Experience = Result.Tables[2];
            DataTable AditionalInformation = Result.Tables[3];

            MemoryStream stream = new MemoryStream();

            Mustache mustache = new Mustache();
            string blockPattern = @"{{><model>}}";

            using (FileStream fileStream = System.IO.File.OpenRead(rutaPlantilla))
            {
                stream.SetLength(fileStream.Length);
                fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
            }

            using (DocX doc = DocX.Load(stream))
            {
                dynamic template = new Template(rutaPlantilla)
                   .Build()
                   .Compile();

                #region person

                object person = new
                {
                    // Informacion basica
                    firstname = Convert.ToString(Person.Rows[0]["Nombres"]).ToUpper(),
                    lastname = Convert.ToString(Person.Rows[0]["Apellidos"]).ToUpper(),
                    document = Convert.ToString(Person.Rows[0]["NumeroDocumento"]),
                    documentType = Convert.ToString(Person.Rows[0]["TipoDocumento"]),
                    address = Convert.ToString(Person.Rows[0]["Direccion"]),
                    district = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Person.Rows[0]["DireccionDistrito"])),
                    celphone = Convert.ToString(Person.Rows[0]["TelefonoCelular"]),
                    email = Convert.ToString(Person.Rows[0]["CorreoElectronico"]),
                    emailAlternative = Convert.ToString(Person.Rows[0]["CorreoElectronico2"]),
                    profile = Convert.ToString(Person.Rows[0]["Perfil"]),
                    birthdate = Convert.ToString(Person.Rows[0]["FechaNacimiento"])
                };

                if (Convert.ToBoolean(template.person.inlines.Count))
                {
                    foreach (string i in template.person.inlines)
                    {
                        doc.ReplaceText(i, mustache.Compile(i).Render(new { person = person }));
                    }
                }

                #endregion

                #region education

                object education;
                string educationPattern = blockPattern.Replace("<model>", "education");
                Table educationWrapper;

                try
                {
                    educationWrapper = doc
                        .Tables
                        .Where(e => e.Paragraphs[0].Text.Contains(educationPattern))
                        .ToList()[0];

                    // Clean reference template
                    educationWrapper.Rows[1].Remove();

                    foreach (DataRow data in Education.Rows)
                    {
                        educationWrapper.InsertTableAfterSelf(template.education.blocks[1]);

                        education = new
                        {
                            institute = Convert.ToString(data["Institucion"]),
                            study = Convert.ToString(data["Estudio"]),
                            period = ConvertirMes(Convert.ToInt32(data["FechaInicioMes"])) + Convert.ToString(data["FechaInicioAno"]).Substring(2, 2) +
                                "-" + (data["FechaFinMes"] == DBNull.Value && data["FechaFinAno"] == DBNull.Value
                                    ? "Cont"
                                    : ConvertirMes(Convert.ToInt32(data["FechaFinMes"])) + Convert.ToString(data["FechaFinAno"]).Substring(2, 2))
                        };

                        foreach (string i in template.education.inlines)
                        {
                            doc.ReplaceText(i, mustache.Compile(i).Render(new { education = education }));
                        }
                    }

                    // Clean reference pointer
                    educationWrapper.Rows[0].Remove();
                }
                catch (Exception e) { }

                #endregion

                #region experience

                string experiencePattern = blockPattern.Replace("<model>", "experience");
                Table experienceWrapper;

                Dictionary<string, dynamic> enterprises = new Dictionary<string, dynamic>();
                Dictionary<string, List<object>> experiences = new Dictionary<string, List<object>>();
                string[] enterpriseFields = new string[] { "Ciudad", "DescripcionEmpresa", "PaisDescripcion", "Empresa" };

                DataTable Enterprises = Experience.DefaultView.ToTable(true, enterpriseFields);
                foreach (DataRow enterprise in Enterprises.Rows)
                {
                    List<object> aux = new List<object>();
                    int timeOfExperience = 0;

                    DataTable experiencesOfEnterprise = Experience.Select("Empresa = '" + Convert.ToString(enterprise["Empresa"]) + "'").CopyToDataTable();
                    foreach (DataRow data in experiencesOfEnterprise.Rows)
                    {
                        string period = String.Empty;
                        string periodStart = String.Empty;
                        string periodEnd = String.Empty;

                        periodStart = ConvertirMes(Convert.ToInt32(data["FechaInicioCargoMes"])) +
                            Convert.ToString(data["FechaInicioCargoAno"]).Substring(2, 2);

                        periodEnd = (data["FechaFinCargoMes"] == DBNull.Value && data["FechaFinCargoAno"] == DBNull.Value)
                            ? "Cont"
                            : ConvertirMes(Convert.ToInt32(data["FechaFinCargoMes"])) +
                                Convert.ToString(data["FechaFinCargoAno"]).Substring(2, 2);

                        period = String.Format("{0}-{1}", periodStart, periodEnd);

                        aux.Add(new
                        {
                            period = period,
                            office = Convert.ToString(data["NombreCargo"]),
                            officeDescription = Convert.ToString(data["DescripcionCargo"])
                        });

                        int yearStart = Convert.ToInt32(data["FechaInicioCargoAno"]);
                        int monthStart = Convert.ToInt32(data["FechaInicioCargoMes"]);
                        int yearEnd = (data["FechaFinCargoAno"] == DBNull.Value)
                            ? DateTime.Now.Year
                            : Convert.ToInt32(data["FechaFinCargoAno"]);
                        int monthEnd = (data["FechaFinCargoMes"] == DBNull.Value)
                            ? DateTime.Now.Month
                            : Convert.ToInt32(data["FechaFinCargoMes"]);

                        timeOfExperience += (yearEnd - yearStart) * 12 + monthEnd - monthStart;
                    }

                    enterprises.Add(Convert.ToString(enterprise["Empresa"]), new
                    {
                        enterprise = Convert.ToString(enterprise["Empresa"]),
                        enterpriseDescription = Convert.ToString(enterprise["DescripcionEmpresa"]),
                        enterpriseTimeOfExperience = String.Format("({0} años, {1} meses)", Math.Truncate(timeOfExperience / 12.0), timeOfExperience % 12),
                        enterpriseCountry = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(enterprise["PaisDescripcion"])),
                        enterpriseCity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(enterprise["Ciudad"]))
                    });
                    experiences.Add(Convert.ToString(enterprise["Empresa"]), aux);
                }

                try
                {
                    experienceWrapper = doc
                        .Tables
                        .Where(e => e.Paragraphs[0].Text.Contains(experiencePattern))
                        .ToList()[0];

                    // Clean reference template
                    experienceWrapper.Rows[1].Remove();

                    foreach (var data in enterprises)
                    {
                        Table enterpriseWrapper = experienceWrapper.InsertTableAfterSelf(template.experience.blocks[1]);

                        // Render enterprise info
                        for (int i = 0; i < enterpriseFields.Count() + 1; i++)
                        {
                            doc.ReplaceText(
                                template.experience.inlines[i],
                                mustache
                                    .Compile(template.experience.inlines[i])
                                    .Render(new
                                    {
                                        experience = enterprises[data.Key]
                                    })
                            );
                        }

                        Table enterpriseExperienceWrapper = enterpriseWrapper.Rows[1].Tables[0];
                        foreach (object experience in experiences[data.Key])
                        {
                            enterpriseExperienceWrapper.InsertTableAfterSelf(template.experience.blocks[2]);

                            foreach (string i in template.experience.inlines)
                            {
                                doc.ReplaceText(i, mustache.Compile(i).Render(new { experience = experience }));
                            }
                        }

                        enterpriseExperienceWrapper.Rows[0].Remove();
                    }

                    // Clean reference pointer
                    experienceWrapper.Rows[0].Remove();
                }
                catch (Exception e) { }

                #endregion

                #region aditional information

                object aditionalInformation;
                string aditionalInformationPattern = blockPattern.Replace("<model>", "aditionalInformation");
                Table aditionalInformationWrapper;

                try
                {
                    aditionalInformationWrapper = doc
                        .Tables
                        .Where(e => e.Paragraphs[0].Text.Contains(aditionalInformationPattern))
                        .ToList()[0];

                    // Clean reference template
                    aditionalInformationWrapper.Rows[1].Remove();

                    foreach (DataRow data in AditionalInformation.Rows)
                    {
                        aditionalInformationWrapper.InsertTableAfterSelf(template.aditionalInformation.blocks[1]);

                        aditionalInformation = new
                        {
                            knowledge = Convert.ToString(data["Conocimiento"]),
                            level = Convert.ToString(data["NivelConocimientoDescripcion"]),
                            institute = Convert.ToString(data["InstituciónDeEstudio"]),
                            date = Convert.ToString(data["FechaConocimientoHastaAno"])
                        };

                        foreach (string i in template.aditionalInformation.inlines)
                        {
                            doc.ReplaceText(i, mustache.Compile(i).Render(new { aditionalInformation = aditionalInformation }));
                        }
                    }

                    // Clean reference pointer
                    aditionalInformationWrapper.Rows[0].Remove();
                }
                catch (Exception e) { }

                #endregion

                doc.Save();     

            }

            return stream;
        }


        public FileResult DescargarDesdeBD(string idOfertaPostulante)
        {
            int idOfertaPostulanteEntero = Convert.ToInt32(Helper.Desencriptar(idOfertaPostulante));
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();

            DataTable dtResultado = lnPlantilla.ObtenerDocumentoCV(idOfertaPostulanteEntero);

            byte[] arrayCV = (byte[])dtResultado.Rows[0]["DocumentoCV"];
            string codigoAlumno = Convert.ToString(dtResultado.Rows[0]["CodAlumnoUtp"]);

            return File(arrayCV, "application/octet-stream", codigoAlumno + ".docx");       
        }

        public string ConvertirMes(int nroMes)
        {
            string mes = "";

            switch (nroMes)
            {
                case 1: mes = "Ene"; break;
                case 2: mes = "Feb"; break;
                case 3: mes = "Mar"; break;
                case 4: mes = "Abr"; break;
                case 5: mes = "May"; break;
                case 6: mes = "Jun"; break;
                case 7: mes = "Jul"; break;
                case 8: mes = "Ago"; break;
                case 9: mes = "Set"; break;
                case 10: mes = "Oct"; break;
                case 11: mes = "Nov"; break;
                case 12: mes = "Dic"; break;
            }

            return mes;
        }
    }


}