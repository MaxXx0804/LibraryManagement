namespace LibraryManagement
{
    partial class home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(home));
            this.close_Btn = new System.Windows.Forms.Button();
            this.username_Tb = new System.Windows.Forms.TextBox();
            this.password_Tb = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // close_Btn
            // 
            this.close_Btn.BackColor = System.Drawing.Color.Transparent;
            this.close_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_Btn.FlatAppearance.BorderSize = 0;
            this.close_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_Btn.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_Btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.close_Btn.Location = new System.Drawing.Point(866, -2);
            this.close_Btn.Name = "close_Btn";
            this.close_Btn.Size = new System.Drawing.Size(53, 41);
            this.close_Btn.TabIndex = 2;
            this.close_Btn.Text = "x";
            this.close_Btn.UseVisualStyleBackColor = false;
            this.close_Btn.Click += new System.EventHandler(this.close_Btn_Click);
            // 
            // username_Tb
            // 
            this.username_Tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username_Tb.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_Tb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.username_Tb.Location = new System.Drawing.Point(511, 334);
            this.username_Tb.Name = "username_Tb";
            this.username_Tb.Size = new System.Drawing.Size(199, 14);
            this.username_Tb.TabIndex = 4;
            // 
            // password_Tb
            // 
            this.password_Tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password_Tb.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_Tb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.password_Tb.Location = new System.Drawing.Point(511, 387);
            this.password_Tb.Name = "password_Tb";
            this.password_Tb.Size = new System.Drawing.Size(199, 14);
            this.password_Tb.TabIndex = 5;
            this.password_Tb.UseSystemPasswordChar = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::LibraryManagement.Properties.Resources.login3;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(921, 627);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(560, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "Log In";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 617);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.password_Tb);
            this.Controls.Add(this.username_Tb);
            this.Controls.Add(this.close_Btn);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MK Log In";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button close_Btn;
        private System.Windows.Forms.TextBox username_Tb;
        private System.Windows.Forms.TextBox password_Tb;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}

