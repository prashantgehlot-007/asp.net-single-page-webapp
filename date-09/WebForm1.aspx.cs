using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace date_09
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=H:\Mywork\date-09\date-09\App_Data\Database1.mdf;Integrated Security=True");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //save
            try
            {
                cmd = new SqlCommand("Insert Into Emp (ECode,EName,ESal) values(@ECode,@EName,@ESal)",conn);
                cmd.Parameters.AddWithValue("ECode", TextBox1.Text);
                cmd.Parameters.AddWithValue("EName", TextBox2.Text);
                cmd.Parameters.AddWithValue("ESal", TextBox3.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("data saved");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Search button
            try
            {
                cmd = new SqlCommand("Select * From Emp Where ECode = @ECode", conn);
                cmd.Parameters.AddWithValue("@ECode", TextBox1.Text);
                conn.Open();
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    TextBox1.Text = dr["ECode"].ToString();
                    TextBox2.Text = dr["EName"].ToString();
                    TextBox3.Text = dr["ESal"].ToString();
                }
                else
                {
                    Response.Write("Data not found");
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //update date
            try
            {
                cmd = new SqlCommand("Update Emp Set EName = @EName,ESal = @ESal Where ECode = @ECode", conn);
                cmd.Parameters.AddWithValue("@ECode", TextBox1.Text);
                cmd.Parameters.AddWithValue("@EName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@ESal", TextBox3.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("Data updated");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //Delete button
            try
            {
                cmd = new SqlCommand("Delete From Emp Where ECode = @ECode", conn);
                cmd.Parameters.AddWithValue("@ECode", TextBox1.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("Date Updated");
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //show all
            SqlDataAdapter da = new SqlDataAdapter("Select * From Emp", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Emp");
            GridView1.DataSource = ds.Tables["Emp"];
            GridView1.DataBind();
        }
    }
}