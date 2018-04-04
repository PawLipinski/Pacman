using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman
{
    class Pac : Field
    {
        public struct Direction
        {
            public int x;
            public int y;

            public Direction(int p1, int p2)
            {
                x=p1;
                y=p1;
            }
        }

        private Direction pacmanDirection;
        private Ellipse shapeOfTheHero;
        private int PacSizeModule = Field.Module - 3;
        private Board gameBoard;
        private Key demandKey;
        private Key currentKey;
        private Dictionary<Key,Direction> directions;

        public Pac(Board gameBoard)
            : base()
        {
            shapeOfTheHero = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            shapeOfTheHero.Fill = mySolidColorBrush;
            shapeOfTheHero.StrokeThickness = 0;
            shapeOfTheHero.Stroke = Brushes.Black;

            shapeOfTheHero.Width = PacSizeModule;
            shapeOfTheHero.Height = PacSizeModule;

            this.gameBoard = gameBoard;

            this.YCoord = 16 * Field.Module;
            this.XCoord = 5 * Field.Module;
            this.demandKey = Key.None;
            this.currentKey = Key.None;

            this.directions = new Dictionary<Key,Direction>();
            directions.Add(Key.Left,new Direction(-5,0));
            directions.Add(Key.Right,new Direction( 5,0));
            directions.Add(Key.Up,new Direction(0,-5));
            directions.Add(Key.Down,new Direction(0,5));
        }


        public void PrintTheHero()
        {
            Canvas.SetTop(shapeOfTheHero, this.YCoord + 1.5);
            Canvas.SetLeft(shapeOfTheHero, this.XCoord + 1.5);
            gameBoard.gameCanvas.Children.Add(shapeOfTheHero);
        }

        public void MoveTheHero(int x, int y)
        {
            if (!CheckCollision(x, y))
            {
                gameBoard.gameCanvas.Children.Remove(shapeOfTheHero);
                this.XCoord = x;
                this.YCoord = y;
                PrintTheHero();
            }
        }

        private bool CheckCollision(int x, int y)
        {
            bool isCollision = false;

            foreach (var item in gameBoard.Walls)
            {
                if (((x + Field.Module > item.XCoord) && (x < item.XCoord + Field.Module)) && ((y + Field.Module > item.YCoord) && (y < item.YCoord + Field.Module)))
                {
                    isCollision = true;
                }
            }

            return isCollision;
        }

        public void MoveTheHeroRelative(int x, int y)
        {
            int efX = this.XCoord + x;
            int efY = this.YCoord + y;

            this.MoveTheHero(efX, efY);
        }

        public void ChangeDirection(Direction dir)
        {
            if (!CheckCollision(this.XCoord + dir.x, this.YCoord + dir.y))
            {
                this.currentKey = demandKey;
                demandKey = Key.None;
                this.pacmanDirection = dir;
            }
        }

        public void DemandChangeDirection(Key direction)
        {
            if (currentKey == Key.None)
            {
                currentKey = direction;
            }
            int tolerance = 1;

            int projectedX = this.XCoord;
            int projectedY = this.YCoord;

            bool canBeChanged = false;

            for (int i = 0; i < tolerance; i++)
            {
                projectedX += pacmanDirection.x;
                projectedY += pacmanDirection.y;

                if (!CheckCollision(projectedX, projectedY))
                {
                    canBeChanged = true;
                }
            }

            if (canBeChanged)
            {
                this.demandKey = direction;
            }
        }

        public void Move()
        {
            //if ((currentKey != demandKey) && (demandKey != Key.None))
            //{
            try
            {
                ChangeDirection(directions[demandKey]);
            }
            catch (Exception)
            {
            }
            //}
            MoveTheHeroRelative(pacmanDirection.x, pacmanDirection.y);
        }
    }
}
