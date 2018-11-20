using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_form
{
    public partial class ProBug : Form
    {
        public SessionModel session { get; set; }
        public ProBug(SessionModel sm)
        {
            InitializeComponent();
            session = sm;
            fillComboBox();
        }

        void fillComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\BugTracker.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand sda = new SqlCommand("Select Title from Issue where SolvedBy='" + session.UserName + "'AND IssueStatus <> 'Completed'", con);
                sda.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sc = new SqlDataAdapter(sda);
                sc.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["Title"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\BugTracker.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("Select * from Issue where Title ='" + comboBox1.Text + "'", con);
            sqlCmd.ExecuteNonQuery();
            SqlDataReader sdr;
            sdr = sqlCmd.ExecuteReader();
            while (sdr.Read())
            {
                string description = (string)sdr["Description"].ToString();
                richTextBox3.Text = description;

                string solution = (string)sdr["Solution"].ToString();
                richTextBox4.Text = solution;

                BugTrackerEntities1 bte = new BugTrackerEntities1();
                var item = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                byte[] arr = item.Image;
                MemoryStream ms = new MemoryStream(arr);
                pictureBox1.Image = Image.FromStream(ms);
            }
            con.Close();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BugTrackerEntities1 bte = new BugTrackerEntities1();
                var data = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                data.Solution = richTextBox4.Text;
                data.IssueStatus = "Pending";
                bte.Entry(data).State = EntityState.Modified;
                bte.SaveChanges();
                MessageBox.Show("Saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
