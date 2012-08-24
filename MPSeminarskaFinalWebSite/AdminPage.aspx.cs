using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class AdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IspolniPredmeti();

        }
        else
        {
            if (lstStudenti.SelectedIndex != -1)
            {
                if (Cache["tabelaPoeniStudent"] != null)
                {
                    Table tblPoeni = Cache["tabelaPoeniStudent"] as Table;
                    tblStudentPoeniHolder.Controls.Add(tblPoeni);
                    btnPromeniPoeni.Visible = true;
                }
            }
        }
    }

    /// <summary>
    /// Функција со која се исполнува листата со студенти за даден предмет
    /// </summary>
    private void IspolniStudenti()
    {
        //TODO да се додаде во повикот predmet_kod
        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);
        List<ListItem> studenti = StoredProcedures.GetStudentiOrdered(predmet_kod);
        lstStudenti.Items.AddRange(studenti.ToArray());
    }

    
    /// <summary>
    /// Функција со која се исполнува листата со предмети за даден професор
    /// </summary>
    protected void IspolniPredmeti()
    {
        String profesor_kod = Session["indeks"].ToString();
        List<ListItem> predmeti = StoredProcedures.GetPredmetiOrdered(profesor_kod);
        lstPredmeti.Items.AddRange(predmeti.ToArray());
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се селектира друг предмет
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstPredmeti_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstStudenti.Items.Clear();
        hideStudentPoeni();
        IspolniStudenti();
        int kod;
        if (Int32.TryParse(lstPredmeti.SelectedValue, out kod))
        {
            selektirajPredmet(kod);
        }
    }

    /// <summary>
    /// Функција која го крие копччете за промена на поени и табелата со поени за даден студент
    /// </summary>
    private void hideStudentPoeni()
    {
        btnPromeniPoeni.Visible = false;
        tblStudentPoeniHolder.Visible = false;
    }

    /// <summary>
    /// Функција која се повикува при селектирање даден предмет и соодветно ги пополнува името и URL-то на сликата
    /// </summary>
    /// <param name="kod">Кој предмет треба да се селектира</param>
    private void selektirajPredmet(int kod)
    {
        List<String> predmetInfo = StoredProcedures.GetPredmet(kod);
        txtImePredmet.Text = predmetInfo[0];
        txtSlika.Text = predmetInfo[1];
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне на копчето за променување на предмет
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPromeni_Click(object sender, EventArgs e)
    {
        string predmet_ime = txtImePredmet.Text;
        string predmet_slika = txtSlika.Text;
        int selectedIndex = lstPredmeti.SelectedIndex;
        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);
      

        StoredProcedures.UpdatePredmet(predmet_ime, predmet_slika, predmet_kod);
        lstPredmeti.Items.Clear();
        IspolniPredmeti();
        //TODO Мислам дека нема потреба да се тргаат
       // IzbrisiPodatociP();
        lstPredmeti.SelectedIndex = selectedIndex;
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне исчисти копчето за предмет
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnIscisti_Click(object sender, EventArgs e)
    {
        IzbrisiPodatociP();
    }

    /// <summary>
    /// Исчисти ги текст полињата за предметот
    /// </summary>
    protected void IzbrisiPodatociP()
    {
        txtImePredmet.Text = "";
        txtSlika.Text = "";
    }

    /// <summary>
    ///  Настан кој се генерира кога ќе се кликне внеси копчето за предмет
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVnesi_Click(object sender, EventArgs e)
    {
        ///TODO Make stored procedure
        lblPoraka.Text = "";
        string kodpredmet = "0";

        string konekcijaString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        
        SqlCommand komanda = new SqlCommand();
        
        komanda.Connection = konekcija;
        
        komanda.CommandText = " INSERT INTO Predmet (Ime, Slika) VALUES (@Ime, @Slika)";
        komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtImePredmet.Text;
        komanda.Parameters.Add("@Slika", SqlDbType.NVarChar).Value = txtSlika.Text;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();


        }

        catch (Exception e1)
        {
            if (e1.Message.Contains("Violation of PRIMARY KEY"))
            {
                lblPoraka.Text = "Неможете да го внесете предметот бидејќи веќе постои таков.";
            }
            else
            {
                lblPoraka.Text = e1.Message;
            }

        }
        finally
        {
            konekcija.Close();
        }

        
        SqlCommand komanda2 = new SqlCommand();
        
        komanda2.Connection = konekcija;

        komanda2.CommandText = "SELECT Kod FROM Predmet WHERE Ime=@ime";

        komanda2.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtImePredmet.Text;
        //komanda2.Parameters.AddWithValue("@ime", txtImePredmet.Text);
        try
        {
            konekcija.Open();

            SqlDataReader citac = komanda2.ExecuteReader();
            if (citac.Read())
            {

                kodpredmet = citac["Kod"].ToString();
            }

            citac.Close();

        }

        catch (Exception e1)
        {
            if (e1.Message.Contains("Violation of PRIMARY KEY"))
            {
                lblPoraka.Text = "Неможете да го внесете предметот бидејќи веќе постои таков.";
            }
            else
            {
                lblPoraka.Text = e1.Message;
            }
        }
        finally
        {
            konekcija.Close();
        }

        SqlCommand komanda1 = new SqlCommand();
        komanda1.Connection = konekcija;
        komanda1.CommandText = " INSERT INTO Predmet_Profesor (Kod, profesor_kod) VALUES (@Kod, @indeks)";
        komanda1.Parameters.AddWithValue("@Kod", kodpredmet);
        komanda1.Parameters.AddWithValue("@indeks", Session["indeks"].ToString());


        try
        {
            konekcija.Open();
            komanda1.ExecuteNonQuery();


        }

        catch (Exception e1)
        {
            if (e1.Message.Contains("Violation of PRIMARY KEY"))
            {
                lblPoraka.Text = "Неможете да го внесете предметот бидејќи веќе постои таков.";
            }
            else
            {
                lblPoraka.Text = e1.Message;
            }
        }
        finally
        {
            konekcija.Close();
        }
        lstPredmeti.Items.Clear();
        IspolniPredmeti();
        
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне избриши копчето за предмет
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBrisi_Click(object sender, EventArgs e)
    {
        if (lstPredmeti.SelectedIndex != -1)
        {
            int predmet_kod;
            if (Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod))
            {
                StoredProcedures.DeletePredmet(predmet_kod);

                lstPredmeti.Items.Clear();
                lstStudenti.Items.Clear();
                IspolniPredmeti();
                IscistiPodatociS();
                IzbrisiPodatociP();
                hideStudentPoeni();
            }
        }
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне промени копчето за студент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPromeniS_Click(object sender, EventArgs e)
    {
        string student_ime = txtImeStudent.Text;
        string student_prezime = txtPrezimeStudent.Text;
        string indeks = lstStudenti.SelectedValue;

        StoredProcedures.UpdateStudent(student_ime, student_prezime, indeks);
        lstStudenti.Items.Clear();
        IspolniStudenti();
        //TODO nema potreba da se cistat
        //IscistiPodatociS();
    }

    
    /// <summary>
    /// Настан кој се генрира кога ќе се селектира нов студент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lstStudenti_SelectedIndexChanged(object sender, EventArgs e)
    {
        string indeks = lstStudenti.SelectedValue;
        selektirajStudent(indeks);
        
    }

    private void selektirajStudent(string indeks)
    {
        //TODO extract stored procedure
        string konekcijaString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection konekcija = new SqlConnection(konekcijaString);
      
        string sqlString = "SELECT * FROM Student  WHERE Indeks= @indeks";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;
        try
        {
            konekcija.Open();
            SqlDataReader citac = komanda.ExecuteReader();
            if (citac.Read())
            {
                txtImeStudent.Text = citac["Ime"].ToString();
                txtPrezimeStudent.Text = citac["Prezime"].ToString();
                txtIndeksStudent.Text = citac["Indeks"].ToString();
                citac.Close();

            }

        }
        catch (Exception e)
        {
            lblPoraka1.Text = e.Message;
        }
        finally
        {
            konekcija.Close();
        }

        konekcija = new SqlConnection();
        konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        sqlString = "SELECT uslov_ime, Poeni FROM Osvoeni_Poeni WHERE student_indeks=@indeks AND predmet_kod=@predmet_kod";
        komanda = new SqlCommand(sqlString, konekcija);
        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);
        komanda.Parameters.Add("@predmet_kod", SqlDbType.Int).Value = predmet_kod;
        komanda.Parameters.Add("@indeks", SqlDbType.NVarChar).Value = indeks;

        try
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(komanda);
            adapter.Fill(ds, "StudentPoeni");
            ispolniPoeni(ds);
        }
        catch (Exception e)
        {
            lblPoraka1.Text = e.Message;
        }
        finally
        {
            konekcija.Close();
        }
       
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне на исчисти копчето за студент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnIscistiS_Click(object sender, EventArgs e)
    {
        IscistiPodatociS();
    }

    protected void btnVnesiS_Click(object sender, EventArgs e)
    {
        //TODO extract stored procedure
        SqlConnection konekcija = new SqlConnection();
        konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlCommand komanda = new SqlCommand();
        

        komanda.Connection = konekcija;
        

        komanda.CommandText = " INSERT INTO Student (Indeks, Ime, Prezime ) VALUES (@Indeks, @Ime, @Prezime)";
        komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtImeStudent.Text;
        komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = txtPrezimeStudent.Text;
        komanda.Parameters.Add("@Indeks", SqlDbType.NVarChar).Value = txtIndeksStudent.Text;

        try
        {
            konekcija.Open();
            komanda.ExecuteNonQuery();


        }

        catch (Exception e1)
        {
            if (e1.Message.Contains("Violation of PRIMARY KEY"))
            {
                lblPoraka1.Text = "Неможете да го внесете студентот бидејќи веќе постои таков.";
            }
            else
            {
                lblPoraka1.Text = e1.Message;
            }

        }
        finally
        {
            konekcija.Close();
        }

        SqlCommand komanda1 = new SqlCommand();
        komanda1.Connection = konekcija;
        komanda1.CommandText = " INSERT INTO Student_Predmet (student_indeks, predmet_kod) VALUES (@student_indeks, @predmet_kod)";
        komanda1.Parameters.AddWithValue("@student_indeks", txtIndeksStudent.Text);
        komanda1.Parameters.AddWithValue("@predmet_kod", lstPredmeti.SelectedValue);


        try
        {
            konekcija.Open();
            komanda1.ExecuteNonQuery();


        }

        catch (Exception e1)
        {
            if (e1.Message.Contains("Violation of PRIMARY KEY"))
            {
                lblPoraka1.Text = "Неможете да го внесете студентот бидејќи веќе постои таков.";
            }
            else
            {
                lblPoraka1.Text = e1.Message;
            }
        }
        finally
        {
            konekcija.Close();
        }
        lstStudenti.Items.Clear();
        IspolniStudenti();
        
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне на избриши копчето за студент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnIzbrisiS_Click(object sender, EventArgs e)
    {
        if (lstPredmeti.SelectedIndex != -1)
        {
            string indeks = lstStudenti.SelectedValue;

            StoredProcedures.DeleteStudent(indeks);

            lstStudenti.Items.Clear();
            IspolniStudenti();
            IscistiPodatociS();
        }
    }

    /// <summary>
    /// Исчистување на текст полињата за студент
    /// </summary>
    protected void IscistiPodatociS()
    {
        txtImeStudent.Text = "";
        txtPrezimeStudent.Text = "";
        txtIndeksStudent.Text = "";
    }

    /// <summary>
    /// Функција која ја исполнува табелата со поените кои ги има соодветниот студент
    /// </summary>
    /// <param name="ds">DataSet од каде што се повлекуваат податоците за табелата</param>
    private void ispolniPoeni(DataSet ds)
    {
        tblStudentPoeniHolder.Controls.Clear();
        Table tblPoeni = new Table();
        tblPoeni.CssClass = "tabelaPoeni";
        tblPoeni.GridLines = GridLines.Both;

        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell = null;

        TableRow inputRow = new TableRow();
        TableCell inputCell = null;

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            headerCell = new TableHeaderCell();
            headerCell.Text = row[0].ToString();
            headerRow.Cells.Add(headerCell);

            inputCell = new TableCell();
            TextBox inputTextBox = new TextBox();
            inputTextBox.Text = row[1].ToString();
            inputCell.Controls.Add(inputTextBox);
            inputRow.Cells.Add(inputCell);
        }

        tblPoeni.Rows.Add(headerRow);
        tblPoeni.Rows.Add(inputRow);
        Cache["tabelaPoeniStudent"] = tblPoeni;
        tblStudentPoeniHolder.Visible = true;
        btnPromeniPoeni.Visible = true;
        tblStudentPoeniHolder.Controls.Add(tblPoeni);
    }

}