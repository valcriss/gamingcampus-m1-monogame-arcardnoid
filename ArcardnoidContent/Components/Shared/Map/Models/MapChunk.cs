using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidContent.Tools;
using Newtonsoft.Json;
using System.Reflection;

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
            List<MapChunk> chunks = new();
            string location = Assembly.GetExecutingAssembly().Location;
            string? directory = Path.GetDirectoryName(location) ?? throw new Exception("Directory not found");
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
                foreach (string file in Directory.GetFiles(Path.Combine(directory, "Maps/Chunks"), filter, SearchOption.AllDirectories))
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
            string location = Assembly.GetExecutingAssembly().Location;
            string? directory = Path.GetDirectoryName(location) ?? throw new Exception("Directory not found");
            return DynamicGameMap.LoadFromFile<MapChunk>(Path.Combine(directory, "Maps/Chunks/23/0/1/grass-03.json"));
        }

        public readonly void AddActor(EncounterType type, int x, int y)
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

        public readonly EncounterType CheckCollision(int x, int y)
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

        public readonly void ClearActor(int x, int y)
        {
            int count = Layers.Count(c => c.Name == "Actor Layer");
            if (count == 0)
            {
                return;
            }
            MapLayer actorLayer = Layers.FirstOrDefault(c => c.Name == "Actor Layer");

            string line = actorLayer.Data[y];
            string[] table = line.Split(',');
            table[x] = "";
            actorLayer.Data[y] = string.Join(",", table);
        }

        public readonly List<MapChunkEntrance> FilterEntrances(List<MapChunkDoorType> opositeDoorTypes)
        {
            List<MapChunkEntrance> entrances = new();
            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (opositeDoorTypes.Contains(GetDoorType(entrance)))
                {
                    entrances.Add(entrance);
                }
            }
            return entrances;
        }

        public readonly List<MapChunkDoor> GetAllDoors()
        {
            List<MapChunkDoor> doors = new();

            foreach (MapChunkEntrance entrance in Entrances)
            {
                doors.Add(new MapChunkDoor(GetDoorType(entrance), entrance.X, entrance.Y));
            }

            return doors;
        }

        public readonly List<MapChunkDoor> GetAllDoors(int x, int y)
        {
            List<MapChunkDoor> doors = new();

            foreach (MapChunkEntrance entrance in Entrances)
            {
                doors.Add(new MapChunkDoor(GetDoorType(entrance), x + entrance.X, y + entrance.Y));
            }

            return doors;
        }

        public readonly List<MapChunkDoor> GetAllDoors(int x, int y, MapChunkEntrance possibleEntrance)
        {
            List<MapChunkDoor> doors = new();

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

        public readonly List<MapChunkEntrance> GetCompatibleOppositeEntrances(MapChunkDoorType doorType)
        {
            List<MapChunkEntrance> entrances = new();
            foreach (MapChunkEntrance entrance in Entrances)
            {
                if (IsDoorTypeCompatibleOpposite(doorType, entrance))
                {
                    entrances.Add(entrance);
                }
            }
            return entrances;
        }

        public readonly bool HasEncounters()
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

        #endregion Public Methods

        #region Private Methods

        private static string GetEncountTypeCode(EncounterType type)
        {
            return type switch
            {
                EncounterType.Archer => "33",
                EncounterType.Warrior => "29",
                EncounterType.Torch => "28",
                EncounterType.Gold => "31",
                EncounterType.Meat => "32",
                EncounterType.Sheep => "16",
                EncounterType.Tnt => "30",
                _ => "",
            };
        }

        private static EncounterType GetEncountTypeFromCode(string code)
        {
            return code switch
            {
                "33" => EncounterType.Archer,
                "29" => EncounterType.Warrior,
                "28" => EncounterType.Torch,
                "31" => EncounterType.Gold,
                "32" => EncounterType.Meat,
                "16" => EncounterType.Sheep,
                "30" => EncounterType.Tnt,
                _ => EncounterType.None,
            };
        }

        private readonly MapChunkDoorType GetDoorType(MapChunkEntrance entrance)
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

        private readonly bool IsDoorTypeCompatibleOpposite(MapChunkDoorType doorType, MapChunkEntrance entrance)
        {
            MapChunkDoorType chunkDoorType = GetDoorType(entrance);
            return doorType switch
            {
                MapChunkDoorType.Top => chunkDoorType == MapChunkDoorType.Bottom || chunkDoorType == MapChunkDoorType.BottomRight || chunkDoorType == MapChunkDoorType.BottomLeft,
                MapChunkDoorType.Right => chunkDoorType == MapChunkDoorType.Left || chunkDoorType == MapChunkDoorType.TopLeft || chunkDoorType == MapChunkDoorType.BottomLeft,
                MapChunkDoorType.Bottom => chunkDoorType == MapChunkDoorType.Top || chunkDoorType == MapChunkDoorType.TopLeft || chunkDoorType == MapChunkDoorType.TopRight,
                MapChunkDoorType.Left => chunkDoorType == MapChunkDoorType.Right || chunkDoorType == MapChunkDoorType.TopRight || chunkDoorType == MapChunkDoorType.BottomRight,
                _ => false,
            };
        }

        #endregion Private Methods
    }
}