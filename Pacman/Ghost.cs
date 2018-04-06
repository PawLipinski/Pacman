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
    class Ghost : Creature
    {
        Color ghostColor;
        Random randomizer;

        public Ghost(Board gameBoard, int x, int y, Color c)
            : base(gameBoard)
        {
            this.XCoord = x * Field.Module;
            this.YCoord = y * Field.Module;

            this.ghostColor = c;

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

        public void Move(int randomNumber, bool shouldMove)
        {

            if (shouldMove)
            {
                this.DemandChangeDirection(directions.Keys.ElementAt(randomNumber));
            }
            try
            {
                ChangeDirection(directions[demandKey]);
            }
            catch (Exception){}

            MoveTheHeroRelative(creatureDirection.x, creatureDirection.y);
        }
    }
}
