using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman
{
    class Pac : Field
    {
        private Ellipse shapeOfTheHero;

        public Pac() : base()
        {
            shapeOfTheHero = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            shapeOfTheHero.Fill = mySolidColorBrush;
            shapeOfTheHero.StrokeThickness = 2;
            shapeOfTheHero.Stroke = Brushes.Black;
        }
    }
}
