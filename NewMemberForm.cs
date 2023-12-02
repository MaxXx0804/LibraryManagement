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
                if (rb_NewMember_Student.Checked)
                {
                    string sql = "INSERT INTO Student (student_name, student_id, student_year_level, student_section) VALUES (@name, @id, @yearlevel, @section)";
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    string name = txt_NewMember_LastName.Text + ", " + txt_NewMember_FirstName.Text + " " + txt_NewMember_MiddleName.Text;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", txt_NewMember_StudentID.Text);
                    cmd.Parameters.AddWithValue("@yearlevel", txt_NewMember_YearLevel.Text);
                    cmd.Parameters.AddWithValue("@section", txt_NewMember_Section.Text);
                    
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
