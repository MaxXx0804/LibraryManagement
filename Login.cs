using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_OOP_and_DSA
{
    public partial class frm_Login : Form
    {
        public static Dashboard ds;
        private NotifyIcon notifyIcon;
        private string temporaryUser = "admin";
        private string temporaryPassword = "passwords";
        public frm_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txt_Login_Password.Text == temporaryPassword && txt_Login_Username.Text == temporaryUser)
            {
                ds = new Dashboard();
                ds.Show();
                this.Hide();
                notifyIcon = new NotifyIcon();
                notifyIcon.Icon = this.Icon;
                notifyIcon.Visible = false;
            }
            else
            {
                MessageBox.Show("Wrong credentials");
            }
        }
    }
}
