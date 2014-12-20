using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Novacode;
using System.Drawing;
using UTP.PortalEmpleabilidad.Logica;
using System.Data;

namespace UTPPrototipo.Controllers
{
    public class PlantillaController : Controller
    {
        // GET: Plantilla
        public ActionResult Index()
        {
            return View();
        }

        public FileResult GenerarCV()
        {
            //1. Obtener información del alumno.
            int idCV = 1; //Demo

            LNPlantillaCV lnPlantilla = new LNPlantillaCV();
            DataSet dsResultado = lnPlantilla.ObtenerDatosParaPlantilla(idCV);

            MemoryStream stream = new MemoryStream();

            using (FileStream fileStream = System.IO.File.OpenRead(Server.MapPath("~/Plantillas/PlantillaVER2.docx")))
            {
                stream.SetLength(fileStream.Length);
                fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
            }

            //MemoryStream stream = new MemoryStream();

            using (DocX doc = DocX.Load(stream))
            {
                //Pruebas de escritura:
                //Paragraph par = doc.InsertParagraph();
                //par.Append("Esto es una prueba").Font(new FontFamily("Times New Roman")).FontSize(32).Color(Color.Blue).Bold();

                doc.ReplaceText("<NOMBRES>", Convert.ToString(dsResultado.Tables[0].Rows[0]["Nombres"]));
                doc.ReplaceText("<APELLIDOS>", " " + Convert.ToString(dsResultado.Tables[0].Rows[0]["Apellidos"]));
                doc.ReplaceText("<Direccion>", " " + Convert.ToString(dsResultado.Tables[0].Rows[0]["Direccion"]));
                doc.ReplaceText("<DireccionDistrito>", " " + Convert.ToString(dsResultado.Tables[0].Rows[0]["DireccionDistrito"]));
                doc.ReplaceText("<TelefonoCelular>", " | " + Convert.ToString(dsResultado.Tables[0].Rows[0]["TelefonoCelular"]));
                doc.ReplaceText("<CorreoElectronico>", " | " + Convert.ToString(dsResultado.Tables[0].Rows[0]["CorreoElectronico"]));
                doc.ReplaceText("<CorreoElectronico2>", " | " + Convert.ToString(dsResultado.Tables[0].Rows[0]["CorreoElectronico2"]));
                                
                doc.ReplaceText("<Perfil>", Convert.ToString(dsResultado.Tables[0].Rows[0]["Perfil"]));

                //Estudios

                DataTable dtEstudios = dsResultado.Tables[1];
                DataTable dtExperiencia = dsResultado.Tables[2];
                DataTable dtInfoAdicional = dsResultado.Tables[3];

                //1. Se completa la información de estudios:
                Table tblTempEstudios = doc.Tables[1];
                Table tblTempInfoAdicional = doc.Tables[3];

                Table tblEstudios = tblTempEstudios.InsertTableAfterSelf(dtEstudios.Rows.Count, 2);
                //Table tblInfoAdicional = tblTempEstudios.InsertTableAfterSelf(dtInfoAdicional.Rows.Count, 1);
                Table tblInfoAdicional = tblTempEstudios.InsertTableAfterSelf(1, 1); //Se inserta al menos una fila

                tblEstudios.Design = tblTempEstudios.Design;
                tblInfoAdicional.Design = tblTempInfoAdicional.Design;
                //tblEstudios.AutoFit = AutoFit.ColumnWidth;

                //Se recorre la tabla de estudios y se completa la data:
                for (int fila = 0; fila <= tblEstudios.RowCount - 1; fila++)
                {
                    for (int celda = 0; celda <= tblEstudios.Rows[fila].Cells.Count-1; celda++)
                    {
                        Paragraph cell_paragraph = tblEstudios.Rows[fila].Cells[celda].Paragraphs[0];
                        
                        string institucion = Convert.ToString(dtEstudios.Rows[fila]["Institucion"]);
                        string estudio = Convert.ToString(dtEstudios.Rows[fila]["Estudio"]);
                        int fechaInicioMes = Convert.ToInt32(dtEstudios.Rows[fila]["FechaInicioMes"]);
                        int fechaInicioAno = Convert.ToInt32(dtEstudios.Rows[fila]["FechaInicioAno"]);
                        int fechaFinMes = Convert.ToInt32(dtEstudios.Rows[fila]["FechaFinMes"]);
                        int fechaFinAno = Convert.ToInt32(dtEstudios.Rows[fila]["FechaFinAno"]);

                        string periodo = ConvertirMes(fechaInicioMes) + fechaInicioAno.ToString().Substring(2, 2) + "-" + ConvertirMes(fechaFinMes) + fechaFinAno.ToString().Substring(2, 2);

                        if (celda == 0)
                        {
                            tblEstudios.Rows[fila].Cells[celda].Width = 100;
                            cell_paragraph.Append(periodo);
                            cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        }
                        else
                            if (celda == 1)
                            {
                                tblEstudios.Rows[fila].Cells[celda].Width = 400;
                                //cell_paragraph.InsertText(institucion + "\r" + estudio + "\r", false);                                
                                cell_paragraph.Append(institucion);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9).Bold();
                                cell_paragraph.AppendLine(estudio);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                                cell_paragraph.AppendLine();                                
                            }
                            else
                                cell_paragraph.InsertText("no existen datos", false);
                    }
                }

                //Se recorre la tabla de informacion adicional y se completa los datos:
                for (int fila = 0; fila <= tblInfoAdicional.RowCount - 1; fila++)
                {
                    for (int celda = 0; celda <= tblInfoAdicional.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblInfoAdicional.Rows[fila].Cells[celda].Paragraphs[0];

                        string tipoConocimiento = "tipoConocimiento"; // Convert.ToString(dtInfoAdicional.Rows[fila]["TipoConocimientoDescripcion"]);
                        string conocimiento = "conocimiento"; //Convert.ToString(dtInfoAdicional.Rows[fila]["Conocimiento"]);
                        string nivelConocimiento = "nivelConocimiento"; //Convert.ToString(dtInfoAdicional.Rows[fila]["NivelConocimientoDescripcion"]);
                        string pais = "pais"; //Convert.ToString(dtInfoAdicional.Rows[fila]["PaisDescripcion"]);
                        string ciudad = "ciudad"; //Convert.ToString(dtInfoAdicional.Rows[fila]["Ciudad"]);
                        string institucionEstudio = "institucionEstudio"; //Convert.ToString(dtInfoAdicional.Rows[fila]["InstituciónDeEstudio"]);
                        string aniosExperiencia = "aniosExperiencia"; //Convert.ToString(dtInfoAdicional.Rows[fila]["AñosExperiencia"]);

                        cell_paragraph.Append(tipoConocimiento);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9).Bold();
                        cell_paragraph.AppendLine(conocimiento);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        cell_paragraph.AppendLine(nivelConocimiento);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        cell_paragraph.AppendLine(pais);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        cell_paragraph.AppendLine(ciudad);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        cell_paragraph.AppendLine(institucionEstudio);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        cell_paragraph.AppendLine(aniosExperiencia);
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        cell_paragraph.AppendLine();        
                    }
                }

                //Se eliminal las tablas temporales:
                tblTempEstudios.Remove();
                tblTempInfoAdicional.Remove();

               //2. Se completa la experiencia.
               

               //3. Se completa la información adicional
               
               

               doc.Save();               
            
            }
            //MemoryStream stream = new MemoryStream();
            //DocX doc = DocX.Create("");
            string codigoAlumno = Convert.ToString(dsResultado.Tables[0].Rows[0]["CodAlumnoUtp"]);

            return File(stream.ToArray(), "application/octet-stream", codigoAlumno + ".docx");            
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