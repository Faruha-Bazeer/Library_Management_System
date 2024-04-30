using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Library_Management_System
{
    public partial class AddMember : Form
    {
        string Gender;
        public AddMember()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm? ", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Clear();
            comboBoxType.SelectedIndex = -1;
            comboBoxTitle.SelectedIndex = -1;
            txtFname.Clear();
            txtSname.Clear();
            radioButton1.Checked= false;
            radioButton2.Checked= false;
            txtAddress.Clear();
            txtMobile.Clear();
            txtEmail.Clear();
        }

        private void btnSaveinfo_Click(object sender, EventArgs e)
        {
            if (txtNo.Text != "" && txtFname.Text != "" && txtSname.Text != "" && txtAddress.Text != "" && txtMobile.Text != "" && txtEmail.Text != "")
            {

                Int64 MNo = Int64.Parse(txtNo.Text);
                string Fname = txtFname.Text;
                string Sname = txtSname.Text;
                string MAddress = txtAddress.Text;
                Int64 MobileNo = Int64.Parse(txtMobile.Text);
                string Email = txtEmail.Text;


                if (radioButton1.Checked == true)
                {
                    Gender = "Male";
                }
                else
                {
                    Gender = "Female";
                }

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                cmd.CommandText = "Insert into AddNewMember(MNo, MType, Title, Fname, SName, Gender, MAddress, MobileNo, Email) Values (" + MNo + ",'" + comboBoxType.SelectedItem + "','" + comboBoxTitle.SelectedItem + "','" + Fname + "','" + Sname + "' , '" + Gender + "','" + MAddress + "'," + MobileNo + ", '" + Email + "')";
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please Fill Empty Fields","Suggestion",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

    }
}
