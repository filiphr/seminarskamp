using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// .aspx Страна на која се прикажуваат предметите кои даден професор(асистент) ги предава или кои даден студент ги слуша
/// </summary>
public partial class IzborNaPredmeti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["indeks"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            PopulateControls();
            
        }
    }
    /// <summary>
    /// Исполнување на страната IzborNaPredmeti во зависност од улогата на најавениот корисник
    /// </summary>
    private void PopulateControls()
    {
        if (User.IsInRole("Студент"))
        {
            string indeks = Session["indeks"].ToString();
            DataSet ds = StoriraniProceduri.GetPredmetiForStudent(indeks);
            DataList lstTmp = ((DataList)this.LoginView1.FindControl("lstStudent"));
            lstTmp.DataSource = ds;
            lstTmp.DataBind();

           
        } else if (User.IsInRole("Професор"))
        {
            string profesor_kod = Session["indeks"].ToString();
            DataSet ds = StoriraniProceduri.GetPredmetiForProfesor(profesor_kod);
            DataList lstTmp = ((DataList)this.LoginView1.FindControl("lstProfesor"));
            lstTmp.DataSource = ds;
            lstTmp.DataBind();
        }
    }
}