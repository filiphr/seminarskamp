using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// .aspx Страна за администрација
/// </summary>
public partial class AdminStrana : System.Web.UI.Page
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
        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);
        List<ListItem> studenti = StoriraniProceduri.GetStudentiOrdered(predmet_kod);
        lstStudenti.Items.AddRange(studenti.ToArray());
    }

    
    /// <summary>
    /// Функција со која се исполнува листата со предмети за даден професор
    /// </summary>
    protected void IspolniPredmeti()
    {
        String profesor_kod = Session["indeks"].ToString();
        List<ListItem> predmeti = StoriraniProceduri.GetPredmetiOrdered(profesor_kod);
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
        lblPromeniPoeniPoraka.Text = "";
        btnPromeniPoeni.Visible = false;
        tblStudentPoeniHolder.Visible = false;
    }

    /// <summary>
    /// Функција која се повикува при селектирање даден предмет и соодветно ги пополнува името и URL-то на сликата
    /// </summary>
    /// <param name="kod">Кој предмет треба да се селектира</param>
    private void selektirajPredmet(int kod)
    {
        List<String> predmetInfo = StoriraniProceduri.GetPredmet(kod);
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
        lblPredmetPoraka.Text = "";
        string predmet_ime = txtImePredmet.Text;
        string predmet_slika = txtSlika.Text;
        int selectedIndex = lstPredmeti.SelectedIndex;
        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);
        int efekt = 0;

        try {
            efekt = StoriraniProceduri.UpdatePredmet(predmet_ime, predmet_slika, predmet_kod);
        }
        catch (Exception err)
        {
            lblPredmetPoraka.Text = err.Message;
        }

        if (efekt != 0)
        {
            lstPredmeti.Items.Clear();
            IspolniPredmeti();
            lstPredmeti.SelectedIndex = selectedIndex;
        }
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне исчисти копчето за предмет
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnIscisti_Click(object sender, EventArgs e)
    {
        IscistiPodatociPredmet();
    }

    /// <summary>
    /// Исчисти ги текст полињата за предметот
    /// </summary>
    protected void IscistiPodatociPredmet()
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
        lblPredmetPoraka.Text = "";
        string predmet_ime = txtImePredmet.Text;
        string predmet_slika = txtSlika.Text;
        int profesor_kod;

        string predmet_kodString = StoriraniProceduri.InsertPredmetAndGetKod(predmet_ime, predmet_slika);

        int predmet_kod;
        if (Int32.TryParse(Session["indeks"].ToString(), out profesor_kod) && Int32.TryParse(predmet_kodString, out predmet_kod))
        {
            int efekt = 0;
            efekt = StoriraniProceduri.InsertPredmetProfesor(profesor_kod, predmet_kod);
            if (efekt != 0)
            {
                lstPredmeti.Items.Clear();
                IspolniPredmeti();
            }
            else
            {
                lblPredmetPoraka.Text = "Веќе го предавате тој предмет";
            }
        }
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
                StoriraniProceduri.DeletePredmet(predmet_kod);

                lstPredmeti.Items.Clear();
                lstStudenti.Items.Clear();
                IspolniPredmeti();
                IscistiPodatociStudent();
                IscistiPodatociPredmet();
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

        StoriraniProceduri.UpdateStudent(student_ime, student_prezime, indeks);
        lstStudenti.Items.Clear();
        IspolniStudenti();
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

    /// <summary>
    /// Функција која се повикува кога се селектира студент
    /// </summary>
    /// <param name="indeks"></param>
    private void selektirajStudent(string indeks)
    {
        List<string> studentInfo = StoriraniProceduri.GetStudent(indeks);

        if (studentInfo.Count >= 3)
        {
            txtIndeksStudent.Text = studentInfo[0];// citac["Indeks"].ToString();
            txtImeStudent.Text = studentInfo[1];// citac["Ime"].ToString();
            txtPrezimeStudent.Text = studentInfo[2]; // citac["Prezime"].ToString();
        }

        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);

        DataSet ds = StoriraniProceduri.GetOsvoeniPoeniAndUslov(predmet_kod, indeks);
        ispolniPoeni(ds);
    }

   

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне на исчисти копчето за студент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnIscistiS_Click(object sender, EventArgs e)
    {
        IscistiPodatociStudent();
    }

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне на внеси копчето за студент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVnesiS_Click(object sender, EventArgs e)
    {
        string student_indeks = txtIndeksStudent.Text;
        string student_ime = txtImeStudent.Text;
        string student_prezime = txtPrezimeStudent.Text;


        StoriraniProceduri.InsertStudent(student_indeks, student_ime, student_prezime);

        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);

        try
        {
            StoriraniProceduri.InsertStudentIntoStudentPredmet(student_indeks, predmet_kod);
        }
        catch (Exception err)
        {
            lblPoraka1.Text = err.Message;
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
            int predmet_kod;
            Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);

            StoriraniProceduri.DeleteStudentFromStudentPredmet(indeks, predmet_kod);

            lstStudenti.Items.Clear();
            IspolniStudenti();
            IscistiPodatociStudent();
        }
    }

    /// <summary>
    /// Исчистување на текст полињата за студент
    /// </summary>
    protected void IscistiPodatociStudent()
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

    /// <summary>
    /// Настан кој се генерира кога ќе се кликне на промени копчето за поени
    /// Професорот променува поени на даден студент кој е селектиран во листа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPromeniPoeni_Click(object sender, EventArgs e)
    {
        int predmet_kod;
        Int32.TryParse(lstPredmeti.SelectedValue, out predmet_kod);

        string student_indeks = lstStudenti.SelectedValue;


        Table tblPoeni = (tblStudentPoeniHolder.Controls[0] as Table);

        //Мапа каде што како клуч се чува условот а како вредност, освоените поени за тој услов
        Dictionary<String, String> uslovPoeni = new Dictionary<string, string>();

        int numberOfCells = tblPoeni.Rows[0].Cells.Count;

        for (int i = 0; i < numberOfCells; i++)
        {
            string keyString = tblPoeni.Rows[0].Cells[i].Text;
            string valueString = ((TextBox)tblPoeni.Rows[1].Cells[i].Controls[0]).Text;

            //Додај го парот Име на Услов со освоени поени за тој услов
            uslovPoeni.Add(keyString, valueString);
        }

        int efekt = StoriraniProceduri.UpdateOsvoeniPoeniProfesor(uslovPoeni, student_indeks, predmet_kod);
        lblPromeniPoeniPoraka.Text = String.Format("Успешно се променети поени по {0} услови.", efekt);
    }
}