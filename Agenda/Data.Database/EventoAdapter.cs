using Business.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class EventoAdapter
    {
        public Evento BuscarEvento(Evento ev)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    Evento evento = context.Eventos.Include("usuario.zonaHoraria").Include("color").Where(e => e.id_evento == ev.id_evento).SingleOrDefault();
                    if (evento != null)
                    {
                        if (!evento.todo_el_dia)
                        {
                            evento.fecha_hora_evento = evento.fecha_hora_evento.AddHours(evento.usuario.zonaHoraria.diferencia);
                            evento.fecha_evento_string = evento.fecha_hora_evento.ToString("dd/MM/yyyy");
                            evento.fecha_evento_completa_string = evento.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + evento.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + evento.fecha_hora_evento.ToString("dd") + " de " + evento.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + evento.fecha_hora_evento.ToString("yyyy");
                            evento.hora_evento_string = evento.fecha_hora_evento.ToString("HH:mm");
                        }
                        else
                        {
                            evento.fecha_evento_string = evento.fecha_hora_evento.ToString("dd/MM/yyyy");
                            evento.fecha_evento_completa_string = evento.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + evento.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + evento.fecha_hora_evento.ToString("dd") + " de " + evento.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + evento.fecha_hora_evento.ToString("yyyy");
                            evento.hora_evento_string = null;
                        }
                        if (evento.fecha_hora_recordatorio != null)
                        {
                            evento.fecha_hora_recordatorio = ((DateTime)evento.fecha_hora_recordatorio).AddHours(evento.usuario.zonaHoraria.diferencia);
                            evento.fecha_hora_recordatorio_string = ((DateTime)evento.fecha_hora_recordatorio).ToString("dd/MM/yyyy HH:mm");
                            evento.recordatorio = true;
                        }
                        else
                        {
                            evento.fecha_hora_recordatorio_string = null;
                            evento.recordatorio = false;
                        }
                    }
                    return evento;
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar evento", e);
                throw excepcionManejada;
            }
        }

        public List<Evento> BuscarEventosDelUsuario(Usuario user)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    List<Evento> eventos = context.Eventos.Include("usuario.zonaHoraria").Include("color").Where(e => e.usuario.id_usuario == user.id_usuario).ToList();
                    if (eventos != null && eventos.Count > 0)
                    {
                        foreach (Evento e in eventos)
                        {
                            if (!e.todo_el_dia)
                            {
                                e.fecha_hora_evento = e.fecha_hora_evento.AddHours(e.usuario.zonaHoraria.diferencia);
                                e.fecha_evento_string = e.fecha_hora_evento.ToString("dd/MM/yyyy");
                                e.fecha_evento_completa_string = e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + e.fecha_hora_evento.ToString("dd") + " de " + e.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + e.fecha_hora_evento.ToString("yyyy");
                                e.hora_evento_string = e.fecha_hora_evento.ToString("HH:mm");
                            }
                            else
                            {
                                e.fecha_evento_string = e.fecha_hora_evento.ToString("dd/MM/yyyy");
                                e.fecha_evento_completa_string = e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + e.fecha_hora_evento.ToString("dd") + " de " + e.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + e.fecha_hora_evento.ToString("yyyy");
                                e.hora_evento_string = null;
                            }
                            if (e.fecha_hora_recordatorio != null)
                            {
                                e.fecha_hora_recordatorio = ((DateTime)e.fecha_hora_recordatorio).AddHours(e.usuario.zonaHoraria.diferencia);
                                e.fecha_hora_recordatorio_string = ((DateTime)e.fecha_hora_recordatorio).ToString("dd/MM/yyyy HH:mm");
                                e.recordatorio = true;
                            }
                            else
                            {
                                e.fecha_hora_recordatorio_string = null;
                                e.recordatorio = false;
                            }
                        }
                    }
                    return eventos.OrderBy(e => e.fecha_hora_evento).ThenByDescending(e => e.todo_el_dia).ToList();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar eventos", e);
                throw excepcionManejada;
            }
        }

        public List<Evento> BuscarEventosDelUsuario(Usuario user, DateTime fecha)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    List<Evento> eventos = context.Eventos.Include("usuario.zonaHoraria").Include("color").Where(e => e.usuario.id_usuario == user.id_usuario).ToList();
                    eventos = eventos.Where(e => (e.todo_el_dia == true && 
                                                 e.fecha_hora_evento.Year == fecha.Year &&
                                                 e.fecha_hora_evento.Month == fecha.Month &&
                                                 e.fecha_hora_evento.Day == fecha.Day) ||
                                                 (e.todo_el_dia == false && 
                                                 e.fecha_hora_evento.AddHours(user.zonaHoraria.diferencia).Year == fecha.Year &&
                                                 e.fecha_hora_evento.AddHours(user.zonaHoraria.diferencia).Month == fecha.Month &&
                                                 e.fecha_hora_evento.AddHours(user.zonaHoraria.diferencia).Day == fecha.Day)).OrderByDescending(e => e.todo_el_dia).ThenBy(e => e.fecha_hora_evento).ToList();
                    if (eventos != null && eventos.Count > 0)
                    {
                        foreach (Evento e in eventos)
                        {
                            if (!e.todo_el_dia)
                            {
                                e.fecha_hora_evento = e.fecha_hora_evento.AddHours(e.usuario.zonaHoraria.diferencia);
                                e.fecha_evento_string = e.fecha_hora_evento.ToString("dd/MM/yyyy");
                                e.fecha_evento_completa_string = e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + e.fecha_hora_evento.ToString("dd") + " de " + e.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + e.fecha_hora_evento.ToString("yyyy");
                                e.hora_evento_string = e.fecha_hora_evento.ToString("HH:mm");
                            }
                            else
                            {
                                e.fecha_evento_string = e.fecha_hora_evento.ToString("dd/MM/yyyy");
                                e.fecha_evento_completa_string = e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + e.fecha_hora_evento.ToString("dd") + " de " + e.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + e.fecha_hora_evento.ToString("yyyy");
                                e.hora_evento_string = null;
                            }
                            if (e.fecha_hora_recordatorio != null)
                            {
                                e.fecha_hora_recordatorio = ((DateTime)e.fecha_hora_recordatorio).AddHours(e.usuario.zonaHoraria.diferencia);
                                e.fecha_hora_recordatorio_string = ((DateTime)e.fecha_hora_recordatorio).ToString("dd/MM/yyyy HH:mm");
                                e.recordatorio = true;
                            }
                            else
                            {
                                e.fecha_hora_recordatorio_string = null;
                                e.recordatorio = false;
                            }
                        }
                    }
                    return eventos;
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar eventos", e);
                throw excepcionManejada;
            }
        }

        public void RegistrarEvento(Evento ev)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.Eventos.Add(ev);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al registrar evento", e);
                throw excepcionManejada;
            }
        }

        public void ActualizarEvento(Evento ev)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    Evento eventoAActualizar = context.Eventos.Find(ev.id_evento);

                    if (eventoAActualizar != null)
                    {
                        eventoAActualizar.nombre = ev.nombre;
                        eventoAActualizar.descripcion = ev.descripcion;
                        eventoAActualizar.todo_el_dia = ev.todo_el_dia;
                        eventoAActualizar.fecha_hora_evento = ev.fecha_hora_evento;
                        if (eventoAActualizar.fecha_hora_recordatorio != null)
                        {
                            if (ev.fecha_hora_recordatorio != null)
                            {
                                if (!eventoAActualizar.fecha_hora_recordatorio.Equals(ev.fecha_hora_recordatorio))
                                {
                                    eventoAActualizar.fecha_hora_recordatorio = ev.fecha_hora_recordatorio;
                                    eventoAActualizar.recordatorio_enviado = false;
                                }
                            }
                            else
                            {
                                eventoAActualizar.fecha_hora_recordatorio = null;
                                eventoAActualizar.recordatorio_enviado = null;
                            }
                        }
                        else
                        {
                            if (ev.fecha_hora_recordatorio != null)
                            {
                                eventoAActualizar.fecha_hora_recordatorio = ev.fecha_hora_recordatorio;
                                eventoAActualizar.recordatorio_enviado = false;
                            }
                        }
                        eventoAActualizar.id_color = ev.id_color;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al actualizar evento", e);
                throw excepcionManejada;
            }
        }

        public void EliminarEvento(Evento ev)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    Evento eventoAEliminar = context.Eventos.Find(ev.id_evento);

                    if(eventoAEliminar != null)
                    {
                        context.Eventos.Remove(eventoAEliminar);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al eliminar evento", e);
                throw excepcionManejada;
            }
        }

        public List<Evento> BuscarEventosAEnviarRecordatorio()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    List<Evento> eventos = context.Eventos.Include("usuario.zonaHoraria").Include("color").Where(e => e.fecha_hora_recordatorio <= DateTime.UtcNow && e.recordatorio_enviado == false).OrderBy(e => e.fecha_hora_recordatorio).ToList();
                    if (eventos != null && eventos.Count > 0)
                    {
                        foreach (Evento e in eventos)
                        {
                            if (!e.todo_el_dia)
                            {
                                e.fecha_hora_evento = e.fecha_hora_evento.AddHours(e.usuario.zonaHoraria.diferencia);
                                e.fecha_evento_string = e.fecha_hora_evento.ToString("dd/MM/yyyy");
                                e.fecha_evento_completa_string = e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + e.fecha_hora_evento.ToString("dd") + " de " + e.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + e.fecha_hora_evento.ToString("yyyy");
                                e.hora_evento_string = e.fecha_hora_evento.ToString("HH:mm");
                            }
                            else
                            {
                                e.fecha_evento_string = e.fecha_hora_evento.ToString("dd/MM/yyyy");
                                e.fecha_evento_completa_string = e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + e.fecha_hora_evento.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + e.fecha_hora_evento.ToString("dd") + " de " + e.fecha_hora_evento.ToString("MMMM", new CultureInfo("es-ES")) + " de " + e.fecha_hora_evento.ToString("yyyy");
                                e.hora_evento_string = null;
                            }
                            if (e.fecha_hora_recordatorio != null)
                            {
                                e.fecha_hora_recordatorio = ((DateTime)e.fecha_hora_recordatorio).AddHours(e.usuario.zonaHoraria.diferencia);
                                e.fecha_hora_recordatorio_string = ((DateTime)e.fecha_hora_recordatorio).ToString("dd/MM/yyyy HH:mm");
                                e.recordatorio = true;
                            }
                            else
                            {
                                e.fecha_hora_recordatorio_string = null;
                                e.recordatorio = false;
                            }
                        }
                    }
                    return eventos;
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar eventos", e);
                throw excepcionManejada;
            }
        }

        public void ActualizarRecordatorioAEnviado(Evento ev)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    Evento eventoAActualizar = context.Eventos.Find(ev.id_evento);

                    if (eventoAActualizar != null)
                    {
                        eventoAActualizar.recordatorio_enviado = true;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al actualizar evento", e);
                throw excepcionManejada;
            }
        }
    }
}
