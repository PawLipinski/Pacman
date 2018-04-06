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


        //public void PrintTheHero()
        //{
        //    Canvas.SetTop(this.FieldShape, this.YCoord + 1.5);
        //    Canvas.SetLeft(this.FieldShape, this.XCoord + 1.5);
        //    gameBoard.gameCanvas.Children.Add(this.FieldShape);
        //}

        //public void MoveTheHero(int x, int y)
        //{
        //    if (!CheckCollision(x, y))
        //    {
        //        gameBoard.gameCanvas.Children.Remove(this.FieldShape);
        //        this.XCoord = x;
        //        this.YCoord = y;
        //        this.standsStill = false;
        //        PrintTheHero();
        //    }
        //    else
        //    {
        //        this.standCounter++;
        //    }

        //    if (standCounter >= 2)
        //    {
        //        standsStill = true;
        //        standCounter = 0;
        //    }
        //}

        //private bool CheckCollision(int x, int y)
        //{
        //    bool isCollision = false;

        //    foreach (var item in gameBoard.Walls)
        //    {
        //        if (((x + Field.Module > item.XCoord) && (x < item.XCoord + Field.Module)) && ((y + Field.Module > item.YCoord) && (y < item.YCoord + Field.Module)))
        //        {
        //            isCollision = true;
        //        }
        //    }

        //    return isCollision;
        //}

        //public void MoveTheHeroRelative(int x, int y)
        //{
        //    if (currentKey == Key.None)
        //    {
        //        return;
        //    }

        //    int efX = this.XCoord + x;
        //    int efY = this.YCoord + y;

        //    this.MoveTheHero(efX, efY);
        //}

        //public void ChangeDirection(Direction dir)
        //{
        //    if (!CheckCollision(this.XCoord + dir.x, this.YCoord + dir.y))
        //    {
        //        this.currentKey = demandKey;
        //        demandKey = Key.None;
        //        this.pacmanDirection = dir;
        //    }
        //}

        //public void DemandChangeDirection(Key direction)
        //{
        //    if ((currentKey == Key.None) || (standsStill))
        //    {
        //        CurrentKey = direction;
        //        return;
        //    }

        //    int tolerance = 8;

        //    int projectedX = this.XCoord;
        //    int projectedY = this.YCoord;

        //    bool canBeChanged = false;

        //    for (int i = 0; i < tolerance; i++)
        //    {
        //        projectedX += pacmanDirection.x;
        //        projectedY += pacmanDirection.y;

        //        try
        //        {
        //            int projectedReroutedX = projectedX + this.directions[direction].x;
        //            int projectedReroutedY = projectedY + this.directions[direction].y;

        //            if (!CheckCollision(projectedReroutedX, projectedReroutedY))
        //            {
        //                canBeChanged = true;
        //            }
        //        }
        //        catch { }

        //    }

        //    if (canBeChanged)
        //    {
        //        this.demandKey = direction;
        //    }
        //}

        public override void Move()
        {
            try
            {
                ChangeDirection(directions[demandKey]);
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
