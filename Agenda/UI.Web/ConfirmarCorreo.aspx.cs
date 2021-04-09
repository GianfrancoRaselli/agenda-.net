using Business.Entities;
using Business.Logic;
using Business.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class ConfirmarCorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userSesion"] != null)
            {
                Session["userACrearCuenta"] = null;
                Session["codigoCrearCuenta"] = null;

                Response.Redirect("~/Inicio.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (Session["userACrearCuenta"] == null)
                {
                    Response.Redirect("~/Index.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    if (Session["codigoCrearCuenta"] == null)
                    {
                        enviarCorreoConCodigo((Usuario)Session["userACrearCuenta"]);

                        divVolverAtras.Visible = false;
                        divCodigoIncorrecto.Visible = false;
                        divCambioCorreo.Visible = false;
                        lblAvdertencia.Text = "Advertencia: tiene un intento para ingresar el código enviado a " + ((Usuario)Session["userACrearCuenta"]).email;
                        divCodigo.Visible = true;
                        divReenviarCodigo.Visible = true;
                        divCambiarCorreo.Visible = true;
                    }
                    else
                    {
                        lblAvdertencia.Text = "Advertencia: tiene un intento para ingresar el código enviado a " + ((Usuario)Session["userACrearCuenta"]).email;
                    }
                }
            }
        }

        protected void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {
            if (Session["userACrearCuenta"] != null)
            {
                if (Session["codigoCrearCuenta"] != null)
                {
                    if (txtCodigo.Text.Length == 6 && Validaciones.EsUnNumero(txtCodigo.Text) && (int.Parse(txtCodigo.Text)).Equals((int)Session["codigoCrearCuenta"]))
                    {
                        Usuario user = (Usuario)Session["userACrearCuenta"];

                        Session["userACrearCuenta"] = null;
                        Session["codigoCrearCuenta"] = null;

                        new UsuarioLogic().RegistrarUsuario(user);

                        ZonaHoraria zonaHoraria = new ZonaHoraria();
                        zonaHoraria.id_zona_horaria = user.id_zona_horaria;
                        user.zonaHoraria = new ZonaHorariaLogic().BuscarZonaHoraria(zonaHoraria);

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
                        Session["codigoCrearCuenta"] = null;

                        divVolverAtras.Visible = false;
                        divCodigo.Visible = false;
                        divCambioCorreo.Visible = false;
                        divReenviarCodigo.Visible = false;
                        divCambiarCorreo.Visible = false;
                        divCodigoIncorrecto.Visible = true;
                    }

                    txtCodigo.Text = "";
                }
                else
                {
                    enviarCorreoConCodigo((Usuario)Session["userACrearCuenta"]);

                    divVolverAtras.Visible = false;
                    divCodigoIncorrecto.Visible = false;
                    divCambioCorreo.Visible = false;
                    lblAvdertencia.Text = "Advertencia: tiene un intento para ingresar el código enviado a " + ((Usuario)Session["userACrearCuenta"]).email;
                    divCodigo.Visible = true;
                    divReenviarCodigo.Visible = true;
                    divCambiarCorreo.Visible = true;
                }
            }
            else
            {
                Response.Redirect("~/Index.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnConfirmarCorreo_Click(object sender, EventArgs e)
        {
            if (Session["userACrearCuenta"] != null)
            {
                Usuario userACrearCuenta = (Usuario)Session["userACrearCuenta"];
                userACrearCuenta.email = txtEmail.Text;
                Session["userACrearCuenta"] = userACrearCuenta;

                txtEmail.Text = "";

                Session["codigoCrearCuenta"] = null;
                enviarCorreoConCodigo((Usuario)Session["userACrearCuenta"]);

                divVolverAtras.Visible = false;
                divCodigoIncorrecto.Visible = false;
                divCambioCorreo.Visible = false;
                lblAvdertencia.Text = "Advertencia: tiene un intento para ingresar el código enviado a " + ((Usuario)Session["userACrearCuenta"]).email;
                divCodigo.Visible = true;
                divReenviarCodigo.Visible = true;
                divCambiarCorreo.Visible = true;
            }
            else
            {
                Response.Redirect("~/Index.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void enviarCorreoConCodigo(Usuario userACrearCuenta)
        {
            try
            {
                if (userACrearCuenta != null)
                {
                    if (Session["codigoCrearCuenta"] == null) Session["codigoCrearCuenta"] = GeneradorCodigos.GenerarCodigoDe6Digitos();

                    new EnvioCorreoLogic().EnviarCorreoConfirmarCorreo(userACrearCuenta, (int)Session["codigoCrearCuenta"]);
                }
                else
                {
                    Response.Redirect("~/Index.aspx", false);
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

        protected void lnkVolverAtras_Click(object sender, EventArgs e)
        {
            divVolverAtras.Visible = false;
            divCodigoIncorrecto.Visible = false;
            divCambioCorreo.Visible = false;
            lblAvdertencia.Text = "Advertencia: tiene un intento para ingresar el código enviado a " + ((Usuario)Session["userACrearCuenta"]).email;
            divCodigo.Visible = true;
            divReenviarCodigo.Visible = true;
            divCambiarCorreo.Visible = true;
        }

        protected void reenviarCodigo_Click(object sender, EventArgs e)
        {
            if (Session["userACrearCuenta"] != null)
            {
                enviarCorreoConCodigo((Usuario)Session["userACrearCuenta"]);

                divVolverAtras.Visible = false;
                divCodigoIncorrecto.Visible = false;
                divCambioCorreo.Visible = false;
                lblAvdertencia.Text = "Advertencia: tiene un intento para ingresar el código enviado a " + ((Usuario)Session["userACrearCuenta"]).email;
                divCodigo.Visible = true;
                divReenviarCodigo.Visible = true;
                divCambiarCorreo.Visible = true;
            }
            else
            {
                Response.Redirect("~/Index.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void lnkCambiarCorreo_Click(object sender, EventArgs e)
        {
            divCodigo.Visible = false;
            divCodigoIncorrecto.Visible = false;
            divReenviarCodigo.Visible = false;
            divCambiarCorreo.Visible = false;
            divCambioCorreo.Visible = true;
            divVolverAtras.Visible = true;
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}