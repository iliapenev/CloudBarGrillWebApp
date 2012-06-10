using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CloudBarGrillWebApp
{
    public partial class Task3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["ISONAPPHARBOR"] == "No")
            {
                TextBox1.Text = "You are not on AppHarbor";
            }
            else
            {
                TextBox1.Text = "You are Somewhere";
            }
            if (ConfigurationManager.AppSettings["ISONAPPHARBOR"] == "Yes")
            {
                TextBox1.Text = "You are on AppHarbor";
            }
        }
    }
}