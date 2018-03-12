using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace SeaBattleGame
{
    public class SeaBattlePicture:PictureBox
    {
      CellStatus renderingMode;

      public  CellStatus RenderingMode
      {
            get
            {
                return renderingMode;
            }
            set
            {
                renderingMode = value;
                Invalidate();
                Update();
            }
      }

      public Location CellLocation { get; set; }

      public Ship ShipIntoCell { get; set; }    
               
        public SeaBattlePicture():base()
        {
            Paint += PaintBox;
        }

        Rectangle ractangle;

        void PaintBox(object sender,PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = 
                System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            Color color = Color.FromKnownColor(KnownColor.Gainsboro);

            ractangle = e.ClipRectangle;

            switch (RenderingMode)
            {
                case CellStatus.Completion:
                    color = Color.RoyalBlue;
                    break;
                case CellStatus.Crippled:
                    color = Color.RoyalBlue;
                    break;
                case CellStatus.Drowned:
                    color = Color.Red;
                    break;
                case CellStatus.ValidLocation:
                    color = Color.Green;
                    break;
                case CellStatus.NotValidLocation:
                    color = Color.Orange;
                    break;
            }

            e.Graphics.Clear(color);

            if ((RenderingMode == CellStatus.Crippled) ||
                (RenderingMode == CellStatus.Drowned)) PaintCross(e);

            if (RenderingMode == CellStatus.Miss) PaintPoint(e);
        }

        void PaintCross(PaintEventArgs e)
        {
            int padding = 2;

            e.Graphics.DrawLine(Pens.Black,
                new Point(ractangle.Left + padding, ractangle.Top + padding),
                new Point(ractangle.Right - padding, ractangle.Bottom - padding));

            e.Graphics.DrawLine(Pens.Black,
                new Point(ractangle.Right - padding, ractangle.Top + padding),
                new Point(ractangle.Left + padding, ractangle.Bottom - padding));
        }

        void PaintPoint(PaintEventArgs e)
        {


            int padding = 10;

            e.Graphics.DrawEllipse(Pens.Black,
                new Rectangle(ractangle.Left + padding, ractangle.Top + padding,
                ractangle.Width - 2 * padding, ractangle.Height - 2 * padding));

            e.Graphics.FillEllipse(Brushes.Black,
                new Rectangle(ractangle.Left + padding, ractangle.Top + padding,
                ractangle.Width - 2 * padding, ractangle.Height - 2 * padding));
        }
    }
}
