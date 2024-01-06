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
    public partial class PaymentPrompt : Form
    {
        public PaymentPrompt()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;

            Pen pen = new Pen(Color.Black, 2);
            int startX = 50, startY = 100, endX = 250, endY = 100;
           
            graph.DrawLine(pen, startX, startY, endX, endY);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
