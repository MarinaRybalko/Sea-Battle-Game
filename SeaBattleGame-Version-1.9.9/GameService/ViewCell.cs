using System;
using System.Drawing;
using System.Windows.Forms;
using GameCore;

namespace GameService
{
    public class ViewCell
    {
        private Rectangle _rectangle;
        private Color _color;
        private void PaintCross(PaintEventArgs e)
        {
            var padding = 2;

            e.Graphics.DrawLine(Pens.Black,
                new Point(_rectangle.Left + padding, _rectangle.Top + padding),
                new Point(_rectangle.Right - padding, _rectangle.Bottom - padding));

            e.Graphics.DrawLine(Pens.Black,
                new Point(_rectangle.Right - padding, _rectangle.Top + padding),
                new Point(_rectangle.Left + padding, _rectangle.Bottom - padding));
        }
        private void PaintPoint(PaintEventArgs e)
        {


            var padding = 10;

            e.Graphics.DrawEllipse(Pens.Black,
                new Rectangle(_rectangle.Left + padding, _rectangle.Top + padding,
                    _rectangle.Width - 2 * padding, _rectangle.Height - 2 * padding));

            e.Graphics.FillEllipse(Brushes.Black,
                new Rectangle(_rectangle.Left + padding, _rectangle.Top + padding,
                    _rectangle.Width - 2 * padding, _rectangle.Height - 2 * padding));
        }
        public void PaintCell(object sender, PaintEventArgs e)
        {
            var cell = sender as Cell;
           e.Graphics.CompositingQuality =
           System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            _color = Color.FromKnownColor(KnownColor.Gainsboro);

            _rectangle = e.ClipRectangle;
            if (cell == null) return;
            switch (cell.CellStatus)
            {
                case CellStatus.Completion:
                    _color = Color.RoyalBlue;
                    break;
                case CellStatus.Crippled:
                    _color = Color.RoyalBlue;
                    break;
                case CellStatus.Drowned:
                    _color = Color.Red;
                    break;
                case CellStatus.ValidLocation:
                    _color = Color.Green;
                    break;
                case CellStatus.NotValidLocation:
                    _color = Color.Orange;
                    break;
                case CellStatus.Empty:
                    break;
                case CellStatus.Miss:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //cell.BackColor = Color.Azure;
            e.Graphics.Clear(_color);

            if ((cell.CellStatus == CellStatus.Crippled) ||
                (cell.CellStatus == CellStatus.Drowned)) PaintCross(e);

            if (cell.CellStatus == CellStatus.Miss) PaintPoint(e);
        }     
    
    }
}
