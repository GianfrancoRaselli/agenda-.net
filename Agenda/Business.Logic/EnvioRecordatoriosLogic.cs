using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Business.Entities;
using Data.Database;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace Business.Logic
{
    public class EnvioRecordatoriosLogic
    {
        public void Start()
        {
            Thread hiloAudicionRecordatorios = new Thread(ComenzarAudicionRecordatorios);
            hiloAudicionRecordatorios.Start();
        }

        private void ComenzarAudicionRecordatorios()
        {
            EventoAdapter eventoData = new EventoAdapter();
            List<Evento> eventosAEnviarRecordatorio = null;

            while (true)
            {
                eventosAEnviarRecordatorio = eventoData.BuscarEventosAEnviarRecordatorio();

                if (eventosAEnviarRecordatorio != null && eventosAEnviarRecordatorio.Count > 0)
                {
                    foreach (Evento ev in eventosAEnviarRecordatorio)
                    {
                        try
                        {
                            EnviarRecordatorio(ev);
                            eventoData.ActualizarRecordatorioAEnviado(ev);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }

        private void EnviarRecordatorio(Evento ev)
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
                mail.From = new MailAddress("miagendacorreoelectronico@gmail.com", "Recordatorio Evento", System.Text.Encoding.UTF8);
                mail.To.Add(ev.usuario.email);
                mail.Subject = ev.nombre;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                if (ev.todo_el_dia)
                {
                    mail.Body = "Nombre: " + ev.nombre + "\n" +
                                "Descripción: " + ev.descripcion + "\n" +
                                "Fecha Evento: " + ev.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + ev.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + ev.fecha_hora_evento.ToString("dd") + " de " + ev.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + ev.fecha_hora_evento.ToString("yyyy");
                }
                else
                {
                    mail.Body = "Nombre: " + ev.nombre + "\n" +
                                "Descripción: " + ev.descripcion + "\n" +
                                "Fecha Evento: " + ev.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + ev.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + ev.fecha_hora_evento.ToString("dd") + " de " + ev.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + ev.fecha_hora_evento.ToString("yyyy") + "\n" +
                                "Hora Evento: " + ev.fecha_hora_evento.ToShortTimeString() + "\n\n" +
                                "Horario correspondiente a la zona horaria " + ev.usuario.zonaHoraria.descripcion;
                }
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Exception excepcionManejada = new Exception("Error al enviar correo", ex);
                throw excepcionManejada;
            }
            finally
            {
                if (smtp != null) smtp.Dispose();
            }
        }
    }
}
