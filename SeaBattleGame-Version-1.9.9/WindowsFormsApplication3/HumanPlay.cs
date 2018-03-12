using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleGame
{
    class HumanPlay:Player
    {
        bool canMove;
        public string Name { get; set; }
        public HumanPlay(Field field):base(field)
        {
            OponentChanged += Oponent_Changed;

            SubscriptionOwnField();
        }

        private void Oponent_Changed(object sender, EventArgs e)
        {
            if (Oponent != null) UnsubscriptionOponentField();

            SubscriptionOponentField();
        }

        void SubscriptionOwnField()
        {
            foreach (var value in ownField.CellField)
            {
                value.MouseClick += MouseEvent.MouseClicked;


            }
        }

        void SubscriptionOponentField()
        {
            foreach (var value in oponentField.CellField)
            {
                value.MouseClick += Cell_Click;
            }
        }

        void UnsubscriptionOponentField()
        {
            foreach (var value in oponentField.CellField)
            {
                value.MouseClick -= Cell_Click;
            }
        }

        public override void Move()
        {
            canMove = true;
        }
        void Cell_Click(object sender, MouseEventArgs e)
        {
            if (!canMove) return;

            SeaBattlePicture cell = (SeaBattlePicture)sender;
            cell.Enabled = false;

            CellStatus shotResult = Form1.RightField.Shot(cell);

            if (shotResult == CellStatus.Miss)
            {
                canMove = false;
                CallTransferMove();                
            }
        }
    }
}
