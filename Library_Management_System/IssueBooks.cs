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
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {}

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("Select BookName From AddBook", con);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(reader.GetString(i));
                }
            }
            reader.Close();
            con.Close();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEnterMemberNo.Text != "")
            {
                Int64 MNo = Int64.Parse(txtEnterMemberNo.Text);
                SqlConnection con = new SqlConnection();
                con.ConnectionString =  "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Select * From AddNewMember Where MNo ="+MNo+"";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtMemberType.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtTitle.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtFname.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtSname.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtGender.Text = ds.Tables[0].Rows[0][6].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0][7].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0][8].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0][9].ToString();

                }
                else
                {
                    txtMemberType.Clear();
                    txtTitle.Clear();
                    txtFname.Clear();
                    txtSname.Clear();
                    txtGender.Clear();
                    txtAddress.Clear();
                    txtMobileNo.Clear();
                    txtEmail.Clear();

                    MessageBox.Show("Invalid Member Number. Check and Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (txtMemberType.Text != "")
            {
                if(comboBoxBooks.SelectedIndex != -1) 
                {
                    Int64 MNo = Int64.Parse(txtEnterMemberNo.Text);
                    string MType = txtMemberType.Text;
                    string Title = txtTitle.Text;
                    string Fname = txtFname.Text;
                    string Sname = txtSname.Text;
                    string Gender = txtGender.Text;
                    string MAddress = txtAddress.Text;
                    Int64 MobileNo = Int64.Parse(txtMobileNo.Text);
                    string Email = txtEmail.Text;
                    string BookName = comboBoxBooks.Text;
                    string Book_Issued_Date = dateTimePicker1.Text;

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    con.Open();
                    cmd.CommandText = "Insert into Issue_Return_Books(MNo,MType,Title,Fname,Sname,Gender,MAddress,MobileNo,Email,BookName,Book_Issued_Date)Values(" + MNo + ",'"+MType+ "','"+Title+ "','"+Fname+ "','"+Sname+ "','"+Gender+ "','"+MAddress+ "'," + MobileNo + ",'" + Email+ "','" + BookName+ "','" + Book_Issued_Date+ "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select Book.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Member Number.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void txtEnterMemberNo_TextChanged(object sender, EventArgs e)
        {
            if (txtEnterMemberNo.Text == "")
            {
                txtMemberType.Clear();
                txtTitle.Clear();
                txtFname.Clear();
                txtSname.Clear();
                txtGender.Clear();
                txtAddress.Clear();
                txtMobileNo.Clear();
                txtEmail.Clear();
                comboBoxBooks.SelectedIndex = -1;
             
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnterMemberNo.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ? ","Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
