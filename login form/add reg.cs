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
    public partial class add_reg : Form
    {
        public add_reg()
        {
            InitializeComponent();
        }
        //initiate saves
        private void button1_Click(object sender, EventArgs e)
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
                string userrole = "";
                if(Admin.Checked)
                {
                    userrole = "admin";
                }
                else
                {
                    userrole = "developer";
                }
                BugTrackerEntities1 bte = new BugTrackerEntities1();
                var data = new Person
                {
                    UserName = textBox1.Text,
                    Password = textBox2.Text,
                    Role = userrole
                };
                bte.People.Add(data);
                bte.SaveChanges();
                MessageBox.Show("User Saved");
            }
        }

        private void Admin_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
