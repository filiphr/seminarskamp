using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Овој веб сервис Ви овозможува да прегледате просечен успех по одреден предмет. Во полето услов внесете го името на условот кој сакате да го проверите, а соодветниот код за предметот во полето код.")]
    public string PresmetajProsek(string uslov, int kod)
    {
        string prosek = "";

        string konekcijaString =ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(konekcijaString);

        SqlCommand kom1 = new SqlCommand();
        kom1.CommandText = " SELECT AVG(Poeni) FROM Osvoeni_Poeni WHERE uslov_ime=@uslov AND predmet_kod=@kod";
        kom1.Parameters.Add("@uslov", SqlDbType.NVarChar).Value = uslov;
        kom1.Parameters.Add("@kod", SqlDbType.Int).Value = kod;
        kom1.Connection = con;

        try
        {
            con.Open();
            prosek = kom1.ExecuteScalar().ToString();
        }
        catch
        {

        }
        finally
        {
            con.Close();
        }
        
        return prosek;
    }



}