using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Context
    {
        Dictionary<PlayerNo, Player> players = new Dictionary<PlayerNo, Player>();

        internal Dictionary<PlayerNo, Player> Players
        {
            get { return players; }
            set { players = value; }
        }
        ImageSize size = ImageSize.FortyFive;

        internal ImageSize Size
        {
            get { return size; }
            set { size = value; }
        }
        public Context()
        {
            players.Add(PlayerNo.One, new Player("player1"));
            players.Add(PlayerNo.Two, new Player("player2"));

        }
    }
}
