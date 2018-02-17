using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleGame
{
    public partial class GreetingForm : Form
    {
        
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[a-z]|[а-я]+$";
            if(Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
            {
                Form1.playerName = textBox1.Text;
                this.Close();
                    
            }
            else
            {
                errorProvider1.SetError(textBox1,"Ucorrect name");
                this.Show();
            }
        }
        private void GreetingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                string pattern = @"^[a-z]|[а-я]+$";
            
                if (Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
                {
                Form1.playerName = textBox1.Text;
                    e.Cancel = false;
                
                }
                else
                {
                    errorProvider1.SetError(textBox1, "Ucorrect name");
                    e.Cancel = true;
                    this.Show();
                }
            
        }

       
    }
}
