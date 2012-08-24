using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Collections;
using System.Web.UI.WebControls;

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
		{ }
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

	public static int UpdatePredmetUslov(String uslov, double min_procent, double procent, int predmet_kod, double maks)
	{
		int efekt = 0;
		string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
		SqlConnection konekcija = new SqlConnection(konekcijaString);
		SqlCommand komanda = new SqlCommand("UpdateUslov", konekcija);
		komanda.CommandType = CommandType.StoredProcedure;

		komanda.Parameters.Add("@uslov_ime", SqlDbType.NVarChar).Value = uslov;
		komanda.Parameters.Add("@predmet_kod ", SqlDbType.Int).Value = predmet_kod;
		komanda.Parameters.Add("@min_procent", SqlDbType.Real).Value = min_procent;
		komanda.Parameters.Add("@procent ", SqlDbType.Real).Value = procent;
		komanda.Parameters.Add("@maks_poeni ", SqlDbType.Real).Value = maks;

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

	public static int UpdateSkala(int ocena, double dolna, double gorna, int predmet_kod)
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
		catch (Exception err)
		{
			throw new Exception(err.Message, err);
		}
		finally
		{
			konekcija.Close();
		}

		return ds;
	}

	public static DataSet GetPredmetUslovWithKod(int predmet_kod)
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
		catch (Exception err)
		{
			throw new Exception(err.Message, err);
		}
		finally
		{
			konekcija.Close();
		}

		return ds;
	}

	public static void UpdateOsvoeniPoeni(DataSet ds, int predmet_kod, string indeks)
	{
		DataColumnCollection dcColection = ds.Tables[0].Columns;

		DataColumn indeksCol = dcColection[indeks];

		DataTable indeksi = new DataView(ds.Tables[0]).ToTable(false, indeks);

		dcColection.Remove(indeksCol);

		
			foreach (DataColumn col in dcColection)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{

					string student_indeks =  indeksi.Rows[i][indeks].ToString();
					string uslov_ime = col.ColumnName;
					double poeni;
					Double.TryParse(ds.Tables[0].Rows[i][col].ToString(), out poeni);
					UpdateOneRowOsvoeniPoeni(uslov_ime, predmet_kod, student_indeks, poeni);
				}

			}

	}

	private static int UpdateOneRowOsvoeniPoeni(string uslov_ime, int predmet_kod, string student_indeks, double poeni)
	{
		int efekt = 0;
		string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
		SqlConnection konekcija = new SqlConnection(konekcijaString);

		SqlCommand komanda = new SqlCommand("UpdateOneRowOsvoeniPoeni", konekcija);
		komanda.CommandType = CommandType.StoredProcedure;

		komanda.Parameters.Add("@uslov_ime", SqlDbType.NVarChar).Value = uslov_ime;
		komanda.Parameters.Add("@predmet_kod ", SqlDbType.Int).Value = predmet_kod;
		komanda.Parameters.Add("@student_indeks", SqlDbType.NVarChar).Value = student_indeks;
		komanda.Parameters.Add("@poeni ", SqlDbType.Real).Value = poeni;

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

	public static List<ListItem> GetStudentiOrdered(int predmet_kod)
	{
		
		string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetStudentiOrdered", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@predmet_kod", SqlDbType.Int).Value = predmet_kod;
        List<ListItem> studenti = new List<ListItem>();
        

		try
		{
			konekcija.Open();
			SqlDataReader citac = komanda.ExecuteReader();
			while (citac.Read())
			{
                ListItem element = new ListItem();
				element.Text = citac["Ime"].ToString() + " " + citac["Prezime"];
				element.Value = citac["Indeks"].ToString();
                studenti.Add(element);
			}
			citac.Close();
		}
		catch (Exception e)
		{
            //TODO
			//lblPoraka1.Text = e.Message;
		}
		finally
		{
			konekcija.Close();
		}
        return studenti;
	}

    public static List<ListItem> GetPredmetiOrdered(String profesor_kod)
    {
        
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetPredmetiOrdered", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        
        komanda.Parameters.Add("@profesor_kod", SqlDbType.NVarChar).Value=profesor_kod;

        List<ListItem> predmeti = new List<ListItem>();

        try
        {
            konekcija.Open();
            SqlDataReader citac = komanda.ExecuteReader();
            while (citac.Read())
            {

                ListItem element = new ListItem();
                element.Text = citac["Ime"].ToString();
                element.Value = citac["Kod"].ToString();
                predmeti.Add(element);

            }
            citac.Close();
        }
        catch (Exception e)
        {
            //TODO
            //lblPoraka.Text = e.Message;
        }
        finally
        {
            konekcija.Close();
        }

        return predmeti;
    }

    public static List<String> GetPredmet(int kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        
        SqlCommand komanda = new SqlCommand("GetPredmet", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = kod;
        List<String> info = new List<String>();
        try
        {
            konekcija.Open();
            SqlDataReader citac = komanda.ExecuteReader();
            if (citac.Read())
            {
                info.Add(citac["Ime"].ToString());
                info.Add(citac["Slika"].ToString());
                citac.Close();

            }

        }
        catch (Exception e)
        {
            //TODO
            //lblPoraka.Text = e.Message;
        }
        finally
        {
            konekcija.Close();
        }
        return info;
    }

    public static void UpdatePredmet(string predmet_ime, string predmet_slika, int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("UpdatePredmet", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@ime", SqlDbType.NVarChar).Value = predmet_ime;
        komanda.Parameters.Add("@slika", SqlDbType.NVarChar).Value = predmet_slika;
        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = predmet_kod;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();

        }
        catch (Exception e1)
        {
            //TODO
            //lblPoraka.Text = e1.Message;
        }
        finally
        {
            konekcija.Close();
        }
    }

    public static void DeletePredmet(int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("DeletePredmet", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = predmet_kod;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();

        }
        catch (Exception e1)
        {
            //TODO
           //lblPoraka.Text = e1.Message;
        }
        finally
        {
            konekcija.Close();
        }
    }

    public static void UpdateStudent(string student_ime, string student_prezime, string indeks)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("UpdateStudent", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@ime", SqlDbType.NVarChar).Value = student_ime;
        komanda.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = student_prezime;
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;


        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();

        }
        catch (Exception e1)
        {
            //TODO
            //lblPoraka.Text = e1.Message;
        }
        finally
        {
            konekcija.Close();
        }
        //TODO return
    }

    public static void DeleteStudent(string indeks)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("DeleteStudent", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();

        }
        catch (Exception e1)
        {
            //TODO
            //lblPoraka1.Text = e1.Message;
        }
        finally
        {
            konekcija.Close();
        }

        //TODO return
    }
}