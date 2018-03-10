using System;

namespace GameCore
{
    public class Player
    {   
        /// <summary>
        /// Occurs when player transfer move to another one
        /// </summary>
        public event EventHandler TransferMove;
        /// <summary>
        /// Returns and sets oponent field
        /// </summary>
        public Field OponentField { get; protected set; }
        private Player _oponent;
        /// <summary>
        /// Occurs when players oponent changed
        /// </summary>
        public event EventHandler OponentChanged;
        /// <summary>
        /// Returns and sets own field
        /// </summary>
        public Field OwnField { get; set; }
        /// <summary>
        /// Returns and sets players oponent 
        /// </summary>
        public Player Oponent
        {
            get { return _oponent; }
            set {
                    _oponent = value;
                    OponentField = _oponent.OwnField;
                    OponentChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Initialize a new instance of the <see cref="Player"/> class
        /// </summary>
        /// <param name="field"></param>
        public Player(Field field)
        {
            OwnField = field;
        }
        /// <summary>
        /// Transfers move to another player
        /// </summary>
        public void CallTransferMove()
        {
            TransferMove?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Make a move
        /// </summary>
        public virtual void Move()
        {
            
        }
    }
}
