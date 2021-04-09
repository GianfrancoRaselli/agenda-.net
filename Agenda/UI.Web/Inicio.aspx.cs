using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Inicio : System.Web.UI.Page
    {
        Usuario userSesion = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            userSesion = (Usuario)Session["userSesion"];

            if (userSesion == null)
            {
                Response.Redirect("~/Index.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                UsuarioLogic ul = new UsuarioLogic();
                userSesion = ul.BuscarUsuario(userSesion);
                Session["userSesion"] = userSesion;

                Master.FindControl("btnAgregarEvento").Visible = true;
                if (!Page.IsPostBack)
                {
                    cargarColores();
                    cargarZonasHorarias();
                    buscarEventosDeHoy();
                }
            }
        }

        private void cargarColores()
        {
            ListItem item;

            try
            {
                ColorLogic cl = new ColorLogic();

                foreach (Color c in cl.BuscarColores())
                {
                    item = new ListItem(c.descripcion, c.id_color.ToString());
                    if (c.descripcion == "Gris") item.Selected = true;
                    this.colorDropDownList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void cargarZonasHorarias()
        {
            ListItem item;

            try
            {
                ZonaHorariaLogic zhl = new ZonaHorariaLogic();

                foreach (ZonaHoraria zh in zhl.BuscarZonasHorarias())
                {
                    item = new ListItem(zh.descripcion, zh.diferencia.ToString());
                    if (userSesion.zonaHoraria.id_zona_horaria == zh.id_zona_horaria) item.Selected = true;
                    this.zonaHorariaDropDownList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void buscarEventosDeHoy()
        {
            DateTime fechaDeHoy = DateTime.UtcNow.AddHours(userSesion.zonaHoraria.diferencia);
            fechaEventosTextBox.Value = fechaDeHoy.ToString("dd/MM/yyyy");
            buscarEventosDelUsuario(fechaDeHoy);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarEventosDelUsuario(DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null));
        }

        protected void lnkAnterior_Click(object sender, EventArgs e)
        {
            DateTime fechaEventos = DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null).AddDays(-1);
            fechaEventosTextBox.Value = fechaEventos.ToString("dd/MM/yyyy");
            buscarEventosDelUsuario(fechaEventos);
        }

        protected void lnkPosterior_Click(object sender, EventArgs e)
        {
            DateTime fechaEventos = DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null).AddDays(1);
            fechaEventosTextBox.Value = fechaEventos.ToString("dd/MM/yyyy");
            buscarEventosDelUsuario(fechaEventos);
        }

        private void buscarEventosDelUsuario(DateTime fecha)
        {
            lblFecha.Text = fecha.ToString("dddd", new CultureInfo("es-ES")).First().ToString().ToUpper() + fecha.ToString("dddd", new CultureInfo("es-ES")).Substring(1) + ", " + fecha.ToString("dd") + " de " + fecha.ToString("MMMM", new CultureInfo("es-ES")) + " de " + fecha.ToString("yyyy");

            try
            {
                EventoLogic el = new EventoLogic();
                List<Evento> eventos = el.BuscarEventosDelUsuario(userSesion, fecha);

                if (eventos != null && eventos.Count > 0)
                {
                    lblEventos.Visible = false;

                    rptEventos.DataSource = eventos;
                    rptEventos.DataBind();
                    rptEventos.Visible = true;

                    lblZonaHoraria.Text = "Todos los horarios corresponden a la zona horaria " + userSesion.zonaHoraria.descripcion;
                    lblZonaHoraria.Visible = true;
                }
                else
                {
                    rptEventos.Visible = false;
                    lblZonaHoraria.Visible = false;
                    lblEventos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnAgregarEvento_Click(object sender, EventArgs e)
        {
            if (nombreTextBox.Text != null && nombreTextBox.Text != "" &&
                descripcionTextArea.Value != null && descripcionTextArea.Value != "" &&
                fechaEventoTextBox.Value != null && fechaEventoTextBox.Value != "" &&
                (todoElDiaCheckBox.Checked == true || (todoElDiaCheckBox.Checked == false && horaEventoTextBox.Value != null && horaEventoTextBox.Value != "")) &&
                (recordatorioCheckBox.Checked == false || (recordatorioCheckBox.Checked == true && fechaRecordatorioTextBox.Value != null && fechaRecordatorioTextBox.Value != "" && horaRecordatorioTextBox.Value != null && horaRecordatorioTextBox.Value != "")))
            {
                Evento evento = new Evento();
                evento.nombre = nombreTextBox.Text;
                evento.descripcion = descripcionTextArea.Value;
                evento.todo_el_dia = todoElDiaCheckBox.Checked;
                if (evento.todo_el_dia)
                {
                    evento.fecha_hora_evento = DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null);
                }
                else
                {
                    evento.fecha_hora_evento = new DateTime(DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null).Year, DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null).Month, DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null).Day,
                                                            DateTime.ParseExact(horaEventoTextBox.Value, "HH:mm", null).Hour, DateTime.ParseExact(horaEventoTextBox.Value, "HH:mm", null).Minute, 0).AddHours(-int.Parse(zonaHorariaDropDownList.SelectedValue));
                }
                if (recordatorioCheckBox.Checked)
                {
                    evento.fecha_hora_recordatorio = new DateTime(DateTime.ParseExact(fechaRecordatorioTextBox.Value, "dd/MM/yyyy", null).Year, DateTime.ParseExact(fechaRecordatorioTextBox.Value, "dd/MM/yyyy", null).Month, DateTime.ParseExact(fechaRecordatorioTextBox.Value, "dd/MM/yyyy", null).Day,
                                                                  DateTime.ParseExact(horaRecordatorioTextBox.Value, "HH:mm", null).Hour, DateTime.ParseExact(horaRecordatorioTextBox.Value, "HH:mm", null).Minute, 0).AddHours(-int.Parse(zonaHorariaDropDownList.SelectedValue));
                    evento.recordatorio_enviado = false;
                }
                else
                {
                    evento.fecha_hora_recordatorio = null;
                    evento.recordatorio_enviado = null;
                }
                evento.id_usuario = userSesion.id_usuario;
                evento.id_color = int.Parse(colorDropDownList.SelectedValue);

                try
                {
                    EventoLogic el = new EventoLogic();
                    el.RegistrarEvento(evento);

                    if (evento.todo_el_dia)
                    {
                        fechaEventosTextBox.Value = evento.fecha_hora_evento.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fechaEventosTextBox.Value = evento.fecha_hora_evento.AddHours(userSesion.zonaHoraria.diferencia).ToString("dd/MM/yyyy");
                    }
                    buscarEventosDelUsuario(DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null));

                    panelAgregarEvento.Visible = false;
                    limpiarPanelAgregarEvento();
                }
                catch (Exception ex)
                {
                    Session["error"] = ex;
                    Response.Redirect("~/Errores.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnEditarEvento_Click(object sender, EventArgs e)
        {
            if (nombreTextBox.Text != null && nombreTextBox.Text != "" &&
                descripcionTextArea.Value != null && descripcionTextArea.Value != "" &&
                fechaEventoTextBox.Value != null && fechaEventoTextBox.Value != "" &&
                (todoElDiaCheckBox.Checked == true || (todoElDiaCheckBox.Checked == false && horaEventoTextBox.Value != null && horaEventoTextBox.Value != "")) &&
                (recordatorioCheckBox.Checked == false || (recordatorioCheckBox.Checked == true && fechaRecordatorioTextBox.Value != null && fechaRecordatorioTextBox.Value != "" && horaRecordatorioTextBox.Value != null && horaRecordatorioTextBox.Value != "")))
            {
                Evento evento = new Evento();
                evento.id_evento = int.Parse(idEventoTextBox.Value);
                evento.nombre = nombreTextBox.Text;
                evento.descripcion = descripcionTextArea.Value;
                evento.todo_el_dia = todoElDiaCheckBox.Checked;
                if (evento.todo_el_dia)
                {
                    evento.fecha_hora_evento = DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null);
                }
                else
                {
                    evento.fecha_hora_evento = new DateTime(DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null).Year, DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null).Month, DateTime.ParseExact(fechaEventoTextBox.Value, "dd/MM/yyyy", null).Day,
                                                            DateTime.ParseExact(horaEventoTextBox.Value, "HH:mm", null).Hour, DateTime.ParseExact(horaEventoTextBox.Value, "HH:mm", null).Minute, 0).AddHours(-int.Parse(zonaHorariaDropDownList.SelectedValue));
                }
                if (recordatorioCheckBox.Checked)
                {
                    evento.fecha_hora_recordatorio = new DateTime(DateTime.ParseExact(fechaRecordatorioTextBox.Value, "dd/MM/yyyy", null).Year, DateTime.ParseExact(fechaRecordatorioTextBox.Value, "dd/MM/yyyy", null).Month, DateTime.ParseExact(fechaRecordatorioTextBox.Value, "dd/MM/yyyy", null).Day,
                                                                  DateTime.ParseExact(horaRecordatorioTextBox.Value, "HH:mm", null).Hour, DateTime.ParseExact(horaRecordatorioTextBox.Value, "HH:mm", null).Minute, 0).AddHours(-int.Parse(zonaHorariaDropDownList.SelectedValue));
                }
                else
                {
                    evento.fecha_hora_recordatorio = null;
                    evento.recordatorio_enviado = null;
                }
                evento.id_usuario = userSesion.id_usuario;
                evento.id_color = int.Parse(colorDropDownList.SelectedValue);

                try
                {
                    EventoLogic el = new EventoLogic();
                    el.ActualizarEvento(evento);

                    if (evento.todo_el_dia)
                    {
                        fechaEventosTextBox.Value = evento.fecha_hora_evento.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fechaEventosTextBox.Value = evento.fecha_hora_evento.AddHours(userSesion.zonaHoraria.diferencia).ToString("dd/MM/yyyy");
                    }
                    buscarEventosDelUsuario(DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null));

                    panelAgregarEvento.Visible = false;
                    limpiarPanelAgregarEvento();
                }
                catch (Exception ex)
                {
                    Session["error"] = ex;
                    Response.Redirect("~/Errores.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            panelAgregarEvento.Visible = false;
            limpiarPanelAgregarEvento();
        }

        protected void lnkCerrarEvento_Click(object sender, EventArgs e)
        {
            panelAgregarEvento.Visible = false;
            limpiarPanelAgregarEvento();
        }

        private void cargarPanelAgregarEvento(Evento evento)
        {
            idEventoTextBox.Value = evento.id_evento.ToString();
            if (evento.nombre != null) { nombreTextBox.Text = evento.nombre; } else { nombreTextBox.Text = ""; };
            if (evento.descripcion != null) { descripcionTextArea.Value = evento.descripcion; } else { descripcionTextArea.Value = ""; };
            if (evento.color != null) { colorDropDownList.SelectedValue = evento.color.id_color.ToString(); };
            zonaHorariaDropDownList.SelectedValue = evento.usuario.zonaHoraria.diferencia.ToString();
            todoElDiaCheckBox.Checked = evento.todo_el_dia;
            if (evento.fecha_hora_evento.Date != null) { fechaEventoTextBox.Value = evento.fecha_hora_evento.ToString("dd/MM/yyyy"); } else { fechaEventoTextBox.Value = ""; };
            if (evento.fecha_hora_evento.TimeOfDay != null) { horaEventoTextBox.Value = evento.fecha_hora_evento.ToString("HH:mm"); } else { horaEventoTextBox.Value = ""; };
            if (todoElDiaCheckBox.Checked == true) { divHoraEvento.Attributes["style"] = "margin-bottom: 0.5%; display: none;"; } else { divHoraEvento.Attributes["style"] = "margin-bottom: 0.5%; display: block;"; };
            if (evento.fecha_hora_recordatorio != null)
            {
                recordatorioCheckBox.Checked = true;
                fechaRecordatorioTextBox.Value = ((DateTime)evento.fecha_hora_recordatorio).ToString("dd/MM/yyyy");
                horaRecordatorioTextBox.Value = ((DateTime)evento.fecha_hora_recordatorio).ToString("HH:mm");
                divFechaRecordatorio.Attributes["style"] = "margin-bottom: 0.5%; display: block;";
                divHoraRecordatorio.Attributes["style"] = "margin-bottom: 0.5%; display: block;";
            }
            else
            {
                recordatorioCheckBox.Checked = false;
                fechaRecordatorioTextBox.Value = "";
                horaRecordatorioTextBox.Value = "";
                divFechaRecordatorio.Attributes["style"] = "margin-bottom: 0.5%; display: none;";
                divHoraRecordatorio.Attributes["style"] = "margin-bottom: 0.5%; display: none;";
            }
        }

        private void limpiarPanelAgregarEvento()
        {
            idEventoTextBox.Value = "";
            nombreTextBox.Text = "";
            descripcionTextArea.Value = "";
            colorDropDownList.SelectedValue = new ColorLogic().BuscarColorPorDescripcion("Gris").id_color.ToString();
            zonaHorariaDropDownList.SelectedValue = userSesion.zonaHoraria.diferencia.ToString();
            todoElDiaCheckBox.Checked = false;
            fechaEventoTextBox.Value = "";
            horaEventoTextBox.Value = "";
            recordatorioCheckBox.Checked = false;
            fechaRecordatorioTextBox.Value = "";
            horaRecordatorioTextBox.Value = "";
            btnAgregarEvento.Visible = false;
            btnEditarEvento.Visible = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            Evento evento = new Evento();
            evento.id_evento = int.Parse(button.CommandArgument);

            try
            {
                EventoLogic el = new EventoLogic();
                evento = el.BuscarEvento(evento);

                cargarPanelAgregarEvento(evento);
                btnAgregarEvento.Visible = false;
                btnEditarEvento.Visible = true;
                panelAgregarEvento.Visible = true;

                buscarEventosDelUsuario(DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null));
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            Evento evento = new Evento();
            evento.id_evento = int.Parse(button.CommandArgument);

            try
            {
                EventoLogic el = new EventoLogic();
                el.EliminarEvento(evento);

                buscarEventosDelUsuario(DateTime.ParseExact(fechaEventosTextBox.Value, "dd/MM/yyyy", null));
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}