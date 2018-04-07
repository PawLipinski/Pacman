using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman
{
    class Pac : Creature
    {

        public Pac(Board gameBoard)
            : base(gameBoard)
        {

            this.standsStill = false;
            this.standCounter = 0;

            this.gameBoard = gameBoard;

            this.YCoord = 16 * Field.Module;
            this.XCoord = 5 * Field.Module;
            this.demandKey = Key.None;
            this.currentKey = Key.None;

            this.creatureDirection = directions[Key.Right];
        }

        public override void ApplyShape() 
        {
            Ellipse shapeOfTheHero = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            shapeOfTheHero.Fill = mySolidColorBrush;
            shapeOfTheHero.StrokeThickness = 0;
            shapeOfTheHero.Stroke = Brushes.Black;
            shapeOfTheHero.Width = PacSizeModule;
            shapeOfTheHero.Height = PacSizeModule;

            this.FieldShape.Children.Add(shapeOfTheHero);
        }

        public override void Move()
        {
            try
            {
                ChangeDirection(demandKey);
            }
            catch (Exception)
            {
            }

            Eat();
            TeleportMe();
            MoveTheHeroRelative(creatureDirection.x, creatureDirection.y);
        }

        private void Eat()
        {
            //Dot dotToRemove = gameBoard.BoardFood.Dots.Find(item => (item.XCoord == this.XCoord * Field.Module) && (item.YCoord == this.YCoord * Field.Module));
            Dot dotToRemove = gameBoard.BoardFood.Dots.Find(item => (item.XCoord == this.XCoord) && (item.YCoord == this.YCoord));

            if (dotToRemove != null)
            {
                gameBoard.BoardFood.RemoveTheDot(dotToRemove);
            }
        }

        private void TeleportMe()
        {
            if (this.XCoord < -Field.Module)
            {
                this.XCoord = 19 * Field.Module;
                return;
            }
            else if (this.XCoord >= 19 * Field.Module)
            {
                this.XCoord = -Field.Module;
                return;
            }
        }

        //public Key CurrentKey
        //{
        //    get { return this.currentKey; }
        //    set
        //    {
        //        this.currentKey = value;
        //        try
        //        {
        //            this.pacmanDirection = directions[currentKey];
        //        }
        //        catch { }
        //    }
        //}
    }
}
