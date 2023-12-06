using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_OOP_and_DSA
{

    public partial class Loading : Form
    {
        private int ticks = 2;
        private NotifyIcon notifyIcon;
        public Loading()
        {
                InitializeComponent();
        }    
    
        private Point lastPoint;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                lastPoint = new Point(e.X, e.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Loading_Load(object sender, EventArgs e)
        {

        }

        private void TextChange_Tick(object sender, EventArgs e)
        {
            string[] LoadingScreenText = { "Finding books...", "Cleaning books...", "Checking dictionary...", "Debugging program..." , "Bombing Hiroshima...", "Starting Trojan Sequence..."};
            Random rand = new Random();
            int num = rand.Next(LoadingScreenText.Length);
            lbl_LoadingScreenText.Text = LoadingScreenText[num];
            if (ticks > 0)
            {
                ticks--;
            }
            else
            {
                frm_Login frm = new frm_Login();
                frm.Show();
                notifyIcon = new NotifyIcon();
                notifyIcon.Icon = this.Icon;
                notifyIcon.Visible = false;
                this.Hide();
                TextChange.Stop();
            }
        }
    }

}
