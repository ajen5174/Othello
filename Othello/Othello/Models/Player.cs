using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    class Player
    {
        public bool PlayerNumber { get; }
        public int OwnedStones { get; set; } = 0;

        public Player(bool playerNumber)
        {
            PlayerNumber = playerNumber;
        }
    }
}
