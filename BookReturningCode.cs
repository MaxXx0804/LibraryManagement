using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using Final_Project_OOP_and_DSA.Properties;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.Tracing;
using Microsoft.IdentityModel.Tokens;

namespace Final_Project_OOP_and_DSA
{
    internal class BookReturningCode
    {
        private static string booksName = "";
        private static List<string> booksReturning = new List<string>();
        private static List<string> booksRemaining = new List<string>();
        private static int diffDays = 0;

        public static void Refresh()
        {   
            frm_Login.ds.flp_BooksReturn.Controls.Clear();
            frm_Login.ds.lbl_BookReturn_BookList.Text = "Book List: ";
            frm_Login.ds.lbl_BookReturn_DateBorrowed.Text = "Date Borrowed: ";
            frm_Login.ds.lbl_BookReturn_DueDate.Text = "Due Date: ";
            frm_Login.ds.lbl_BookReturn_ReturnerType.Text = "Returner Type: ";
            frm_Login.ds.lbl_BookReturn_ReturnerName.Text = "Returner Name: ";
        }
        public static void Reset()
        {
            frm_Login.ds.flp_BooksReturn.Controls.Clear();
            frm_Login.ds.lbl_BookReturn_BookList.Text = "Book List: ";
            frm_Login.ds.lbl_BookReturn_DateBorrowed.Text = "Date Borrowed: ";
            frm_Login.ds.lbl_BookReturn_DueDate.Text = "Due Date: ";
            frm_Login.ds.lbl_BookReturn_ReturnerType.Text = "Returner Type: ";
            frm_Login.ds.lbl_BookReturn_ReturnerName.Text = "Returner Name: ";
            frm_Login.ds.cb_BookReturn_BorrowerType.SelectedIndex = -1;
            frm_Login.ds.cb_BookReturn_Name.Items.Clear();
        }
        public static void ChangeContent(string name)
        {
            Refresh();    
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            List<object[]> results = databaseConnection.QueryDatabaseForReturnBooks(cn, $"SELECT * FROM BookBorrowing WHERE borrower_name = '{name}'");
            foreach (var item in results)
            { 
                //Borrower Name
                frm_Login.ds.lbl_BookReturn_ReturnerName.Text = "Borrower Name: " + item[1].ToString();
                //BookList
                string[] arr = item[2].ToString().Split(new string[] { "~" }, StringSplitOptions.None);
                if (frm_Login.ds.cb_BookReturn_BorrowerType.Text == "Student")
                {
                    DateTime DueDate = (DateTime)item[4];
                    DateTime DateNow = DateTime.Now;
                    TimeSpan timeSpan = DateNow - DueDate;
                    diffDays = timeSpan.Days;
                }
                
                foreach(string x in arr)
                {
                    frm_Login.ds.lbl_BookReturn_BookList.Text += "\n"+x;
                    booksName += x;
                    GetBooks(x);
                }
                //Date Borrower                               
                frm_Login.ds.lbl_BookReturn_DateBorrowed.Text = "Date Borrowed: " + item[3];
                
                //Due
                if (item[4].ToString() != "NULL")
                {
                    frm_Login.ds.lbl_BookReturn_DueDate.Text = "Due Date: " + item[4];
                }
                else
                {
                    frm_Login.ds.lbl_BookReturn_DueDate.Text = "Due Date: Not applicable for returner type.";
                }
                
            }
        }
        public static void GetBooks(string name)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            Debug.WriteLine(name);
            List<string[]> results = databaseConnection.QueryDatabaseDashboardInformationBook(cn, $"SELECT book_title, book_url FROM Books WHERE book_title = '{name}'");
            foreach(string[] arr in results )
            {
                try
                {
                    if (arr != null)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Dock = DockStyle.Fill;
                        pictureBox.Image = (Bitmap)Resources.ResourceManager.GetObject(arr[1].ToString());
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Name = arr[0].ToString();
                        pictureBox.BringToFront();

                        CheckBox cb = new CheckBox
                        {
                            Text = arr[0].ToString(),
                            Dock = DockStyle.Bottom,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Size = new Size(0, 30),
                            ForeColor = Color.Black,
                            Font = new Font("Bahnschrift", 7)
                        };
                        cb.CheckedChanged += new EventHandler(GetBooksName);
                        Panel panel = new Panel()
                        {
                            Size = new Size(125, 175),
                            BackColor = Color.Transparent,
                        };
                        panel.Controls.Add(pictureBox);
                        panel.Controls.Add(cb);
                        frm_Login.ds.flp_BooksReturn.Controls.Add(panel);
                        booksRemaining.Add(arr[0].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                foreach(var item in booksRemaining)
                {
                    Debug.WriteLine($"{item}");
                }
            }
        }
        public static void GetBooksName(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                booksRemaining.Remove(checkBox.Text);
                if (!booksReturning.Contains(checkBox.Text))
                {
                    
                    booksReturning.Add(checkBox.Text);
                }
            }
            else
            {
                booksRemaining.Add(checkBox.Text);
                if (booksReturning.Contains(checkBox.Text))
                {
                    
                    booksReturning.Remove(checkBox.Text);
                }
            }
            foreach (string item in booksReturning)
            {
                Debug.WriteLine(item);
            }
        }
        
