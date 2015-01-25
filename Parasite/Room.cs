using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasite
{
    /// <summary>
    /// Core of the game, serves as a data storage.
    /// </summary>
    class Room
    {
        public int Index { get; private set; } // Used to jump to other Rooms

        public int NorthRoom { get; private set; }

        public int EastRoom { get; private set; }

        public int SouthRoom { get; private set; }

        public int WestRoom { get; private set; }

        public bool HasTrap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="north"></param>
        /// <param name="east"></param>
        /// <param name="south"></param>
        /// <param name="west"></param>
        public Room(int index, int north, int east, int south, int west)
        {
            Index = index;
            NorthRoom = north;
            EastRoom = east;
            SouthRoom = south;
            WestRoom = west;
        }
    }
}
