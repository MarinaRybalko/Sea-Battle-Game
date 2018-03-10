using System;
using System.Linq;
using System.Windows.Forms;
using GameDataLayer;
     

namespace SeaBattleGame
{
    public partial class RatingForm : Form
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="RatingForm"/> class
        /// </summary>
        public RatingForm()
        {
            InitializeComponent();
        }

        private void RatingForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "playersDBDataSet.BattlePlayer". При необходимости она может быть перемещена или удалена.
            
                IRepository<BattlePlayer> topPlayersRepository = new PlayersRepository();
                dataGridView1.DataSource = topPlayersRepository.GetTopTen();

            }
        }
    }
