using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class ProfessorSubject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String predmet = Request.QueryString["ime_predmet"];
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

            lblSkala.Text = "Скалата по предметот " + predmet + " е следната";
            lblUslov.Text = "Променете ги условите и границите за " + predmet;

            predmet_kod = 3;
            IspolniSkala(predmet_kod);
            //TODO
            IspolniUslov();
        }
    }

    private void IspolniUslov()
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "SELECT uslov_ime, Min_Procent, Procent FROM Predmet_Uslov WHERE predmet_kod=@kod";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);

        komanda.Parameters.AddWithValue("@kod", 3);

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Uslov");
            gvUslov.DataSource = ds;
            gvUslov.DataBind();

            ViewState["UslovDataSet"] = ds;
        }
        catch { }
        finally
        {
            konekcija.Close();
        }
    }

    private void IspolniSkala(int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "SELECT * FROM Skala WHERE predmet_kod=@kod";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);
        komanda.Parameters.AddWithValue("@kod", predmet_kod);

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Skala");
            gvSkala.DataSource = ds;
            gvSkala.DataBind();
            ViewState["SkalaDataSet"] = ds;
        }
        catch { }
        finally
        {
            konekcija.Close();
        }
    }
    protected void gvSkala_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["SkalaDataSet"];
        gvSkala.EditIndex = e.NewEditIndex;
        gvSkala.DataSource = ds;
        gvSkala.DataBind();
    }
    protected void gvSkala_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["SkalaDataSet"];
        gvSkala.EditIndex = -1;
        gvSkala.DataSource = ds;
        gvSkala.DataBind();
        lblSkalaEror.Visible = false;
    }
    protected void gvSkala_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowUpdating = e.RowIndex;
        TextBox tbDolna = (TextBox)gvSkala.Rows[rowUpdating].Cells[1].Controls[0];
        TextBox tbGorna = (TextBox)gvSkala.Rows[rowUpdating].Cells[2].Controls[0];

        String errorMessage = "";

        int dolna, gorna;
        bool update = true;

        if (!Int32.TryParse(tbDolna.Text, out dolna))
        {
            update = false;
        }

        if (!Int32.TryParse(tbGorna.Text, out gorna))
        {
            update = false;
        }

        if (update)
        {
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);
            predmet_kod = 3;


            string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection konekcija = new SqlConnection(konekcijaString);
            string sqlString = "UPDATE Skala SET Min=@min, Maks=@maks WHERE predmet_kod=@kod AND ocena_ocena=@ocena";
            SqlCommand komanda = new SqlCommand(sqlString, konekcija);
            komanda.Parameters.AddWithValue("@min", dolna);
            komanda.Parameters.AddWithValue("@maks", gorna);
            komanda.Parameters.AddWithValue("@ocena", gvSkala.Rows[rowUpdating].Cells[0].Text);
            komanda.Parameters.AddWithValue("@kod", predmet_kod);

            int efekt = 0;
            try
            {
                konekcija.Open();
                efekt = komanda.ExecuteNonQuery();
                lblSkalaEror.Visible = false;

            }
            catch (Exception err)
            {
                lblSkalaEror.Text = err.Message;
                lblSkalaEror.Visible = true;
            }
            finally
            {
                konekcija.Close();
                gvSkala.EditIndex = -1;
            }
            if (efekt != 0)
            {
                IspolniSkala(predmet_kod);
            }
        }
        else
        {
            errorMessage += "Внесете нумерички граници";
            lblSkalaEror.Text = errorMessage;
            lblSkalaEror.Visible = true;
        }



    }

    protected void gvUslov_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.EditIndex = e.NewEditIndex;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }
    protected void gvUslov_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.EditIndex = -1;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }
    protected void gvUslov_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowUpdating = e.RowIndex;
        TextBox tbMin = (TextBox)gvUslov.Rows[rowUpdating].Cells[1].Controls[0];
        TextBox tbProcent = (TextBox)gvUslov.Rows[rowUpdating].Cells[2].Controls[0];
        String uslov = gvUslov.Rows[rowUpdating].Cells[0].Text;

        String errorMessage = "";

        int min, procent;
        bool update = true;

        if (!Int32.TryParse(tbMin.Text, out min))
        {
            update = false;
        }

        if (!Int32.TryParse(tbProcent.Text, out procent))
        {
            update = false;
        }

        if (update)
        {
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);
            predmet_kod = 3;


            string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection konekcija = new SqlConnection(konekcijaString);
            string sqlString = "UPDATE Predmet_Uslov SET Min_Procent=@min_procent, Procent=@procent WHERE predmet_kod=@kod AND uslov_ime=@uslov";
            SqlCommand komanda = new SqlCommand(sqlString, konekcija);
            komanda.Parameters.AddWithValue("@uslov", uslov);
            komanda.Parameters.AddWithValue("@min_procent", min);
            komanda.Parameters.AddWithValue("@procent", procent);
            komanda.Parameters.AddWithValue("@kod", predmet_kod);

            int efekt = 0;
            try
            {
                konekcija.Open();
                efekt = komanda.ExecuteNonQuery();
                lblUslovError.Visible = false;

            }
            catch (Exception err)
            {
                lblUslovError.Text = err.Message;
                lblUslovError.Visible = true;
            }
            finally
            {
                konekcija.Close();
                gvUslov.EditIndex = -1;
            }
            if (efekt != 0)
            {
                IspolniUslov();
            }
        }
        else
        {
            errorMessage += "Внесете нумерички граници";
            lblUslovError.Text = errorMessage;
            lblUslovError.Visible = true;
        }



    }
    protected void gvUslov_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int rowDeleting = e.RowIndex;

        String uslov = gvUslov.Rows[rowDeleting].Cells[0].Text;
        int predmet_kod;
        Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);
        predmet_kod = 3;


        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "DELETE from Predmet_Uslov WHERE predmet_kod=@kod AND uslov_ime=@uslov";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);
        komanda.Parameters.AddWithValue("@uslov", uslov);
        komanda.Parameters.AddWithValue("@kod", predmet_kod);

        int efekt = 0;
        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();
            lblUslovError.Visible = false;

        }
        catch (Exception err)
        {
            lblUslovError.Text = err.Message;
            lblUslovError.Visible = true;
        }
        finally
        {
            konekcija.Close();
            gvUslov.EditIndex = -1;
        }
        if (efekt != 0)
        {
            IspolniUslov();
        }
    }
    protected void gvUslov_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUslov.PageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.SelectedIndex = -1;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }
    protected void btnNovaSkala_Click(object sender, EventArgs e)
    {
        string predmet_kod = Request.QueryString["predmet_kod"];
        string predmet = Request.QueryString["predmet"];

        Response.Redirect("~/NovaSkala.aspx?predmet_kod=" + predmet_kod + "&predmet=" + predmet);
    }
}