        private static object GetBooksRemaining()
        {
            object result = "";
            if (!(booksRemaining.Count > 0))
            {
                result = DBNull.Value;
                Debug.WriteLine(result);
            }
            else
            {
                foreach (var item in booksRemaining)
                {
                    result += item + "~";
                    Debug.WriteLine(result);
                }
            }
            return result;
        }
        private static void BookReturnForBooks(string bookName)
        {
            try
            {
                Debug.Write("BookReturnForBooks: ");

                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                sql = "UPDATE Books SET book_status = 'Available' WHERE book_title = @name";

                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", bookName);
                int sent = cmd.ExecuteNonQuery();
                if (sent > 0)
                {
                    Debug.WriteLine("Successful");
                }
                else
                {
                    Debug.WriteLine("Unsuccessful");
                }

                cn.Close();
            }
            catch (Exception ex)
            {

            }
        }
        private static bool BookReturnBookBorrowing()
        {
            try
            {
                Debug.Write("BookReturnBookBorrowing: ");
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                string sql = "";
                if (!(GetBooksRemaining() == DBNull.Value))
                {
                    sql = "UPDATE BookBorrowing SET books_borrowed = @books WHERE borrower_name = @name";
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@books", GetBooksRemaining());
                    cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_BookReturn_Name.Text);
                    int Saved = cmd.ExecuteNonQuery();
                    cn.Close();
                    if (Saved > 0)
                    {
                        Debug.WriteLine("Successful");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Unsuccessful");
                        return false;
                    }
                }
                else
                {
                    sql = "DELETE FROM BookBorrowing WHERE borrower_name = @name";
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_BookReturn_Name.Text);
                    int Saved = cmd.ExecuteNonQuery();
                    cn.Close();
                    if (Saved > 0)
                    {
                        Debug.WriteLine("Successful");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Unsuccessful");
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private static bool BookBorrowedChange()
        {
            Debug.Write("BookBorrowChange: ");
            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection cn = databaseConnection.DatabaseConnect();
            string sql = "";
            int saved = 0;
            try
            {
                if(frm_Login.ds.cb_BookReturn_BorrowerType.Text == "Teacher")
                {
                    sql = "UPDATE Teacher SET teacher_book_borrowed = @books WHERE teacher_name = @name";
                      
                }
                else if (frm_Login.ds.cb_BookReturn_BorrowerType.Text == "Student")
                {
                    sql = "UPDATE Student SET student_book_borrowed = @books WHERE student_name = @name";
                }
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@books", GetBooksRemaining());
                cmd.Parameters.AddWithValue("@name", frm_Login.ds.cb_BookReturn_Name.Text);
                Debug.WriteLine("@books" + GetBooksRemaining());
                Debug.WriteLine("@name" + frm_Login.ds.cb_BookReturn_Name.Text);
                saved = cmd.ExecuteNonQuery();
                if (saved > 0)
                {
                    Debug.WriteLine("Successful");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Unsuccessful");
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return false;
        }

        public static bool InitiateBookReturning()
        {
            Debug.WriteLine(diffDays);
            if (diffDays > 0) {
                DialogResult res = MessageBox.Show("The book is past its due date. Do you want to settle the overdue fee?", "Confirmation", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                {
                    
                }
                else
                {
                    
                }
                MessageBox.Show("This function is still work in progress");
                return false;
            }
            if (booksReturning.Count > 0)
            {
                booksReturning.ForEach(item => BookReturnForBooks(item));
            }
            if(BookBorrowedChange() && BookReturnBookBorrowing())
            {
                MessageBox.Show("Return Successful");
            }
            else
            {
                MessageBox.Show("Return Unsuccessful");
            }
            Reset();
            return true;
        }
    }
}
