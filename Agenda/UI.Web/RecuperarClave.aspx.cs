using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using Business.Util;

namespace UI.Web
{
    public partial class RecuperarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userSesion"] != null)
            {
                Response.Redirect("~/Inicio.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblErrorNombreUsuario.Visible = false;
            divError.Visible = false;
            lblError.Text = "";

            if (txtNombreUsuario.Text.Length < 0)
            {
                lblErrorNombreUsuario.Visible = true;
            }
            else
            {
                try
                {
                    UsuarioLogic ul = new UsuarioLogic();
                    Usuario user = ul.BuscarPorNombreUsuario(txtNombreUsuario.Text);

                    if (user != null)
                    {
                        Session["userARecuperarClave"] = user;

                        enviarCorreoConCodigo((Usuario)Session["userARecuperarClave"]);

                        divNombreUsuario.Visible = false;
                        divCambiarClave.Visible = false;
                        divCodigoIncorrecto.Visible = false;
                        divCodigo.Visible = true;
                        divReenviarCodigo.Visible = true;
                        divVolver.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "No existe usuario con nombre de usuario: " + txtNombreUsuario.Text;
                        divError.Visible = true;
                    }

                    txtNombreUsuario.Text = "";
                }
                catch (Exception ex)
                {
                    Session["error"] = ex;
                    Response.Redirect("~/Errores.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private void enviarCorreoConCodigo(Usuario userARecuperarClave)
        {
            try
            {
                if (userARecuperarClave != null)
                {
                    if (Session["codigoRecuperarClave"] == null) Session["codigoRecuperarClave"] = GeneradorCodigos.GenerarCodigoDe6Digitos();

                    new EnvioCorreoLogic().EnviarCorreoRecuperarClave(userARecuperarClave, (int)Session["codigoRecuperarClave"]);
                }
                else
                {
                    Response.Redirect("~/RecuperarClave.aspx", false);
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

        protected void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {
            if (Session["userARecuperarClave"] != null && Session["codigoRecuperarClave"] != null)
            {
                if (txtCodigo.Text.Length == 6 && Validaciones.EsUnNumero(txtCodigo.Text) && (int.Parse(txtCodigo.Text)).Equals((int)Session["codigoRecuperarClave"]))
                {
                    divNombreUsuario.Visible = false;
                    divCodigo.Visible = false;
                    divCodigoIncorrecto.Visible = false;
                    divReenviarCodigo.Visible = false;
                    divVolver.Visible = true;
                    divCambiarClave.Visible = true;
                }
                else
                {   
                    divNombreUsuario.Visible = false;
                    divCodigo.Visible = false;
                    divCambiarClave.Visible = false;
                    divReenviarCodigo.Visible = false;
                    divVolver.Visible = true;
                    divCodigoIncorrecto.Visible = true;
                }

                Session["codigoRecuperarClave"] = null;
                txtCodigo.Text = "";
            }
            else
            {
                Response.Redirect("~/RecuperarClave.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnConfirmarClave_Click(object sender, EventArgs e)
        {
            if (Session["userARecuperarClave"] != null)
            {
                if (txtClave.Text != null && txtClave.Text != "" && txtClave.Text.Length >= 8)
                {
                    try
                    {
                        Usuario user = (Usuario)Session["userARecuperarClave"];
                        user.contrasenia = txtClave.Text;
                        Session["userSesion"] = user;
                        Session["userARecuperarClave"] = null;

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
            else
            {
                Response.Redirect("~/RecuperarClave.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void reenviarCodigo_Click(object sender, EventArgs e)
        {
            if (Session["userARecuperarClave"] != null)
            {
                enviarCorreoConCodigo((Usuario)Session["userARecuperarClave"]);

                divNombreUsuario.Visible = false;
                divCambiarClave.Visible = false;
                divCodigoIncorrecto.Visible = false;
                divCodigo.Visible = true;
                divReenviarCodigo.Visible = true;
                divVolver.Visible = true;
            }
            else
            {
                Response.Redirect("~/RecuperarClave.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            Session["userARecuperarClave"] = null;
            Session["codigoRecuperarClave"] = null;
            divVolver.Visible = false;
            divReenviarCodigo.Visible = false;
            divCodigoIncorrecto.Visible = false;
            divCambiarClave.Visible = false;
            divCodigo.Visible = false;
            divNombreUsuario.Visible = true;
        }

        protected void lnkVolverInicioSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}