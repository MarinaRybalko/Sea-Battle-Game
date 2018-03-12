
using GameCore;
using NLog;

namespace GameService
{
    public class HumanPlay:Player
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Returns or sets if can player move
        /// </summary>
        public bool CanMove { get; set; }
        /// <summary>
        /// Initialize a new instanse of the <see cref="HumanPlay"/> class
        /// </summary>
        /// <param name="field"></param>
        public HumanPlay(Field field):base(field)
        {
            
        }
        /// <summary>
        /// Make a move
        /// </summary>
        public override void Move()
        {
            Logger.Debug("Attempt to make a shot. Opportunity to move is"+ CanMove );
            CanMove = true;
            Logger.Debug("Attempt to make a shot successfully completed. Opportunity to move is" + CanMove);
        }
       
    }
}
