﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace login_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\BugTracker.mdf;Integrated Security=True;Connect Timeout=30;");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From People where Username='" + textBox1.Text + "' and Password ='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                BugTrackerEntities bte = new BugTrackerEntities();
                var person = bte.People.Where(a => a.UserName == textBox1.Text).SingleOrDefault();
                if(person.Role=="admin")
                {
                    this.Hide();
                    Main ss = new Main();
                    ss.Show();
                }
                else if(person.Role=="client")
                {

                }
                else
                {

                }
            }
            
            else
            {
                MessageBox.Show("Invalid Username and Password");
            }
        }
    }
}
