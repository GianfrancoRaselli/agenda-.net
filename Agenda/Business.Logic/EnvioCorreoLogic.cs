using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class EnvioCorreoLogic
    {
        public void EnviarCorreoRecuperarClave(Usuario userARecuperarClave, int codigoRecuperarClave)
        {
            SmtpClient smtp = null;

            try
            {
                smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("cc09bfa28d2419eb834e847ef0b2c585", "e91339c7c4a0641dcff7721160dde1c6");
                smtp.Port = 587;
                smtp.Host = "in-v3.mailjet.com";
                smtp.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("miagendacorreoelectronico@gmail.com", "Recuperar Contraseña", System.Text.Encoding.UTF8);
                mail.To.Add(userARecuperarClave.email);
                mail.Subject = "Recuperar Contraseña";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Código: " + codigoRecuperarClave;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;

                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al enviar correo", e);
                throw excepcionManejada;
            }
            finally
            {
                if (smtp != null) smtp.Dispose();
            }
        }

        public void EnviarCorreoConfirmarCorreo(Usuario userACrearCuenta, int codigoCrearCuenta)
        {
            SmtpClient smtp = null;

            try
            {
                smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("cc09bfa28d2419eb834e847ef0b2c585", "e91339c7c4a0641dcff7721160dde1c6");
                smtp.Port = 587;
                smtp.Host = "in-v3.mailjet.com";
                smtp.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("miagendacorreoelectronico@gmail.com", "Confirmar Correo", System.Text.Encoding.UTF8);
                mail.To.Add(userACrearCuenta.email);
                mail.Subject = "Confirmar Correo";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Código: " + codigoCrearCuenta;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;

                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al enviar correo", e);
                throw excepcionManejada;
            }
            finally
            {
                if (smtp != null) smtp.Dispose();
            }
        }
    }
}
