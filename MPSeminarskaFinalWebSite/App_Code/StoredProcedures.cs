using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Collections;

/// <summary>
/// Summary description for StoredProcedures
/// </summary>
public class StoredProcedures
{
	public StoredProcedures()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	
	/// <summary>
	/// Го враќа максималниот број на поени, процентот за положување и процентот кој влегува во оценката за одреден премет
	/// </summary>
	/// <returns>Листа со Максимален број на поени, процент за положување и процент со кој влегува во оценката</returns>
    /// <param name="predmetKod">Кодот на предметот</param>
    /// <param name="uslovIme">Името на условот</param>
	public static ArrayList GetMaksPoeniPredmet(String uslovIme, int predmetKod)
	{
        DataTable helpTable = new DataTable();
		SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand("getMaksPoeniPredmet", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@uslov_ime", SqlDbType.NVarChar).Value = uslovIme;
            cmd.Parameters.Add("@predmet_kod ", SqlDbType.Int).Value = predmetKod;

            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(helpTable);

        }
        catch
        {  }
        finally
        {
            connection.Close();
        }

        ArrayList poeni = new ArrayList();
        if (helpTable.Rows.Count != 0)
        {
            poeni.AddRange(helpTable.Rows[0].ItemArray);
        }
		return poeni;
	}


    public static int RemoveUslov(String uslov, int predmet_kod)
    {
        int efekt = 0;
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
       
        SqlCommand komanda = new SqlCommand("RemoveUslov", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@uslov_ime", SqlDbType.NVarChar).Value = uslov;
        komanda.Parameters.Add("@predmet_kod ", SqlDbType.Int).Value = predmet_kod;

        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            throw new Exception(err.Message, err);       
        }
        finally
        {
            konekcija.Close();
        }
        return efekt;
    }

    public static int UpdatePredmetUslov(String uslov, int min, int procent, int predmet_kod)
    {
        int efekt = 0;
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        SqlCommand komanda = new SqlCommand("UpdateUslov", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@uslov_ime", SqlDbType.NVarChar).Value = uslov;
        komanda.Parameters.Add("@predmet_kod ", SqlDbType.Int).Value = predmet_kod;
        komanda.Parameters.Add("@min_procent", SqlDbType.Real).Value = min;
        komanda.Parameters.Add("@procent ", SqlDbType.Real).Value = procent;

        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            throw new Exception(err.Message, err);
        }
        finally
        {
            konekcija.Close();
        }
        return efekt;
    }

    public static int UpdateSkala(int ocena, int dolna, int gorna, int predmet_kod)
    {
        int efekt = 0;
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("UpdateSkala", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@min", SqlDbType.Real).Value = dolna;
        komanda.Parameters.Add("@maks ", SqlDbType.Real).Value = gorna;
        komanda.Parameters.Add("@ocena", SqlDbType.Int).Value = ocena;
        komanda.Parameters.Add("@predmet_kod ", SqlDbType.Int).Value = predmet_kod;
        
        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();

        }
        catch (Exception err)
        {
            throw new Exception(err.Message, err);
        }
        finally
        {
            konekcija.Close();
        }
        return efekt;
    }

    public static DataSet GetSkalaWithKod(int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetSkalaWithKod", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@predmet_kod", SqlDbType.Int).Value = predmet_kod;


        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Skala");
        }
        catch (Exception err) {
            throw new Exception(err.Message, err);
        }
        finally
        {
            konekcija.Close();
        }

        return ds;
    }

    public static DataSet GetUslovWithKod(int predmet_kod)
    {

        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        SqlCommand komanda = new SqlCommand("GetPredmetUslovWithKod", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@predmet_kod", SqlDbType.Int).Value = predmet_kod;


        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds);
        }
        catch (Exception err) { 
            throw new Exception (err.Message, err);
        }
        finally
        {
            konekcija.Close();
        }
        return ds;
    }
}