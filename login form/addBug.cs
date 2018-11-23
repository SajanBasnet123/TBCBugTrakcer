using ICSharpCode.TextEditor.Document;
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
    public partial class addBug : Form
    {
        public SessionModel session { get; set; }
        public addBug()
        {
            InitializeComponent();
        }

        public addBug(SessionModel sm)
        {
            InitializeComponent();
            session = sm;
            fillComboBox();
            fillDevComboBox();
        }

        void fillComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\BugTracker.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand sda = new SqlCommand("Select Title from Issue", con);
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

        void fillDevComboBox()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\BugTracker.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand sda = new SqlCommand("Select UserName from People where Role='developer'", con);
                sda.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sc = new SqlDataAdapter(sda);
                sc.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["UserName"].ToString());
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BugTrackerEntities1 bte = new BugTrackerEntities1();
                var data = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                data.Description = textEditorControl1.Text;
                data.SolvedBy = comboBox2.Text;
                //if (data.IssueStatus == "ReOpened")
                //{
                //    MessageBox.Show("Issue is already Re-Opened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //{
                //    if (checkBox1.Checked)
                //    {
                //        data.IssueStatus = "ReOpened";
                //    }
                //    bte.Entry(data).State = EntityState.Modified;
                //    bte.SaveChanges();
                //    MessageBox.Show("Saved");
                //}
                //


                //delete the code below After uncommenting above
                if (checkBox1.Checked)
                {
                    data.IssueStatus = "ReOpened";
                }
                bte.Entry(data).State = EntityState.Modified;
                bte.SaveChanges();
                MessageBox.Show("Saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                textEditorControl1.Text = description;

                string solution = (string)sdr["Solution"].ToString();
                textEditorControl2.Text = solution;
                
                BugTrackerEntities1 bte = new BugTrackerEntities1();
                var item = bte.Issues.Where(a => a.Title == comboBox1.Text).SingleOrDefault();
                textBox1.Text = item.IssueStatus;
                comboBox2.Text = item.SolvedBy;
                byte[] arr = item.Image;
                MemoryStream ms = new MemoryStream(arr);
                pictureBox1.Image = Image.FromStream(ms);
            }
            con.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEditorControl1_Load(object sender, EventArgs e)
        {
            string dric = Application.StartupPath;
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(dric))
            {
                fsmp = new FileSyntaxModeProvider(dric);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                textEditorControl1.SetHighlighting("C#");
            }
        }

        private void addBug_Load(object sender, EventArgs e)
        {

        }
    }
}
