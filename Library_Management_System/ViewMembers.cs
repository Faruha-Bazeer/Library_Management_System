using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class ViewMembers : Form
    {
        public ViewMembers()
        {
            InitializeComponent();
        }

        private void textMno_TextChanged(object sender, EventArgs e)
        {
            if(textMno.Text != "")
            {
                lblView.Visible = false;
                Image image = Image.FromFile("E:/ITE 1942 - ICT Project - E2240238/Liberay Management System Icon and Images/Liberay Management System/search1.gif");
                pictureBox1.Image = image;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Select * From AddNewMember Where MNo LIKE '"+textMno.Text+"%'";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                lblView.Visible = true;
                Image image = Image.FromFile("E:/ITE 1942 - ICT Project - E2240238/Liberay Management System Icon and Images/Liberay Management System/search.gif");
                pictureBox1.Image = image;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Select * From AddNewMember";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void ViewMembers_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * From AddNewMember";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
        }

        int MemberID;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                MemberID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * From AddNewMember where MemberID= " + MemberID + "";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            txtMemberno.Text = ds.Tables[0].Rows[0][1].ToString();
            txtMemberType.Text = ds.Tables[0].Rows[0][2].ToString();
            txtTitle.Text = ds.Tables[0].Rows[0][3].ToString();
            txtFirstName.Text = ds.Tables[0].Rows[0][4].ToString();
            txtSurName.Text = ds.Tables[0].Rows[0][5].ToString();
            txtGender.Text = ds.Tables[0].Rows[0][6].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0][7].ToString();
            txtPhone.Text = ds.Tables[0].Rows[0][8].ToString();
            txtMail.Text = ds.Tables[0].Rows[0][9].ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unsaved Data Will be lost", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Int64 MNo = Int64.Parse(txtMemberno.Text);
                string MType = txtMemberType.Text;
                string Title = txtTitle.Text;
                string Fname = txtFirstName.Text;
                string Sname = txtSurName.Text;
                string Gender = txtGender.Text;
                string MAddress = txtAddress.Text;
                Int64 MobileNo = Int64.Parse(txtPhone.Text);
                string Email = txtMail.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Update AddNewMember SET MNo = '" + MNo + "',MType = '" + MType + "',Title='" + Title + "' ,Fname='" + Fname + "' ,Sname= '" + Sname + "' ,Gender= '" + Gender + "',MAddress= '" + MAddress + "',MobileNo ='" + MobileNo + "',Email= '" + Email + "' Where MemberID=" + rowid + "";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                ViewMembers_Load(this, null);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewMembers_Load(this, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be deleted. Confirmation Dialog?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Delete From AddNewMember Where MemberID=" + rowid + "";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                ViewMembers_Load(this, null);
            }
        }
    }
}
