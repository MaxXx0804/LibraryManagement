using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Final_Project_OOP_and_DSA
{
    internal class BookBorrowingCode
    {
        public static void BookBorrowing(List<string> BooksBeingBorrowed)
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



                if (frm_Login.ds.cb_Member_BorrowerType.Text == "Student")
                {
                    sql = $"UPDATE Student SET student_book_borrowed = @books WHERE student_name = '{frm_Login.ds.cb_Member_Name.Text}'";
                }
                else if (frm_Login.ds.cb_Member_BorrowerType.Text == "Teacher")
                {
                    sql = $"UPDATE Teacher SET teacher_book_borrowed = @books WHERE teacher_name = '{frm_Login.ds.cb_Member_Name.Text}'";
                };
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@books", BooksToBeSentToDatabase);
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
                        sql = "UPDATE Books SET book_status = 'Borrowed' WHERE book_title = @name";
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
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                cn.Open();
                SqlCommand cmd;
                if (frm_Login.ds.cb_Member_BorrowerType.Text == "Teacher")
                {
                    sql = "INSERT INTO BookBorrowing (borrower_name, books_borrowed, date_borrowed) VALUES (@name, @books, @dateborrowed)";
                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_Member_Name.Text);
                    cmd.Parameters.AddWithValue("@books", books);
                    cmd.Parameters.AddWithValue("@dateborrowed", DateTime.Now.Date);
                }
                else
                {
                    sql = "INSERT INTO BookBorrowing (borrower_name, books_borrowed, date_borrowed, due_date) VALUES (@name, @books, @dateborrowed, @due)";
                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_Member_Name.Text);
                    cmd.Parameters.AddWithValue("@books", books);
                    cmd.Parameters.AddWithValue("@dateborrowed", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@due", DateTime.Now.Date.AddDays(3));
                }
                int Saved = cmd.ExecuteNonQuery();
                if (Saved != 0)
                {
                    Debug.WriteLine("BookBorrowing: Saved");
                    MessageBox.Show("Borrowed Successfully!");
                }
                else
                {
                    Debug.WriteLine("BookBorrowing: Error");
                }
                cn.Close();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
