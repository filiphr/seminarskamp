using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class Site1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int32 brKorisnici = Convert.ToInt32(Application["korisnici"]);

            lblKorisnici.Text = String.Format("На страната се логирани {0} корисници", brKorisnici);
        }
        // Create an instance of the XmlSiteMapProvider class.
        XmlSiteMapProvider testXmlProvider = new XmlSiteMapProvider();
        NameValueCollection providerAttributes = new NameValueCollection(1);
        providerAttributes.Add("siteMapFile", "test.sitemap");
        // Initialize the provider with a provider name and file name.
        testXmlProvider.Initialize("testProvider", providerAttributes);


        if (Session["indeks"] == null)
        {
            Label1.Visible = false;
        }
        else
        {
            Label1.Visible = true;
        }
    }

    /// <summary>
    /// Настан кој се генерира кога некој корисник е одјавен од апликацијата
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        if (Session["indeks"] != null)
        {
            Application.Lock();
            Application["korisnici"] = (int)Application["korisnici"] - 1;
            Application.UnLock();
            Session.Abandon();
        }
       
        Label1.Visible = false;
    }

}
