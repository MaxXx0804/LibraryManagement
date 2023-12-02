namespace Final_Project_OOP_and_DSA
{
    partial class BookInformationDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookInformationDisplay));
            this.panel17 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_BookTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb_BookDisplay_Image = new System.Windows.Forms.PictureBox();
            this.lbl_BookDisplay_BookTitleLabel = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_ISBN = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_Category = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_Author = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_Copyright = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_Publisher = new System.Windows.Forms.Label();
            this.lbl_BookDisplay_Status = new System.Windows.Forms.Label();
            this.panel17.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BookDisplay_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel17.Controls.Add(this.lbl_BookDisplay_BookTitle);
            this.panel17.Controls.Add(this.label21);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(800, 50);
            this.panel17.TabIndex = 6;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "label21";
            // 
            // lbl_BookDisplay_BookTitle
            // 
            this.lbl_BookDisplay_BookTitle.AutoSize = true;
            this.lbl_BookDisplay_BookTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_BookDisplay_BookTitle.Font = new System.Drawing.Font("Bahnschrift", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_BookTitle.ForeColor = System.Drawing.Color.White;
            this.lbl_BookDisplay_BookTitle.Location = new System.Drawing.Point(0, 0);
            this.lbl_BookDisplay_BookTitle.Name = "lbl_BookDisplay_BookTitle";
            this.lbl_BookDisplay_BookTitle.Padding = new System.Windows.Forms.Padding(13);
            this.lbl_BookDisplay_BookTitle.Size = new System.Drawing.Size(236, 50);
            this.lbl_BookDisplay_BookTitle.TabIndex = 1;
            this.lbl_BookDisplay_BookTitle.Text = "Book Title Information";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Controls.Add(this.lbl_BookDisplay_Status);
            this.panel1.Controls.Add(this.lbl_BookDisplay_Publisher);
            this.panel1.Controls.Add(this.lbl_BookDisplay_Copyright);
            this.panel1.Controls.Add(this.lbl_BookDisplay_Author);
            this.panel1.Controls.Add(this.lbl_BookDisplay_Category);
            this.panel1.Controls.Add(this.lbl_BookDisplay_ISBN);
            this.panel1.Controls.Add(this.lbl_BookDisplay_BookTitleLabel);
            this.panel1.Controls.Add(this.pb_BookDisplay_Image);
            this.panel1.Controls.Add(this.panel17);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pb_BookDisplay_Image
            // 
            this.pb_BookDisplay_Image.Location = new System.Drawing.Point(520, 77);
            this.pb_BookDisplay_Image.Name = "pb_BookDisplay_Image";
            this.pb_BookDisplay_Image.Size = new System.Drawing.Size(250, 350);
            this.pb_BookDisplay_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_BookDisplay_Image.TabIndex = 7;
            this.pb_BookDisplay_Image.TabStop = false;
            this.pb_BookDisplay_Image.Click += new System.EventHandler(this.btn_BookDisplay_Information);
            // 
            // lbl_BookDisplay_BookTitleLabel
            // 
            this.lbl_BookDisplay_BookTitleLabel.AutoSize = true;
            this.lbl_BookDisplay_BookTitleLabel.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_BookTitleLabel.Location = new System.Drawing.Point(30, 100);
            this.lbl_BookDisplay_BookTitleLabel.Name = "lbl_BookDisplay_BookTitleLabel";
            this.lbl_BookDisplay_BookTitleLabel.Size = new System.Drawing.Size(33, 13);
            this.lbl_BookDisplay_BookTitleLabel.TabIndex = 8;
            this.lbl_BookDisplay_BookTitleLabel.Text = "Title: ";
            this.lbl_BookDisplay_BookTitleLabel.Click += new System.EventHandler(this.lbl_BookDisplay_BookTitleLabel_Click);
            // 
            // lbl_BookDisplay_ISBN
            // 
            this.lbl_BookDisplay_ISBN.AutoSize = true;
            this.lbl_BookDisplay_ISBN.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_ISBN.Location = new System.Drawing.Point(30, 140);
            this.lbl_BookDisplay_ISBN.Name = "lbl_BookDisplay_ISBN";
            this.lbl_BookDisplay_ISBN.Size = new System.Drawing.Size(37, 13);
            this.lbl_BookDisplay_ISBN.TabIndex = 9;
            this.lbl_BookDisplay_ISBN.Text = "ISBN: ";
            // 
            // lbl_BookDisplay_Category
            // 
            this.lbl_BookDisplay_Category.AutoSize = true;
            this.lbl_BookDisplay_Category.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_Category.Location = new System.Drawing.Point(30, 180);
            this.lbl_BookDisplay_Category.Name = "lbl_BookDisplay_Category";
            this.lbl_BookDisplay_Category.Size = new System.Drawing.Size(56, 13);
            this.lbl_BookDisplay_Category.TabIndex = 10;
            this.lbl_BookDisplay_Category.Text = "Category: ";
            this.lbl_BookDisplay_Category.Click += new System.EventHandler(this.label3_Click);
            // 
            // lbl_BookDisplay_Author
            // 
            this.lbl_BookDisplay_Author.AutoSize = true;
            this.lbl_BookDisplay_Author.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_Author.Location = new System.Drawing.Point(30, 220);
            this.lbl_BookDisplay_Author.Name = "lbl_BookDisplay_Author";
            this.lbl_BookDisplay_Author.Size = new System.Drawing.Size(45, 13);
            this.lbl_BookDisplay_Author.TabIndex = 11;
            this.lbl_BookDisplay_Author.Text = "Author: ";
            this.lbl_BookDisplay_Author.Click += new System.EventHandler(this.lbl_BookDisplay_Author_Click);
            // 
            // lbl_BookDisplay_Copyright
            // 
            this.lbl_BookDisplay_Copyright.AutoSize = true;
            this.lbl_BookDisplay_Copyright.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_Copyright.Location = new System.Drawing.Point(30, 260);
            this.lbl_BookDisplay_Copyright.Name = "lbl_BookDisplay_Copyright";
            this.lbl_BookDisplay_Copyright.Size = new System.Drawing.Size(60, 13);
            this.lbl_BookDisplay_Copyright.TabIndex = 12;
            this.lbl_BookDisplay_Copyright.Text = "Copyright: ";
            // 
            // lbl_BookDisplay_Publisher
            // 
            this.lbl_BookDisplay_Publisher.AutoSize = true;
            this.lbl_BookDisplay_Publisher.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_Publisher.Location = new System.Drawing.Point(30, 300);
            this.lbl_BookDisplay_Publisher.Name = "lbl_BookDisplay_Publisher";
            this.lbl_BookDisplay_Publisher.Size = new System.Drawing.Size(59, 13);
            this.lbl_BookDisplay_Publisher.TabIndex = 13;
            this.lbl_BookDisplay_Publisher.Text = "Publisher: ";
            this.lbl_BookDisplay_Publisher.Click += new System.EventHandler(this.label6_Click);
            // 
            // lbl_BookDisplay_Status
            // 
            this.lbl_BookDisplay_Status.AutoSize = true;
            this.lbl_BookDisplay_Status.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BookDisplay_Status.Location = new System.Drawing.Point(30, 340);
            this.lbl_BookDisplay_Status.Name = "lbl_BookDisplay_Status";
            this.lbl_BookDisplay_Status.Size = new System.Drawing.Size(45, 13);
            this.lbl_BookDisplay_Status.TabIndex = 14;
            this.lbl_BookDisplay_Status.Text = "Status: ";
            // 
            // BookInformationDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BookInformationDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BookInformationDisplay";
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BookDisplay_Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label lbl_BookDisplay_BookTitle;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BookDisplay_BookTitleLabel;
        private System.Windows.Forms.PictureBox pb_BookDisplay_Image;
        private System.Windows.Forms.Label lbl_BookDisplay_Publisher;
        private System.Windows.Forms.Label lbl_BookDisplay_Copyright;
        private System.Windows.Forms.Label lbl_BookDisplay_Author;
        private System.Windows.Forms.Label lbl_BookDisplay_Category;
        private System.Windows.Forms.Label lbl_BookDisplay_ISBN;
        private System.Windows.Forms.Label lbl_BookDisplay_Status;
    }
}