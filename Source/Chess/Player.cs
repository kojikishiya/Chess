using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Player
    {
        private string playerName = string.Empty;

        public string PlayerName
        {
            get { return playerName; }
        }

        public Player(string playerName)
        {
            this.playerName = playerName;
        }
    }
}
