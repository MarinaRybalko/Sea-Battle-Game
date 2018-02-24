using System;
using System.Windows.Forms;
    
namespace GameCore
{
    public class Cell:PictureBox
    {
        public Location CellLocation { get; set; }
        public Ship ShipIntoCell { get; set; }
       
        public CellStatus CellStatus { get; set; }
        public Cell(int x, int y)
        {            
            CellLocation = new Location(x, y);
        }
        public Cell()
        {

        }
    }
}
