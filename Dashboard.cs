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
    public partial class Dashboard : Form
    {
        private int limit = -140;
        private int currentPosition = 0;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

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

        private void button8_Click(object sender, EventArgs e)
        {
            tmr_Sidebar_SidebarExitAnimation.Start();            
        }

        private void tmr_Sidebar_SidebarExitAnimation_Tick(object sender, EventArgs e)
        {
            if(currentPosition > limit)
            {
                currentPosition-=10;
                panel_Sidebar_Sidebar.Location = new Point(currentPosition, 0);
            }
            else
            {
                tmr_Sidebar_SidebarExitAnimation.Stop();
                btn_Sidebar.Visible = true;
                btn_Sidebar_SidebarExit.Visible = false;
                panel_Logo_LogoHandler.Size = new Size(175, 30);
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            tmr_Sidebar_SidebarEntryAnimation.Start();
        }

        private void tmr_Sidebar_SidebarEntryAnimation_Tick(object sender, EventArgs e)
        {
            if (currentPosition < 0)
            {
                currentPosition += 10;
                panel_Sidebar_Sidebar.Location = new Point(currentPosition, 0);
            }
            else
            {
                tmr_Sidebar_SidebarEntryAnimation.Stop();
                btn_Sidebar.Visible = false;
                btn_Sidebar_SidebarExit.Visible = true;
                panel_Logo_LogoHandler.Size = new Size(175, 50);
            }
        }
    }
}

