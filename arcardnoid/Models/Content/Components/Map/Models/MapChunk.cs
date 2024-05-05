using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapChunk
    {
        #region Public Properties

        [JsonProperty("blocks")]
        public MapLayer Blocks { get; set; }

        [JsonProperty("entrances")]
        public List<MapChunkEntrance> Entrances { get; set; } = new List<MapChunkEntrance>();

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("layers")]
        public List<MapLayer> Layers { get; set; } = new List<MapLayer>();

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("spawns")]
        public List<MapChunkSpawn> Spawns { get; set; } = new List<MapChunkSpawn>();

        [JsonProperty("width")]
        public int Width { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static List<MapChunk> AllChunks()
        {
            List<MapChunk> chunks = new List<MapChunk>();
            string[] filters = new string[]
            {
                "22\\0\\1\\*.json",
                "22\\0\\2\\*.json",
                "22\\0\\3\\*.json",
                "23\\0\\1\\*.json",
                "23\\0\\2\\*.json",
                "23\\0\\3\\*.json",
                "32\\0\\1\\*.json",
                "32\\0\\2\\*.json",
                "32\\0\\3\\*.json",
            };
            foreach (var filter in filters)
            {
                foreach (string file in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Maps/Chunks"), filter, SearchOption.AllDirectories))
                {
                    chunks.Add(DynamicGameMap.LoadFromFile<MapChunk>(file));
                }
            }
            return chunks;
        }

        public static MapChunk FromFile(string file)
        {
            return JsonConvert.DeserializeObject<MapChunk>(File.ReadAllText(file));
        }

        public static MapChunk StartingChunk()
        {
            return DynamicGameMap.LoadFromFile<MapChunk>(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Maps/Chunks/23/0/1/grass-03.json"));
        }

        public List<MapChunkDoor> GetAllDoors(int x = 0, int y = 0, MapChunkEntrance possibleEntrance = null)
        {
            List<MapChunkDoor> doors = new List<MapChunkDoor>();

            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (possibleEntrance != null && entrance.X == possibleEntrance.X && entrance.Y == possibleEntrance.Y)
                {
                    continue;
                }
                doors.Add(new MapChunkDoor(GetDoorType(entrance), x + entrance.X, y + entrance.Y));
            }

            return doors;
        }

        #endregion Public Methods

        #region Internal Methods

        internal List<MapChunkEntrance> FilterEntrances(List<MapChunkDoorType> opositeDoorTypes)
        {
            List<MapChunkEntrance> entrances = new List<MapChunkEntrance>();
            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (opositeDoorTypes.Contains(GetDoorType(entrance)))
                {
                    entrances.Add(entrance);
                }
            }
            return entrances;
        }

        internal List<MapChunkEntrance> GetCompatibleOpositeEntrances(MapChunkDoorType doorType)
        {
            List<MapChunkEntrance> entrances = new List<MapChunkEntrance>();
            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (IsDoorTypeCompatibleOposite(doorType, entrance))
                {
                    entrances.Add(entrance);
                }
            }
            return entrances;
        }

        #endregion Internal Methods

        #region Private Methods

        private MapChunkDoorType GetDoorType(MapChunkEntrance entrance)
        {
            if (entrance.X == 0 && entrance.Y == 0)
            {
                return MapChunkDoorType.TopLeft;
            }
            else if (entrance.X == Width - 1 && entrance.Y == 0)
            {
                return MapChunkDoorType.TopRight;
            }
            else if (entrance.X == 0 && entrance.Y == Height - 1)
            {
                return MapChunkDoorType.BottomLeft;
            }
            else if (entrance.X == Width - 1 && entrance.Y == Height - 1)
            {
                return MapChunkDoorType.BottomRight;
            }
            else if (entrance.X == 0)
            {
                return MapChunkDoorType.Left;
            }
            else if (entrance.X == Width - 1)
            {
                return MapChunkDoorType.Right;
            }
            else if (entrance.Y == 0)
            {
                return MapChunkDoorType.Top;
            }
            else if (entrance.Y == Height - 1)
            {
                return MapChunkDoorType.Bottom;
            }
            return MapChunkDoorType.None;
        }

        private bool IsDoorTypeCompatibleOposite(MapChunkDoorType doorType, MapChunkEntrance entrance)
        {
            MapChunkDoorType chunkDoorType = GetDoorType(entrance);
            switch (doorType)
            {
                case MapChunkDoorType.Top:
                    return chunkDoorType == MapChunkDoorType.Bottom || chunkDoorType == MapChunkDoorType.BottomRight || chunkDoorType == MapChunkDoorType.BottomLeft;

                case MapChunkDoorType.Right:
                    return chunkDoorType == MapChunkDoorType.Left || chunkDoorType == MapChunkDoorType.TopLeft || chunkDoorType == MapChunkDoorType.BottomLeft;

                case MapChunkDoorType.Bottom:
                    return chunkDoorType == MapChunkDoorType.Top || chunkDoorType == MapChunkDoorType.TopLeft || chunkDoorType == MapChunkDoorType.TopRight;

                case MapChunkDoorType.Left:
                    return chunkDoorType == MapChunkDoorType.Right || chunkDoorType == MapChunkDoorType.TopRight || chunkDoorType == MapChunkDoorType.BottomRight;
            }
            return false;
        }

        #endregion Private Methods
    }
}