using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;

public partial class ProfessorSubject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String predmet = Request.QueryString["predmet"];
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

            lblSkala.Text = "Скалата по предметот " + predmet + " е следната";
            lblUslov.Text = "Променете ги условите и границите за " + predmet;

            IspolniSkala(predmet_kod);
            IspolniUslov(predmet_kod);
        }
    }


    /// <summary>
    /// Функција која се повикува да се исполни GridView за условите
    /// </summary>
    /// <param name="predmet_kod"></param>
    private void IspolniUslov(int predmet_kod)
    {
        try
        {
            DataSet ds = StoredProcedures.GetPredmetUslovWithKod(predmet_kod);
            gvUslov.DataSource = ds;
            gvUslov.DataBind();
            ViewState["UslovDataSet"] = ds;
        }
        catch { } 
    }

    /// <summary>
    /// Функција која се повикува за да се исполни GridView за скалата
    /// </summary>
    /// <param name="predmet_kod"></param>
    private void IspolniSkala(int predmet_kod)
    {
        try
        {
            DataSet ds = StoredProcedures.GetSkalaWithKod(predmet_kod);
            gvSkala.DataSource = ds;
            gvSkala.DataBind();
            ViewState["SkalaDataSet"] = ds;
        }
        catch { }
    }



    /// <summary>
    /// Настан кој се генерира кога се започнува со ажурирање на GridView gvSkala
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSkala_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["SkalaDataSet"];
        gvSkala.EditIndex = e.NewEditIndex;
        gvSkala.DataSource = ds;
        gvSkala.DataBind();
    }

    /// <summary>
    /// Настан кој се генерира кога се откажува ажурирање на GridVIew gvSkala
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSkala_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["SkalaDataSet"];
        gvSkala.EditIndex = -1;
        gvSkala.DataSource = ds;
        gvSkala.DataBind();
        lblSkalaEror.Visible = false;
    }

    /// <summary>
    /// Настан кој се генерира кога ажурираме оценка
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSkala_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowUpdating = e.RowIndex;
        TextBox tbDolna = (TextBox)gvSkala.Rows[rowUpdating].Cells[1].Controls[0];
        TextBox tbGorna = (TextBox)gvSkala.Rows[rowUpdating].Cells[2].Controls[0];

        String errorMessage = "";

        int dolna, gorna;
        bool update = true;

        if (!Int32.TryParse(tbDolna.Text, out dolna))
        {
            update = false;
        }

        if (!Int32.TryParse(tbGorna.Text, out gorna))
        {
            update = false;
        }

        if (update)
        {
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

            int ocena;
            Int32.TryParse(gvSkala.Rows[rowUpdating].Cells[0].Text, out ocena);

            int efekt = 0;

            try
            {
                efekt = StoredProcedures.UpdateSkala(ocena, dolna, gorna, predmet_kod);
                lblSkalaEror.Visible = false;
            }
            catch (Exception err)
            {
                lblSkalaEror.Text = err.Message;
                lblSkalaEror.Visible = true;
            }
            finally
            {
                gvSkala.EditIndex = -1;
            }

            if (efekt != 0)
            {
                IspolniSkala(predmet_kod);
            }
        }
        else
        {
            errorMessage += "Внесете нумерички граници";
            lblSkalaEror.Text = errorMessage;
            lblSkalaEror.Visible = true;
        }
    }

    /// <summary>
    /// Настан кој се генерира кој некој услов почнува да се ажурира
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUslov_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.EditIndex = e.NewEditIndex;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }

    /// <summary>
    /// Настан кој настанува кога се откажува променување
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUslov_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.EditIndex = -1;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }

    /// <summary>
    /// Настан кој се повикува кога се ажурира некој услов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUslov_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowUpdating = e.RowIndex;
        TextBox tbMin = (TextBox)gvUslov.Rows[rowUpdating].Cells[1].Controls[0];
        TextBox tbProcent = (TextBox)gvUslov.Rows[rowUpdating].Cells[2].Controls[0];
        TextBox tbMaks = (TextBox)gvUslov.Rows[rowUpdating].Cells[3].Controls[0];
        String uslov = gvUslov.Rows[rowUpdating].Cells[0].Text;

        String errorMessage = "";

        double min, procent, maks;
        bool update = true;

        if (!Double.TryParse(tbMin.Text, out min))
        {
            update = false;
        }

        if (!Double.TryParse(tbProcent.Text, out procent))
        {
            update = false;
        }

        if (!Double.TryParse(tbMaks.Text, out maks))
        {
            update = false;
        }

        if (update)
        {
            int efekt = 0;
            int predmet_kod;
            Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);
            try
            {
                efekt = StoredProcedures.UpdatePredmetUslov(uslov, min, procent, predmet_kod, maks);
                lblUslovError.Visible = false;
            }
            catch (Exception err)
            {
                lblUslovError.Text = err.Message;
                lblUslovError.Visible = true;
            }
            finally
            {
                gvUslov.EditIndex = -1;
            }

            if (efekt != 0)
            {
                IspolniUslov(predmet_kod);

            } else
                IspolniUslov(predmet_kod);
        }
        else
        {
            errorMessage += "Внесете нумерички граници";
            lblUslovError.Text = errorMessage;
            lblUslovError.Visible = true;
        }
    }



    /// <summary>
    /// Настан за бришење на еден Услов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUslov_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int rowDeleting = e.RowIndex;

        String uslov = gvUslov.Rows[rowDeleting].Cells[0].Text;
        int predmet_kod;
        Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

        int efekt = 0;

        try
        {
            efekt = StoredProcedures.RemoveUslov(uslov, predmet_kod);
            lblUslovError.Visible = false;
        }
        catch (Exception err)
        {
            lblUslovError.Text = err.Message;
            lblUslovError.Visible = true;
        }
        finally
        {
            gvUslov.EditIndex = -1;
        }

        if (efekt != 0)
        {

            IspolniUslov(predmet_kod);
        }
    }


    /// <summary>
    /// Настан кој се повикува кога се менува страница во GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUslov_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUslov.PageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)ViewState["UslovDataSet"];
        gvUslov.SelectedIndex = -1;
        gvUslov.EditIndex = -1;
        gvUslov.DataSource = ds;
        gvUslov.DataBind();
    }

    /// <summary>
    /// Пренасочување на нова страна каде што корисникот може да внесе нова скала
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovaSkala_Click(object sender, EventArgs e)
    {
        string predmet_kod = Request.QueryString["predmet_kod"];
        string predmet = Request.QueryString["predmet"];

        Response.Redirect("~/NovaSkala.aspx?predmet_kod=" + predmet_kod + "&predmet=" + predmet);
    }

    protected void Import_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        // if you have Excel 2007 uncomment this line of code
        //  string excelConnectionString =string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0",path);

        string ExcelContentType = "application/vnd.ms-excel";
        string Excel2010ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        if (FileUpload1.HasFile)
        {
            //Провери го Content Type на фајлот
            if (FileUpload1.PostedFile.ContentType == ExcelContentType || FileUpload1.PostedFile.ContentType == Excel2010ContentType)
            {
                //Зачувај ја патеката
                string path = string.Concat(Server.MapPath("~/TempFiles/"), FileUpload1.FileName);
                try
                {

                    //Зачувај го фајлот како привремен, подоцна може да се избрише
                    FileUpload1.SaveAs(path);

                    string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);

                    DataSet ds = new DataSet();

                    //Креирање на конекција до Excel Workbook
                    using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
                    {

                        OleDbCommand command = new OleDbCommand
                                ("Select * FROM [" + tbSheetName.Text + "$]", connection);
                        command.CommandType = CommandType.Text;

                        connection.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        adapter.Fill(ds, "ExcelData");

                        int predmet_kod;
                        Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod);

                        StoredProcedures.UpdateOsvoeniPoeni(ds, predmet_kod, tbIndeksFormat.Text);
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
                finally
                {
                    File.Delete(path);
                }
            }
        }
    }
}