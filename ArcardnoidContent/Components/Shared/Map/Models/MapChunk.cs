using ArcardnoidContent.Components.DemoScene;
using ArcardnoidContent.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.Shared.Map.Models
{
    public struct MapChunk
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

        #region Public Constructors

        public MapChunk()
        {
            Blocks = new MapLayer();
            Entrances = new List<MapChunkEntrance>();
            Layers = new List<MapLayer>();
            Spawns = new List<MapChunkSpawn>();
            Height = 0;
            Level = 0;
            Width = 0;
        }

        #endregion Public Constructors

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

        public void AddActor(EncounterType type, int x, int y)
        {
            int count = Layers.Count(c => c.Name == "Actor Layer");
            if (count == 0)
            {
                return;
            }
            MapLayer actorLayer = Layers.FirstOrDefault(c => c.Name == "Actor Layer");

            string line = actorLayer.Data[y];
            string[] table = line.Split(',');
            table[x] = GetEncountTypeCode(type);
            actorLayer.Data[y] = string.Join(",", table);
        }

        public List<MapChunkDoor> GetAllDoors()
        {
            List<MapChunkDoor> doors = new List<MapChunkDoor>();

            foreach (MapChunkEntrance entrance in Entrances)
            {
                doors.Add(new MapChunkDoor(GetDoorType(entrance), entrance.X, entrance.Y));
            }

            return doors;
        }

        public List<MapChunkDoor> GetAllDoors(int x, int y)
        {
            List<MapChunkDoor> doors = new List<MapChunkDoor>();

            foreach (MapChunkEntrance entrance in Entrances)
            {
                doors.Add(new MapChunkDoor(GetDoorType(entrance), x + entrance.X, y + entrance.Y));
            }

            return doors;
        }

        public List<MapChunkDoor> GetAllDoors(int x, int y, MapChunkEntrance possibleEntrance)
        {
            List<MapChunkDoor> doors = new List<MapChunkDoor>();

            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (entrance.X == possibleEntrance.X && entrance.Y == possibleEntrance.Y)
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

        internal List<MapChunkEntrance> GetCompatibleOppositeEntrances(MapChunkDoorType doorType)
        {
            List<MapChunkEntrance> entrances = new List<MapChunkEntrance>();
            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (IsDoorTypeCompatibleOpposite(doorType, entrance))
                {
                    entrances.Add(entrance);
                }
            }
            return entrances;
        }

        internal bool HasEncounters()
        {
            int count = Layers.Count(c => c.Name == "Building Layer");

            if (count == 0)
            {
                return false;
            }
            MapLayer actorLayer = Layers.FirstOrDefault(c => c.Name == "Building Layer");
            for (int y = 0; y < Height; y++)
            {
                string line = actorLayer.Data[y];
                string[] tab = line.Split(',');
                for (int x = 0; x < Width; x++)
                {
                    if (tab[x].ToString().Trim() != "") return true;
                }
            }
            return false;
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

        private string GetEncountTypeCode(EncounterType type)
        {
            switch (type)
            {
                case EncounterType.Archer:
                    return "33";

                case EncounterType.Warrior:
                    return "29";

                case EncounterType.Torch:
                    return "28";

                case EncounterType.Gold:
                    return "31";

                case EncounterType.Meat:
                    return "32";

                case EncounterType.Sheep:
                    return "16";

                case EncounterType.Tnt:
                    return "30";

                default:
                    return "";
            }
        }

        private EncounterType GetEncountTypeFromCode(string code)
        {
            switch (code)
            {
                case "33": return EncounterType.Archer;

                case "29": return EncounterType.Warrior;

                case "28": return EncounterType.Torch;

                case "31": return EncounterType.Gold;

                case "32": return EncounterType.Meat;

                case "16": return EncounterType.Sheep;

                case "30": return EncounterType.Tnt;

                default:
                    return EncounterType.None;
            }
        }

        private bool IsDoorTypeCompatibleOpposite(MapChunkDoorType doorType, MapChunkEntrance entrance)
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

        public EncounterType CheckCollision(int x, int y)
        {
            int count = Layers.Count(c => c.Name == "Actor Layer");
            if (count == 0)
            {
                return EncounterType.None;
            }
            MapLayer actorLayer = Layers.FirstOrDefault(c => c.Name == "Actor Layer");
            string code = actorLayer.GetLayerData(x, y);
            return GetEncountTypeFromCode(code);
        }

        #endregion Private Methods
    }
}
