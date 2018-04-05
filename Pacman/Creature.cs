using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    abstract class Creature : Field
    {
        public Creature(Board board, int x, int y) 
            : base(board, x, y)
        {
            ApplyShape();
        }

        public abstract void ApplyShape();
    }
}
