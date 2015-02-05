using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasite
{
    /// <summary>
    /// Holds game state. Lays out the map and generates procedural content and keeps 
    /// track of rooms and their connections.
    /// </summary>
    class Map
    {
        private Random _random;

        public delegate void OnPlayerMoved(int currentRoom, int roomLocation);

        private readonly Room[] _rooms;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public int FloodRoom { get; set; }

        public int WeaponRoom { get; set; }

        public int PlayerRoomIndex { get; private set; }

        public Room PlayerRoom
        {
            get
            {
                return _rooms[PlayerRoomIndex];
            }
        }

        public Room this[int index]
        {
            get
            {
                return _rooms[index];
            }
        }

        public Map(int seed)
        {
            // Generate random based on seed
            _random = new Random(seed);

            // Create empty rooms
            Rows = 10;
            Columns = 10;

            var lastRow = (Rows - 1) * Columns;

            // Creates array of rooms
            _rooms = new Room[Rows * Columns];

            // Generates the index for the other rooms
            for (var i = 0; i < _rooms.Length; i++)
            {
                // No out of bounds
                var north = i < Columns ? -1 : i - Columns;
                var east = ((i + 1) % Columns) == 0 ? -1 : i + 1;
                var south = i >= lastRow ? -1 : i + Columns;
                var west = (i % Columns) == 0 ? -1 : i + 1;
                var room = new Room(i, north, east, south, west);
                _rooms[i] = room;
            }

            // Allocate random locations for traps
            var trapCount = Rows;

            for (var t = 0; t < trapCount; t++)
            {
                var i = _random.Next(_rooms.Length);
                _rooms[i].HasTrap = true;
            }

            PlaceWeapon();

            // Randomly place the monster in a room without traps on
            var floodRooms = _rooms.Where(e => !e.HasTrap && e.Index != WeaponRoom).ToArray();
            FloodRoom = floodRooms[_random.Next(floodRooms.Length)].Index;

            // Randomly place the monster in a room without traps or flood on
            var startRooms = _rooms.Where(e => !e.HasTrap && e.Index != WeaponRoom && e.Index != FloodRoom).ToArray();
            PlayerRoomIndex = startRooms[_random.Next(startRooms.Length)].Index;
        }

        // Randomly place a weapon in a room without traps or flood on
        public void PlaceWeapon()
        {
            var traplessRoom = _rooms.Where(e => !e.HasTrap && e.Index != FloodRoom && e.Index != PlayerRoomIndex).ToArray();
            WeaponRoom = traplessRoom[_random.Next(traplessRoom.Length)].Index;
        }

        /// <summary>
        /// Map the player movement across the map using the rooms and set designated room.
        /// </summary>
        /// 
        public void MovePlayerNorth(OnPlayerMoved callback)
        {
            var room = PlayerRoom;
            if (room.NorthRoom != -1)
            {
                callback(PlayerRoomIndex, room.NorthRoom);
                PlayerRoomIndex = room.NorthRoom;
            }
        }

        /// <summary>
        /// Map the player movement across the map using the rooms and set designated room.
        /// </summary>
        /// 
        public void MovePlayerEast(OnPlayerMoved callback)
        {
            var room = PlayerRoom;
            if (room.EastRoom != -1)
            {
                callback(PlayerRoomIndex, room.EastRoom);
                PlayerRoomIndex = room.EastRoom;
            }
        }

        /// <summary>
        /// Map the player movement across the map using the rooms and set designated room.
        /// </summary>
        /// 
        public void MovePlayerSouth(OnPlayerMoved callback)
        {
            var room = PlayerRoom;
            if (room.SouthRoom != -1)
            {
                callback(PlayerRoomIndex, room.SouthRoom);
                PlayerRoomIndex = room.SouthRoom;
            }
        }

        /// <summary>
        /// Map the player movement across the map using the rooms and set designated room.
        /// </summary>
        /// 
        public void MovePlayerWest(OnPlayerMoved callback)
        {
            var room = PlayerRoom;
            if (room.WestRoom != -1)
            {
                callback(PlayerRoomIndex, room.WestRoom);
                PlayerRoomIndex = room.WestRoom;
            }
        }
    }
}
