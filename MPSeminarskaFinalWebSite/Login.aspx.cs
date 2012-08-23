using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_LoggedIn1(object sender, EventArgs e)
    {
        string indeks;
        if (Session["indeks"] == null)
        {
            indeks = Login1.UserName.ToString();
            Session["indeks"] = indeks;
        }

        Response.Redirect("ИзборНаПредмети.aspx");

    }
}