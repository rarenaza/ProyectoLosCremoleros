using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public static class LNCorreo
    {
        public static void EnviarCorreo(Mensaje mensaje)
        {
            try
            {
                MailMessage Message = new MailMessage("aldo.chocos@pucp.pe", "aldo.chocos@criteriait.com");
                //MailMessage Message = new MailMessage(mensaje.DeUsuarioCorreoElectronico, mensaje.ParaUsuarioCorreoElectronico);
                Message.Body = mensaje.MensajeTexto;
                Message.Subject = mensaje.Asunto;

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.ipage.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential("aldao@criteriait.com", "P4$$w0rd");  //Cuenta en criteria para el envío de correos.
                client.Send(Message);
            }
            catch (Exception ex)
            {

                int a = 0;
            }
        }
    }
}
