using System;
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
            foreach (var item in richTextBox1.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                richTextBox2.Text = inputHandler.ParseInput(item);
            }
            
            
        }
    }
}
