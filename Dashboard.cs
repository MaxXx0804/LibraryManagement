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
        private int memberNumberOfBooksCanBeSelected = 0;
        private int memberCurrentBookNumberSelected = 0;
        private List<string> BooksBeingBorrowed = new List<string>();

        private Color panelBackground = Color.Transparent;
        private Color labelForeColor = Color.Black;
        public Dashboard()
        {
            InitializeComponent();
            InitializeBookListContent();
            InitializeDashboardContents();
            InitializeBookBorrowingContents();
            
            tc_Dashboard_TabControl.Location = new Point(35, -20);
            //panel_Sidebar_Sidebar.Location = new Point(0, 0);
            panel_Sidebar_Sidebar.Size = new Size(175, 600);
            this.Size = new Size(1100, 600);
            lbl_Member_BookListDisplay.Text += "\n";

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
            Debug.WriteLine("DEBUG: "+studentTotal.Count + " " + teacherTotal.Count);
        }
        public void InitializeBookBorrowingContents()
        {
            InitializeBookListContentByFilterWithCheckBox("SELECT * FROM Books WHERE book_status = 'Available'");
            
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

        public void InitializeBookListContentByFilterWithCheckBox(string filter)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> results = databaseConnection.QueryDatabaseDashboardInformation(cn, filter);
            
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

                    CheckBox cb = new CheckBox
                    {
                        Text = res[0].ToString(),
                        Dock = DockStyle.Bottom,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(0, 30),
                        ForeColor = labelForeColor,
                        Font = new Font("Bahnschrift",7)
                    };
                    cb.CheckedChanged += new EventHandler(memberCheckBoxLimitChecker);
                    Panel panel = new Panel()
                    {
                        Size = new Size(125, 175),
                        BackColor = panelBackground,
                    };
                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(cb);
                    flp_Member_BookDisplay.Controls.Add(panel);
                }
            }
            catch (Exception ex)
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
            else
            {
                tc_Member_New.SelectedTab = tb_Member;
                lbl_Member_DateBorrowed.Text += DateTime.Now.ToString();
                lbl_Member_DueDate.Text += DateTime.Now.AddDays(3).ToString();
            }
        }

        private void panel_NewMember_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tmr_Update_Tick(object sender, EventArgs e)
        {
            InitializeDashboardContents();
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        //PREVENT USER FROM CHANGING THE BORROWER TYPE WHEN THERE ARE SELECTED CHECKBOX
        private void memberCheckBoxLimitChecker(object sender, EventArgs e)
        {
            try
            {
                CheckBox snd = (CheckBox)sender;
                if (memberCurrentBookNumberSelected >= memberNumberOfBooksCanBeSelected && snd.Checked)
                {
                    snd.Checked = false;
                    MessageBox.Show("Exceeded the number of books that can be borrowed");
                }
                else if (!snd.Checked)
                {
                    int end = snd.Text.Length;
                    int start = lbl_Member_BookListDisplay.Text.IndexOf(snd.Text);

                    lbl_Member_BookListDisplay.Text = lbl_Member_BookListDisplay.Text.Remove(start, end + 1);
                    BooksBeingBorrowed.Remove(snd.Text);
                    memberCurrentBookNumberSelected--;
                    Debug.WriteLine(BooksBeingBorrowed.Count);
                }
                else if (memberCurrentBookNumberSelected < memberNumberOfBooksCanBeSelected)
                {
                    lbl_Member_BookListDisplay.Text += snd.Text + "\n";
                    BooksBeingBorrowed.Add(snd.Text);
                    memberCurrentBookNumberSelected++;
                }
            }catch(Exception ex)
            {

            }

        }
        private void cb_Member_BorrowerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> studentName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT student_name FROM Student");
            List<string[]> teacherName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT teacher_name FROM Teacher");
            if (cb_Member_BorrowerType.Text == "Student" || cb_Member_BorrowerType.Text == "Teacher")
            {
                flp_Member_BookDisplay.Enabled = true;
            }
            else
            {
                flp_Member_BookDisplay.Enabled = false;
            }
            if(cb_Member_BorrowerType.Text == "Student")
            {
                cb_Member_Name.Items.Clear();
                memberNumberOfBooksCanBeSelected = 2;
                lbl_Member_BorrowerType.Text = "Borrower Name: Student";
                foreach (string[] x in studentName)
                {
                    cb_Member_Name.Items.Add(x[0]); 
                }
            } 
            else if(cb_Member_BorrowerType.Text == "Teacher")
            {
                cb_Member_Name.Items.Clear();
                lbl_Member_BorrowerType.Text = "Borrower Name: Teacher";
                memberNumberOfBooksCanBeSelected = 5;
                foreach (string[] x in teacherName)
                {
                    cb_Member_Name.Items.Add(x[0]);
                }
            }
            
        }

        private void cb_Member_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Member_Name.Text = "Borrower Name: " + cb_Member_Name.Text;
        }

        private void lbl_Member_BorrowerType_Click(object sender, EventArgs e)
        {

        }

        private void btn_Member_Confirm_Click(object sender, EventArgs e)
        {

        }
    }
}

