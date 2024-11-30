using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Todolist
{
    public partial class todolist : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridData();
            }
        }

        private void BindGridData()
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_ReadTasks", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gvTasks.DataSource = table;
                gvTasks.DataBind();
            }
        }
   
        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_CreateTask", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", txtTitle.Text);
                command.ExecuteNonQuery();
                BindGridData();
                txtTitle.Text = string.Empty;
            }
        }

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditTask")
            {
                int taskId = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_ReadTask", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", taskId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        txtTaskId.Text = reader["Id"].ToString();
                        txtEditTitle.Text = reader["Title"].ToString();
                    }
                    reader.Close();
                    pnlAddTask.Visible = false;
                    pnlEditTask.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteTask")
            {
                int taskId = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_DeleteTask", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", taskId);
                    command.ExecuteNonQuery();
                    BindGridData();
                }
            }
            if (e.CommandName == "CompleteTask")
            {
                int taskId = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_UpdateTaskStatus", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", taskId);
                    command.Parameters.AddWithValue("@Status", true); // Set status to true when task is completed
                    command.ExecuteNonQuery();
                    BindGridData();
                }
            }
            else if (e.CommandName == "IncompleteTask")
            {
                int taskId = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_UpdateTaskStatus", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", taskId);
                    command.Parameters.AddWithValue("@Status", false); // Set status to false when task is not completed
                    command.ExecuteNonQuery();
                    BindGridData();
                }
            }
        }

        protected void btnUpdateTask_Click(object sender, EventArgs e)
        {
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_UpdateTask", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", txtTaskId.Text);
                command.Parameters.AddWithValue("@Title", txtEditTitle.Text);
                command.ExecuteNonQuery();
                BindGridData();
                pnlAddTask.Visible = true;
                pnlEditTask.Visible = false;
            }
        }

        protected void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            pnlAddTask.Visible = true;
            pnlEditTask.Visible = false;
        }

        
    }
}