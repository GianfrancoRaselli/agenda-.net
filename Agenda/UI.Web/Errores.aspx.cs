using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Errores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception excepcion = (Exception)Session["error"];
            if (excepcion != null)
            {
                lblError.Text = excepcion.Message;
            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inicio.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}