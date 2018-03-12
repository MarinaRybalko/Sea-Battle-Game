using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeaBattleGame
{
    public class Player 
    {       
        public event EventHandler TransferMove;

        protected Field oponentField { get; set; }
        Player oponent;

        protected event EventHandler OponentChanged;        

        public Field ownField { get; set; }
        public Player Oponent
        {
            get { return oponent; }
            set {
                    oponent = value;
                    oponentField = oponent.ownField;
                    OponentChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Player(Field field)
        {
            ownField = field;
        }

        protected void CallTransferMove()
        {
            TransferMove?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Move()
        {
           
        }
    }
}
