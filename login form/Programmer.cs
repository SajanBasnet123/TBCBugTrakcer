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
    public partial class Programmer : Form
    {
        public SessionModel session { get; set; }
        public Programmer()
        {
            InitializeComponent();
        }

        public Programmer(SessionModel sm)
        {
            InitializeComponent();
            session = sm;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProBug pb = new ProBug(session);
            pb.MdiParent= this;
            pb.Show();
        }
    }
}
