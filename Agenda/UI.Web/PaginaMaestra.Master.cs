using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario userSesion = (Usuario)Session["userSesion"];

            if (userSesion == null)
            {
                Response.Redirect("~/Index.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                this.lblNombreUsuario.Text = userSesion.nombre_usuario;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            HttpCookie cookieNombreUsuario = new HttpCookie("cookieNombreUsuario");
            cookieNombreUsuario.Value = "";
            cookieNombreUsuario.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookieNombreUsuario);

            HttpCookie cookieClave = new HttpCookie("cookieClave");
            cookieClave.Value = "";
            cookieClave.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookieClave);

            Session.Abandon();
            Response.Redirect("~/Index.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void lnkAgregarEvento_Click(object sender, EventArgs e)
        {
            ((Button)ContentPlaceHolder1.FindControl("btnEditarEvento")).Visible = false;
            ((Button)ContentPlaceHolder1.FindControl("btnAgregarEvento")).Visible = true;
            ((Panel)ContentPlaceHolder1.FindControl("panelAgregarEvento")).Visible = true;
        }
    }
}