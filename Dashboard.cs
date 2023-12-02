using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using Final_Project_OOP_and_DSA.Properties;

namespace Final_Project_OOP_and_DSA
{
    public partial class Dashboard : Form
    {
        private int limit = -140;
        private int currentPosition = 0;
        private Color panelBackground = Color.Transparent;
        private Color labelForeColor = Color.Black;
        public Dashboard()
        {
            InitializeComponent();
            InitializeBookListContent();
            InitializeDashboardContents();

            tc_Dashboard_TabControl.Location = new Point(35, -20);
            //panel_Sidebar_Sidebar.Location = new Point(0, 0);

        }

        public void InitializeDashboardContents()
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> resultBooks = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Books");
            List<string[]> studentTotal = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Student");
            List<string[]> teacherTotal = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Teacher");
            List<string[]> resultBooksAvailable = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Books WHERE book_status = 'Available'");
            List<string[]> resultBooksBorrowed = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Books WHERE book_status = 'Borrowed'");

            lbl_BookListed_Quantity.Text = resultBooks.Count.ToString();
            lbl_RegisteredUser_Quantity.Text = (studentTotal.Count + teacherTotal.Count).ToString();
            lbl_BooksAvailable_Quantity.Text = resultBooksAvailable.Count.ToString();
            lbl_BooksLent_Quantity.Text = resultBooksBorrowed.Count.ToString();
        }
        public void InitializeBookListContent()
        {
            flp_BookList.Controls.Clear();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> results = databaseConnection.QueryDatabase(cn);
            for (int i = 0; i < results.Count; i++)
            {
                object[] res = results[i];

                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = (Bitmap)Resources.ResourceManager.GetObject(res[7].ToString());
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Name = res[7].ToString();
                pictureBox.BringToFront();
                pictureBox.Click += new EventHandler(btn_BookDisplay_Information);

                Label lbl = new Label
                {
                    Text = res[0].ToString(),
                    Dock = DockStyle.Bottom,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(0, 30),
                    ForeColor = labelForeColor,
                    Font = new Font("Bahnschrift", 7)
                };

                Panel panel = new Panel()
                {
                    Size = new Size(125, 175),
                    BackColor = panelBackground,
                };

                panel.Controls.Add(pictureBox);
                panel.Controls.Add(lbl);
                flp_BookList.Controls.Add(panel);
            }
        }
        public void InitializeBookListContentByFilter(string filter)
        {
            flp_BookList.Controls.Clear();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> results = databaseConnection.QueryDatabase(cn, filter);
            try
            {

                for (int i = 0; i < results.Count; i++)
                {
                    object[] res = results[i];

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Dock = DockStyle.Fill;
                    pictureBox.Image = (Bitmap)Resources.ResourceManager.GetObject(res[7].ToString());
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Name = res[7].ToString();
                    pictureBox.BringToFront();
                    pictureBox.Click += new EventHandler(btn_BookDisplay_Information);

                    Label lbl = new Label
                    {
                        Text = res[0].ToString(),
                        Dock = DockStyle.Bottom,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(0, 30),
                        ForeColor = labelForeColor,
                        Font = new Font("Bahnschrift", 7)
                    };

                    Panel panel = new Panel()
                    {
                        Size = new Size(125, 175),
                        BackColor = panelBackground,
                    };
                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(lbl);
                    flp_BookList.Controls.Add(panel);
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_Dashboard;
        }

        private void btn_BookList_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_BookList;
        }

        private void btn_BookBorrowing_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_BookBorrowing;
        }

        private void btn_BookReturning_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_BookReturning;
        }

        private void btn_BorrowerList_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_BorrowerList;
        }

        private void btn_Payment_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_Payment;
        }


        private void btn_Filter_Available_Click(object sender, EventArgs e)
        {
            InitializeBookListContentByFilter("Available");
        }

        private void btn_Filter_Borrowed_Click(object sender, EventArgs e)
        {
            InitializeBookListContentByFilter("Borrowed");
        }

        private void btn_Filter_Reserved_Click(object sender, EventArgs e)
        {
            InitializeBookListContentByFilter("Reserved");
        }

        private void btn_Filter_Fictional_Click(object sender, EventArgs e)
        {
            InitializeBookListContentByFilter("Fictional");
        }

        private void btn_Filter_Non_Fictional_Click(object sender, EventArgs e)
        {
            InitializeBookListContentByFilter("Non-Fictional");
        }

        private void btn_Filter_Academic_Click(object sender, EventArgs e)
        {
            InitializeBookListContentByFilter("Academic");
        }

        private void tb_BookList_Click(object sender, EventArgs e)
        {

        }

        private void btn_Filter_All_Click(object sender, EventArgs e)
        {
            InitializeBookListContent();
        }
        private void btn_BookDisplay_Information(object sender, EventArgs e)
        {
            try
            {
                PictureBox pb = (PictureBox)sender;
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                List<string[]> results = databaseConnection.QueryDatabase(cn, pb.Name);
                string[] contents = results[0];
                BookInformationDisplay bookInformationDisplay = new BookInformationDisplay(results);
                bookInformationDisplay.Show();
            }catch (Exception ex) { }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void btn_ChangeTab(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            if(panel.Name == "panel_NewMember")
            {
                NewMemberForm nmb = new NewMemberForm();
                nmb.Show();
            }
        }

        private void panel_NewMember_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

