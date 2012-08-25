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
/// Класа во која се наоѓаат сторираните процедури кои се користат
/// </summary>
public class StoriraniProceduri
{
    public StoriraniProceduri()
    {

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


    /// <summary>
    /// Отстранување на одреден услов од Predmet_Uslov табелата
    /// </summary>
    /// <param name="uslov">Име на условот</param>
    /// <param name="predmet_kod" > Код на предметот</param>
    /// <returns>Број на редови кои се променети</returns>
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

    /// <summary>
    /// Update на табела Predmet_Uslov
    /// </summary>
    /// <param name="uslov">Име на услов</param>
    /// <param name="min_procent">Нов минимален процент</param>
    /// <param name="procent">Нов процент</param>
    /// <param name="predmet_kod">Код на предметот</param>
    /// <param name="maks">Нови максимални поени</param>
    /// <returns>Број на редови кои се променети</returns>
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


    /// <summary>
    /// Update на бодовната скала
    /// </summary>
    /// <param name="ocena">Оцена за која се прави Update</param>
    /// <param name="dolna">Нова долна граница</param>
    /// <param name="gorna">Нова горна граница</param>
    /// <param name="predmet_kod">Предмет на код за кој се менува бодовната скала</param>
    /// <returns>Број на редови кои се променети</returns>
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


    /// <summary>
    /// Превземање на бодовна скала
    /// </summary>
    /// <param name="predmet_kod">Код на предмет за кој сакаме да ја добиеме бодовната скала</param>
    /// <returns>Dataset во кој се наоѓа новата бодовна скала</returns>
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


    /// <summary>
    /// Превземање на информации за условите на даден предмет
    /// </summary>
    /// <param name="predmet_kod">Кодот на предметот за кој се превземаат информациите</param>
    /// <returns>DataSet во кој се наоѓаат информациите</returns>
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


    /// <summary>
    /// Променување на освоените поени за даден предмет и индекс
    /// </summary>
    /// <param name="ds">DataSet добиен од excel фајл</param>
    /// <param name="predmet_kod">Код на предметот за кој треба да се внесат поени</param>
    /// <param name="indeks">Името на колоната за индексот во DataSet-тот</param>
    /// <returns>Број на промените ќелии</returns>
    public static int UpdateOsvoeniPoeni(DataSet ds, int predmet_kod, string indeks)
    {
        int efekt = 0;
        DataColumnCollection dcColection = ds.Tables[0].Columns;

        DataColumn indeksCol = dcColection[indeks];

        DataTable indeksi = new DataView(ds.Tables[0]).ToTable(false, indeks);

        dcColection.Remove(indeksCol);


        foreach (DataColumn col in dcColection)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                string student_indeks = indeksi.Rows[i][indeks].ToString();
                string uslov_ime = col.ColumnName;
                double poeni;
                Double.TryParse(ds.Tables[0].Rows[i][col].ToString(), out poeni);
                efekt += UpdateOneRowOsvoeniPoeni(uslov_ime, predmet_kod, student_indeks, poeni);
            }

        }
        return efekt;
    }


    /// <summary>
    /// Промена на еден ред од освоени поени
    /// </summary>
    /// <param name="uslov_ime">Име на услов</param>
    /// <param name="predmet_kod">Код на предмет</param>
    /// <param name="student_indeks">Индекс на студент</param>
    /// <param name="poeni">Број на поени</param>
    /// <returns>Дали е успешна извршена промената</returns>
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

    /// <summary>
    /// Сортирана листа со студенти
    /// </summary>
    /// <param name="predmet_kod">Код на предмет за кој се превземаат студентите</param>
    /// <returns>Листа со ListItem која се користи како извор во листата со студенти</returns>
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
        catch
        {
        }
        finally
        {
            konekcija.Close();
        }
        return studenti;
    }

    /// <summary>
    /// Сортирана листа со предмети
    /// </summary>
    /// <param name="profesor_kod">Код на предметот за кој сакаме да добиеме информации</param>
    /// <returns>>Листа со ListItem која се користи како извор во листата со предмети</returns>
    public static List<ListItem> GetPredmetiOrdered(String profesor_kod)
    {

        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetPredmetiOrdered", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@profesor_kod", SqlDbType.NVarChar).Value = profesor_kod;

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
        catch
        {

        }
        finally
        {
            konekcija.Close();
        }

        return predmeti;
    }

    /// <summary>
    /// Добивање на информации одреден предмет во одреден формат
    /// </summary>
    /// <param name="kod">од на предмеотот</param>
    /// <returns>Листа во која на индкес [0] се ноѓа имте на предметот, а на индекс [1] URL-то за сликата </returns>
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
        catch
        {

        }
        finally
        {
            konekcija.Close();
        }
        return info;
    }

    /// <summary>
    /// Промена на даден предмет
    /// </summary>
    /// <param name="predmet_ime">Ново име на предметот</param>
    /// <param name="predmet_slika">Нов стринг за URL на сликата</param>
    /// <param name="predmet_kod">Код на предметот за кој се прави промената</param>
    /// <returns>дали успешно се извршила промената</returns>
    public static int UpdatePredmet(string predmet_ime, string predmet_slika, int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("UpdatePredmet", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@ime", SqlDbType.NVarChar).Value = predmet_ime;
        komanda.Parameters.Add("@slika", SqlDbType.NVarChar).Value = predmet_slika;
        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = predmet_kod;
        int efekt = 0;

        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();

        }
        catch (Exception e1)
        {
            string message = "";
            if (e1.Message.Contains("Violation of UNIQUE KEY"))
            {
                message = "Предмет со такво име веќе постои, изберете друго име";
            }
            else
            {
                message = "Настана грешка при променувањето";
            }
            throw new Exception(message);
        }
        finally
        {
            konekcija.Close();
        }
        return efekt;
    }

    /// <summary>
    /// Бришење на даден предмет од базата
    /// </summary>
    /// <param name="predmet_kod">Код на предметот кој треба да се избрише</param>
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
        catch
        {
        }
        finally
        {
            konekcija.Close();
        }
    }

    /// <summary>
    /// Промена на информации за даден студент
    /// </summary>
    /// <param name="student_ime">Ново име на студентот</param>
    /// <param name="student_prezime">Ново презиме на студентот</param>
    /// <param name="indeks">Индекс на студентот кој треба да се промени</param>
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
        catch
        {

        }
        finally
        {
            konekcija.Close();
        }

    }

    /// <summary>
    /// Бришење на даден студент
    /// </summary>
    /// <param name="indeks">Индекс на студентот кој треба да се избрише</param>
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
        catch
        {

        }
        finally
        {
            konekcija.Close();
        }

    }

    /// <summary>
    /// Информации за Освоените поени за одредени Услови
    /// </summary>
    /// <param name="predmet_kod">Код на предметот за кој сакаме да добиеме информации</param>
    /// <param name="indeks">Индекс на студентот за кој сакаме да добиеме информации</param>
    /// <returns>DataSet во кој се наоѓаат потребните информации</returns>
    public static DataSet GetOsvoeniPoeniAndUslov(int predmet_kod, string indeks)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetOsvoeniPoeniAndUslov", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = predmet_kod;
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Poeni");
        }
        catch { }
        finally
        {
            konekcija.Close();
        }
        return ds;
    }

    /// <summary>
    /// Бодовна скала за даден предмет
    /// </summary>
    /// <param name="predmet_kod">Код на предметот за кој ни треба бодовната скала</param>
    /// <returns>DataSet во кој се наоѓа бодовната скала</returns>
    public static DataSet GetSkala(int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetSkala", konekcija);

        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = predmet_kod;

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Skala");
        }
        catch { }
        finally
        {
            konekcija.Close();
        }
        return ds;
    }


    /// <summary>
    /// Превземање на предмети за одреден студент
    /// </summary>
    /// <param name="indeks">Индекс на студентот за кој сакаме да ги добиеме предметите</param>
    /// <returns>DataSet во кој се наоѓаат информациите</returns>
    public static DataSet GetPredmetiForStudent(string indeks)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetPredmetiForStudent", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);

        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Predmeti");
        }
        catch
        {
        }
        finally
        {
            konekcija.Close();
        }

        return ds;
    }

    /// <summary>
    /// Превземање на предмети за одреден професор(асистент)
    /// </summary>
    /// <param name="profesor_kod">Код на предмет за кој сакаме да добиеме информации</param>
    /// <returns>DataSet во кој се наоѓаат информациите</returns>
    public static DataSet GetPredmetiForProfesor(string profesor_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetPredmetiOrdered", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@profesor_kod", SqlDbType.NVarChar).Value = profesor_kod;

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);

        DataSet predmeti = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(predmeti, "Predmeti");
        }
        catch
        {
        }
        finally
        {
            konekcija.Close();
        }

        return predmeti;
    }

    /// <summary>
    /// Процедура со која внесуваме предмет и го добиваме кодот на ново внесениот предмет
    /// доколку таков предмет веќе постои го добиваме само кодот на предметот
    /// </summary>
    /// <param name="ime">Име на предметот</param>
    /// <param name="slika">стринг за URL на сликата</param>
    /// <returns>string кој претставува кодот на предметот со одреденото име </returns>
    public static String InsertPredmetAndGetKod(string ime, string slika)
    {
        string predmet_kod = null;

        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("InsertPredmetAndGetKod", konekcija);

        komanda.CommandType = CommandType.StoredProcedure;
        //komanda.CommandText = " INSERT INTO Predmet (Ime, Slika) VALUES (@Ime, @Slika)";
        komanda.Parameters.Add("@ime", SqlDbType.NVarChar).Value = ime;
        komanda.Parameters.Add("@slika", SqlDbType.NVarChar).Value = slika;

        try
        {
            konekcija.Open();
            predmet_kod = komanda.ExecuteScalar().ToString();
        }

        catch
        {
        }
        finally
        {
            konekcija.Close();
        }

        if (predmet_kod == null)
        {
            predmet_kod = GetPredmetKod(ime);
        }
        return predmet_kod;
    }

    /// <summary>
    /// Код на предмет со одредено име
    /// </summary>
    /// <param name="ime">Име на предметот за кој сакаме да добиеме код</param>
    /// <returns>Стринг кој го претставува кодот на бараниот предмет</returns>
    private static string GetPredmetKod(string ime)
    {
        string predmet_kod = null;
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetPredmetKod", konekcija);

        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@ime", SqlDbType.NVarChar).Value = ime;

        try
        {
            konekcija.Open();
            predmet_kod = komanda.ExecuteScalar().ToString();
        }

        catch
        {
        }
        finally
        {
            konekcija.Close();
        }
        return predmet_kod;
    }

    /// <summary>
    /// Внес на врска помеѓу професор и предмет
    /// </summary>
    /// <param name="profesor_kod">Кодот на професорот</param>
    /// <param name="predmet_kod">Кодот на предметот</param>
    /// <returns>Дали успешно е извршен внесот</returns>
    public static int InsertPredmetProfesor(int profesor_kod, int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("InsertPredmetProfesor", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@kod", SqlDbType.Int).Value = predmet_kod;
        komanda.Parameters.Add("@profesor_kod", SqlDbType.Int).Value = profesor_kod;


        int efekt = 0;
        try
        {
            konekcija.Open();
            efekt = komanda.ExecuteNonQuery();
        }

        catch
        {
        }
        finally
        {
            konekcija.Close();
        }
        return efekt;
    }


    /// <summary>
    /// Добивање на информации на студент во одреден формат
    /// </summary>
    /// <param name="indeks">Индекс на студентот за кој бараме информации</param>
    /// <returns>Листа на која на индекс [0] се наоѓа индексот, на [2] името и на [3] презимето на бараниот студент</returns>
    public static List<string> GetStudent(string indeks)
    {
        List<string> studentInfo = new List<string>();
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("GetStudent", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;
        try
        {
            konekcija.Open();
            SqlDataReader citac = komanda.ExecuteReader();
            if (citac.Read())
            {
                studentInfo.Add(citac["Indeks"].ToString());
                studentInfo.Add(citac["Ime"].ToString());
                studentInfo.Add(citac["Prezime"].ToString());

                citac.Close();
            }

        }
        catch
        {

        }
        finally
        {
            konekcija.Close();
        }

        return studentInfo;
    }

    /// <summary>
    /// Бришење на врската помеѓу студент и даден предмет
    /// </summary>
    /// <param name="indeks">Индекс на студент кој се брише од врската</param>
    /// <param name="predmet_kod">Кодот на предметот за кој треба да се избрише студентот</param>
    public static void DeleteStudentFromStudentPredmet(string indeks, int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("DeleteStudentFromStudentPredmet", konekcija);
        komanda.CommandType = CommandType.StoredProcedure;
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;
        komanda.Parameters.Add("@predmet_kod", SqlDbType.Int).Value = predmet_kod;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();

        }
        catch
        {
        }
        finally
        {
            konekcija.Close();
        }


    }

    /// <summary>
    /// Внес на инфромации за студент
    /// </summary>
    /// <param name="student_indeks">Индекс на студентот</param>
    /// <param name="student_ime">Име на студентот</param>
    /// <param name="student_prezime">Презиме на студентот</param>
    public static void InsertStudent(string student_indeks, string student_ime, string student_prezime)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        SqlCommand komanda = new SqlCommand("InsertStudent", konekcija);

        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = student_ime;
        komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = student_prezime;
        komanda.Parameters.Add("@Indeks", SqlDbType.NVarChar).Value = student_indeks;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();
        }

        catch
        {
        }
        finally
        {
            konekcija.Close();
        }
    }


    /// <summary>
    /// Поврзување на студент со одреден предмет
    /// </summary>
    /// <param name="student_indeks">Индекс на студентот кој треба да се поврзе</param>
    /// <param name="predmet_kod">Код на предметот со кој треба да се поврзе</param>
    public static void InsertStudentIntoStudentPredmet(String student_indeks, int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);

        SqlCommand komanda = new SqlCommand("InsertStudentIntoStudentPredmet", konekcija);

        komanda.CommandType = CommandType.StoredProcedure;

        komanda.Parameters.Add("@student_indeks", SqlDbType.NVarChar).Value = student_indeks;
        komanda.Parameters.Add("@predmet_kod", SqlDbType.Int).Value = predmet_kod;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();
        }

        catch
        {

        }
        finally
        {
            konekcija.Close();
        }

    }

    /// <summary>
    /// Промена на освоени поени на студент од страна на професор
    /// </summary>
    /// <param name="uslovPoeni">име на услов за кој се променуваат поените</param>
    /// <param name="student_indeks">Индекс на студентот за кој внесуваат податоци</param>
    /// <param name="predmet_kod">Код на предметот за кој се внесува податоци</param>
    /// <returns>Колку услови успешно се променети</returns>
    public static int UpdateOsvoeniPoeniProfesor(Dictionary<string, string> uslovPoeni, string student_indeks, int predmet_kod)
    {
        int efekt = 0;

        foreach (KeyValuePair<string, string> par in uslovPoeni)
        {
            string uslov_ime = par.Key;
            double poeni;
            if (Double.TryParse(par.Value, out poeni))
            {
                efekt += UpdateOneRowOsvoeniPoeni(uslov_ime, predmet_kod, student_indeks, poeni);
            }
        }
        return efekt;
    }
}