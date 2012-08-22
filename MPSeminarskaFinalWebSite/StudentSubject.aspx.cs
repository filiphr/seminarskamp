using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentSubject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            String predmet = Request.QueryString["ime_predmet"];
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

            lblSkala.Text = "Скалата по предметот " + predmet + " е следната";

            IspolniSkalaTabela(predmet_kod);
            IspolniOcenkaGrid();
        }
        else
        {
            if (Cache["tblProverka"] != null)
            {
                Table tblProverka = Cache["tblProverka"] as Table;
                tblProverkaHolder.Controls.Add(tblProverka);
            }
        }
    }

    private double PresmetajPoeni()
    {
        double vkupno = 0;
        Table tblProverka = (tblProverkaHolder.Controls[0] as Table);
        foreach (TableRow row in tblProverka.Rows)
        {
            if (!(row is TableHeaderRow))
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        if (control is TextBox)
                        {
                            double tmp;
                            Double.TryParse(((TextBox)control).Text, out tmp);
                            vkupno += tmp;
                        }
                    }
                }
            }
        }

        return vkupno;
    }

    private void IspolniOcenkaGrid()
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "SELECT * FROM Student_Predmet WHERE predmet_kod=@kod AND student_indeks=@indeks";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);

        komanda.Parameters.AddWithValue("@kod", 3);
        komanda.Parameters.AddWithValue("@indeks", "63/2009");

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataSet ds = new DataSet();

        try
        {
            konekcija.Open();
            adapter.Fill(ds, "Poeni");
            gvRealna.DataSource = ds;
            gvRealna.DataBind();

            if (ds.Tables[0].Rows.Count != 0)
            {
                ispolniProverka(ds);
                lblRealna.Text = "Моментални резултати";
                lblProverka.Text = " Проверете си каква оценка со можете да добиете со вашите поени и колку ви фали за повисока оценка";
            } else
            {
                lblRealna.Text = "Немате внесено поени по овој предмет";
            }

            ViewState["PoeniDataSet"] = ds;
        }
        catch { }
        finally
        {
            konekcija.Close();
        }
    }

    private void ispolniProverka(DataSet ds)
    {
        Table tblProverka = new Table();
        tblProverka.CssClass = "tabelaProverka";
        tblProverka.GridLines = GridLines.Both;


        TableHeaderRow headerRows = new TableHeaderRow();
        TableHeaderCell cell = null;
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            cell = new TableHeaderCell();
            cell.Text = dc.ColumnName;
            cell.CssClass = "headerRow";
            headerRows.Cells.Add(cell);
        }
        headerRows.Cells.RemoveAt(0);
        headerRows.Cells.RemoveAt(0);
        tblProverka.Rows.Add(headerRows);

        TableCell dataCell = null;
        TableRow dataRow = null;
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            dataRow = new TableRow();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                dataCell = new TableCell();
                TextBox dataTextBox = new TextBox();
                dataTextBox.Text = row[dc].ToString();
                dataTextBox.CssClass = "dataText";
                //dataCell.Text = row[dc].ToString();
                dataCell.Controls.Add(dataTextBox);
                dataRow.Cells.Add(dataCell);
            }
            dataRow.Cells.RemoveAt(0);
            dataRow.Cells.RemoveAt(0);
            tblProverka.Rows.Add(dataRow);
        }

        Cache["tblProverka"] = tblProverka;
        tblProverkaHolder.Controls.Add(tblProverka);
    }

    private void IspolniSkalaTabela(int predmet_kod)
    {
        string konekcijaString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection konekcija = new SqlConnection(konekcijaString);
        string sqlString = "SELECT * FROM Skala WHERE predmet_kod=@kod";
        SqlCommand komanda = new SqlCommand(sqlString, konekcija);
        komanda.Parameters.AddWithValue("@kod", 3);

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
    protected void presmetaj_Click(object sender, EventArgs e)
    {
        double vkupno = PresmetajPoeni();
        lblPoeni.Text = vkupno.ToString();
        string ocena = PresmetajOcena(vkupno);
        lblOcenka.Text = ocena;
    }

    private string PresmetajOcena(double vkupno)
    {
        string ocena = "5";
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
}