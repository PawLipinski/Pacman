using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pacman
{
    class GhostManager
    {
        Board gameBoard;
        private List<Ghost> listOfGhosts;
        private Random randomizer;

        public GhostManager(Board gameBoard)
        {
            this.gameBoard = gameBoard;
            listOfGhosts = new List<Ghost>();
            Arrange();
            PrintThemAll();
            this.randomizer = new Random();
        }

        public void Arrange()
        {
            listOfGhosts.Add(new Ghost(this.gameBoard, 8, 10, Color.FromRgb(255, 0, 0)));
            listOfGhosts.Add(new Ghost(this.gameBoard, 9, 10, Color.FromRgb(0, 255, 255)));
            listOfGhosts.Add(new Ghost(this.gameBoard, 10, 10, Color.FromRgb(255, 0, 255)));

            Field destinationField = new Field();
            destinationField.XCoord = 20;
            destinationField.YCoord = 20;

            //listOfGhosts.ElementAt(0).destination = destinationField;
        }

        public void PrintThemAll()
        {
            foreach (Ghost item in listOfGhosts)
            {
                item.PrintTheHero();
            }
        }

        public void MoveThemAll()
        {
            foreach (Ghost item in listOfGhosts)
            {
                //shouldMove = randomizer.Next(0, 100) > 90;
                item.Move(randomizer);
            }
        }

        public List<Ghost> ListOfGhosts { get { return this.listOfGhosts; } }
    }
}
