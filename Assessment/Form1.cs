﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assessment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputHandler inputHandler = new InputHandler();
            string outputResult = "";
            foreach (var item in InputRichTextBox.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    inputHandler.ParseInput(item, out outputResult);
                    if (String.Empty != outputResult)
                    {
                        OutputRichTextBox.Text += outputResult + Environment.NewLine;
                    }
                }
                catch
                {
                    OutputRichTextBox.Text += "I have no idea what you are talking about" + Environment.NewLine;
                }                
            }

            
        }
    }
}
