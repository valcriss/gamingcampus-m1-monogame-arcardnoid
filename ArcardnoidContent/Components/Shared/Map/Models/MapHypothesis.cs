using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidContent.Components.Shared.Map.Models;
using ArcardnoidContent.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene
{
    public class MapHypothesis
    {
        #region Private Delegates

        private delegate MapLayer MapChunkMapLayerFilter(MapChunk chunk);

        #endregion Private Delegates

        #region Public Properties

        public List<MapBridgePosition> BridgePositions { get; set; } = new List<MapBridgePosition>();
        public List<MapChunkPosition> Chunks { get; set; } = new List<MapChunkPosition>();
        public MapChunk FinalChunk { get; set; }
        public int Height { get; set; }
        public int[,] Map { get; set; } = new int[0, 0];
        public Queue<MapChunkDoor> OpenedDoor { get; set; }

        public int PlayerPositionX { get; private set; }

        public int PlayerPositionY { get; private set; }

        public int PositionX
        {
            get
            {
                return ((29 - Width) * 64 / 2) + 28;
            }
        }

        public int PositionY
        {
            get
            {
                return ((15 - Height) * 64 / 2) + 60;
            }
        }

        public int Width { get; set; }

        #endregion Public Properties

        #region Private Properties

        private int CurrentGold { get; set; } = 0;
        private int CurrentMeat { get; set; } = 0;
        private int CurrentSheep { get; set; } = 0;
        private IRandom EncountersRandom { get; set; }

        #endregion Private Properties

        #region Private Fields

        private const int MAXIMUM_GOLD = 1;
        private const int MAXIMUM_MEAT = 1;
        private const int MAXIMUM_SHEEP = 1;

        #endregion Private Fields

        #region Public Constructors

        public MapHypothesis(int seed, int width, int height)
        {
            Width = width;
            Height = height;
            Chunks = new List<MapChunkPosition>();
            OpenedDoor = new Queue<MapChunkDoor>();
            EncountersRandom = GameServiceProvider.GetService<IRandomService>().GetRandom(seed);
            InitializeMap();
        }

        #endregion Public Constructors

        #region Public Methods

        public MapHypothesis Accept()
        {
            FinalChunk = ConcatenateAsChunk();
            return this;
        }

        public bool AddChunk(MapChunk chunk, int x, int y, MapChunkEntrance possibleEntrance)
        {
            if (!IsChunkPositionValid(chunk, x, y))
            {
                return false;
            }

            // Generation des encounters
            if (!chunk.HasEncounters() && chunk.Spawns.Count > 0)
            {
                EncounterType? encounterType = GetEncounterType();
                if (encounterType == null) return false;
                AddEncounter(encounterType.Value, chunk);
            }

            AddChunkToMap(chunk, x, y);
            Chunks.Add(new MapChunkPosition() { MapChunk = chunk, X = x, Y = y });
            foreach (MapChunkDoor door in chunk.GetAllDoors(x, y, possibleEntrance))
            {
                OpenedDoor.Enqueue(door);
            }
            return true;
        }

        public void AddStartingChunk(MapChunk chunk, int x, int y)
        {
            PlayerPositionX = x;
            PlayerPositionY = y + 1;
            AddChunkToMap(chunk, x, y);
            Chunks.Add(new MapChunkPosition() { MapChunk = chunk, X = x, Y = y });
            foreach (MapChunkDoor door in chunk.GetAllDoors(x, y))
            {
                OpenedDoor.Enqueue(door);
            }
        }

        public MapChunk ConcatenateAsChunk()
        {
            MapChunk concatenatedChunk = new MapChunk();

            concatenatedChunk.Width = Width;
            concatenatedChunk.Height = Height;
            concatenatedChunk.Level = 1;

            // Mise a jour des blocs
            concatenatedChunk.Blocks = ConcatenateLayer("blocks", c => c.Blocks);
            concatenatedChunk.Spawns = ConcatenateSpawn();

            // Mise a jour des layers de base
            string[] baseLayers = new string[] { "WaterSplash", "Terrain Layer 1", "Shadow Layer", "Terrain Layer 2", "Deco Layer" };
            foreach (string layer in baseLayers)
            {
                concatenatedChunk.Layers.Add(ConcatenateLayers(layer));
            }

            // Ajout des layers de ponts
            for (int index = 0; index < BridgePositions.Count; index++)
            {
                MapBridgePosition position = BridgePositions[index];
                concatenatedChunk.Layers.Add(CreateBridgeLayer(index, position));
            }

            // Ajout des layers de batiments et acteurs
            string[] extrasLayers = new string[] { "Building Layer", "Actor Layer" };
            foreach (string layer in extrasLayers)
            {
                concatenatedChunk.Layers.Add(ConcatenateLayers(layer));
            }

            // Trim chunk
            concatenatedChunk = TrimChunk(concatenatedChunk);

            // Mise a jour des blocs restants
            UpdateEmptyCellsBlocks(concatenatedChunk);

            return concatenatedChunk;
        }

        public int GetCoverage()
        {
            int total = Width * Height;
            int covered = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Map[x, y] != -1)
                    {
                        covered++;
                    }
                }
            }
            return Math.Max(0, Math.Min(100, (int)Math.Ceiling((covered / (float)total) * 100)));
        }

        public MapChunkDoor GetFirstOpenedDoor()
        {
            return OpenedDoor.Dequeue();
        }

        public bool HasOpenedDoors()
        {
            return OpenedDoor.Count > 0;
        }

        #endregion Public Methods

        #region Private Methods

        private void AddChunkToMap(MapChunk chunk, int x, int y)
        {
            for (int xx = x - 1; xx <= x + chunk.Width; xx++)
            {
                for (int yy = y - 1; yy <= y + chunk.Height; yy++)
                {
                    if (xx < 0 || xx >= Width || yy < 0 || yy >= Height)
                    {
                        continue;
                    }
                    Map[xx, yy] = 1;
                }
            }
        }

        private void AddEncounter(EncounterType type, MapChunk chunk)
        {
            MapChunkSpawn? spawn = chunk.Spawns.RandomList(EncountersRandom)?.First();
            if (spawn == null) return;
            chunk.AddActor(type, spawn.Value.X, spawn.Value.Y);
            if (type == EncounterType.Gold)
            {
                CurrentGold = CurrentGold + 1;
            }
            else if (type == EncounterType.Meat)
            {
                CurrentMeat++;
            }
            else if (type == EncounterType.Sheep)
            {
                CurrentSheep++;
            }
        }

        private MapLayer ConcatenateLayer(string name, MapChunkMapLayerFilter filter)
        {
            int[,] initialLayer = GetInitialLayer(Width, Height);
            foreach (MapChunkPosition position in Chunks)
            {
                MapLayer mapLayer = filter(position.MapChunk);
                for (int y = 0; y < position.MapChunk.Height; y++)
                {
                    string line = mapLayer.Data[y];
                    for (int x = 0; x < position.MapChunk.Width; x++)
                    {
                        string cell = line.Split(',')[x];
                        if (cell.Trim() == "XX")
                        {
                            initialLayer[position.X + x, position.Y + y] = 99;
                        }
                        else if (cell.Trim() != "")
                        {
                            initialLayer[position.X + x, position.Y + y] = int.Parse(cell);
                        }
                    }
                }
            }
            MapLayer newLayer = new MapLayer();
            newLayer.Name = name;
            newLayer.Data = initialLayer.ToStringData();
            return newLayer;
        }

        private MapLayer ConcatenateLayers(string name)
        {
            int[,] initialLayer = GetInitialLayer(Width, Height);
            foreach (MapChunkPosition position in Chunks)
            {
                MapLayer mapLayer = position.MapChunk.Layers.FirstOrDefault(c => c.Name == name);
                for (int y = 0; y < position.MapChunk.Height; y++)
                {
                    string line = mapLayer.Data[y];
                    for (int x = 0; x < position.MapChunk.Width; x++)
                    {
                        string cell = line.Split(',')[x];
                        if (cell.Trim() != "")
                        {
                            initialLayer[position.X + x, position.Y + y] = int.Parse(cell);
                        }
                    }
                }
            }
            MapLayer newLayer = new MapLayer();
            newLayer.Name = name;
            newLayer.Data = initialLayer.ToStringData();
            return newLayer;
        }

        private List<MapChunkSpawn> ConcatenateSpawn()
        {
            List<MapChunkSpawn> spawns = new List<MapChunkSpawn>();
            for (int index = 0; index < Chunks.Count; index++)
            {
                MapChunkPosition position = Chunks[index];
                foreach (MapChunkSpawn spawn in position.MapChunk.Spawns)
                {
                    spawns.Add(new MapChunkSpawn()
                    {
                        X = position.X + spawn.X,
                        Y = position.Y + spawn.Y
                    });
                }
            }
            return spawns;
        }

        private MapLayer CreateBridgeLayer(int index, MapBridgePosition position)
        {
            int[,] initialLayer = GetInitialLayer(Width, Height);
            Point from = new Point(position.FromX, position.FromY);
            Point to = new Point(position.ToX, position.ToY);
            switch (position.DoorType)
            {
                case MapChunkDoorType.Top:
                    for (int y = (int)from.Y; y >= to.Y; y--)
                    {
                        initialLayer[(int)from.X, y] = 20;
                    }
                    break;

                case MapChunkDoorType.Bottom:
                    for (int y = (int)from.Y; y <= to.Y; y++)
                    {
                        initialLayer[(int)from.X, y] = 20;
                    }
                    break;

                case MapChunkDoorType.Left:
                    for (int x = (int)from.X; x >= to.X; x--)
                    {
                        initialLayer[x, (int)from.Y] = 20;
                    }
                    break;

                case MapChunkDoorType.Right:
                    for (int x = (int)from.X; x <= to.X; x++)
                    {
                        initialLayer[x, (int)from.Y] = 20;
                    }
                    break;
            }
            MapLayer newLayer = new MapLayer();
            newLayer.Name = $"Bridge-{index}";
            newLayer.Data = initialLayer.ToStringData();
            return newLayer;
        }

        private EncounterType? GetEncounterType()
        {
            List<EncounterType> types = new List<EncounterType>() { EncounterType.Archer, EncounterType.Warrior, EncounterType.Torch, EncounterType.Tnt };
            if (CurrentGold < MAXIMUM_GOLD)
            {
                types.Add(EncounterType.Gold);
            }
            if (CurrentMeat < MAXIMUM_MEAT)
            {
                types.Add(EncounterType.Meat);
            }
            if (CurrentSheep < MAXIMUM_SHEEP)
            {
                types.Add(EncounterType.Sheep);
            }
            return types.RandomList(EncountersRandom)?.First();
        }

        private int[,] GetInitialLayer(int width, int height)
        {
            int[,] layer = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    layer[x, y] = -1;
                }
            }
            return layer;
        }

        private void InitializeMap()
        {
            Map = new int[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Map[x, y] = -1;
                }
            }
        }

        private bool IsChunkPositionValid(MapChunk chunk, int x, int y)
        {
            for (int xx = x; xx < x + chunk.Width; xx++)
            {
                for (int yy = y; yy < y + chunk.Height; yy++)
                {
                    if (xx < 0 || xx >= Width || yy < 0 || yy >= Height)
                    {
                        return false;
                    }
                    if (Map[xx, yy] != -1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private MapChunk TrimChunk(MapChunk chunk)
        {
            int startX = chunk.GetChunkStartX();
            int startY = chunk.GetChunkStartY();
            int endX = chunk.GetChunkEndX();
            int endY = chunk.GetChunkEndY();
            PlayerPositionX -= startX;
            PlayerPositionY -= startY;
            // Mise a jour des spawns
            for (int i = 0; i < chunk.Spawns.Count; i++)
            {
                MapChunkSpawn spawn = chunk.Spawns[i];
                spawn.X -= startX;
                spawn.Y -= startY;
                chunk.Spawns[i] = spawn;
            }

            chunk.Blocks = chunk.Blocks.Trim(startX, startY, endX, endY);
            // Mise a jour des layers
            for (int i = 0; i < chunk.Layers.Count; i++)
            {
                chunk.Layers[i] = chunk.Layers[i].Trim(startX, startY, endX, endY);
            }
            int finalstartX = chunk.GetChunkStartX();
            int finalstartY = chunk.GetChunkStartY();
            int finalendX = chunk.GetChunkEndX();
            int finalendY = chunk.GetChunkEndY();
            Width = finalendX + 1;
            Height = finalendY + 1;
            return chunk;
        }

        private void UpdateEmptyCellsBlocks(MapChunk concatenatedChunk)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    bool found = false;
                    foreach (MapLayer layer in concatenatedChunk.Layers)
                    {
                        string cell = layer.Data[y].Split(',')[x];
                        if (cell.Trim() != "")
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        string line = concatenatedChunk.Blocks.Data[y];
                        string[] cells = line.Split(',');
                        cells[x] = "XX";
                        concatenatedChunk.Blocks.Data[y] = string.Join(",", cells);
                    }
                }
            }
        }

        #endregion Private Methods
    }
}