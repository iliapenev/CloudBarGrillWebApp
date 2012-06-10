using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CloudBarGrillWebApp
{

    public partial class AddItem : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");

        }
        protected void addItemButton_Click(object sender, EventArgs e)
        {
            HtmlInputText nameInputBox = (HtmlInputText)LoginView1.FindControl("nameInputBox");
            HtmlInputText ingredientsInputBox = (HtmlInputText)LoginView1.FindControl("ingredientsInputBox");
            HtmlInputText priceInputBox = (HtmlInputText)LoginView1.FindControl("priceInputBox");
            Label Label1 = (Label)LoginView1.FindControl("Label1");
                CloudBarGrillDataEntities context = new CloudBarGrillDataEntities();
                Item newItem = new Item();
                newItem.Name = nameInputBox.Value;
                newItem.Ingredients = ingredientsInputBox.Value;
                newItem.Price = decimal.Parse(priceInputBox.Value);

                try
                {
                    context.Items.AddObject(newItem);
                    context.SaveChanges();
                    Label1.Text = "Successfuly added item";
                    
                    if (IsPostBack)
                    {
                        nameInputBox.Value = "";
                        ingredientsInputBox.Value = "";
                        priceInputBox.Value = "";
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }

            

        }
    }
}