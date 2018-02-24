using System;

namespace GameCore
{
    public class Player
    {       
        public event EventHandler TransferMove;

        public Field OponentField { get; protected set; }
        private Player _oponent;

        public event EventHandler OponentChanged;        

        public Field OwnField { get; set; }
        public Player Oponent
        {
            get { return _oponent; }
            set {
                    _oponent = value;
                    OponentField = _oponent.OwnField;
                    OponentChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Player(Field field)
        {
            OwnField = field;
        }

        public void CallTransferMove()
        {
            TransferMove?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Move()
        {
            
        }
    }
}
