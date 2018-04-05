using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Food
    {
        public Board gameBoard;

        public List<Dot> dots;

        public Food()
        {
            this.dots = new List<Dot>();
        }

        public void GenerateFood()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    var isThere = gameBoard.Walls.First(item => (item.XCoord == j*Field.Module) && (item.YCoord == i*Field.Module));

                    if (isThere==null)
                    {

                    }
                }

            }
        }
    }

    class Dot : Field
    {

    }
}
