using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IzborNaPredmeti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // don't reload data during postbacks
        if (!IsPostBack)
        {
            PopulateControls();
            
        }
    }

    private void PopulateControls()
    {
        if (User.IsInRole("Студент"))
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand kom = new SqlCommand();
           
            kom.CommandText = " SELECT Predmet.Ime, Predmet.Kod, Predmet.Slika FROM Predmet INNER JOIN   Student_Predmet  ON Student_Predmet.predmet_kod=Predmet.Kod WHERE  student_indeks=@indeks";
            kom.Parameters.AddWithValue("@indeks", Session["indeks"].ToString());

           

            kom.Connection = con;
            SqlDataAdapter adapter = new SqlDataAdapter(kom);

          
            DataSet ds = new DataSet();
          

            try
            {
                con.Open();
                adapter.Fill(ds, "Predmeti");
                
                ((DataList)this.LoginView1.FindControl("list")).DataSource = ds.Tables["Predmeti"];
                ((DataList)this.LoginView1.FindControl("list")).DataBind();

               
            }
            catch (Exception e)
            {
                // lblporaka.Text = e.Message;
            }
            finally
            {
                con.Close();
            }
        }

        if (User.IsInRole("Професор"))
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlCommand kom1 = new SqlCommand();
            kom1.CommandText = " SELECT Predmet.Ime, Predmet.Kod, Predmet.Slika FROM Predmet, Predmet_Profesor as pp WHERE pp.Kod=Predmet.Kod AND profesor_kod=@indeks";  
            
            kom1.Parameters.AddWithValue("@indeks", Session["indeks"].ToString());
            kom1.Connection = con;
            SqlDataAdapter adapter1 = new SqlDataAdapter(kom1);
            DataSet ds1 = new DataSet();
            try
            {
                con.Open();

                adapter1.Fill(ds1, "Predmeti");


                ((DataList)this.LoginView1.FindControl("list1")).DataSource = ds1.Tables["Predmeti"];
                ((DataList)this.LoginView1.FindControl("list1")).DataBind();
            }
            catch (Exception e)
            {
                // lblporaka.Text = e.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void LoginView1_ViewChanged(object sender, EventArgs e)
    {

    }
}