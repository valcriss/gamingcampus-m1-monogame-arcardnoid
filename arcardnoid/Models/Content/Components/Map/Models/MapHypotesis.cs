using System;
using System.Collections.Generic;
using System.Linq;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapHypotesis
    {
        #region Public Properties

        public int Height { get; set; }
        public int[,] Map { get; set; }
        public Queue<MapChunkDoor> OpenedDoor { get; set; }
        public int Width { get; set; }

        public int PositionX
        {
            get
            {
                return (((29 - Width) * 64) / 2) + 28;
            }
        }
        public int PositionY
        {
            get
            {
                return (((16 - Height) * 64) / 2) + 28;
            }
        }

        #endregion Public Properties

        #region Private Properties

        public List<MapChunkPosition> Chunks { get; set; }
        public MapChunk FinalChunk { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MapHypotesis(int width, int height)
        {
            Width = width;
            Height = height;
            Chunks = new List<MapChunkPosition>();
            OpenedDoor = new Queue<MapChunkDoor>();
            InitializeMap();
        }

        #endregion Public Constructors

        #region Public Methods

        public bool AddChunk(MapChunk chunk, int x, int y, MapChunkEntrance possibleEntrance)
        {
            if (!IsChunkPositionValid(chunk, x, y))
            {
                return false;
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
            AddChunkToMap(chunk, x, y);
            Chunks.Add(new MapChunkPosition() { MapChunk = chunk, X = x, Y = y });
            foreach (MapChunkDoor door in chunk.GetAllDoors(x, y))
            {
                OpenedDoor.Enqueue(door);
            }
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

        public MapHypotesis Accept()
        {
            int w = 0;
            int h = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Map[x, y] != -1)
                    {
                        w = Math.Max(w, x);
                        h = Math.Max(h, y);
                    }
                }
            }
            Width = (w < Width - 1 ? w : w + 1);
            Height = (h < Height - 1 ? h : h + 1);
            FinalChunk = ConcatenateAsChunk();
            return this;
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

        public MapChunk ConcatenateAsChunk()
        {
            MapChunk concatenatedChunk = new MapChunk();


            concatenatedChunk.Width = Width;
            concatenatedChunk.Height = Height;
            concatenatedChunk.Level = 1;

            // Mise a jour des blocs
            concatenatedChunk.Blocks = new MapLayer();

            // Mise a jour des layers de base
            string[] baseLayers = new string[] { "WaterSplash", "Terrain Layer 1", "Shadow Layer", "Terrain Layer 2", "Deco Layer", "Building Layer", "Actor Layer" };
            foreach (string layer in baseLayers)
            {
                int[,] initialLayer = GetInitialLayer(Width, Height);
                foreach (MapChunkPosition position in Chunks)
                {
                    MapLayer mapLayer = position.MapChunk.Layers.FirstOrDefault(c => c.Name == layer);
                    if (mapLayer == null)
                    {
                        continue;
                    }
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
                newLayer.Name = layer;
                newLayer.Data = initialLayer.ToStringData();
                concatenatedChunk.Layers.Add(newLayer);
            }

            // Ajout des layers de ponts
            return concatenatedChunk;
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

        #endregion Private Methods
    }
}