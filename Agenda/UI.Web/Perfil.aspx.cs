using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Perfil : System.Web.UI.Page
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

                cargarPerfil();
                if (!Page.IsPostBack) cargarZonasHorarias();
            }
        }

        private void cargarPerfil()
        {
            lblNombreUsuario.Text = userSesion.nombre_usuario;
            lblClave.Text = "";
            for (int i = 0; i < userSesion.contrasenia.Length; i++)
            {
                lblClave.Text = lblClave.Text + "*";
            }
            lblNombreApellido.Text = userSesion.nombre_apellido;
            lblTelefono.Text = userSesion.telefono;
            lblEmail.Text = userSesion.email;
            lblZonaHoraria.Text = userSesion.zonaHoraria.descripcion;
        }

        private void cargarZonasHorarias()
        {
            ListItem item = null;

            try
            {
                ZonaHorariaLogic zhl = new ZonaHorariaLogic();

                foreach (ZonaHoraria zh in zhl.BuscarZonasHorarias())
                {
                    item = new ListItem(zh.descripcion, zh.id_zona_horaria.ToString());
                    this.dropDownListZonasHorarias.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void actualizarCookies()
        {
            HttpCookie cookieNombreUsuario = new HttpCookie("cookieNombreUsuario");
            cookieNombreUsuario.Value = userSesion.nombre_usuario;
            cookieNombreUsuario.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cookieNombreUsuario);

            HttpCookie cookieClave = new HttpCookie("cookieClave");
            cookieClave.Value = userSesion.contrasenia;
            cookieClave.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cookieClave);
        }

        protected void LinkButtonEditar_Click(object sender, EventArgs e)
        {
            this.alerta.Visible = false;

            LinkButton linkButton = (LinkButton)sender;

            linkButton.Visible = false;
            switch (linkButton.ID)
            {
                case "LinkButtonEditarNombreUsuario":
                    this.LinkButtonGuardarNombreUsuario.Visible = true;
                    this.LinkButtonCancelarNombreUsuario.Visible = true;
                    this.lblNombreUsuario.Visible = false;
                    this.txtNombreUsuario.Text = userSesion.nombre_usuario;
                    this.txtNombreUsuario.Visible = true;
                    break;
                case "LinkButtonEditarClave":
                    this.LinkButtonGuardarClave.Visible = true;
                    this.LinkButtonCancelarClave.Visible = true;
                    this.lblClave.Visible = false;
                    this.txtClave.Text = "";
                    for (int i = 0; i < userSesion.contrasenia.Length; i++)
                    {
                        this.txtClave.Text = this.txtClave.Text + "*";
                    }
                    this.txtClave.Visible = true;
                    break;
                case "LinkButtonEditarNombreApellido":
                    this.LinkButtonGuardarNombreApellido.Visible = true;
                    this.LinkButtonCancelarNombreApellido.Visible = true;
                    this.lblNombreApellido.Visible = false;
                    this.txtNombreApellido.Text = userSesion.nombre_apellido;
                    this.txtNombreApellido.Visible = true;
                    break;
                case "LinkButtonEditarTelefono":
                    this.LinkButtonGuardarTelefono.Visible = true;
                    this.LinkButtonCancelarTelefono.Visible = true;
                    this.lblTelefono.Visible = false;
                    this.txtTelefono.Text = userSesion.telefono;
                    this.txtTelefono.Visible = true;
                    break;
                case "LinkButtonEditarEmail":
                    this.LinkButtonGuardarEmail.Visible = true;
                    this.LinkButtonCancelarEmail.Visible = true;
                    this.lblEmail.Visible = false;
                    this.txtEmail.Text = userSesion.email;
                    this.txtEmail.Visible = true;
                    break;
                case "LinkButtonEditarZonaHoraria":
                    this.LinkButtonGuardarZonaHoraria.Visible = true;
                    this.LinkButtonCancelarZonaHoraria.Visible = true;
                    this.lblZonaHoraria.Visible = false;
                    this.dropDownListZonasHorarias.SelectedValue = userSesion.zonaHoraria.id_zona_horaria.ToString();
                    this.dropDownListZonasHorarias.Visible = true;
                    break;
            }
        }

        protected void LinkButtonCancelar_Click(object sender, EventArgs e)
        {
            this.alerta.Visible = false;

            LinkButton linkButton = (LinkButton)sender;

            linkButton.Visible = false;
            switch (linkButton.ID)
            {
                case "LinkButtonCancelarNombreUsuario":
                    this.LinkButtonGuardarNombreUsuario.Visible = false;
                    this.LinkButtonEditarNombreUsuario.Visible = true;
                    this.txtNombreUsuario.Visible = false;
                    this.lblNombreUsuario.Visible = true;
                    break;
                case "LinkButtonCancelarClave":
                    this.LinkButtonGuardarClave.Visible = false;
                    this.LinkButtonEditarClave.Visible = true;
                    this.txtClave.Visible = false;
                    this.lblClave.Visible = true;
                    break;
                case "LinkButtonCancelarNombreApellido":
                    this.LinkButtonGuardarNombreApellido.Visible = false;
                    this.LinkButtonEditarNombreApellido.Visible = true;
                    this.txtNombreApellido.Visible = false;
                    this.lblNombreApellido.Visible = true;
                    break;
                case "LinkButtonCancelarTelefono":
                    this.LinkButtonGuardarTelefono.Visible = false;
                    this.LinkButtonEditarTelefono.Visible = true;
                    this.txtTelefono.Visible = false;
                    this.lblTelefono.Visible = true;
                    break;
                case "LinkButtonCancelarEmail":
                    this.LinkButtonGuardarEmail.Visible = false;
                    this.LinkButtonEditarEmail.Visible = true;
                    this.txtEmail.Visible = false;
                    this.lblEmail.Visible = true;
                    break;
                case "LinkButtonCancelarZonaHoraria":
                    this.LinkButtonGuardarZonaHoraria.Visible = false;
                    this.LinkButtonEditarZonaHoraria.Visible = true;
                    this.dropDownListZonasHorarias.Visible = false;
                    this.lblZonaHoraria.Visible = true;
                    break;
            }
        }

        protected void LinkButtonGuardar_Click(object sender, EventArgs e)
        {
            this.alerta.Visible = false;

            UsuarioLogic ul = new UsuarioLogic();

            LinkButton linkButton = (LinkButton)sender;
            switch (linkButton.ID)
            {
                case "LinkButtonGuardarNombreUsuario":
                    userSesion.nombre_usuario = this.txtNombreUsuario.Text;

                    try
                    {
                        ul.ActualizarUsuario(userSesion);

                        Session["userSesion"] = userSesion;
                        actualizarCookies();
                        cargarPerfil();
                        ((Label)Master.FindControl("lblNombreUsuario")).Text = userSesion.nombre_usuario;

                        linkButton.Visible = false;
                        this.LinkButtonCancelarNombreUsuario.Visible = false;
                        this.LinkButtonEditarNombreUsuario.Visible = true;
                        this.txtNombreUsuario.Visible = false;
                        this.lblNombreUsuario.Visible = true;

                        this.textoAlerta.InnerText = "Usuario actualizado";
                        this.alerta.Attributes["style"] = "background-color: #31DE35";
                        this.alerta.Visible = true;
                    }
                    catch (Exception)
                    {
                        userSesion = (Usuario)Session["userSesion"];

                        this.textoAlerta.InnerText = "Usuario no actualizado";
                        this.alerta.Attributes["style"] = "background-color: #EC3434";
                        this.alerta.Visible = true;
                    }
                    break;
                case "LinkButtonGuardarClave":
                    userSesion.contrasenia = this.txtClave.Text;

                    try
                    {
                        ul.ActualizarUsuario(userSesion);

                        Session["userSesion"] = userSesion;
                        actualizarCookies();
                        cargarPerfil();

                        linkButton.Visible = false;
                        this.LinkButtonCancelarClave.Visible = false;
                        this.LinkButtonEditarClave.Visible = true;
                        this.txtClave.Visible = false;
                        this.lblClave.Visible = true;

                        this.textoAlerta.InnerText = "Usuario actualizado";
                        this.alerta.Attributes["style"] = "background-color: #31DE35";
                        this.alerta.Visible = true;
                    }
                    catch (Exception)
                    {
                        userSesion = (Usuario)Session["userSesion"];

                        this.textoAlerta.InnerText = "Usuario no actualizado";
                        this.alerta.Attributes["style"] = "background-color: #EC3434";
                        this.alerta.Visible = true;
                    }
                    break;
                case "LinkButtonGuardarNombreApellido":
                    userSesion.nombre_apellido = this.txtNombreApellido.Text;

                    try
                    {
                        ul.ActualizarUsuario(userSesion);

                        Session["userSesion"] = userSesion;
                        cargarPerfil();

                        linkButton.Visible = false;
                        this.LinkButtonCancelarNombreApellido.Visible = false;
                        this.LinkButtonEditarNombreApellido.Visible = true;
                        this.txtNombreApellido.Visible = false;
                        this.lblNombreApellido.Visible = true;

                        this.textoAlerta.InnerText = "Usuario actualizado";
                        this.alerta.Attributes["style"] = "background-color: #31DE35";
                        this.alerta.Visible = true;
                    }
                    catch (Exception)
                    {
                        userSesion = (Usuario)Session["userSesion"];

                        this.textoAlerta.InnerText = "Usuario no actualizado";
                        this.alerta.Attributes["style"] = "background-color: #EC3434";
                        this.alerta.Visible = true;
                    }
                    break;
                case "LinkButtonGuardarTelefono":
                    userSesion.telefono = this.txtTelefono.Text;

                    try
                    {
                        ul.ActualizarUsuario(userSesion);

                        Session["userSesion"] = userSesion;
                        cargarPerfil();

                        linkButton.Visible = false;
                        this.LinkButtonCancelarTelefono.Visible = false;
                        this.LinkButtonEditarTelefono.Visible = true;
                        this.txtTelefono.Visible = false;
                        this.lblTelefono.Visible = true;

                        this.textoAlerta.InnerText = "Usuario actualizado";
                        this.alerta.Attributes["style"] = "background-color: #31DE35";
                        this.alerta.Visible = true;
                    }
                    catch (Exception)
                    {
                        userSesion = (Usuario)Session["userSesion"];

                        this.textoAlerta.InnerText = "Usuario no actualizado";
                        this.alerta.Attributes["style"] = "background-color: #EC3434";
                        this.alerta.Visible = true;
                    }
                    break;
                case "LinkButtonGuardarEmail":
                    userSesion.email = this.txtEmail.Text;

                    try
                    {
                        ul.ActualizarUsuario(userSesion);

                        Session["userSesion"] = userSesion;
                        cargarPerfil();

                        linkButton.Visible = false;
                        this.LinkButtonCancelarEmail.Visible = false;
                        this.LinkButtonEditarEmail.Visible = true;
                        this.txtEmail.Visible = false;
                        this.lblEmail.Visible = true;

                        this.textoAlerta.InnerText = "Usuario actualizado";
                        this.alerta.Attributes["style"] = "background-color: #31DE35";
                        this.alerta.Visible = true;
                    }
                    catch (Exception)
                    {
                        userSesion = (Usuario)Session["userSesion"];

                        this.textoAlerta.InnerText = "Usuario no actualizado";
                        this.alerta.Attributes["style"] = "background-color: #EC3434";
                        this.alerta.Visible = true;
                    }
                    break;
                case "LinkButtonGuardarZonaHoraria":
                    if (dropDownListZonasHorarias.SelectedValue != "Seleccione su zona horaria")
                    {
                        userSesion.id_zona_horaria = int.Parse(dropDownListZonasHorarias.SelectedValue);

                        try
                        {
                            ul.ActualizarUsuario(userSesion);

                            ZonaHoraria zonaHoraria = new ZonaHoraria();
                            zonaHoraria.id_zona_horaria = userSesion.id_zona_horaria;
                            ZonaHorariaLogic zhl = new ZonaHorariaLogic();
                            userSesion.zonaHoraria = zhl.BuscarZonaHoraria(zonaHoraria);

                            Session["userSesion"] = userSesion;
                            cargarPerfil();

                            linkButton.Visible = false;
                            this.LinkButtonCancelarZonaHoraria.Visible = false;
                            this.LinkButtonEditarZonaHoraria.Visible = true;
                            this.dropDownListZonasHorarias.Visible = false;
                            this.lblZonaHoraria.Visible = true;

                            this.textoAlerta.InnerText = "Usuario actualizado";
                            this.alerta.Attributes["style"] = "background-color: #31DE35";
                            this.alerta.Visible = true;
                        }
                        catch (Exception)
                        {
                            userSesion = (Usuario)Session["userSesion"];

                            this.textoAlerta.InnerText = "Usuario no actualizado";
                            this.alerta.Attributes["style"] = "background-color: #EC3434";
                            this.alerta.Visible = true;
                        }
                    }
                    else
                    {
                        this.textoAlerta.InnerText = "Seleccione una zona horaria";
                        this.alerta.Attributes["style"] = "background-color: #F0B435";
                        this.alerta.Visible = true;
                    }
                    break;
            }
        }
    }
}