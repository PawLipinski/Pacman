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
    class Ghost : Creature
    {
        Color ghostColor;

        public Field destination;

        public Ghost(Board gameBoard, int x, int y, Color c)
            : base(gameBoard)
        {
            this.XCoord = x * Field.Module;
            this.YCoord = y * Field.Module;

            this.ghostColor = c;

            this.destination = null;

            SolidColorBrush myBrush = new SolidColorBrush();
            myBrush.Color = this.ghostColor;
            foreach (Shape item in this.FieldShape.Children)
            {
                item.Fill = myBrush;
            }
        }

        public override void ApplyShape()
        {
            Ellipse head = new Ellipse();
            Rectangle body = new Rectangle();
            SolidColorBrush myBrush = new SolidColorBrush();
            myBrush.Color = this.ghostColor;

            head.Fill = body.Fill = myBrush;

            head.Height = 10;
            head.Width = 15;
            Canvas.SetTop(head, 3);
            Canvas.SetLeft(head, 2.5);

            body.Height = 9;
            body.Width = 15;
            Canvas.SetTop(body, 8);
            Canvas.SetLeft(body, 2.5);

            this.FieldShape.Children.Add(head);
            this.FieldShape.Children.Add(body);

        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        public void ChangeDirection(Random randomizer)
        {
            if (gameBoard.CheckPositionClean(this.XCoord, this.YCoord))
            {
                List<Key> availableDirs = gameBoard.FieldAvailableDirections(this.XCoord, this.YCoord);

                bool shouldTakeRandomPath = true;
                //if (this.destination == null)
                //{
                shouldTakeRandomPath = GoForPacman();
                //}
                if (destination == null)
                {
                    int range = availableDirs.Count();

                    var changeKey = availableDirs.ElementAt(randomizer.Next(range));

                    if (currentKey == Key.None)
                    {
                        this.currentKey = changeKey;
                        this.creatureDirection = directions[currentKey];
                    }
                    if ((changeKey != oposites[currentKey]) || (standsStill == true))
                    {
                        this.currentKey = changeKey;
                        this.creatureDirection = directions[currentKey];
                    }
                }
            }
        }

        public void Move(Random randomizer)
        {
            try
            {
                ChangeDirection(randomizer);
            }
            catch (Exception) { }

            TeleportMe();
            MoveTheHeroRelative(creatureDirection.x, creatureDirection.y);
        }

        public bool GoForPacman()
        {
            Pac prey = this.gameBoard.Pacman;

            Field destin = null;
            var result = this.gameBoard.IsAnotherFieldVisible(prey, this, ref destin);
            this.destination = destin;
            if (destination != null)
            {
                if (result != Key.None)
                {
                    this.currentKey = result;
                    this.creatureDirection = directions[currentKey];
                    return false;
                }
            }
            else
            {
                if ((this.XCoord == destination.XCoord) && (this.YCoord == destination.YCoord))
                {
                    this.destination = null;
                    return true;
                }
                else if (this.standsStill)
                {
                    this.destination = null;
                }
                return true;
            }

            this.destination = null;
            return true;
        }

        //public bool GoToDestination(List<Key> availableDirs)
        //{
        //    if ((this.XCoord == this.destination.XCoord) && (this.YCoord == this.destination.YCoord))
        //    {
        //        this.destination = null;
        //        return true;
        //    }
        //    else
        //    {
        //        List<Key> interestingDirections = new List<Key>();

        //        bool result = true;
        //        if (this.XCoord > this.destination.XCoord)
        //        {
        //            if (availableDirs.Contains(Key.Left))
        //            {
        //                this.currentKey = Key.Left;
        //                this.creatureDirection = directions[currentKey];
        //                result = false;
        //            }
        //        }
        //        else if (this.XCoord < this.destination.XCoord)
        //        {
        //            if (availableDirs.Contains(Key.Right))
        //            {
        //                this.currentKey = Key.Right;
        //                this.creatureDirection = directions[currentKey];
        //                result = false;
        //            }
        //        }
        //        else if (this.YCoord < this.destination.YCoord)
        //        {
        //            if (availableDirs.Contains(Key.Down))
        //            {
        //                this.currentKey = Key.Down;
        //                this.creatureDirection = directions[currentKey];
        //                result = false;
        //            }
        //        }
        //        else if (this.YCoord > this.destination.YCoord)
        //        {
        //            if (availableDirs.Contains(Key.Up))
        //            {
        //                this.currentKey = Key.Up;
        //                this.creatureDirection = directions[currentKey];
        //                result = false;
        //            }
        //        }
        //        return result;
        //    }
        //}
    }
}
