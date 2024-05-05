using arcardnoid.Models.Content.Components.Map.Models;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;

namespace arcardnoid.Models.Content.Components.Map
{
    public class MapGenerator
    {
        #region Private Properties

        private List<MapChunk> Chunks { get; set; }
        private FastRandom Random { get; set; }
        private MapChunk StartingChunk { get; set; }
        public MapHypotesis MapHypotesis { get; set; } = null;
        public MapChunk MapChunk { get; set; } = null;

        #endregion Private Properties

        #region Private Fields

        private const int MAP_HEIGHT = 16;
        private const int MAP_WIDTH = 29;
        private const int STARTING_CHUNK_X = 0;
        private const int STARTING_CHUNK_Y = 7;

        #endregion Private Fields

        #region Public Constructors

        public MapGenerator(int seed = 123456)
        {
            Chunks = new List<MapChunk>();
            Random = new FastRandom(seed);
        }

        #endregion Public Constructors

        #region Public Methods

        public void GenerateMap()
        {
            LoadChunks();
            List<MapHypotesis> mapHypotesisList = new List<MapHypotesis>();

            while (mapHypotesisList.Count < 15)
            {
                MapHypotesis hypotesis = RandomMap();
                if (hypotesis != null)
                {
                    mapHypotesisList.Add(hypotesis);
                }
            }
            MapHypotesis = mapHypotesisList.OrderByDescending(m => m.GetCoverage()).First().Accept();
        }

        #endregion Public Methods

        #region Private Methods

        private List<MapChunk> GetPossibleChunks(MapHypotesis mapHypotesis, List<MapChunkDoorType> opositeDoorTypes)
        {
            List<MapChunk> chunks = Chunks.Where(c => c.GetAllDoors().Any(d => opositeDoorTypes.Contains(d.DoorType))).ToList();
            if (mapHypotesis.GetCoverage() < 20)
            {
                return chunks.Where(c => c.Entrances.Count > 1).ToList();
            }
            return chunks;
        }

        private void LoadChunks()
        {
            Chunks = MapChunk.AllChunks();
            StartingChunk = MapChunk.StartingChunk();
        }

        private MapHypotesis RandomMap()
        {
            MapHypotesis mapHypotesis = new MapHypotesis(MAP_WIDTH, MAP_HEIGHT);
            mapHypotesis.AddStartingChunk(StartingChunk, STARTING_CHUNK_X, STARTING_CHUNK_Y);
            while (mapHypotesis.HasOpenedDoors())
            {
                MapChunkDoor openedDoor = mapHypotesis.GetFirstOpenedDoor();

                bool result = TryCloseDoor(mapHypotesis, openedDoor);
                if (!result)
                {
                    return null;
                }
            }
            return mapHypotesis;
        }

        private bool TryCloseDoor(MapHypotesis mapHypotesis, MapChunkDoor openedDoor)
        {
            List<MapChunkDoorType> opositeDoorTypes = openedDoor.GetOpositeDoorTypes().RandomList(Random);
            List<MapChunk> possibleChunks = GetPossibleChunks(mapHypotesis, opositeDoorTypes).RandomList(Random);

            foreach (MapChunk chunk in possibleChunks)
            {
                if (TryPlaceChunk(chunk, mapHypotesis, openedDoor))
                {
                    return true;
                }
            }
            return false;
        }

        private bool TryPlaceChunk(MapChunk chunk, MapHypotesis mapHypotesis, MapChunkDoor openedDoor)
        {
            List<MapChunkDoorType> basicChunkDoorTypes = (new List<MapChunkDoorType>() { MapChunkDoorType.Top, MapChunkDoorType.Right, MapChunkDoorType.Bottom, MapChunkDoorType.Left }).RandomList(Random);
            foreach (MapChunkDoorType doorType in basicChunkDoorTypes)
            {
                List<MapChunkEntrance> compatibleEntrances = chunk.GetCompatibleOpositeEntrances(doorType).RandomList(Random);
                if (compatibleEntrances.Count == 0) continue;
                foreach (MapChunkEntrance possibleEntrance in compatibleEntrances)
                {
                    Point possibleChunkPosition = openedDoor.GetOpositeChunkPosition(doorType, possibleEntrance);
                    if (mapHypotesis.AddChunk(chunk, possibleChunkPosition.X, possibleChunkPosition.Y, possibleEntrance))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion Private Methods
    }
}