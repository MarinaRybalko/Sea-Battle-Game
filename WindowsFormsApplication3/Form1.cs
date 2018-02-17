using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Style;
using System.Reflection;
using System.IO;

namespace SeaBattleGame
{
    public partial class Form1 : Form
    {
        RatingForm ratingForm;
        GreetingForm greetingform;
        public static Field RightField { get; set; }
        public static Field LeftField { get; set; }

        public static Player RightPlayer { get; set; }
        public static Player LeftPlayer { get; set; }
        public static string playerName { get; set; }
      
        public Form1()
        {
            InitializeComponent();

            LeftField = new Field();
            RightField = new Field();

            LeftPlayer = new HumanPlay(LeftField);

            Statistics.GetLabel(label6, label7, label2, label4, label5, label1);
         
           

            Statistics.BeforeGame += BeforeGame;
            Statistics.BeginGame += BeginGame;
            Statistics.EndGame += EndGame;

            button1.Click += Statistics.Begin_Clicked;
            button3.Click += Statistics.Ok_Clicked;

            button2.Click += LeftField.RandomShips.RandomClicked;
            FillTable(tableLayoutPanel1, LeftField.CellField);
            FillTable(tableLayoutPanel2, RightField.CellField);
            LeftField.DisplayCompletionCell();
            greetingform = new GreetingForm();
            greetingform.ShowDialog();
          
        }

        void FillTable(TableLayoutPanel table, SeaBattlePicture[,] PictBox)
        {
             int sizeCell = table.Width / table.ColumnCount;

             for (var i = 0; i < Field.Size; i++)
             {
                  for (var j = 0; j < Field.Size; j++)
                  { 
                       PictBox[i, j].Height = RightField.CellField[i, j].Width = sizeCell;
                       PictBox[i, j].Margin = new Padding(1, 1, 1, 1);
                       PictBox[i, j].RenderingMode = CellStatus.Empty;
                       table.Controls.Add(PictBox[i, j], j,i );
                  }
             }
        }

        void BeforeGame(object sender, EventArgs e)
        {
           

            button3.Visible = false;

            button1.Visible = true;
            button2.Visible = true;
        


        }

       void BeginGame(object sender, EventArgs e)
        {
           
            button1.Visible = false;
            button2.Visible = false;

            RightPlayer = new MyBotPlayer(RightField);


            RightPlayer.Oponent = LeftPlayer;
            LeftPlayer.Oponent = RightPlayer;

            RightPlayer.TransferMove += Statistics.Transfer_Move;
            LeftPlayer.TransferMove += Statistics.Transfer_Move;
               
        }

        void EndGame(object sender, EventArgs e)
        {
            RightPlayer.TransferMove -= Statistics.Transfer_Move;
            LeftPlayer.TransferMove -= Statistics.Transfer_Move;

            button3.Visible = true;
        }

       
        void ButtonEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            button.BackColor = Color.Blue;
            button.ForeColor = Color.Gold;
        }

        void ButtonLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            button.BackColor = Color.DodgerBlue;
            button.ForeColor = Color.White;
        }
        
       

        private void aquaStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Assembly assemblyAqua;
            try
            {
                assemblyAqua = Assembly.Load(Properties.Resources.StyleAqua);
                Type [] types  = assemblyAqua.GetTypes();
                foreach(var t in types)
                {
                    if (t.ToString().Contains("AquaStyle"))
                    {
                        
                        MethodInfo setstyle = t.GetMethod("SetBackgroundStyle");
                        object obj = Activator.CreateInstance(t);
                        object[] param = new object[] { this };
                        setstyle.Invoke(obj, param);
                    }

                }     
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Sorry. This style temporarily unavailable");
            }
        }

        private void grayStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Assembly assemblyAqua = null ;
            try
            {
                assemblyAqua = Assembly.Load(Properties.Resources.StyleGray);

                Type[] types = assemblyAqua.GetTypes();
                foreach (var t in types)
                {
                    if (t.ToString().Contains("GrayStyle"))
                    {

                        MethodInfo setstyle = t.GetMethod("SetBackgroundStyle");
                        object obj = Activator.CreateInstance(t);
                        object[] param = new object[] { this };
                        setstyle.Invoke(obj, param);
                    }

                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Sorry. This style temporarily unavailable");
            }
        }

       
        private void topPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ratingForm = new RatingForm();
            ratingForm.ShowDialog();
        }

        private void aboutGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sea Battle "+Environment.NewLine+"Version 1.0.0"+Environment.NewLine+"Developer: Marina Rybalko");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

     }
}
