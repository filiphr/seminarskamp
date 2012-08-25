using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;

/// <summary>
/// .aspx Страна преку која професор (асистент) може да прикачи excel фајл со поени за студентите или да провери просек по даден услов за предметот
/// </summary>
public partial class PostaviPoeni : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Настан кој се генрира при прикачување на excel фајлот
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Import_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
    
        string ExcelContentType = "application/vnd.ms-excel";
        string Excel2010ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        if (FileUpload1.HasFile)
        {
            //Провери го Content Type на фајлот
            if (FileUpload1.PostedFile.ContentType == ExcelContentType || FileUpload1.PostedFile.ContentType == Excel2010ContentType)
            {
                //Зачувај ја патеката
                string path = string.Concat(Server.MapPath("~/TempFiles/"), FileUpload1.FileName);

                int efekt = 0;
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

                        efekt = StoriraniProceduri.UpdateOsvoeniPoeni(ds, predmet_kod, tbIndeksFormat.Text);
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

                if (efekt != 0)
                {
                    lblError.Text = String.Format("Успешно извршивте внесување на {0} ќелии.", efekt);
                }
            }
        }
    }

    /// <summary>
    /// Настан кој повикува пресметка на просек по даден услов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnUslovProsek_Click(object sender, EventArgs e)
    {
        string uslov_ime = tbUslovProsek.Text;
        int predmet_kod;
        if (Int32.TryParse(Request.QueryString["predmet_kod"], out predmet_kod))
        {
            WebService prosek = new WebService();
            string prosekResult = prosek.PresmetajProsek(uslov_ime, predmet_kod);
            lblProsek.Text = String.Format("Просекот на вашите студенти на {0} е {1}", uslov_ime, prosekResult);
        }
    }
}
