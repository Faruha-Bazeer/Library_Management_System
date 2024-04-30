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
    public partial class ViewBooks : Form
    {
        public ViewBooks()
        {
            InitializeComponent();
        }

        private void ViewBooks_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * From AddBook";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];




        }
        int BookID;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                BookID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * From AddBook where BookID= "+BookID+"";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            txtBisbn.Text = ds.Tables[0].Rows[0][1].ToString();
            txtBname.Text = ds.Tables[0].Rows[0][2].ToString();
            txtAuthorname.Text = ds.Tables[0].Rows[0][3].ToString();
            txtBpublication.Text = ds.Tables[0].Rows[0][4].ToString();
            txtDate.Text = ds.Tables[0].Rows[0][5].ToString();
            txtBprice.Text = ds.Tables[0].Rows[0][6].ToString();
            txtBquantity.Text = ds.Tables[0].Rows[0][7].ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if(txtBookName.Text!= "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Select * From AddBook where BookName LIKE '" + txtBookName.Text + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Select * From AddBook where BookID= " + BookID + "";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBookName.Clear();
            panel2.Visible = false;
            ViewBooks_Load(this, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string ISBN = txtBisbn.Text;
                string BookName = txtBname.Text;
                string BookAuthor = txtAuthorname.Text;
                string BookPublication = txtBpublication.Text;
                string BookPublicationDate = txtDate.Text;
                Int64 BookPrice = Int64.Parse(txtBprice.Text);
                Int64 BookQuantity = Int64.Parse(txtBquantity.Text);

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Update AddBook SET ISBN = '" + ISBN + "',BookName = '" + BookName + "',BookAuthor='" + BookAuthor + "' ,BookPublication='" + BookPublication + "' ,BookPublicationDate= '" + BookPublicationDate + "' ,BookPrice = " + BookPrice + ", BookQuantity = " + BookQuantity + "Where BookID=" + rowid + "";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                ViewBooks_Load(this, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be deleted. Confirmation Dialog?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Delete From AddBook Where BookID=" + rowid + "";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                ViewBooks_Load(this, null);
            }
        }
    }
}
