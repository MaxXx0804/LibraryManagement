using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

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
            frm_Login.ds.lbl_Reserve_BookList.Text = string.Empty;
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


                lbl_name.Text = "Name: " + name;



                borrow.Name = name;
                cancel.Name = name;
                borrow.Click += new EventHandler(BorrowOnClick);
                cancel.Click += new EventHandler(CancelOnClick);
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
            frm_Login.ds.flp_ViewReservation.Controls.Clear();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            string contents = "";
            List<object[]> results = databaseConnection.QueryDatabaseForReserveBooks(cn, $"SELECT * FROM Reservation");
            foreach (var item in results)
            {

                string[] arr = item[0].ToString().Split(new string[] { "~" }, StringSplitOptions.None);


                foreach (string x in arr)
                {
                    contents += "\n" + x;
                }
                new ViewReservationContainers(item[1].ToString(), item[3].ToString(), contents, item[2].ToString());
                contents = "";
                //Array.Clear(arr,0,arr.Length);

            }

        }
        public static void CancelOnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "SELECT * FROM Reservation WHERE borrower_name = @name";
                cn.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", btn.Name);

                DataRow dr;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {

                    da.Fill(ds);
                    dr = ds.Tables[0].Rows[0];
                    Debug.WriteLine(dr.ToString());
                }

                DateTime dn = DateTime.Now.Date;
                TimeSpan ts = (DateTime)dr["date_reserve"] - dn;
                int timespan = ts.Days;
                var confirmation = MessageBox.Show("Do you want to cancel this reservation? This can't be undone.","Reservation", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes) 
                {
                    ReturnBooksExpiredDate(btn, dr);
                    Start();
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void BorrowOnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "SELECT * FROM Reservation WHERE borrower_name = @name";
                cn.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", btn.Name);

                DataRow dr;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {

                    da.Fill(ds);
                    dr = ds.Tables[0].Rows[0];
                    Debug.WriteLine(dr.ToString());
                }

                DateTime dn = DateTime.Now.Date;
                TimeSpan ts = (DateTime)dr["date_reserve"] - dn;
                int timespan = ts.Days;
                if (timespan == 0)
                {
                    var borrowNow = MessageBox.Show("Borrow this book now?", "Borrow", MessageBoxButtons.YesNo);
                    if(borrowNow == DialogResult.Yes)
                    {
                        BorrowBooks(dr);
                        RemoveFromDatabase(dr);
                    }
                }
                else if(timespan > 0)
                {
                    MessageBox.Show("You can't borrow a book that is not reserved today");
                }
                else if(timespan < 0)
                {
                    MessageBox.Show("Book is passed the reserved date");
                    var returnNow = MessageBox.Show("Do you want to cancel this reservation now?", "Reservation", MessageBoxButtons.YesNo);
                    if (returnNow == DialogResult.Yes)
                    {
                        ReturnBooksExpiredDate(btn, dr);
                    }
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        public static void RemoveFromDatabase(DataRow dr)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                cn.Open();
                sql = "DELETE FROM Reservation WHERE borrower_name = @name";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", dr["borrower_name"]);
                int saved = cmd.ExecuteNonQuery();
                if(saved > 0)
                {
                    Debug.WriteLine("RemoveFromDatabase: Successful");
                }
                else
                {
                    Debug.WriteLine("RemoveFromDatabase: Error");
                }
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }
        public static void BorrowBooks(DataRow dr)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            string sql = "";
            sql = "UPDATE Books SET book_status = 'Borrowed' WHERE book_title = @title";
            cn.Open();
            string[] arr = dr["book_title"].ToString().Split(new string[] { "~" }, StringSplitOptions.None);
            foreach (var item in arr) {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@title", item);
                int saved = cmd.ExecuteNonQuery();
                if (saved > 0)
                {
                    Debug.WriteLine("BorrowBooks: Successful");
                }
                else
                {
                    Debug.WriteLine("BorrowBooks: Error");
                }
            }
            BorrowBooksUserChange(dr);
            cn.Close();
        }
        public static void BorrowBooksUserChange(DataRow dr)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            string sql = "";
            
            if (dr["borrower_type"].ToString() == "Student")
            {
                sql = "UPDATE Student SET student_book_borrowed = @books WHERE student_name = @name";
            }
            else
            {
                sql = "UPDATE Student SET teacher_book_borrowed = @books WHERE teacher_name = @name";
            }
            cn.Open();
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@books", dr["book_title"].ToString());
            cmd.Parameters.AddWithValue("@name", dr["borrower_name"].ToString());
            int saved = cmd.ExecuteNonQuery();
            if(saved > 0)
            {
                Debug.WriteLine("BorrowBooksUserChange: Successful");
                BorrowBookBookBorrowing(dr);
            }
            else
            {
                Debug.WriteLine("BorrowBooksUserChange: Error");
            }
        }
        public static void BorrowBookBookBorrowing(DataRow dr)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            string sql = "";
            SqlCommand cmd;
            cn.Open();
            if (dr["borrower_type"].ToString() == "Student")
            {
                sql = "INSERT INTO BookBorrowing (borrower_name, books_borrowed, date_borrowed, due_date) VALUES (@name, @books, @dateborrowed, @due)";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", dr["borrower_name"].ToString());
                cmd.Parameters.AddWithValue("@books", dr["book_title"].ToString());
                cmd.Parameters.AddWithValue("@dateborrowed", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@due", DateTime.Now.Date.AddDays(3));
            }
            else
            {
                sql = "INSERT INTO BookBorrowing (borrower_name, books_borrowed, date_borrowed) VALUES (@name, @books, @dateborrowed)";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", dr["borrower_name"].ToString());
                cmd.Parameters.AddWithValue("@books", dr["book_title"].ToString());
                cmd.Parameters.AddWithValue("@dateborrowed", DateTime.Now.Date);
            }
            int saved = cmd.ExecuteNonQuery();
            if(saved > 0)
            {
                Debug.WriteLine("BorrowBookBookBorrowing: Successful");
            }
            else
            {
                Debug.WriteLine("BorrowBookBookBorrowing: Error");
            }
            cn.Close();
        }
        public static void ReturnBooksExpiredDate(Button btn, DataRow dr)
        {
            try
            {
                Debug.Write("ReturnBooksExpiredDate: ");
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                sql = "DELETE FROM Reservation WHERE borrower_name = @name";
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", btn.Name);
                int saved = cmd.ExecuteNonQuery();
                cn.Close();
                if (saved > 0)
                {
                    Debug.WriteLine("Successful");
                }
                else
                {
                    Debug.WriteLine("Error");
                }
                ChangeUserStatus(dr);
                
            }
            catch (Exception ex)
            {

            }
        }
        public static void ChangeUserStatus(DataRow dr)
        {
            Debug.Write("ChangeUserStatus: ");
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                if (dr["borrower_type"].ToString() == "Student")
                {
                    sql = "UPDATE Student SET student_book_borrowed = @booksborrowed WHERE student_name = @name";   
                }
                else
                {
                    sql = "UPDATE Teacher SET teacher_book_borrowed = @booksborrowed WHERE teacher_name = @name";
                }
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@booksborrowed", DBNull.Value);
                cmd.Parameters.AddWithValue("@name", dr["borrower_name"].ToString());
                int saved = cmd.ExecuteNonQuery();
                cn.Close();
                if (saved > 0)
                {
                    Debug.WriteLine("Successful");
                    string[] arr = dr["book_title"].ToString().Split(new string[] { "~" }, StringSplitOptions.None);
                    foreach (var item in arr)
                    {
                        ChangeBookStatus(item);
                    }
                    
                }
                else
                {
                    Debug.WriteLine("Error");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        public static void ChangeBookStatus(string bookname)
        {
            try
            {
                Debug.Write("ChangeBookStatus");
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                sql = "UPDATE Books SET book_status = @status WHERE book_title = @bookname";
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@status", "Available");
                cmd.Parameters.AddWithValue("@bookname", bookname);
                int saved = cmd.ExecuteNonQuery();
                cn.Close();
                if(saved > 0)
                {
                    Debug.WriteLine("Successful: " + bookname);
                }
                else
                {
                    Debug.WriteLine("Error");
                }
                frm_Login.ds.ResetAll();
                Start();
            }
            catch (Exception ex)
            {

            }
        }
        public static void BookReserve(List<string> BooksBeingBorrowed)
        {
            string[] BookName = new string[BooksBeingBorrowed.Count];
            int i = 0;
            foreach (string x in BooksBeingBorrowed)
            {
                BookName[i] = x;
                i++;
            }
            string BooksToBeSentToDatabase = string.Empty;
            BooksToBeSentToDatabase = string.Join("~", BookName);
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";

                var arr = BooksToBeSentToDatabase.Split(new string[] { "~" }, StringSplitOptions.None);



                if (frm_Login.ds.cb_Reserve_BorrowerType.Text == "Student")
                {
                    sql = $"UPDATE Student SET student_book_borrowed = @books WHERE student_name = @name";
                }
                else if (frm_Login.ds.cb_Reserve_BorrowerType.Text == "Teacher")
                {
                    sql = $"UPDATE Teacher SET teacher_book_borrowed = @books WHERE teacher_name = @name";
                };
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@books", "RESERVED");
                cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_Reserve_Name.Text);
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
                sql = "INSERT INTO Reservation (book_title, borrower_name, date_reserve, borrower_type) VALUES (@books, @name, @datereserved, @borrowertype)";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_Reserve_Name.Text);
                cmd.Parameters.AddWithValue("@books", books);
                cmd.Parameters.AddWithValue("@datereserved", frm_Login.ds.dtp_DateReserved.Value);
                cmd.Parameters.AddWithValue("@borrowertype", frm_Login.ds.cb_Reserve_BorrowerType.Text);


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
