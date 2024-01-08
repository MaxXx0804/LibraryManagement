using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Final_Project_OOP_and_DSA
{
    internal class BookReserved
    {
        private static void Reset()
        {
            frm_Login.ds.cb_Reserve_BorrowerType.SelectedIndex = -1;
            frm_Login.ds.cb_Reserve_BorrowerType.Enabled = true;
            frm_Login.ds.cb_Reserve_Name.Items.Clear();
            frm_Login.ds.flp_Reserve.Controls.Clear();
        }
        class ViewReservationContainers
        {

            public string name;
            public string borrowertype;
            public string booksreserved;
            public string datereserved;

            Panel main_container = new Panel
            {
                Size = new Size(200, 250),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Panel top_panel = new Panel
            {
                Size = new Size(200, 30),
                BackColor = Color.FromArgb(50, 50, 50),
                Dock = DockStyle.Top,
            };
            Label lbl_name = new Label
            {
                Font = new Font("Bahnschrift", 9),
                Location = new Point(3, 50),
                AutoSize = true,
                BackColor = Color.Transparent,
            };
            Label lbl_borrowertype = new Label
            {
                Font = new Font("Bahnschrift", 9),
                AutoSize = true,
                Location = new Point(3, 80),
                BackColor = Color.Transparent,
            };
            Label lbl_booksreserved = new Label
            {
                Font = new Font("Bahnschrift", 9),
                AutoSize = true,
                Location = new Point(3, 120),
                BackColor = Color.Transparent,
            };
            Label lbl_datereserved = new Label
            {
                Font = new Font("Bahnschrift", 9),
                Location = new Point(3, 190),
                AutoSize = true,
                BackColor = Color.Transparent,
            };

            Button borrow = new Button
            {
                Text = "Borrow",
                Location = new Point(2, 215),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 1 },
                Font = new Font("Bahnschrift", 9),
                Size = new Size(95, 30),
            };
            Button cancel = new Button
            {
                Text = "Cancel",
                Location = new Point(102, 215),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(244, 105, 105),
                Font = new Font("Bahnschrift", 9),
                FlatAppearance = { BorderSize = 1 },
                Size = new Size(95, 30)
            };

            public ViewReservationContainers(string username, string userborrowertype, string userbooksreserved, string userdatereserved)
            {
                
                name = username;
                borrowertype = userborrowertype;
                booksreserved = userbooksreserved;
                datereserved = userdatereserved;
                

                lbl_name.Text = "Name: " + borrowertype;
                
                
                
                //borrow.Name = name;
                main_container.Controls.Add(top_panel);
                main_container.Controls.Add(lbl_name);
                lbl_borrowertype.Text = "Borrower Type: " + borrowertype;
                main_container.Controls.Add(lbl_borrowertype);
                lbl_booksreserved.Text = "Books Reserved: " + booksreserved;
                main_container.Controls.Add(lbl_booksreserved);
                lbl_datereserved.Text = "Date Reserved: " + datereserved;
                main_container.Controls.Add(lbl_datereserved);
                main_container.Controls.Add(borrow);
                main_container.Controls.Add(cancel);
                frm_Login.ds.flp_ViewReservation.Controls.Add(main_container);

            }
        }
        public static void Start()
        {

            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            string contents = "";
            List<object[]> results = databaseConnection.QueryDatabaseForReserveBooks(cn, $"SELECT * FROM Reservation");
            foreach (var item in results)
            {
                //Borrower Name
                //BookList

                string[] arr = item[1].ToString().Split(new string[] { "~" }, StringSplitOptions.None);
                

                foreach (string x in arr)
                {
                     contents += "\n" + x;
                    
                }
                new ViewReservationContainers(item[2].ToString(), "Student", contents, item[3].ToString());

            }

        }

        public static void BookReserve(List<string> BooksBeingBorrowed)
        {
            string[] BookName = new string[5];
            int i = 0;
            foreach (string x in BooksBeingBorrowed)
            {
                BookName[i] = x;
                i++;
            }
            string BooksToBeSentToDatabase = string.Join("~", BookName);
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";

                var arr = BooksToBeSentToDatabase.Split(new string[] { "~" }, StringSplitOptions.None);



                if (frm_Login.ds.cb_Reserve_BorrowerType.Text == "Student")
                {
                    sql = $"UPDATE Student SET student_book_borrowed = @books WHERE student_name = '{frm_Login.ds.cb_Reserve_Name.Text}'";
                }
                else if (frm_Login.ds.cb_Reserve_BorrowerType.Text == "Teacher")
                {
                    sql = $"UPDATE Teacher SET teacher_book_borrowed = @books WHERE teacher_name = '{frm_Login.ds.cb_Reserve_Name.Text}'";
                };
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@books", "RESERVED");
                int Saved = cmd.ExecuteNonQuery();
                if (Saved != 0)
                {
                    BookStatusChanger(BooksToBeSentToDatabase);
                    BookBorrowingChanger(BooksToBeSentToDatabase);
                }
                else
                {
                    Debug.WriteLine("Denied Saving.");
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        public static void BookStatusChanger(string books)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                string[] arr = books.Split(new string[] { "~" }, StringSplitOptions.None);
                foreach (string x in arr)
                {
                    if (x != "" && x != null)
                    {
                        sql = "UPDATE Books SET book_status = 'Reserved' WHERE book_title = @name";
                        SqlCommand cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@name", x);
                        cn.Open();
                        int Saved = cmd.ExecuteNonQuery();
                        if (Saved != 0)
                        {
                            Debug.WriteLine("BookStatus: Saved.");
                        }
                        else
                        {
                            Debug.WriteLine("BookStatus: Error.");
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public static void BookBorrowingChanger(string books)
        {
            try
            {
                Debug.Write("BookBorrowing");
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                cn.Open();
                SqlCommand cmd;
                sql = "INSERT INTO Reservation (book_title, borrower_name, date_reserve) VALUES (@books, @name, @datereserved)";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_Reserve_Name.Text);
                cmd.Parameters.AddWithValue("@books", books);
                cmd.Parameters.AddWithValue("@datereserved", frm_Login.ds.dtp_DateReserved.Value);
                
                
                int Saved = cmd.ExecuteNonQuery();
                if (Saved != 0)
                {
                    Debug.WriteLine("Saved");
                    MessageBox.Show("Reserved Successfully!");
                }
                else
                {
                    Debug.WriteLine("Error");
                }
                cn.Close();
                Reset();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
