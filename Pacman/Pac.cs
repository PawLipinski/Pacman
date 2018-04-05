﻿using System;
using System.Collections.Generic;
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
                x = p1;
                y = p2;
            }
        }

        private Direction pacmanDirection;
        private Ellipse shapeOfTheHero;
        private int PacSizeModule = Field.Module - 3;
        private Board gameBoard;
        private Key demandKey;
        private Key currentKey;
        private bool standsStill;
        private int standCounter;
        private Dictionary<Key, Direction> directions;

        public Pac(Board gameBoard)
            : base()
        {
            shapeOfTheHero = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            shapeOfTheHero.Fill = mySolidColorBrush;
            shapeOfTheHero.StrokeThickness = 0;
            shapeOfTheHero.Stroke = Brushes.Black;
            this.standsStill = false;
            this.standCounter = 0;

            shapeOfTheHero.Width = PacSizeModule;
            shapeOfTheHero.Height = PacSizeModule;

            this.gameBoard = gameBoard;

            this.YCoord = 16 * Field.Module;
            this.XCoord = 5 * Field.Module;
            this.demandKey = Key.None;
            this.currentKey = Key.None;

            this.directions = new Dictionary<Key, Direction>();
            directions.Add(Key.Left, new Direction(-5, 0));
            directions.Add(Key.Right, new Direction(5, 0));
            directions.Add(Key.Up, new Direction(0, -5));
            directions.Add(Key.Down, new Direction(0, 5));

            this.pacmanDirection = directions[Key.Right];
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
            if (!CheckCollision(this.XCoord + dir.x, this.YCoord + dir.y))
            {
                this.currentKey = demandKey;
                demandKey = Key.None;
                this.pacmanDirection = dir;
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
                projectedX += pacmanDirection.x;
                projectedY += pacmanDirection.y;

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

        public void Move()
        {
            try
            {
                ChangeDirection(directions[demandKey]);
            }
            catch (Exception)
            {
            }

            MoveTheHeroRelative(pacmanDirection.x, pacmanDirection.y);
        }

        public Key CurrentKey
        {
            get { return this.currentKey; }
            set
            {
                this.currentKey = value;
                try
                {
                    this.pacmanDirection = directions[currentKey];
                }
                catch { }
            }
        }
    }
}
