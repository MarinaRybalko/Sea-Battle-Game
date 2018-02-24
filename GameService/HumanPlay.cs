
using GameCore;

namespace GameService
{
    public class HumanPlay:Player
    {
        public bool CanMove { get; set; }
        public string Name { get; set; }
        public HumanPlay(Field field):base(field)
        {
            
        }

        public override void Move()
        {
            CanMove = true;
        }
       
    }
}
