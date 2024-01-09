using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using Final_Project_OOP_and_DSA.Properties;
using System.Windows.Forms.VisualStyles;

namespace Final_Project_OOP_and_DSA
{
    public partial class Dashboard : Form
    {
        private int limit = -140;
        private int currentPosition = 0;
        private int memberNumberOfBooksCanBeSelected = 0;
        private int memberCurrentBookNumberSelected = 0;

        private int reserveNumberOfBooksCanBeSelected = 0;
        private int reserveCurrentBookNumberSelected = 0;

        private List<string> BooksBeingReserve = new List<string>();
        private List<string> BooksBeingBorrowed = new List<string>();

        private Color panelBackground = Color.Transparent;
        private Color labelForeColor = Color.Black;
        public void ResetAll()
        {
            InitializeBookListContent();
            InitializeDashboardContents();
            InitializeBookBorrowingContents();
            InitializeBorrowerListContent();
            InitializeBookReserveContents();
            CHANGEDEFAULTSETTINGS();
        }
        public Dashboard()
        {
            
            InitializeComponent();
            InitializeBookListContent();
            InitializeDashboardContents();
            InitializeBookBorrowingContents();
            InitializeBorrowerListContent();
            InitializeBookReserveContents();
            CHANGEDEFAULTSETTINGS();

            
            
        }
        public void CHANGEDEFAULTSETTINGS()
        {
            dtp_DateReserved.MinDate = DateTime.Now.Date.AddDays(1); 
            dtp_DateReserved.MaxDate = DateTime.Now.AddDays(7);
            tc_Dashboard_TabControl.Location = new Point(35, -20);
            panel_Sidebar_Sidebar.Size = new Size(175, 600);
            lbl_Member_BookListDisplay.Text += "\n";
        }
        public void InitializeBorrowerListContent()
        {
            flp_BorrowerList_Teacher.Controls.Clear();
            flp_BorrowerList_Student.Controls.Clear();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> studentTotal = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Student");
            List<string[]> teacherTotal = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Teacher");
            foreach (string[] x in studentTotal)
            {
                FlowLayoutPanel flp = new FlowLayoutPanel()
                {
                    Size = new Size(1035, 30),
                    FlowDirection = FlowDirection.TopDown,
                    BorderStyle = BorderStyle.FixedSingle,
                };
                for(int i = 0; i < 4; i++)
                {
                    if (x[i] != null)
                    {
                        Panel panel = new Panel()
                        {
                            Size = new Size(250, 30)
                        };
                        Label lbl = new Label()
                        {
                            Text = x[i],
                            AutoSize = true,
                            Font = new Font("Tahoma", 10)
                            
                        };
                        panel.Controls.Add(lbl);
                        flp.Controls.Add(panel);
                    }
                }
                flp_BorrowerList_Student.Controls.Add(flp);
            }
            foreach (string[] x in teacherTotal)
            {
                FlowLayoutPanel flp = new FlowLayoutPanel()
                {
                    Size = new Size(1035, 30),
                    FlowDirection = FlowDirection.TopDown,
                    BorderStyle = BorderStyle.FixedSingle,
                };
                for (int i = 0; i < 3; i++)
                {
                    if (x[i] != null)
                    {
                        
                        Panel panel = new Panel()
                        {
                            Size = new Size(250, 30)
                        };
                        Label lbl = new Label()
                        {
                            Text = x[i],
                            AutoSize = true,
                            Font = new Font("Tahoma", 10)

                        };
                        panel.Controls.Add(lbl);
                        flp.Controls.Add(panel);
                    }
                }
                flp_BorrowerList_Teacher.Controls.Add(flp);
            }
        }
        public void InitializeDashboardContents()
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();

                int numberOfPayment = 0;

                List<string[]> resultBooks = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Books");
                List<string[]> studentTotal = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Student");
                List<string[]> teacherTotal = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Teacher");
                List<string[]> resultBooksAvailable = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Books WHERE book_status = 'Available'");
                List<string[]> resultBooksBorrowed = databaseConnection.QueryDatabaseDashboardInformation(cn, "SELECT * FROM Books WHERE book_status = 'Borrowed'");
                List<object[]> resultBooksPayment = databaseConnection.QueryDatabaseForReturnBooks(cn, "SELECT * FROM BookBorrowing");

