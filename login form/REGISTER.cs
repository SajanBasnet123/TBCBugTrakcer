using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_form
{
    //initiate registrate form
    public partial class REGISTER : Form
    {
        public REGISTER()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void REGISTER_Load(object sender, EventArgs e)
        {

        }
        //exit  application
        private void button1_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
          
        }

        //Procedure to Saves client

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("One or More Fields are empty");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Password Do not Match");
            }
            else
            {
                BugTrackerEntities1 bte = new BugTrackerEntities1();
                var data = new Person
                {
                    UserName = textBox1.Text,
                    Password = textBox2.Text,
                    Role = "Client"
                };
                bte.People.Add(data);
                bte.SaveChanges();
                MessageBox.Show("User Saved");
            }
        }
    }
}
