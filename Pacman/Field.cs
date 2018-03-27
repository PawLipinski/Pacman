using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pacman
{
    abstract class Field
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public static int Module = 20;

        protected Canvas fieldShape;

        public Field()
        {
            fieldShape = new Canvas();
            fieldShape.Width = Field.Module;
            fieldShape.Height = Field.Module;
        }

        public Canvas FieldShape { get { return this.fieldShape;} }
    }
}
