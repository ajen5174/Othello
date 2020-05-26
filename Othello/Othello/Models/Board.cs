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
        private bool CheckNeighborInDirection(Stone s, int x, int y, int dirX, int dirY, bool colorToPlace) //either this needs to be passed the color we are trying to place, or we check if the space is valid at all
        {
            Stone neighbor;// = Spaces[dirX, dirY];
            Stone initialNeighbor;
            int testX = x + dirY;
            int testY = y + dirX;
            //start with left neighbor
            bool hasPassedOppositeColor = false;
            initialNeighbor = Spaces[testX, testY];

            //need to change this, right now it compares the color of the blank space, which is incorrect
            //also need to add a check for swapping after hitting the edge of the board.
            while (testX >= 0 && testX < Spaces.GetLength(0) && testY >= 0 && testY < Spaces.GetLength(1))
            {
                testX += dirY;//
                testY += dirX;//
                neighbor = Spaces[testX, testY];
                if(initialNeighbor.IsActive && neighbor.IsActive && initialNeighbor.Color != neighbor.Color)//this probably needs to be changed to check against color to place
                {
                    hasPassedOppositeColor = true;
                }
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
                else //if our neighbor is active
                {
                    if (neighbor.Color == colorToPlace)//if our neighbor matches our color we need to know if we've passed the opposite color yet
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
                
            }

            return false;


        }

        public bool CheckStoneIsValid(Stone s, int x, int y, bool color)
        {
            //the idea behind this method is to take the initial space to be tested and call CheckNeighborsInDirection 8 times from that point

            return
            CheckNeighborInDirection(s, x, y, 1, 0, color) || //right
            CheckNeighborInDirection(s, x, y, 1, 1, color) || //top right
            CheckNeighborInDirection(s, x, y, 1, -1, color) || //bottom right

            CheckNeighborInDirection(s, x, y, 0, 1, color) || //up
            CheckNeighborInDirection(s, x, y, 0, -1, color) || //down

            CheckNeighborInDirection(s, x, y, -1, 0, color) || //left
            CheckNeighborInDirection(s, x, y, -1, 1, color) || //top left
            CheckNeighborInDirection(s, x, y, -1, -1, color);//bottom left



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
    }
}
