﻿using ArcardnoidContent.Components.Shared.Map.Models;
using ArcardnoidContent.Tools;
using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidContent.Components.GameScene
{
    public class MapGenerator
    {
        #region Public Properties

        public MapChunk MapChunk { get; set; }
        public MapHypothesis MapHypothesis { get; set; } = null;

        #endregion Public Properties

        #region Private Properties

        private List<MapChunk> Chunks { get; set; }
        private Random Random { get; set; }
        private int Seed { get; set; }
        private MapChunk StartingChunk { get; set; }
        private List<MapChunk> StoredChunks { get; set; }

        #endregion Private Properties

        #region Private Fields

        private const int MAP_HEIGHT = 15;
        private const int MAP_HYPOTHESIS_COUNT = 20;
        private const int MAP_WIDTH = 29;
        private const int STARTING_CHUNK_X = 0;
        private const int STARTING_CHUNK_Y = 7;

        #endregion Private Fields

        #region Public Constructors

        public MapGenerator(int seed = 123456)
        {
            Seed = seed;
            Chunks = new List<MapChunk>();
            Random = new Random(seed);
        }

        #endregion Public Constructors

        #region Public Methods

        public void GenerateMap()
        {
            LoadChunks();
            List<MapHypothesis> mapHypotesisList = new List<MapHypothesis>();

            while (mapHypotesisList.Count < MAP_HYPOTHESIS_COUNT)
            {
                MapHypothesis hypotesis = RandomMap();
                if (hypotesis != null)
                {
                    mapHypotesisList.Add(hypotesis);
                }
            }
            MapHypothesis = mapHypotesisList.OrderByDescending(m => m.GetCoverage()).First().Accept();
        }

        #endregion Public Methods

        #region Private Methods

        private List<MapChunk> GetPossibleChunks(MapHypothesis mapHypotesis, List<MapChunkDoorType> opositeDoorTypes)
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
            StoredChunks = MapChunk.AllChunks();
            StartingChunk = MapChunk.StartingChunk();
        }

        private MapHypothesis RandomMap()
        {
            Chunks = StoredChunks.Clone();
            MapHypothesis mapHypotesis = new MapHypothesis(Seed, MAP_WIDTH, MAP_HEIGHT);
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

        private bool TryCloseDoor(MapHypothesis mapHypotesis, MapChunkDoor openedDoor)
        {
            List<MapChunkDoorType> opositeDoorTypes = openedDoor.GetOppositeDoorTypes().RandomList(Random);
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

        private bool TryPlaceChunk(MapChunk chunk, MapHypothesis mapHypotesis, MapChunkDoor openedDoor)
        {
            List<MapChunkDoorType> basicChunkDoorTypes = (new List<MapChunkDoorType>() { MapChunkDoorType.Top, MapChunkDoorType.Right, MapChunkDoorType.Bottom, MapChunkDoorType.Left }).RandomList(Random);
            foreach (MapChunkDoorType doorType in basicChunkDoorTypes)
            {
                List<MapChunkEntrance> compatibleEntrances = chunk.GetCompatibleOppositeEntrances(doorType).RandomList(Random);
                if (compatibleEntrances.Count == 0) continue;
                foreach (MapChunkEntrance possibleEntrance in compatibleEntrances)
                {
                    Point possibleChunkPosition = openedDoor.GetOppositeChunkPosition(doorType, possibleEntrance);

                    if (mapHypotesis.AddChunk(chunk, (int)possibleChunkPosition.X, (int)possibleChunkPosition.Y, possibleEntrance))
                    {
                        mapHypotesis.BridgePositions.Add(new MapBridgePosition()
                        {
                            DoorType = doorType,
                            FromX = openedDoor.X,
                            FromY = openedDoor.Y,
                            ToX = (int)possibleChunkPosition.X + possibleEntrance.X,
                            ToY = (int)possibleChunkPosition.Y + possibleEntrance.Y
                        });
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion Private Methods
    }
}