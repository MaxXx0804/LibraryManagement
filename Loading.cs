﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_OOP_and_DSA
{

    public partial class Loading : Form
    {
        private int ticks = 2;
        private NotifyIcon notifyIcon;
        public static frm_Login frm;
        public Loading()
        {
                InitializeComponent();
        }    

        private void Loading_Load(object sender, EventArgs e)
        {

        }

        private void TextChange_Tick(object sender, EventArgs e)
        {
            string[] LoadingScreenText = { "Finding books...", "Cleaning books...", "Checking dictionary...", "Debugging program..." , "No one knows what the future holds...", "To be strong is not just about physical strength...","Water is illegal in some places...", "Chipi Chipi Chapa Chapa..."};
            Random rand = new Random();
            int num = rand.Next(LoadingScreenText.Length);
            lbl_LoadingScreenText.Text = LoadingScreenText[num];
            if (ticks > 0)
            {
                ticks--;
            }
            else
            {
                frm = new frm_Login();
                frm.Show();
                notifyIcon = new NotifyIcon();
                notifyIcon.Icon = this.Icon;
                notifyIcon.Visible = false;
                this.Hide();
                TextChange.Stop();
            }
        }
    }

}
