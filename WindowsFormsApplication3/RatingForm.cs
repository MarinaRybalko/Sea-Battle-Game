using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleGame
{
    public partial class RatingForm : Form
    {
        public RatingForm()
        {
            InitializeComponent();
        }

        private void RatingForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "playersDBDataSet.BattlePlayer". При необходимости она может быть перемещена или удалена.
           // this.battlePlayerTableAdapter.Fill(this.playersDBDataSet.BattlePlayer);
            using (PlayersDBEntities db = new PlayersDBEntities())
            {
              
                var query = from p in db.BattlePlayer
                            orderby p.Rating descending
                            select new { Name = p.Name, Rating =p.Rating};

                dataGridView1.DataSource = query.Take(10). ToList();

            }
        }
    }
}
