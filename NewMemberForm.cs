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
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Azure;

namespace Final_Project_OOP_and_DSA
{
    public partial class NewMemberForm : Form
    {
        public NewMemberForm()
        {
            InitializeComponent();
            rb_NewMember_Student.Checked = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void rb_NewMember_Student_CheckedChanged(object sender, EventArgs e)
        {
            panel_StudentInfo.Visible = rb_NewMember_Student.Checked;
            panel_EmployeeInfo.Visible = rb_NewMember_Teacher.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel_StudentInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_NewMember_Register_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection cn = databaseConnection.DatabaseConnect();
                
                {

                    if (rb_NewMember_Student.Checked)
                    {
                        if (txt_NewMember_LastName.Text != "" && txt_NewMember_FirstName.Text != "" && txt_NewMember_StudentID.Text != "" && txt_NewMember_YearLevel.Text != "" && txt_NewMember_Section.Text != "")
                        {
                            string sql = "INSERT INTO Student (student_name, student_id, student_year_level, student_section) VALUES (@name, @id, @yearlevel, @section)";
                            cn.Open();
                            SqlCommand cmd = new SqlCommand(sql, cn);
                            string name = txt_NewMember_LastName.Text + ", " + txt_NewMember_FirstName.Text + " " + txt_NewMember_MiddleName.Text;
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@id", txt_NewMember_StudentID.Text);
                            cmd.Parameters.AddWithValue("@yearlevel", txt_NewMember_YearLevel.Text);
                            cmd.Parameters.AddWithValue("@section", txt_NewMember_Section.Text);

                            int Saved = cmd.ExecuteNonQuery();
                            if (Saved != 0)
                            {
                                MessageBox.Show("Registered Successfully!");
                            }
                            else
                            {
                                MessageBox.Show("There is a problem registering!");
                            }
                            cn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Make sure to fill up all the necessary information!");
                        }
                    }
                    else
                    {
                        if (txt_NewMember_LastName.Text != "" && txt_NewMember_FirstName.Text != "" && txt_NewMember_EmployeeID.Text != "" && txt_NewMember_Department.Text != "")
                        {
                            string sql = "INSERT INTO Teacher (teacher_name, employee_id, teacher_department) VALUES (@name, @id, @department)";
                            cn.Open();
                            SqlCommand cmd = new SqlCommand(sql, cn);
                            string name = txt_NewMember_LastName.Text + ", " + txt_NewMember_FirstName.Text + " " + txt_NewMember_MiddleName.Text;
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@id", txt_NewMember_EmployeeID.Text);
                            cmd.Parameters.AddWithValue("@department", txt_NewMember_Department.Text);

                            int Saved = cmd.ExecuteNonQuery();
                            if (Saved != 0)
                            {
                                MessageBox.Show("Registered Successfully!");
                            }
                            else
                            {
                                MessageBox.Show("There is a problem registering!");
                            }
                            cn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Make sure to fill up all the necessary information!");
                        }
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
