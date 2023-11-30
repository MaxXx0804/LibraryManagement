using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagement
{
    public partial class home : Form
    {

        public home()
        {
            InitializeComponent();

            close_Btn.BackColor = Color.Transparent;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void close_Btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
