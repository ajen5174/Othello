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
        public Stone[,] Spaces { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">the empty space that needs to be checked</param>
        /// <param name="x">the starting x position</param>
        /// <param name="y">the starting y position</param>
        /// <param name="dirX">represents the x-direction to test</param>
        /// <param name="dirY">represents the y-direction to test</param>
        /// <returns></returns>
        private bool CheckNeighborInDirection(Stone s, int x, int y, int dirX, int dirY)
        {
            Stone neighbor;// = Spaces[dirX, dirY];
            int testX = x + dirX;
            int testY = y + dirY;
            //start with left neighbor
            bool hasPassedOppositeColor = false;


            //need to change this, right now it compares the color of the blank space, which is incorrect
            //also need to add a check for swapping after hitting the edge of the board.
            while (testX >= 0 && testX < Spaces.GetLength(0) && testY >= 0 && testY < Spaces.GetLength(1))
            {
                neighbor = Spaces[testX, testY];
                if (!neighbor.IsActive)//if our neighbor is not active, then we have hit a blank space
                {
                    if (hasPassedOppositeColor)//if we have passed the opposite color, then we know this is a valid space
                    {
                        return true;
                    }
                    else //if we haven't passed the opposite color, we know that this neighbor doesn't make our space valid, so we skip to the next
                    {
                        break;
                    }
                }
                else //if our neighbor is not active
                {
                    if (neighbor.Color == s.Color)//if our neighbor matches our color we need to know if we've passed the opposite color yet
                    {
                        if (hasPassedOppositeColor)//if we have, then this is a valid space
                        {
                            return true;
                        }
                        else//if we haven't, then we know this isn't a valid space and we move to the next neighbor
                        {
                            break;
                        }
                    }
                }
                testX += dirX;//
                testY += dirY;//
            }


            throw new NotImplementedException();
        }

        private bool CheckNeighbors(Stone s, int x, int y)
        {
            

            throw new NotImplementedException();
        }

        public Stone[] ValidSpaces()
        {
            // logic
            List<Stone> list = new List<Stone>();
            for(int i = 0; i < Spaces.GetLength(0); i++)
            {
                for(int j = 0; j < Spaces.GetLength(1); j++)
                {
                    Stone s = Spaces[i, j];
                    if (!s.IsActive)//test for if it's active or not
                    {
                        //if any of the neighbors are the opposite color, then we can place there
                        if(CheckNeighbors(s, i, j))
                        {
                            list.Add(s);
                        }
                    }
                }
               
            }

            return list.ToArray();
        }

        public int ValidSpaceCount()
        {
            return ValidSpaces().Length;
        }
    }
}
