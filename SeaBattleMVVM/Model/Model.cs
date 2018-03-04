using GameDataLayer;
using GameService;

namespace SeaBattleMVVM.Model
{
    public class Model
    {
        public IRepository<BattlePlayer> Players { get; }
       
        public Model(GameController game)
        {
            Game = game;
            Players = new PlayersRepository();
            
        }


        public GameController Game { get; }
    }
}
