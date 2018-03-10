using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SeaBattleGame
{
    public partial class GreetingForm : Form
    {
       
        /// <summary>
        /// Initialize a new instance of the <see cref="GreetingForm"/> class
        /// </summary>
        public GreetingForm()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            const string pattern = @"^[a-z]|[а-я]+$";
            if(Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
            {
                Close();
                    
            }
            else
            {
                errorProvider1.SetError(textBox1,"Uncorrect name");
                Show();
            }
        }
        private void GreetingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string pattern = @"^[a-z]|[а-я]+$";

            if (Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
                {
               // MyDelegateEvent(textBox1.Text);
                e.Cancel = false;
                
                }
                else
                {
                    errorProvider1.SetError(textBox1, "Ucorrect name");
                    e.Cancel = true;
                    Show();
                }
        }

       
    }
}
