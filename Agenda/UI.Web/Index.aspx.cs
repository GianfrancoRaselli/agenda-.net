using Business.Entities;
using Business.Logic;
using Business.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userSesion"] != null)
            {
                Response.Redirect("~/Inicio.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    HttpCookie cookieNombreUsuario = Request.Cookies["cookieNombreUsuario"];
                    HttpCookie cookieClave = Request.Cookies["cookieClave"];

                    if (cookieNombreUsuario != null && cookieClave != null)
                    {
                        string nombreUsuario = cookieNombreUsuario.Value;
                        string clave = cookieClave.Value;

                        if (nombreUsuario != null && clave != null && !nombreUsuario.Equals("") && !clave.Equals(""))
                        {
                            try
                            {
                                UsuarioLogic ul = new UsuarioLogic();

                                Usuario user = new Usuario();
                                user.nombre_usuario = nombreUsuario;
                                user.contrasenia = clave;

                                user = ul.ValidarUsuario(user);

                                if (user != null)
                                {
                                    Session["userSesion"] = user;
                                    Response.Redirect("~/Inicio.aspx", false);
                                    Context.ApplicationInstance.CompleteRequest();
                                }
                            }
                            catch (Exception ex)
                            {
                                Session["error"] = ex;
                                Response.Redirect("~/Errores.aspx", false);
                                Context.ApplicationInstance.CompleteRequest();
                            }
                        }
                    }
                    
                    cargarZonasHorarias();
                }
            }
        }

        private void cargarZonasHorarias()
        {
            ListItem item = new ListItem("Seleccione su zona horaria", "Seleccione su zona horaria");
            item.Selected = true;
            this.dropDownListZonasHorarias.Items.Add(item);

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

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            lblErrorInicioSesion.Text = "";
            lblTitulo.Text = "Ingresar";
            ((Panel)this.FindControl("panelRegistrarse")).Attributes["style"] = "display: none;";
            ((Panel)this.FindControl("panelIngresar")).Attributes["style"] = "display: block;";
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Registrarse";
            ((Label)this.FindControl("lblErrorNombreUsuario")).Attributes["style"] = "color: red; display: none;";
            ((Panel)this.FindControl("panelIngresar")).Attributes["style"] = "display: none;";
            ((Panel)this.FindControl("panelRegistrarse")).Attributes["style"] = "display: block;";
        }

        protected void lnkIngresar_Click(object sender, EventArgs e)
        {
            lblErrorInicioSesion.Text = "";
            lblTitulo.Text = "Ingresar";
            ((Panel)this.FindControl("panelRegistrarse")).Attributes["style"] = "display: none;";
            ((Panel)this.FindControl("panelIngresar")).Attributes["style"] = "display: block;";
        }

        protected void lnkRegistrarse_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Registrarse";
            ((Label)this.FindControl("lblErrorNombreUsuario")).Attributes["style"] = "color: red; display: none;";
            ((Panel)this.FindControl("panelIngresar")).Attributes["style"] = "display: none;";
            ((Panel)this.FindControl("panelRegistrarse")).Attributes["style"] = "display: block;";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblErrorInicioSesion.Text = "";

            try
            {
                UsuarioLogic ul = new UsuarioLogic();

                Usuario user = new Usuario();
                user.nombre_usuario = this.txtUsuario.Text;
                user.contrasenia = this.txtContrasenia.Text;

                user = ul.ValidarUsuario(user);

                if (user != null)
                {
                    Session["userSesion"] = user;

                    HttpCookie cookieNombreUsuario = new HttpCookie("cookieNombreUsuario");
                    cookieNombreUsuario.Value = user.nombre_usuario;
                    cookieNombreUsuario.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookieNombreUsuario);

                    HttpCookie cookieClave = new HttpCookie("cookieClave");
                    cookieClave.Value = user.contrasenia;
                    cookieClave.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookieClave);

                    Response.Redirect("~/Inicio.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblErrorInicioSesion.Text = "Nombre de usuario y/o contraseña no registrados";
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("~/Errores.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnRegistrarte_Click(object sender, EventArgs e)
        {
            if (nombreUsuarioTextBox.Text != null && nombreUsuarioTextBox.Text != "" &&
                contraseniaTextBox.Text != null && contraseniaTextBox.Text != "" && contraseniaTextBox.Text.Length >= 8 &&
                nombreApellidoTextBox.Text != null && nombreApellidoTextBox.Text != "" &&
                emailTextBox.Text != null && emailTextBox.Text != "" && Regex.IsMatch(emailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*") && Regex.Replace(emailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", String.Empty).Length == 0 &&
                dropDownListZonasHorarias.SelectedValue != "Seleccione su zona horaria")
            {
                Usuario user = new Usuario();
                user.nombre_usuario = nombreUsuarioTextBox.Text;
                user.contrasenia = contraseniaTextBox.Text;
                user.nombre_apellido = nombreApellidoTextBox.Text;
                user.email = emailTextBox.Text;
                user.id_zona_horaria = int.Parse(dropDownListZonasHorarias.SelectedValue);

                try
                {
                    UsuarioLogic ul = new UsuarioLogic();

                    if (!ul.ExisteUsuario(user))
                    {
                        lblErrorNombreUsuario.Text = "";

                        Session["userACrearCuenta"] = user;
                        Session["codigoCrearCuenta"] = GeneradorCodigos.GenerarCodigoDe6Digitos();

                        new EnvioCorreoLogic().EnviarCorreoConfirmarCorreo((Usuario)Session["userACrearCuenta"], (int)Session["codigoCrearCuenta"]);

                        Response.Redirect("~/ConfirmarCorreo.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        lblErrorNombreUsuario.Text = "El nombre de usuario ya se encuentra registrado";
                        ((Label)this.FindControl("lblErrorNombreUsuario")).Attributes["style"] = "color: red; display: block;";
                    }
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

        protected void lnkRecordarClave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RecuperarClave.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}