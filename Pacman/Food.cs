using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman
{
    class Food
    {
        public Board gameBoard;

        public List<Dot> dots;

        public Food(Board gameBoard)
        {
            this.gameBoard = gameBoard;
            this.dots = new List<Dot>();
        }

        public void GenerateFood()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    var isThere = gameBoard.Walls.Find(item => (item.XCoord == j*Field.Module) && (item.YCoord == i*Field.Module));

                    if (isThere==null)
                    {
                        dots.Add(new Dot(gameBoard, j, i));
                    }
                }

            }
        }

        public void PrintTheDots()
        {
            foreach (var item in dots)
            {
                item.PrintDot();
            }
        }

        public void RemoveTheDot(Dot dot)
        {
            dot.GetsEaten();
            this.dots.Remove(dot);
        }

        public List<Dot> Dots { get { return this.dots; } }
    }

    class Dot : Field
    {
        private Ellipse dotShape;
        private Board gameBoard;

        public Dot(Board gameBoard, int x, int y)
            : base()
        {
            this.XCoord = x*Field.Module;
            this.YCoord = y*Field.Module;
            this.gameBoard = gameBoard;

            this.dotShape = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            this.dotShape.Fill = mySolidColorBrush;
            this.dotShape.StrokeThickness = 0;
            this.dotShape.Stroke = Brushes.Black;

            this.dotShape.Width = 2;
            this.dotShape.Height = 2;
        }

        public void PrintDot()
        {
            Canvas.SetTop(dotShape, this.YCoord + Field.Module/2);
            Canvas.SetLeft(dotShape, this.XCoord + Field.Module/2);
            this.gameBoard.gameCanvas.Children.Add(dotShape);
        }

        public void GetsEaten()
        {
            this.gameBoard.gameCanvas.Children.Remove(this.dotShape);
        }
    }
}
