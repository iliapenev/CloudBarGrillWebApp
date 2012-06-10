using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using RestSharp;
using System.Configuration;

namespace CloudBarGrillWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        private CloudBarGrillDataEntities context = new CloudBarGrillDataEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //  if (ConfigurationManager.AppSettings["ISONHOLIDAY"] == "Yes")
            var menuItems = (from all in context.Items select all);
            if (!IsPostBack)
            {
                DataTable table = new DataTable();
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Ingredients", typeof(string));
                table.Columns.Add("Price", typeof(decimal));

                DataRow row;

                foreach (var item in menuItems)
                {
                    row = table.NewRow();
                    row["Name"] = item.Name;
                    row["Ingredients"] = item.Ingredients;
                    row["Price"] = item.Price.ToString("#0.00");
                    table.Rows.Add(row);
                }
                
                MainMenuGrid.DataSource = table;
                MainMenuGrid.DataBind();
            }
        }

       

        protected void OrderButton_Click(object sender, EventArgs e)
        {
            List<Order> currentOrder = new List<Order>();
            currentOrder = getOrders();

            if (sendEmailCheck.Checked == true)
            {
                sendEmailWithOrder(emailInputBox.Value, currentOrder);
            }
            storeOrderInDB(currentOrder);
            nameInputBox.Value = "";
            addressInputBox.Value = "";
            emailInputBox.Value = "";
        }

        private List<Order> getOrders()
        {
            List<Order> orderList = new List<Order>();
            foreach (GridViewRow gvr in MainMenuGrid.Rows)
            {
                if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                {
                    Order order = new Order();
                    order.Name = nameInputBox.Value;
                    order.Address = addressInputBox.Value ;
                    order.Email = emailInputBox.Value;
                    order.ItemName = gvr.Cells[1].Text;
                    orderList.Add(order);
                }
            }
            return orderList;
        }

        private void sendEmailWithOrder(string toEmail, List<Order> currentOrder)
        {
            StringBuilder emailText = new StringBuilder();
            foreach (var item in currentOrder)
	        {
		        emailText.Append("Item: " + item.ItemName + " User: " + item.Name + " Address: " + item.Address + " e-mail: " + item.Email + "\n");
	        }

            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", ConfigurationManager.AppSettings["MAILGUN_API_KEY"]);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "app907.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "CloudBarGrill <cloudbargrill@cloudbargrill.apphb.com>");
            request.AddParameter("to", toEmail);
            request.AddParameter("subject", "Your Order at CloudBarGrill");

            request.AddParameter("text", "Hello,\nyour order is \n\n" + emailText + "\n\n Regards,\n CloudBarGrill");
            request.Method = Method.POST;
            
            var response = client.Execute(request);
            Label2.Text = " E-mail status: " + response.StatusDescription;
        }

        private void storeOrderInDB(List<Order> currentOrder)
        {
               
            foreach (var item in currentOrder)
	            {
                    try
                        {
                            context.Orders.AddObject(item);
                            context.SaveChanges();
                            Label1.Text = "Successfuly made order";
                        }
                    catch (Exception ex)
                        {
                            Label1.Text = ex.Message;
                        }
                   }
          }

        protected void AddItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddItem.aspx");
        }
     
        protected void GetOrders_Click(object sender, EventArgs e)
        {
            var menuItems = (from all in context.Orders select all);

            Response.AddHeader("Content-disposition", "attachment; filename=" + "orders.txt");
            Response.ContentType = "application/octet-stream";
            Response.Clear();

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Response.OutputStream, Encoding.UTF8))
            {
                foreach (var item in menuItems)
                {
                   writer.WriteLine("Item: " + item.ItemName + " User: " + item.Name + " Address: " + item.Address + " e-mail: " + item.Email);
                   writer.WriteLine("---");
                }
            }
            Response.End();
        }
 
    }
}



/*
 * Create an ASP.NET “Online Restaurant” application which displays a restaurant menu. 
 * Each item in the menu has a name, ingredients and a price. 
 * The menu should be visible to everyone. 
 * There should be administrators, which authenticate with a username and password and 
 * can add items to the menu. Deploy the application to AppHarbor
Edit the “Online Restaurant” application from exercise 4 
 * so that it can be switched off from AppHarbor 
 * (i.e. display only a message, saying “The restaurant is on a holiday”).
 * Hint: use configuration variables

Edit the “Online Restaurant” application, so that items from the menu can be ordered (purchased). 
 * When a user orders an item he should enter his name, address and e-mail address.
 * After that, an e-mail which notifies the user he successfully made an order  
 * should be sent (the e-mail should mention the user’s order, name and address).
 * The administrators should be able to download a text file with 
 * all of the information for all of the orders (item ordered, user name, address, e-mail).
*/