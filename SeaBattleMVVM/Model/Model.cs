using GameDataLayer;
using GameService;

namespace SeaBattleMVVM.Model
{
    public class Model
    {
        /// <summary>
        /// Returns or sets players repository
        /// </summary>
        public IRepository<BattlePlayer> Players { get; }
       /// <summary>
       /// Initialize a new instance of the <see cref="Model"/> class
       /// </summary>
       /// <param name="game"></param>
        public Model(GameController game)
        {
            Game = game;
            Players = new PlayersRepository();
            
        }
        /// <summary>
        /// Returns or sets game algorithm
        /// </summary>
        public GameController Game { get; }
    }
}
