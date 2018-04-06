using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pacman
{
    abstract class Creature : Field
    {
        public struct Direction
        {
            public int x;
            public int y;

            public Direction(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }

        protected Direction creatureDirection
            ;
        protected int PacSizeModule = Field.Module - 3;
        protected Board gameBoard;
        protected Key demandKey;
        protected Key currentKey;
        protected bool standsStill;
        protected int standCounter;
        protected Dictionary<Key, Direction> directions;

        public Creature(Board board)
            : base()
        {
            this.gameBoard = board;
            ApplyShape();

            this.directions = new Dictionary<Key, Direction>();
            directions.Add(Key.Left, new Direction(-5, 0));
            directions.Add(Key.Right, new Direction(5, 0));
            directions.Add(Key.Up, new Direction(0, -5));
            directions.Add(Key.Down, new Direction(0, 5));
        }

        public abstract void ApplyShape();

        public void PrintTheHero()
        {
            Canvas.SetTop(this.FieldShape, this.YCoord + 1.5);
            Canvas.SetLeft(this.FieldShape, this.XCoord + 1.5);
            gameBoard.gameCanvas.Children.Add(this.FieldShape);
        }

        public void MoveTheHero(int x, int y)
        {
            if (!CheckCollision(x, y))
            {
                gameBoard.gameCanvas.Children.Remove(this.FieldShape);
                this.XCoord = x;
                this.YCoord = y;
                this.standsStill = false;
                PrintTheHero();
            }
            else
            {
                this.standCounter++;
            }

            if (standCounter >= 2)
            {
                standsStill = true;
                standCounter = 0;
            }
        }

        public bool CheckCollision(int x, int y)
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

            //return this.gameBoard.CheckField(x,y);
        }

        public void MoveTheHeroRelative(int x, int y)
        {
            if (currentKey == Key.None)
            {
                return;
            }

            int efX = this.XCoord + x;
            int efY = this.YCoord + y;

            this.MoveTheHero(efX, efY);
        }

        public void ChangeDirection(Direction dir)
        {
            if (gameBoard.CheckPositioinClean(this.XCoord, this.YCoord))
            {
                if (!CheckCollision(this.XCoord + dir.x, this.YCoord + dir.y))
                {
                    this.currentKey = demandKey;
                    demandKey = Key.None;
                    this.creatureDirection = dir;
                }
            }
        }

        public void DemandChangeDirection(Key direction)
        {
            if ((currentKey == Key.None) || (standsStill))
            {
                CurrentKey = direction;
                return;
            }

            int tolerance = 8;

            int projectedX = this.XCoord;
            int projectedY = this.YCoord;

            bool canBeChanged = false;

            for (int i = 0; i < tolerance; i++)
            {
                projectedX += creatureDirection.x;
                projectedY += creatureDirection.y;

                try
                {
                    int projectedReroutedX = projectedX + this.directions[direction].x;
                    int projectedReroutedY = projectedY + this.directions[direction].y;

                    if (!CheckCollision(projectedReroutedX, projectedReroutedY))
                    {
                        canBeChanged = true;
                    }
                }
                catch { }

            }

            if (canBeChanged)
            {
                this.demandKey = direction;
            }
        }

        public abstract void Move();

        public Key CurrentKey
        {
            get { return this.currentKey; }
            set
            {
                this.currentKey = value;
                try
                {
                    this.creatureDirection = directions[currentKey];
                }
                catch { }
            }
        }

    }
}
