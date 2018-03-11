
using System.Windows.Forms;

namespace GameCore
{
    public class Cell:PictureBox

    {
      
        /// <summary>
        /// Returns or sets cell location on field
        /// </summary>
        public Location CellLocation { get; set; }
        /// <summary>
        /// Returns or sets ship that belong current cell
        /// </summary>
        public Ship ShipIntoCell { get; set; }
        /// <summary>
        /// Returns or sets cell status that describes shot result
        /// </summary>
        public CellStatus CellStatus { get; set; }
        /// <summary>
        /// Initialize a new instance of the <see cref="Cell"/> class
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        public Cell(int x, int y)
    {
        CellLocation = new Location(x, y);
    }
        /// <inheritdoc  />
        /// <summary>
        /// Initialize a new instance of the <see cref="Cell"/> classby default
        /// </summary>
        public Cell()
    {

    }

        
    }
}
