using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace Othello.Models
{
    class Board
    {
        public Rectangle[,] Spaces { get; set; }

        public Rectangle[] ValidSpaces()
        {
            // logic

            return null;
        }

        public int ValidSpaceCount()
        {
            return ValidSpaces().Length;
        }
    }
}
