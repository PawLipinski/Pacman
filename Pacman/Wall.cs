using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman
{
    class Wall : Field
    {
        //public enum orientation { Horizontal, Vertical, LeftTopCorner, LeftBottomCorner, RightTopCorner, RightBottomCorner };

        public Wall()
            : base()
        {

            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(Colors.Gray);
            rect.Width = Field.Module;
            rect.Height = Field.Module;

            fieldShape.Children.Add(rect);
        }

        public Wall(int x, int y)
            : this()
        {
            this.XCoord = x*Field.Module;
            this.YCoord = y*Field.Module;
        }

        public Wall MirrorWall()
        {
            if (this.XCoord / 20 <= 9)
            {
                return new Wall(Board.XSize- this.XCoord/Field.Module - 1, this.YCoord/Field.Module);
            }
            return null;
        }

    }
}
