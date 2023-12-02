using Final_Project_OOP_and_DSA.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_OOP_and_DSA
{
    public partial class BookInformationDisplay : Form
    {
        public BookInformationDisplay()
        {
            InitializeComponent();
        }
        public BookInformationDisplay(List<string[]> results)
        {
            InitializeComponent();
            string[] contents = results[0];
            Debug.WriteLine(contents[0]);
            lbl_BookDisplay_BookTitle.Text = contents[0] + " Information";
            lbl_BookDisplay_BookTitleLabel.Text += contents[0];
            lbl_BookDisplay_ISBN.Text += contents[1];
            lbl_BookDisplay_Category.Text += contents[2];
            lbl_BookDisplay_Author.Text += contents[3];
            lbl_BookDisplay_Copyright.Text += contents[4];
            lbl_BookDisplay_Publisher.Text += contents[5];
            lbl_BookDisplay_Status.Text += contents[6];
            pb_BookDisplay_Image.Image = (Bitmap)Resources.ResourceManager.GetObject(contents[7]);
            this.Show();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbl_BookDisplay_BookTitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void lbl_BookDisplay_Author_Click(object sender, EventArgs e)
        {

        }

        private void btn_BookDisplay_Information(object sender, EventArgs e)
        {

        }
    }
}
