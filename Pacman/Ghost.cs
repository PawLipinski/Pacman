using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Ghost : Creature
    {
        public Ghost(Board gameBoard, int x, int y) 
            : base(gameBoard, x , y)
        {
        }

        public override void ApplyShape()
        {
            throw new NotImplementedException();
        }
    }
}
