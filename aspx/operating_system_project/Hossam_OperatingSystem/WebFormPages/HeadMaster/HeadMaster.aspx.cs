using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HeadMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Password"] != null)
        {
            if (!IsPostBack)
            {
                DataTable ta = new DataTable();
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                ta = ws.GetManagers(Session["UserName"].ToString(), Session["Password"].ToString());

                UserName_Label.Text ="Welcome   "+ Session["Name"].ToString();
                GridView.DataSource = ta;
                GridView.DataBind();
            }
        }
                else Response.Redirect(@"~/Login.aspx");
    }
    protected void Button_managers_Click(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Password"] != null)
            {
                DataTable ta = new DataTable();
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                ta = ws.GetManagers(Session["UserName"].ToString(), Session["Password"].ToString());

                UserName_Label.Text = Session["Name"].ToString();
                GridView.DataSource = ta;
                GridView.DataBind();
            }
        else Response.Redirect(@"~/Login.aspx");
    }
    protected void Button_employees_Click(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Password"] != null)
            {
                DataTable ta = new DataTable();
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                ta = ws.GetAllEmployees(Session["UserName"].ToString(), Session["Password"].ToString());

                UserName_Label.Text = Session["Name"].ToString();
                GridView.DataSource = ta;
                GridView.DataBind();
            }
        else Response.Redirect(@"~/Login.aspx");

    }
    protected void Button_employees_by_managers_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (int.TryParse(TextBoxManagerId.Text, out  id))
        {
            if (Session["UserName"] != null && Session["Password"] != null)
            {
                DataTable ta = new DataTable();
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                ta = ws.GetEmployeesByManagerId(Session["UserName"].ToString(), Session["Password"].ToString(), id);

                UserName_Label.Text = Session["Name"].ToString();
                GridView.DataSource = ta;
                GridView.DataBind();
            }
            else Response.Redirect(@"~/Login.aspx");
        }
        else TextBoxManagerId.Text = "enter a number";
    }
    protected void Button_AddManagers_Click(object sender, EventArgs e)
    {
         
        if (Session["UserName"] != null && Session["Password"] != null)
        {
            Response.Redirect("/WebFormPages/HeadMaster/AddManagers.aspx");
        }
        else Response.Redirect(@"~/Login.aspx");
    }
}