                foreach (var item in resultBooksPayment)
                {
                    if (item != null)
                    {
                        DateTime DueDate = (DateTime)item[4];
                        Debug.WriteLine(item[0]);
                        DateTime DateNow = DateTime.Now;
                        TimeSpan timeSpan = DateNow - DueDate;
                        if (timeSpan.Days > 0)
                        {
                            numberOfPayment++;
                        }
                    }
                };
                lbl_BookListed_Quantity.Text = resultBooks.Count.ToString();
                lbl_RegisteredUser_Quantity.Text = (studentTotal.Count + teacherTotal.Count).ToString();
                lbl_BooksAvailable_Quantity.Text = resultBooksAvailable.Count.ToString();
                lbl_BooksLent_Quantity.Text = resultBooksBorrowed.Count.ToString();
                lbl_PendingPayment_Quantity.Text = numberOfPayment.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void InitializeBookBorrowingContents()
        {

            InitializeBookListContentByFilterWithCheckBox("SELECT * FROM Books WHERE book_status = 'Available'");
            
        }
        public void InitializeReturnBooksContent()
        {
            flp_BooksReturn.Controls.Clear();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> results = databaseConnection.QueryDatabaseDashboardInformation(cn,"SELECT * FROM Books WHERE book_status = 'Borrowed'");
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
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                        Size = new Size(0, 30),
                        ForeColor = labelForeColor,
                        Font = new Font("Bahnschrift", 7)
                    };
                    cb.CheckedChanged += new EventHandler(memberCheckBoxLimitChecker);
                    Panel panel = new Panel()
                    {
                        Size = new Size(125, 175),
                        BackColor = panelBackground,
                    };
                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(cb);
                    flp_BooksReturn.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
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
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
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
        public void InitializeBookReserveContents()
        {
            InitializeBookListContentByFilterWithCheckBoxForBookReserve("SELECT * FROM Books WHERE book_status = 'Available'");
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
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
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
            flp_Member_BookDisplay.Controls.Clear();
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
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
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
        public void InitializeBookListContentByFilterWithCheckBoxForBookReserve(string filter)
        {
            flp_Reserve.Controls.Clear();
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
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                        Size = new Size(0, 30),
                        ForeColor = labelForeColor,
                        Font = new Font("Bahnschrift", 7)
                    };
                    cb.CheckedChanged += new EventHandler(reserveCheckBoxLimitChecker);
                    Panel panel = new Panel()
                    {
                        Size = new Size(125, 175),
                        BackColor = panelBackground,
                    };
                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(cb);
                    flp_Reserve.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //SIDEBAR FUNCTIONS
        #region
        private void button8_Click(object sender, EventArgs e)
        {
            tmr_Sidebar_SidebarExitAnimation.Start();
            btn_Dashboard.Enabled = false;
            btn_BookList.Enabled = false;
            btn_BookBorrowing.Enabled = false;
            btn_BookReturning.Enabled = false;
            btn_BorrowerList.Enabled = false;
            btn_Payment.Enabled = false;
            btn_Sidebar_Logout.Enabled = false;
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
            btn_Dashboard.Enabled = true;
            btn_BookList.Enabled = true;
            btn_BookBorrowing.Enabled = true;
            btn_BookReturning.Enabled = true;
            btn_BorrowerList.Enabled = true;
            btn_Payment.Enabled = true;
            btn_Sidebar_Logout.Enabled = true;
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
        #endregion
        //CHANGE TAB BUTTONS
        #region
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
            tc_Dashboard_TabControl.SelectedTab = tb_Reservation;
        }
        #endregion
        //CHANGE FILTER IN BOOKLIST
        #region
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
        private void btn_Filter_All_Click(object sender, EventArgs e)
        {
            InitializeBookListContent();
        }
        #endregion



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

        

        private void tmr_Update_Tick(object sender, EventArgs e)
        {
            InitializeDashboardContents();
        }

       
        //CHECKS IF THE USER EXCEEDS THE BOOK LIMIT
        #region

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
                }
                else if (memberCurrentBookNumberSelected < memberNumberOfBooksCanBeSelected)
                {
                    lbl_Member_BookListDisplay.Text += snd.Text + "\n";
                    BooksBeingBorrowed.Add(snd.Text);
                    memberCurrentBookNumberSelected++;
                }
                if(memberCurrentBookNumberSelected > 0)
                {
                    cb_Member_BorrowerType.Enabled = false;
                }
                else if(memberCurrentBookNumberSelected == 0)
                {
                    cb_Member_BorrowerType.Enabled = true;
                }
            }catch(Exception ex)
            {

            }

        }
        private void reserveCheckBoxLimitChecker(object sender, EventArgs e)
        {
            try
            {
                CheckBox snd = (CheckBox)sender;
                if (reserveCurrentBookNumberSelected >= reserveNumberOfBooksCanBeSelected && snd.Checked)
                {
                    snd.Checked = false;
                    MessageBox.Show("Exceeded the number of books that can be borrowed");
                }
                else if (!snd.Checked)
                {
                    int end = snd.Text.Length;
                    int start = lbl_Reserve_BookList.Text.IndexOf(snd.Text);

                    lbl_Reserve_BookList.Text = lbl_Reserve_BookList.Text.Remove(start, end + 1);
                    BooksBeingReserve.Remove(snd.Text);
                    reserveCurrentBookNumberSelected--;
                }
                else if (reserveCurrentBookNumberSelected < reserveNumberOfBooksCanBeSelected)
                {
                    lbl_Reserve_BookList.Text += snd.Text + "\n";
                    BooksBeingReserve.Add(snd.Text);
                    reserveCurrentBookNumberSelected++;
                }
                if (reserveCurrentBookNumberSelected > 0)
                {
                    cb_Reserve_BorrowerType.Enabled = false;
                }
                else if (reserveCurrentBookNumberSelected == 0)
                {
                    cb_Reserve_BorrowerType.Enabled = true;
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
        //PUSH USERS ONTO CB_MEMBER_BORROWERTYPE WHILE CHECKING IF THE USER HAS BOOKS BORROWED
        #region
        private void cb_Member_BorrowerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> studentName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT student_name, student_book_borrowed FROM Student");
            List<string[]> teacherName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT teacher_name, teacher_book_borrowed FROM Teacher");
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
                lbl_Member_BorrowerType.Text = "Borrower Type: Student";
                foreach (string[] x in studentName)
                {
                    if (x[1] == null || x[1] == "")
                    {
                        cb_Member_Name.Items.Add(x[0]);
                    }
                }
            } 
            else if(cb_Member_BorrowerType.Text == "Teacher")
            {
                cb_Member_Name.Items.Clear();
                lbl_Member_BorrowerType.Text = "Borrower Type: Teacher";
                memberNumberOfBooksCanBeSelected = 5;
                foreach (string[] x in teacherName)
                {
                    if (x[1] == null || x[1] == "")
                    {
                        cb_Member_Name.Items.Add(x[0]);
                    }
                }
            }
            
        }
        private void cb_BookReturn_BorrowerType_SelectedIndexChanged(object sender, EventArgs e)
        {

            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> studentName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT student_name, student_book_borrowed FROM Student");
            List<string[]> teacherName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT teacher_name, teacher_book_borrowed FROM Teacher");
            if (cb_BookReturn_BorrowerType.Text == "Student" || cb_BookReturn_BorrowerType.Text == "Teacher")
            {
                flp_BooksReturn.Enabled = true;
            }
            else
            {
                flp_BooksReturn.Enabled = false;
            }
            if (cb_BookReturn_BorrowerType.Text == "Student")
            {

                cb_BookReturn_Name.Items.Clear();
                lbl_BookReturn_ReturnerType.Text = "Borrower Type: Student";
                memberNumberOfBooksCanBeSelected = 2;
                foreach (string[] x in studentName)
                {
                    if (x[1] != null || x[1] != "")
                    {
                        cb_BookReturn_Name.Items.Add(x[0]);
                    }
                }
            }
            else if (cb_BookReturn_BorrowerType.Text == "Teacher")
            {
                cb_BookReturn_Name.Items.Clear();
                lbl_BookReturn_ReturnerType.Text = "Borrower Type: Teacher";
                memberNumberOfBooksCanBeSelected = 5;
                foreach (string[] x in teacherName)
                {
                    if (x[1] != null || x[1] != "")
                    {
                        cb_BookReturn_Name.Items.Add(x[0]);
                    }
                }
            }
        }
        private void cb_Reserve_BorrowerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Reserve_BorrowerType.Text = cb_Reserve_BorrowerType.Text;
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<string[]> studentName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT student_name, student_book_borrowed FROM Student");
            List<string[]> teacherName = databaseConnection.QueryDatabaseDashboardInformationBook(cn, "SELECT teacher_name, teacher_book_borrowed FROM Teacher");
            if (cb_Reserve_BorrowerType.Text == "Student" || cb_Reserve_BorrowerType.Text == "Teacher")
            {
                flp_Reserve.Enabled = true;
            }
            else
            {
                flp_Reserve.Enabled = false;
            }
            if (cb_Reserve_BorrowerType.Text == "Student")
            {
                cb_Reserve_Name.Items.Clear();
                reserveNumberOfBooksCanBeSelected = 2;

                foreach (string[] x in studentName)
                {
                    if (x[1] == null || x[1] == "")
                    {
                        cb_Reserve_Name.Items.Add(x[0]);
                    }
                }
            }
            else if (cb_Reserve_BorrowerType.Text == "Teacher")
            {
                cb_Reserve_Name.Items.Clear();
                reserveNumberOfBooksCanBeSelected = 5;
                foreach (string[] x in teacherName)
                {
                    if (x[1] == null || x[1] == "")
                    {
                        cb_Reserve_Name.Items.Add(x[0]);
                    }
                }
            }
        }
        #endregion
        private void cb_Member_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Member_Name.Text = "Borrower Name: " + cb_Member_Name.Text;
        }

        private void btn_Member_Confirm_Click(object sender, EventArgs e)
        {
            BookBorrowingCode.BookBorrowing(BooksBeingBorrowed);
            InitializeBookListContentByFilterWithCheckBox("SELECT * FROM Books WHERE book_status = 'Available'");
            memberCurrentBookNumberSelected = 0;
            ResetAll();
        }
        private void btn_Reserve_Reserve_Click(object sender, EventArgs e)
        {
            BookReserved.BookReserve(BooksBeingReserve);
            InitializeBookListContentByFilterWithCheckBoxForBookReserve("SELECT * FROM Books WHERE book_status = 'Available'");
            reserveCurrentBookNumberSelected = 0;
            ResetAll();
        }


        private void cb_BookReturn_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookReturningCode.ChangeContent(cb_BookReturn_Name.Text);
        }

        private void btn_Sidebar_Logout_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_Login login = new frm_Login();
            login.Show();
        }

        private void btn_BookReturn_Return_Click(object sender, EventArgs e)
        {
            if (BookReturningCode.InitiateBookReturning())
            {
                InitializeBookListContentByFilterWithCheckBox("SELECT * FROM Books WHERE book_status = 'Available'");
                ResetAll();
            }
            
        }

        

        private void btn_Member_Clear_Click(object sender, EventArgs e)
        {
            cb_Member_Name.Items.Clear();
            cb_BookReturn_BorrowerType.SelectedIndex = -1;
            flp_Member_BookDisplay.Controls.Clear();
        }

        private void txt_Payment_PastDue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int passDue = Convert.ToInt32(txt_Payment_PastDue.Text);
                int amountToPay = 20 * passDue;
                lbl_Payment_DaysDelayed.Text = passDue.ToString();
                lbl_Payment_Subtotal.Text = $"P{amountToPay.ToString()}.00";
                lbl_Payment_Amount.Text = $"P{amountToPay.ToString()}.00";
                lbl_Payment_Total.Text = $"P{amountToPay.ToString()}.00";
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void btn_Payment_Pay_Click(object sender, EventArgs e)
        {
            if (BookReturningCode.ByPassBookReturning())
            {
                InitializeBookListContentByFilterWithCheckBox("SELECT * FROM Books WHERE book_status = 'Available'");
                lbl_Payment_DaysDelayed.Text = "0";
                lbl_Payment_Subtotal.Text = $"P0.00";
                lbl_Payment_Amount.Text = $"P0.00";
                lbl_Payment_Total.Text = $"P0.00";
                lbl_Payment_BorrowerName.Text = "";
                lbl_Payment_StudentID.Text = "";
                lbl_Payment_Amount.Text = "P0.00";
                txt_Payment_PastDue.Text = "0";
            }
        }

       

        

        private void dtp_DateReserved_ValueChanged(object sender, EventArgs e)
        {
            lbl_Reserve_DateReserved.Text = dtp_DateReserved.Text;
        }
        private void txt_Payment_PastDue_ControlRemoved(object sender, ControlEventArgs e)
        {

        }
        private void label26_Click(object sender, EventArgs e)
        {

        }
        private void tb_BookList_Click(object sender, EventArgs e)
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
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tb_Dashboard_Click(object sender, EventArgs e)
        {

        }
        private void panel_NewMember_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void cb_Reserve_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Reserve_Name.Text = cb_Reserve_Name.Text;
        }

        private void btn_ViewReservations_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_ViewReservation;
            BookReserved.Start();
            Debug.WriteLine("Working");
        }

        private void btn_ViewReservation_Return_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_Reservation;
            Debug.WriteLine("Working");
        }

        private void btn_ViewReservation_Click(object sender, EventArgs e)
        {
            tc_Dashboard_TabControl.SelectedTab = tb_ViewReservation;
            BookReserved.Start();
        }

    }
}

