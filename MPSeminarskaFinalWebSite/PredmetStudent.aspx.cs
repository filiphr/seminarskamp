using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


/// <summary>
/// .aspx Страна на која се прикажува предмет кој го одбрал даден студент
/// </summary>
public partial class PredmetStudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            String predmet = Request.QueryString["predmet"];
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

            lblSkala.Text = "Скалата по предметот " + predmet + " е следната";

            IspolniSkalaTabela(predmet_kod);
            IspolniPoeniTabela(predmet_kod);
            IspolniUslov(predmet_kod);
        }
        else
        {
            if (Cache["tabelaPoeni"] != null)
            {
                Table tblPoeni = Cache["tabelaPoeni"] as Table;
                tblPoeniHolder.Controls.Add(tblPoeni);
            }
        }
    }

    /// <summary>
    /// Функција со која се исполнува GriedView за условот
    /// </summary>
    /// <param name="predmet_kod">Кодот според кој ќе го најдеме предметот</param>
    private void IspolniUslov(int predmet_kod)
    {
        try
        {
            DataSet ds = StoriraniProceduri.GetPredmetUslovWithKod(predmet_kod);
            gvUslov.DataSource = ds;
            gvUslov.DataBind();

            ViewState["UslovDataSet"] = ds;
        }
        catch { }
    }


    /// <summary>
    /// Функција со која се исполнува табелата со поени на студентот
    /// </summary>
    /// <param name="predmet_kod">Кодот според кој ќе ги најдеме поените</param>
    private void IspolniPoeniTabela(int predmet_kod)
    {
        string indeks = Session["indeks"].ToString();

        DataSet ds = StoriraniProceduri.GetOsvoeniPoeniAndUslov(predmet_kod, indeks);

        if (ds.Tables[0].Rows.Count != 0)
        {
            ispolniPoeni(ds);
            lblRealna.Text = "Моментални резултати";
            lblProverka.Text = "Внесете ги вашите поени во текст полињата доколку сакате да си ја проверите можната оценка. Внесете не нумерички податок за да не ви се пресметува соодветниот услов";
        }
        else
        {
            lblRealna.Text = "Немате внесено поени по овој предмет";
        }

        ViewState["PoeniDataSet"] = ds;
    }

    

    /// <summary>
    /// Функција која ја исполнува табелата со освоените поени и со поените кои може самиот корисник да си ги менува
    /// </summary>
    /// <param name="ds">DataSet од каде што се повлекуваат податоците за табелата</param>
    private void ispolniPoeni(DataSet ds)
    {
        Table tblPoeni = new Table();
        tblPoeni.CssClass = "tabelaPoeni";
        tblPoeni.GridLines = GridLines.Both;

        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell = null;

        TableRow dataRow = new TableRow();
        TableCell dataCell = null;

        TableRow inputRow = new TableRow();
        TableCell inputCell = null;

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            headerCell = new TableHeaderCell();
            headerCell.Text = row[0].ToString() + "<br/>(макс " + row[2].ToString() + " поени)";
            headerRow.Cells.Add(headerCell);

            dataCell = new TableCell();
            dataCell.Text = row[1].ToString();
            dataRow.Cells.Add(dataCell);

            inputCell = new TableCell();
            TextBox inputTextBox = new TextBox();
            inputTextBox.Text = row[1].ToString();
            inputCell.Controls.Add(inputTextBox);
            inputRow.Cells.Add(inputCell);
        }

        tblPoeni.Rows.Add(headerRow);
        tblPoeni.Rows.Add(dataRow);
        tblPoeni.Rows.Add(inputRow);
        Cache["tabelaPoeni"] = tblPoeni;
        tblPoeniHolder.Controls.Add(tblPoeni);
    }

    /// <summary>
    /// Функција која го исполнува GridView-то каде што се наоѓа скалата
    /// </summary>
    /// <param name="predmet_kod">Кодот на предметот за кој се исполнува скалата</param>
    private void IspolniSkalaTabela(int predmet_kod)
    {
        DataSet ds =StoriraniProceduri.GetSkala(predmet_kod);
        gvSkala.DataSource = ds;
        gvSkala.DataBind();
        ViewState["SkalaDataSet"] = ds;
    }

   

    /// <summary>
    /// Клик на копчето за пресметување на оценка
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void presmetaj_Click(object sender, EventArgs e)
    {
        double vkupno = PresmetajPoeni();
        if (vkupno >= 0)
        {
            lblPoeni.Text = String.Format("Имате вкупно {0:f2} поени.", vkupno);
        }
        string ocena = PresmetajOcena(vkupno);
        lblOcenka.Text = ocena;
    }


    /// <summary>
    /// Функција со која се пресметуваат вкупниот број на поени (проценти) на студентот
    /// </summary>
    /// <returns>Бројот на освоени поени (проценти)</returns>
    private double PresmetajPoeni()
    {
        int predmet_kod;
        Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);


        Table tblPoeni = (tblPoeniHolder.Controls[0] as Table);

        //Мапа каде што како клуч се чува условот а како вредност, освоените поени за тој услов
        Dictionary<String, String> uslovPoeni = new Dictionary<string, string>();

        int numberOfCells = tblPoeni.Rows[0].Cells.Count;

        for (int i = 0; i < numberOfCells; i++)
        {
            string help = tblPoeni.Rows[0].Cells[i].Text;
            string valueString = ((TextBox)tblPoeni.Rows[2].Cells[i].Controls[0]).Text;

            //Одстрани го додатниот текст од клучот
            string keyString = help.Remove(help.IndexOf("<br/>"));

            //Додај го парот Име на Услов со освоени поени за тој услов
            uslovPoeni.Add(keyString, valueString);
        }

        double vkupno = 0;
        foreach (KeyValuePair<string, string> par in uslovPoeni)
        {
            double osvoeniPoeni;
            if (Double.TryParse(par.Value, out osvoeniPoeni))
            {
                //Доколку е нумеричка вредност, доколку не треба да влезе во вкупниот број на поени
                //корисникот треба да внесе не валиден податок
                ArrayList maksPoeni = StoriraniProceduri.GetMaksPoeniPredmet(par.Key, predmet_kod);
                double maksMozni = Convert.ToDouble(maksPoeni[0]);//Максимален број на поени кој студентот може да ги освои за одредениот услов
                double minProcent = Convert.ToDouble(maksPoeni[1]);//Минималниот процент кој треба студентот да го освои за да го положи предметот
                double procent = Convert.ToDouble(maksPoeni[2]);//Процент со кој условот влегува во финалната оценка

                double osvoenProcent = 0;
                if (maksMozni != 0)
                {
                    osvoenProcent = osvoeniPoeni / maksMozni * 100;
                }

                if (osvoenProcent < minProcent)
                {
                    double falatPoeni = minProcent / 100 * maksMozni - osvoeniPoeni;

                    lblPoeni.Text = String.Format("Не го исполнувате условот да имате минимум {0} на {1}" 
                       + " за да имате шанса да го положите предметот. Потребни ви се уште {2:f2}"
                        + " за да го задоволите условот за {1}", minProcent, par.Key, falatPoeni);
                    return -1;
                }

                vkupno += osvoenProcent * procent / 100; //Додај колку проценти влегуваат во вкупниот број
            }
        }

        return vkupno;
    }

    /// <summary>
    /// Врз осова на вкупниот број на поени ја пресметува оценката која сутдентот треба да ја добие
    /// </summary>
    /// <param name="vkupno">Вкупниот број на освоени проценти</param>
    /// <returns>Оценката која студентот ја има</returns>
    private string PresmetajOcena(double vkupno)
    {
        string ocena = "5";

        if (vkupno < 0)
        {
            return ocena;
        }
        double maxO = 0;
        DataSet dsSkala = ViewState["SkalaDataSet"] as DataSet;
        foreach (DataRow row in dsSkala.Tables[0].Rows)
        {
            double min, max;
            Double.TryParse(row[2].ToString(), out min);
            Double.TryParse(row[3].ToString(), out max);
            maxO = max;
            if (vkupno >= min && vkupno <= max)
            {
                return row[1].ToString();
            }
        }

        if (vkupno > maxO)
            ocena = "10";

        return ocena;
    }

    /// <summary>
    /// Справување со настанот кога се менува страна во GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUslov_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUslov.PageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.SelectedIndex = -1;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }
}