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

        public Board()
        {
            Spaces = new Stone[8, 8];   
            for(int i = 0; i < Spaces.GetLength(0); i++)
            {
                for(int j = 0; j < Spaces.GetLength(1); j++)
                {
                    Spaces[i, j] = new Stone();
                    Spaces[i, j].x = i;
                    Spaces[i, j].y = j;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">the empty space that needs to be checked</param>
        /// <param name="x">the starting x position</param>
        /// <param name="y">the starting y position</param>
        /// <param name="dirX">represents the x-direction to test</param>
        /// <param name="dirY">represents the y-direction to test</param>
        /// <returns></returns>
        private bool CheckNeighborInDirection(Stone s, int x, int y, int dirX, int dirY, bool colorToPlace) 
        {
            Stone neighbor;// = Spaces[dirX, dirY];
            Stone initialNeighbor;
            int testX = x + dirX;
            int testY = y + dirY;
            if (testX < 0 || testX >= Spaces.GetLength(0) || testY < 0 || testY >= Spaces.GetLength(1))//if we have hit an edge before looping, then we know this direction won't make the space valid
            {
                return false;
            }
            //start with left neighbor
            bool hasPassedOppositeColor = false;
            initialNeighbor = Spaces[testX, testY];
            if(!initialNeighbor.IsActive)// if the first neighbor isn't active then we get out
            {
                return false;
            }

            if(initialNeighbor.Color == colorToPlace)//if our first neighbor matches the color we want, we get out
            {
                return false;
            }
            //also need to add a check for swapping after hitting the edge of the board.
            while (testX >= 0 && testX < Spaces.GetLength(0) && testY >= 0 && testY < Spaces.GetLength(1))
            {
                
                neighbor = Spaces[testX, testY];
                testX += dirX;//
                testY += dirY;//
                if(neighbor.IsActive)
                {
                    if (testX < 0 || testX >= Spaces.GetLength(0) || testY < 0 || testY >= Spaces.GetLength(1))//if we have hit an edge, we check to see if the color has stayed the same
                    {
                        if (neighbor.Color != colorToPlace)//if the color is still opposite, then this is valid
                        {
                            hasPassedOppositeColor = true;
                            break;
                        }
                    }
                    else
                    {
                        //if our neighbor is active, neighbor's color matches the color we want to place, AND neighbor's color is opposite to the first space we checked then this is valid
                        if ((initialNeighbor.Color != neighbor.Color && neighbor.Color == colorToPlace))
                        {
                            hasPassedOppositeColor = true;
                            break;
                        }
                    }
                }
                else//if our neighbor is not active, we are done
                {
                    break;
                }
                

                
                
            }

            return hasPassedOppositeColor;


        }

        public bool CheckStoneIsValid(Stone s, int x, int y, bool color)
        {
            //the idea behind this method is to take the initial space to be tested and call CheckNeighborsInDirection 8 times from that point

            return
            CheckNeighborInDirection(s, x, y, 1, 0, color) || //right
            CheckNeighborInDirection(s, x, y, 1, 1, color) || //bottom right
            CheckNeighborInDirection(s, x, y, 1, -1, color) || //top right

            CheckNeighborInDirection(s, x, y, 0, 1, color) || //up
            CheckNeighborInDirection(s, x, y, 0, -1, color) || //down

            CheckNeighborInDirection(s, x, y, -1, 0, color) || //left
            CheckNeighborInDirection(s, x, y, -1, 1, color) || //bottom left
            CheckNeighborInDirection(s, x, y, -1, -1, color);//top left



        }

        public Stone[] ValidSpaces(bool color)
        {
            //this probably needs to be passed the color we want to check for valid spaces, then we can just run the method twice when we want to check for conditions that work
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
                        if(CheckStoneIsValid(s, i, j, color))
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
            return ValidSpaces(true).Length + ValidSpaces(false).Length;//gonna be wrong
        }

        public bool WinCondition()
        {
            int whiteCount = 0;
            int blackCount = 0;
            bool colorWinner;

            for (int i = 0; i < Spaces.GetLength(0); i++)
            {
                for (int j = 0; j < Spaces.GetLength(1); j++)
                {
                    if (Spaces[i,j].Color == false)
                    {
                        blackCount++;
                    }
                    else
                    {
                        whiteCount++;
                    }
                }
            }

            colorWinner = whiteCount > blackCount;//true = white wins; false = black wins

            return colorWinner;
        }
    }
}
