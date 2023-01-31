using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsDemo.eContactClasses;

namespace WindowsFormsDemo
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }
        ContactClass c = new ContactClass();
        private void EContact_Load(object sender, EventArgs e)
        {
            //Load data on data gridview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void txtboxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get value from input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;
            bool success = c.Insert(c);
            if (success)
            {
                MessageBox.Show("New Contact Successfully Inserted");
                //call clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed To ADD New Contact. Try Again.");
            }
            //Load data on data gridview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if (success)
            {
                MessageBox.Show("Contact successfully deleted.");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete contact.Please try again.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get Data from text box
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Update(c);
            if (success)
            {
                MessageBox.Show("Contact has been successfully updated");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact. Please Try again");
            }
           
        }
        //Close picture button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Method to clear all fields
        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNumber.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get Data from Data Grid View and Load it to the textboxes
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }
        static string myConnString = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //get the value from the text box
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myConnString);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+"%' " +
                "OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
