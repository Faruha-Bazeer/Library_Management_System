using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class ReturnBooks : Form
    {
        public ReturnBooks()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Int64 MemberNo = Int64.Parse(txtEnter.Text);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * From Issue_Return_Books Where MNo =" + MemberNo + "AND Book_Return_Date IS NULL";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
             
            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Invalid Member Number Or No Book Issued.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ReturnBooks_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            txtEnter.Clear();
        }

        string Bname;
        string Bdate;
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel1.Visible=true;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                Bname = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                Bdate = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
            txtBname.Text = Bname;
            txtDate.Text = Bdate;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Int64 MemberNo = Int64.Parse(txtEnter.Text);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            con.Open();
            cmd.CommandText = "Update Issue_Return_Books Set Book_Return_Date = '" + dateTimePicker1.Text+ "' Where MNo = " + MemberNo + "";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Return Successful.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            ReturnBooks_Load(this, null);
        }

        private void txtEnter_TextChanged(object sender, EventArgs e)
        {
            if(txtEnter.Text == "")
            {
                panel1.Visible = false;
                dataGridView1.DataSource = null;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnter.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ? ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible=false;
        }
    }
}
