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
using System.Text;

namespace UTPPrototipo.Controllers
{
    public class PlantillaController : Controller
    {
        // GET: Plantilla
        public ActionResult Index()
        {
            return View();
        }

        public FileResult GenerarCV(int idCV)
        {
            //1. Obtener información del alumno.
            //int idCV = 3; //Demo

            LNPlantillaCV lnPlantilla = new LNPlantillaCV();
            DataSet dsResultado = lnPlantilla.ObtenerDatosParaPlantilla(idCV);

            MemoryStream stream = new MemoryStream();

            using (FileStream fileStream = System.IO.File.OpenRead(Server.MapPath("~/Plantillas/PlantillaVER3.docx")))
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

                //Se obtiene las tablas que vienen en el dataset.
                DataTable dtEstudios = dsResultado.Tables[1]; //SQL
                DataTable dtExperiencia = dsResultado.Tables[2]; //SQL
                DataTable dtInfoAdicional = dsResultado.Tables[3]; //SQL

                //1. Se completa la información de estudios. Se guarda en temporal para obtener el diseño en el Word y no se pierda el orden de las tablas al insertar filas en éstas.
                Table tblTempEstudios = doc.Tables[1]; //Plantilla Word
                Table tblTempExperiencia = doc.Tables[2]; //Plantilla Word
                Table tblTempInfoAdicional = doc.Tables[3]; //Plantilla Word

                Table tblEstudios = tblTempEstudios.InsertTableAfterSelf(dtEstudios.Rows.Count, 2);
                Table tblExperiencia = tblTempExperiencia.InsertTableAfterSelf(dtExperiencia.Rows.Count, 2);
                Table tblInfoAdicional = tblTempInfoAdicional.InsertTableAfterSelf(dtInfoAdicional.Rows.Count, 1); //Se inserta las filas y columnas.

                //Se pasa el diseño del word a la nueva tabla.
                tblEstudios.Design = tblTempEstudios.Design;
                tblExperiencia.Design = tblTempEstudios.Design;
                tblInfoAdicional.Design = tblTempInfoAdicional.Design;
                //tblEstudios.AutoFit = AutoFit.ColumnWidth;

                //Se recorre la tabla de estudios y se completa la data:
                #region Se recorre la tabla de estudios y se completa la data:
                for (int fila = 0; fila <= tblEstudios.RowCount - 1; fila++)
                {                    
                    for (int celda = 0; celda <= tblEstudios.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblEstudios.Rows[fila].Cells[celda].Paragraphs[0];

                        string institucion = Convert.ToString(dtEstudios.Rows[fila]["Institucion"]);
                        string estudio = Convert.ToString(dtEstudios.Rows[fila]["Estudio"]);
                        int fechaInicioMes = Convert.ToInt32(dtEstudios.Rows[fila]["FechaInicioMes"]);
                        int fechaInicioAno = Convert.ToInt32(dtEstudios.Rows[fila]["FechaInicioAno"]);
                        int fechaFinMes = 0;
                        if (dtEstudios.Rows[fila]["FechaFinMes"] != DBNull.Value)
                        {
                            fechaFinMes = Convert.ToInt32(dtEstudios.Rows[fila]["FechaFinMes"]);
                        }
                        int fechaFinAno = 0;
                        if (dtEstudios.Rows[fila]["FechaFinAno"] != DBNull.Value)
                        {
                            fechaFinAno = Convert.ToInt32(dtEstudios.Rows[fila]["FechaFinAno"]);
                        }
                        string periodo = ConvertirMes(fechaInicioMes) + fechaInicioAno.ToString().Substring(2, 2);
                        if (fechaFinMes > 0 && fechaFinAno > 0)
                        {
                            periodo = periodo + " - " + ConvertirMes(fechaFinMes) + fechaFinAno.ToString().Substring(2, 2);  
                        }
                        if (celda == 0)
                        {
                            tblEstudios.Rows[fila].Cells[celda].Width = 120;
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
                #endregion

                #region Se recorre la tabla de experiencia (WORD) y se completa los datos:

                for (int fila = 0; fila <= tblExperiencia.RowCount - 1; fila++)
                {
                    for (int celda = 0; celda <= tblExperiencia.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblExperiencia.Rows[fila].Cells[celda].Paragraphs[0];

                        if (fila == 0) //Se agrega un salto de línea en cada celda de la fila.
                        {
                            cell_paragraph.AppendLine();
                        }

                        //Si es la primera celda de la fila =>
                        if (celda == 0)
                        {
                            int fechaInicioMes = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaInicioCargoMes"]);
                            int fechaInicioAno = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaInicioCargoAno"]);
                            //int fechaFinMes = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaFinCargoMes"]);
                            //int fechaFinAno = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaFinCargoAno"]);

                            ////Se arma el texto del periodo.
                            //string periodo = ConvertirMes(fechaInicioMes) + fechaInicioAno.ToString().Substring(2, 2) + " - " + ConvertirMes(fechaFinMes) + fechaFinAno.ToString().Substring(2, 2);
                            int fechaFinMes = 0;
                            if (dtExperiencia.Rows[fila]["FechaFinCargoMes"] != DBNull.Value)
                            {
                                fechaFinMes = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaFinCargoMes"]);
                            }
                            int fechaFinAno = 0;
                            if (dtExperiencia.Rows[fila]["FechaFinCargoAno"] != DBNull.Value)
                            {
                                fechaFinAno = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaFinCargoAno"]);
                            }
                            string periodo = ConvertirMes(fechaInicioMes) + fechaInicioAno.ToString().Substring(2, 2);
                            if (fechaFinMes > 0 && fechaFinAno > 0)
                            {
                                periodo = periodo + " - " + ConvertirMes(fechaFinMes) + fechaFinAno.ToString().Substring(2, 2);
                            }
                            else 
                            {
                                periodo = periodo + " - Act";
                            }


                            tblExperiencia.Rows[fila].Cells[celda].Width = 120;  //Se coloca el ancho de la celda.
                            cell_paragraph.Append(periodo); //Se indica el valor.
                            cell_paragraph.Font(new FontFamily("Arial")).FontSize(9); //Se establece el formato de la celda.
                        }
                        else
                            if (celda == 1)
                            {
                                string empresa = Convert.ToString(dtExperiencia.Rows[fila]["Empresa"]);
                                string descripcionEmpresa = Convert.ToString(dtExperiencia.Rows[fila]["DescripcionEmpresa"]);
                                string ciudad = Convert.ToString(dtExperiencia.Rows[fila]["Ciudad"]);
                                string pais = Convert.ToString(dtExperiencia.Rows[fila]["PaisDescripcion"]);
                                string cargo = Convert.ToString(dtExperiencia.Rows[fila]["NombreCargo"]);
                                string descripcionCargo = Convert.ToString(dtExperiencia.Rows[fila]["DescripcionCargo"]);

                                tblExperiencia.Rows[fila].Cells[celda].Width = 500; //Se coloca el ancho.                                
                                cell_paragraph.Append(empresa + " (" + ciudad + " - " + pais + ")");
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9).Bold();

                                cell_paragraph.AppendLine(descripcionEmpresa);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                                cell_paragraph.AppendLine(cargo);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                                cell_paragraph.AppendLine(descripcionCargo);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                                cell_paragraph.AppendLine(); //Salto de línea para separar los cargos.
                            }
                            //else
                            //    cell_paragraph.InsertText("no existen datos", false);
                    }
                }

                #endregion

                #region Se recorre la tabla de informacion adicional y se completa los datos:
                for (int fila = 0; fila <= tblInfoAdicional.RowCount - 1; fila++)
                {
                    //Actualmente sólo va a haber una columna en esta tabla. Este for sólo va a recorrer una vez.
                    for (int celda = 0; celda <= tblInfoAdicional.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblInfoAdicional.Rows[fila].Cells[celda].Paragraphs[0];

                        //Se obtiene la data de la tabla que viene de la BD.
                        //string tipoConocimiento = "tipoConocimiento"; // Convert.ToString(dtInfoAdicional.Rows[fila]["TipoConocimientoDescripcion"]);
                        string conocimiento = Convert.ToString(dtInfoAdicional.Rows[fila]["Conocimiento"]);
                        string nivelConocimientoDescripcion = Convert.ToString(dtInfoAdicional.Rows[fila]["NivelConocimientoDescripcion"]);
                        string fechaDesde = Convert.ToString(dtInfoAdicional.Rows[fila]["FechaConocimientoDesdeAno"]);
                        string fechaHasta = Convert.ToString(dtInfoAdicional.Rows[fila]["FechaConocimientoHastaAno"]);
                        //string pais = "pais"; //Convert.ToString(dtInfoAdicional.Rows[fila]["PaisDescripcion"]);
                        //string ciudad = "ciudad"; //Convert.ToString(dtInfoAdicional.Rows[fila]["Ciudad"]);
                        string institucionEstudio = Convert.ToString(dtInfoAdicional.Rows[fila]["InstituciónDeEstudio"]);
                        //string aniosExperiencia = "aniosExperiencia"; //Convert.ToString(dtInfoAdicional.Rows[fila]["AñosExperiencia"]);

                        //Se arma la cadena de cada fila en la tabla de Información adicional.
                        StringBuilder infoAdicional = new StringBuilder();
                        infoAdicional.Append(conocimiento + " ");
                        infoAdicional.Append(nivelConocimientoDescripcion + ", ");
                        infoAdicional.Append(fechaDesde + "-");
                        infoAdicional.Append(fechaHasta + ". ");
                        infoAdicional.Append(institucionEstudio + ".");

                        tblInfoAdicional.Rows[fila].Cells[celda].Width = 600;                        
                        cell_paragraph.AppendLine(infoAdicional.ToString());
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9); //Se da formato a la nueva fila.

                        //cell_paragraph.AppendLine(nivelConocimientoDescripcion);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        //cell_paragraph.AppendLine(fechaDesde);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        //cell_paragraph.AppendLine(fechaHasta);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        //cell_paragraph.AppendLine(institucionEstudio);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                      
                        cell_paragraph.AppendLine();
                    }
                }
                #endregion
                //Se recorre la tabla de informacion adicional y se completa los datos:
                
                //Se eliminal las tablas temporales:
                tblTempEstudios.Remove();
                tblTempExperiencia.Remove();
                tblTempInfoAdicional.Remove();
                                            
               doc.Save();               
            
            }
      
            string codigoAlumno = Convert.ToString(dsResultado.Tables[0].Rows[0]["CodAlumnoUtp"]);

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
            DataSet dsResultado = lnPlantilla.ObtenerDatosParaPlantilla(idCV);

            MemoryStream stream = new MemoryStream();

            using (FileStream fileStream = System.IO.File.OpenRead(rutaPlantilla))
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

                //Se obtiene las tablas que vienen en el dataset.
                DataTable dtEstudios = dsResultado.Tables[1]; //SQL
                DataTable dtExperiencia = dsResultado.Tables[2]; //SQL
                DataTable dtInfoAdicional = dsResultado.Tables[3]; //SQL

                //1. Se completa la información de estudios. Se guarda en temporal para obtener el diseño en el Word y no se pierda el orden de las tablas al insertar filas en éstas.
                Table tblTempEstudios = doc.Tables[1]; //Plantilla Word
                Table tblTempExperiencia = doc.Tables[2]; //Plantilla Word
                Table tblTempInfoAdicional = doc.Tables[3]; //Plantilla Word

                Table tblEstudios = tblTempEstudios.InsertTableAfterSelf(dtEstudios.Rows.Count, 2);
                Table tblExperiencia = tblTempExperiencia.InsertTableAfterSelf(dtExperiencia.Rows.Count, 2);
                Table tblInfoAdicional = tblTempInfoAdicional.InsertTableAfterSelf(dtInfoAdicional.Rows.Count, 1); //Se inserta las filas y columnas.

                //Se pasa el diseño del word a la nueva tabla.
                tblEstudios.Design = tblTempEstudios.Design;
                tblExperiencia.Design = tblTempEstudios.Design;
                tblInfoAdicional.Design = tblTempInfoAdicional.Design;
                //tblEstudios.AutoFit = AutoFit.ColumnWidth;

                //Se recorre la tabla de estudios y se completa la data:
                #region Se recorre la tabla de estudios y se completa la data:
                for (int fila = 0; fila <= tblEstudios.RowCount - 1; fila++)
                {
                    for (int celda = 0; celda <= tblEstudios.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblEstudios.Rows[fila].Cells[celda].Paragraphs[0];

                        string institucion = Convert.ToString(dtEstudios.Rows[fila]["Institucion"]);
                        string estudio = Convert.ToString(dtEstudios.Rows[fila]["Estudio"]);
                        int fechaInicioMes = Convert.ToInt32(dtEstudios.Rows[fila]["FechaInicioMes"]);
                        int fechaInicioAno = Convert.ToInt32(dtEstudios.Rows[fila]["FechaInicioAno"]);
                        int fechaFinMes = Convert.ToInt32(dtEstudios.Rows[fila]["FechaFinMes"]);
                        int fechaFinAno = Convert.ToInt32(dtEstudios.Rows[fila]["FechaFinAno"]);

                        string periodo = ConvertirMes(fechaInicioMes) + fechaInicioAno.ToString().Substring(2, 2) + " - " + ConvertirMes(fechaFinMes) + fechaFinAno.ToString().Substring(2, 2);

                        if (celda == 0)
                        {
                            tblEstudios.Rows[fila].Cells[celda].Width = 120;
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
                #endregion

                #region Se recorre la tabla de experiencia (WORD) y se completa los datos:

                for (int fila = 0; fila <= tblExperiencia.RowCount - 1; fila++)
                {
                    for (int celda = 0; celda <= tblExperiencia.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblExperiencia.Rows[fila].Cells[celda].Paragraphs[0];

                        if (fila == 0) //Se agrega un salto de línea en cada celda de la fila.
                        {
                            cell_paragraph.AppendLine();
                        }

                        //Si es la primera celda de la fila =>
                        if (celda == 0)
                        {
                            int fechaInicioMes = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaInicioCargoMes"]);
                            int fechaInicioAno = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaInicioCargoAno"]);
                            int fechaFinMes = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaFinCargoMes"]);
                            int fechaFinAno = Convert.ToInt32(dtExperiencia.Rows[fila]["FechaFinCargoAno"]);

                            //Se arma el texto del periodo.
                            string periodo = ConvertirMes(fechaInicioMes) + fechaInicioAno.ToString().Substring(2, 2) + " - " + ConvertirMes(fechaFinMes) + fechaFinAno.ToString().Substring(2, 2);

                            tblExperiencia.Rows[fila].Cells[celda].Width = 120;  //Se coloca el ancho de la celda.
                            cell_paragraph.Append(periodo); //Se indica el valor.
                            cell_paragraph.Font(new FontFamily("Arial")).FontSize(9); //Se establece el formato de la celda.
                        }
                        else
                            if (celda == 1)
                            {
                                string empresa = Convert.ToString(dtExperiencia.Rows[fila]["Empresa"]);
                                string descripcionEmpresa = Convert.ToString(dtExperiencia.Rows[fila]["DescripcionEmpresa"]);
                                string ciudad = Convert.ToString(dtExperiencia.Rows[fila]["Ciudad"]);
                                string pais = Convert.ToString(dtExperiencia.Rows[fila]["PaisDescripcion"]);
                                string cargo = Convert.ToString(dtExperiencia.Rows[fila]["NombreCargo"]);
                                string descripcionCargo = Convert.ToString(dtExperiencia.Rows[fila]["DescripcionCargo"]);

                                tblExperiencia.Rows[fila].Cells[celda].Width = 500; //Se coloca el ancho.                                
                                cell_paragraph.Append(empresa + " (" + ciudad + " - " + pais + ")");
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9).Bold();

                                cell_paragraph.AppendLine(descripcionEmpresa);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                                cell_paragraph.AppendLine(cargo);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                                cell_paragraph.AppendLine(descripcionCargo);
                                cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                                cell_paragraph.AppendLine(); //Salto de línea para separar los cargos.
                            }
                        //else
                        //    cell_paragraph.InsertText("no existen datos", false);
                    }
                }

                #endregion

                #region Se recorre la tabla de informacion adicional y se completa los datos:
                for (int fila = 0; fila <= tblInfoAdicional.RowCount - 1; fila++)
                {
                    //Actualmente sólo va a haber una columna en esta tabla. Este for sólo va a recorrer una vez.
                    for (int celda = 0; celda <= tblInfoAdicional.Rows[fila].Cells.Count - 1; celda++)
                    {
                        Paragraph cell_paragraph = tblInfoAdicional.Rows[fila].Cells[celda].Paragraphs[0];

                        //Se obtiene la data de la tabla que viene de la BD.
                        //string tipoConocimiento = "tipoConocimiento"; // Convert.ToString(dtInfoAdicional.Rows[fila]["TipoConocimientoDescripcion"]);
                        string conocimiento = Convert.ToString(dtInfoAdicional.Rows[fila]["Conocimiento"]);
                        string nivelConocimientoDescripcion = Convert.ToString(dtInfoAdicional.Rows[fila]["NivelConocimientoDescripcion"]);
                        string fechaDesde = Convert.ToString(dtInfoAdicional.Rows[fila]["FechaConocimientoDesdeAno"]);
                        string fechaHasta = Convert.ToString(dtInfoAdicional.Rows[fila]["FechaConocimientoHastaAno"]);
                        //string pais = "pais"; //Convert.ToString(dtInfoAdicional.Rows[fila]["PaisDescripcion"]);
                        //string ciudad = "ciudad"; //Convert.ToString(dtInfoAdicional.Rows[fila]["Ciudad"]);
                        string institucionEstudio = Convert.ToString(dtInfoAdicional.Rows[fila]["InstituciónDeEstudio"]);
                        //string aniosExperiencia = "aniosExperiencia"; //Convert.ToString(dtInfoAdicional.Rows[fila]["AñosExperiencia"]);

                        //Se arma la cadena de cada fila en la tabla de Información adicional.
                        StringBuilder infoAdicional = new StringBuilder();
                        infoAdicional.Append(conocimiento + " ");
                        infoAdicional.Append(nivelConocimientoDescripcion + ", ");
                        infoAdicional.Append(fechaDesde + "-");
                        infoAdicional.Append(fechaHasta + ". ");
                        infoAdicional.Append(institucionEstudio + ".");

                        tblInfoAdicional.Rows[fila].Cells[celda].Width = 600;
                        cell_paragraph.AppendLine(infoAdicional.ToString());
                        cell_paragraph.Font(new FontFamily("Arial")).FontSize(9); //Se da formato a la nueva fila.

                        //cell_paragraph.AppendLine(nivelConocimientoDescripcion);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        //cell_paragraph.AppendLine(fechaDesde);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        //cell_paragraph.AppendLine(fechaHasta);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);
                        //cell_paragraph.AppendLine(institucionEstudio);
                        //cell_paragraph.Font(new FontFamily("Arial")).FontSize(9);

                        cell_paragraph.AppendLine();
                    }
                }
                #endregion
                //Se recorre la tabla de informacion adicional y se completa los datos:

                //Se eliminal las tablas temporales:
                tblTempEstudios.Remove();
                tblTempExperiencia.Remove();
                tblTempInfoAdicional.Remove();

                doc.Save();

            }

            return stream;
        }


        public FileResult DescargarDesdeBD(int idOfertaPostulante)
        {
            LNPlantillaCV lnPlantilla = new LNPlantillaCV();

            DataTable dtResultado = lnPlantilla.ObtenerDocumentoCV(idOfertaPostulante);

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