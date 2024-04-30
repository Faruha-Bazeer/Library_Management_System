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
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtIsbn.Text != "" && txtName.Text != "" && txtAuthor.Text != "" && txtPublication.Text != "" && txtQuantity.Text != "")
            {


                Int64 ISBN = Int64.Parse(txtIsbn.Text);
                String BookName = txtName.Text;
                String BookAuthor = txtAuthor.Text;
                String BookPublication = txtPublication.Text;
                String BookPublicationDate = dateTimePicker1.Text;
                Int64 BookPrice = Int64.Parse(txtPrice.Text);
                Int64 BookQuantity = Int64.Parse(txtQuantity.Text);

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DELL\\SQLEXPRESS ; database = LibraryManagement;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                cmd.CommandText = "insert into AddBook(ISBN, BookName, BookAuthor, BookPublication, BookPublicationDate, BookPrice, BookQuantity) Values ('" + ISBN + "','" + BookName + "','" + BookAuthor + "','" + BookPublication + "','" + BookPublicationDate + "'," + BookPrice + "," + BookQuantity + ")";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Book Added.", "Successfully....!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtIsbn.Clear();
                txtName.Clear();
                txtAuthor.Clear();
                txtPublication.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
            }
            else
            {
                MessageBox.Show("Empty Fields Not Allowed.","Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This Will Delete Your Unsaved Data.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)== DialogResult.OK)
            {
                this.Close();
            }
            

            
        }
    }
}
