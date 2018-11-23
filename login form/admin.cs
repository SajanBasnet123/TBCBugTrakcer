using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    public partial class admin : Form
    {
        public SessionModel session { get; set; }
        public admin()
        {
            InitializeComponent();
        }

        public admin(SessionModel sm)
        {
            InitializeComponent();
            session = sm;
        }
        private void viewIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addBug ab = new addBug(session);
            ab.MdiParent = this;
            ab.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_reg ar = new add_reg();
            ar.MdiParent = this;
            ar.Show();
        }

        private void gitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();

            driver.Url = ("https://github.com/login");


            driver.FindElement(By.Id("login_field")).SendKeys("SajanBasnet123");
            driver.FindElement(By.Id("password")).SendKeys("github123");
        }
        //file move to github//
        private void admin_Load(object sender, EventArgs e)
        {

        }
    }
}
