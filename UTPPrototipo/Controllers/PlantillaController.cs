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
using System.Runtime.InteropServices;

using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Common;
using UTPPrototipo.Utiles;

using Microsoft.Office.Interop.Word;
using Novacode;
using Mustache;
using UTP.PortalEmpleabilidad.Modelo;

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
                case "non-empty":
                    output = (text != String.Empty ? Regex.Unescape(filterValue) : String.Empty);
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
        private RegexOptions REGEX = RegexOptions.ECMAScript;
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
            List<Novacode.Table> blockTemplates;

            try
            {
                Novacode.Table blockTemplatesWrapper = word
                    .Tables
                    .Where(e => e.Paragraphs[0].Text.Contains(blockPattern))
                    .ToList()[0];

                blockTemplates = this.CascadeBlock(blockTemplatesWrapper, new List<Novacode.Table>());
            }
            catch (Exception e)
            {
                blockTemplates = new List<Novacode.Table>();
            }

            return new
            {
                name = model,
                inlines = inlineTemplates,
                blocks = blockTemplates
            };
        }

        public List<Novacode.Table> CascadeBlock(Novacode.Table block, List<Novacode.Table> blocks)
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
        [HttpGet]
        public ActionResult GenerarPDFMasivo()
        {
            return View();
        }

        [ActionName("GenerarPDFMasivo")]
        [HttpPost]
        public ActionResult GenerarPDFMasivo_POST()
        {

            LNOfertaPostulante objOfertaPostulante = new LNOfertaPostulante();
            string fileSourcePath = Server.MapPath("~/Plantillas/template.docx");

            System.Data.DataTable dt = objOfertaPostulante.OfertaPostulantesListar();

            OfertaPostulante objOfertaPostulanteBE;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objOfertaPostulanteBE = new OfertaPostulante();
                objOfertaPostulante = new LNOfertaPostulante();

                try
                {
                    objOfertaPostulanteBE.IdOfertaPostulante = Convert.ToInt32(dt.Rows[i]["IdOfertaPostulante"].ToString());
                    objOfertaPostulanteBE.IdOferta = Convert.ToInt32(dt.Rows[i]["IdOferta"].ToString());
                    objOfertaPostulanteBE.IdAlumno = Convert.ToInt32(dt.Rows[i]["IdAlumno"].ToString());
                    objOfertaPostulanteBE.IdCV = Convert.ToInt32(dt.Rows[i]["IdCV"].ToString());
                    objOfertaPostulanteBE.ModificadoPor = "SYSTEM";

                    objOfertaPostulanteBE.DocumentoCV = Word2PDF(CrearCurriculum(objOfertaPostulanteBE.IdCV, fileSourcePath).ToArray());

                    objOfertaPostulante.Actualizar(objOfertaPostulanteBE);
                }
                catch (Exception)
                {

                }
            }

            return Content("");
        }

        // GET: Plantilla
        public ActionResult Index()
        {
            return View();
        }

        public FileResult GenerarWord(int idCV)
        {
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();

            System.Data.DataTable dtResultado = lnPlantilla.ObtenerDatosParaPlantilla(idCV).Tables[0];

            string nombres = Convert.ToString(dtResultado.Rows[0]["Nombres"]);
            string apellidos = Convert.ToString(dtResultado.Rows[0]["Apellidos"]);
            string filename = String.Format("CV - {0} {1}.docx", nombres, apellidos);

            string filePath = Server.MapPath("~/Plantillas/template.docx");

            return File(this.CrearCurriculum(idCV, filePath).ToArray(), "application/octet-stream", filename);
        }

        public FileResult GenerarPDF(int idCV)
        {
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();

            System.Data.DataTable dtResultado = lnPlantilla.ObtenerDatosParaPlantilla(idCV).Tables[0];

            string nombres = Convert.ToString(dtResultado.Rows[0]["Nombres"]);
            string apellidos = Convert.ToString(dtResultado.Rows[0]["Apellidos"]);
            string filename = String.Format("CV - {0} {1}.pdf", nombres, apellidos);

            string fileSourcePath = Server.MapPath("~/Plantillas/template.docx");

            return File(this.Word2PDF(this.CrearCurriculum(idCV, fileSourcePath).ToArray()), "application/octet-stream", filename);
        }

        public FileResult DescargarConvenioPlantilla(int idPlantilla)
        {
            byte[] fileBytes = null;
            string fileName = null;
            switch (idPlantilla)
            {
                case 1:
                    fileName = "modelo-de-convenio-de-practicas-pre-profesionales.doc";
                    fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Plantillas/" + fileName));
                    break;
                case 2:
                    fileName = "modelo-de-convenio-de-practicas-profesionales.doc";
                    fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Plantillas/" + fileName));
                    break;
                case 3:
                    fileName = "plan-de-capacitacion.doc";
                    fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Plantillas/" + fileName));
                    break;
                case 4:
                    fileName = "ley-sobre-modalidades-formativas-laborales.pdf";
                    fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Plantillas/" + fileName));
                    break;
                default:
                    break;
            }         
   
            return File(fileBytes, "application/octec-stream", fileName);
        }

        public byte[] Word2PDF(byte[] file)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "tmp\\";
            string word = dir + System.Guid.NewGuid() + ".docx";
            string pdf = word.Replace("docx", "pdf");
            byte[] output;

            try
            {
                if (!System.IO.Directory.Exists(dir)) 
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                using (FileStream fs = System.IO.File.Create(word))
                {
                    fs.Write(file, 0, file.Length);
                    fs.Close();
                }
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                app.Visible = false;
                app.Documents.Open(word).SaveAs2(pdf, WdExportFormat.wdExportFormatPDF);
                app.Documents.Close();
                app.Quit();
                output = System.IO.File.ReadAllBytes(pdf);

                // Clean temporal files
                System.IO.File.Delete(word);
                System.IO.File.Delete(pdf);
            }
            catch(Exception e)
            {
                output = new byte[] { };
                throw e;
            }

            return output;
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
            System.Data.DataTable Person = Result.Tables[0];
            System.Data.DataTable Education = Result.Tables[1];
            System.Data.DataTable Experience = Result.Tables[2];
            System.Data.DataTable AditionalInformation = Result.Tables[3];

            string blockPattern = @"{{><model>}}";
            string celphonePattern = @"(\d{3})";
            RegexOptions REGEX = RegexOptions.ECMAScript;

            MemoryStream stream = new MemoryStream();
            Mustache mustache = new Mustache();

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
                    firstname = Convert.ToString(Person.Rows[0]["Nombres"]).ToUpper(),
                    lastname = Convert.ToString(Person.Rows[0]["Apellidos"]).ToUpper(),
                    document = Convert.ToString(Person.Rows[0]["NumeroDocumento"]),
                    documentType = Convert.ToString(Person.Rows[0]["TipoDocumento"]),
                    address = Helper.TitleCase(Convert.ToString(Person.Rows[0]["Direccion"])),
                    district = Helper.TitleCase(Convert.ToString(Person.Rows[0]["DireccionDistrito"])),
                    celphone = String.Join("-", Regex.Split(Convert.ToString(Person.Rows[0]["TelefonoCelular"]), celphonePattern, REGEX).Where(w => w != String.Empty).ToArray()),
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
                Novacode.Table educationWrapper;

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
                                    : ConvertirMes(Convert.ToInt32(data["FechaFinMes"])) + Convert.ToString(data["FechaFinAno"]).Substring(2, 2)),
                            cycle = Convert.ToInt32(data["CicloEquivalente"] == DBNull.Value ? 0 : data["CicloEquivalente"]) == 0
                                ? String.Empty
                                : String.Format("{0} Ciclo", CycleStudy(Convert.ToInt32(data["CicloEquivalente"]))),
                            merit = Convert.ToString(data["Merito"])
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
                Novacode.Table experienceWrapper;

                Dictionary<string, dynamic> enterprises = new Dictionary<string, dynamic>();
                Dictionary<string, List<object>> experiences = new Dictionary<string, List<object>>();
                string[] enterpriseFields = new string[] { "Ciudad", "DescripcionEmpresa", "PaisDescripcion", "Empresa" };

                System.Data.DataTable Enterprises = Experience.DefaultView.ToTable(true, enterpriseFields);
                foreach (DataRow enterprise in Enterprises.Rows)
                {
                    List<object> aux = new List<object>();
                    int timeOfExperience = 0;

                    System.Data.DataTable experiencesOfEnterprise = Experience.Select("Empresa = '" + Convert.ToString(enterprise["Empresa"]) + "'").CopyToDataTable();
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
                            office = Helper.TitleCase(Convert.ToString(data["NombreCargo"])),
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

                        timeOfExperience += (yearEnd - yearStart) * 12 + monthEnd - monthStart + 1;
                    }

                    enterprises.Add(Convert.ToString(enterprise["Empresa"]), new
                    {
                        enterprise = Helper.TitleCase(Convert.ToString(enterprise["Empresa"])),
                        enterpriseDescription = Convert.ToString(enterprise["DescripcionEmpresa"]),
                        enterpriseTimeOfExperience = (Convert.ToInt32(Math.Truncate(timeOfExperience / 12.0)) == 0)
                            ? String.Format("({0} " + Pluralize(timeOfExperience % 12, "mes", "es") + ")", timeOfExperience % 12)
                            : String.Format("({0} " + Pluralize(Convert.ToInt32(Math.Truncate(timeOfExperience / 12.0)), "año") + ", {1} " + Pluralize(timeOfExperience % 12, "mes", "es") + ")", Math.Truncate(timeOfExperience / 12.0), timeOfExperience % 12),
                        enterpriseCountry = Helper.TitleCase(Convert.ToString(enterprise["PaisDescripcion"])),
                        enterpriseCity = Helper.TitleCase(Convert.ToString(enterprise["Ciudad"]))
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
                        Novacode.Table enterpriseWrapper = experienceWrapper.InsertTableAfterSelf(template.experience.blocks[1]);

                        // Render enterprise info
                        foreach (string i in template.experience.inlines)
                        {
                            doc.ReplaceText(i, mustache.Compile(i).Render(new { experience = enterprises[data.Key] }));
                        }

                        Novacode.Table enterpriseExperienceWrapper = enterpriseWrapper.Rows[1].Tables[0];
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
                Novacode.Table aditionalInformationWrapper;

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
        
        public string Pluralize(int quantity, string text, string suffix = "s")
        {
            return quantity != 1 ? text + suffix : text;
        }

        public string CycleStudy(int cycle)
        {
            string[] cycles = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

            return cycle == 0 ? String.Empty : cycles[cycle - 1];
        }

        public FileResult DescargarDesdeBD(string idOfertaPostulante)
        {
            int idOfertaPostulanteEntero = Convert.ToInt32(Helper.Desencriptar(idOfertaPostulante));
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();

            System.Data.DataTable dtResultado = lnPlantilla.ObtenerDocumentoCV(idOfertaPostulanteEntero);

            byte[] arrayCV = (byte[])dtResultado.Rows[0]["DocumentoCV"];

            string nombres = Convert.ToString(dtResultado.Rows[0]["Nombres"]);
            string apellidos = Convert.ToString(dtResultado.Rows[0]["Apellidos"]);
            string filename = String.Format("CV - {0} {1}.pdf", nombres, apellidos);

            return File(arrayCV, "application/octet-stream", filename);       
        }

        public string ConvertirMes(int mes)
        {
            string[] meses = new string[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Dic" };

            return mes == 0 ? String.Empty : meses[mes - 1];
        }
    }


}