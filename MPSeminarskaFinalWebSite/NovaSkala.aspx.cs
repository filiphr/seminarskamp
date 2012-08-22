using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class NovaSkala : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);
            predmet_kod = 3;

            ispolniTabela(predmet_kod);
        }
    }

    private void ispolniTabela(int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "SELECT ocena_ocena, Min, Maks FROM Skala WHERE predmet_kod=@kod";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);

        komanda.Parameters.AddWithValue("@kod", predmet_kod);

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Skala");
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                string ocena = row[0].ToString();
                Label lblOcena = (Label)tblSkala.FindControl("lblOcena" + ocena);
                TextBox tbDolna = (TextBox)tblSkala.FindControl("tbDolna" + ocena);
                TextBox tbGorna = (TextBox)tblSkala.FindControl("tbGorna" + ocena);

                lblOcena.Text = ocena;
                tbDolna.Text = row[1].ToString();
                tbGorna.Text = row[2].ToString();
            }


            ViewState["SkalaDataSet"] = ds;
        }
        catch { }
        finally
        {
            konekcija.Close();
        }
    }
    protected void btnSkala_Click(object sender, EventArgs e)
    {
        zacuvajSkala();
    }

    private void zacuvajSkala()
    {
        lblSuccess.Text = "";
        int predmet_kod;
        Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);
        predmet_kod = 3;


        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "UPDATE Skala SET Min=@min, Maks=@maks WHERE predmet_kod=@kod AND ocena_ocena=@ocena";

        string sqlString2 = "UPDATE Skala SET Min = CASE ocena_ocena "
        + "WHEN @ocena5 THEN @min5 "
        + "WHEN @ocena6 THEN @min6 "
        + "WHEN @ocena7 THEN @min7 "
        + "WHEN @ocena8 THEN @min8 "
        + "WHEN @ocena9 THEN @min9 "
        + "WHEN @ocena10 THEN @min10 END,"
        + " Maks = CASE ocena_ocena "
        + "WHEN @ocena5 THEN @maks5 "
        + "WHEN @ocena6 THEN @maks6 "
        + "WHEN @ocena7 THEN @maks7 "
        + "WHEN @ocena8 THEN @maks8 "
        + "WHEN @ocena9 THEN @maks9 "
        + "WHEN @ocena10 THEN @maks10 END "
        + " WHERE predmet_kod=@kod";

        SqlCommand komanda = new SqlCommand(sqlString2, konekcija);

        komanda.Parameters.AddWithValue("@kod", predmet_kod);

        for (int i = 5; i < 11; i++)
        {
            Label lblOcena = (Label)tblSkala.FindControl("lblOcena" + i);
            TextBox tbDolna = (TextBox)tblSkala.FindControl("tbDolna" + i);
            TextBox tbGorna = (TextBox)tblSkala.FindControl("tbGorna" + i);

            string ocena = "@ocena" + i;
            string min = "@min" + i;
            string maks = "@maks" + i;

            komanda.Parameters.AddWithValue(ocena, lblOcena.Text);
            komanda.Parameters.AddWithValue(min, tbDolna.Text);
            komanda.Parameters.AddWithValue(maks, tbGorna.Text);
        }


        int efekt = 0;
        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();

        }
        catch (Exception err)
        {
            lblError.Text = err.Message;
        }
        finally
        {
            konekcija.Close();

        }
        if (efekt != 0)
        {
            ispolniTabela(predmet_kod);
            lblSuccess.Text = "Успешно ја зачувавте скалата";
        }
    }
    protected void lbtnNazad_Click(object sender, EventArgs e)
    {
        string predmet_kod = Request.QueryString["predmet_kod"];
        string predmet = Request.QueryString["predmet"];

        Response.Redirect("~/ProfessorSubject.aspx?predmet_kod=" + predmet_kod + "&predmet=" + predmet);
    }
}