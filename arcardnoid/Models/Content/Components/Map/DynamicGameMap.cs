using arcardnoid.Models.Content.Components.Map.Cells;
using arcardnoid.Models.Content.Components.Map.Models;
using arcardnoid.Models.Framework;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace arcardnoid.Models.Content.Components.Map
{
    public class DynamicGameMap : Component
    {
        #region Private Fields

        private Texture2D _blockTexture;
        private string Filter { get; set; }
        private bool _forceDebug;
        private MapItem _mapItem;
        private List<Texture2D> _mapTextures = new List<Texture2D>();

        #endregion Private Fields

        #region Public Constructors

        public DynamicGameMap(bool forceDebug = false) : base("DynamicGameMap", 28, 28)
        {
            _forceDebug = forceDebug;
        }

        public void SetForceDebug(bool forceDebug)
        {
            _forceDebug = forceDebug;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            if (BaseGame.DebugMode || _forceDebug)
            {
                DrawGrid();
            }
        }

        public override void Load()
        {
            base.Load();
            _blockTexture = Game.Content.Load<Texture2D>("map/halt");
            _mapItem = new MapItem() { Width = 29, Height = 16, Size = 64 };
            _mapItem.Assets = LoadFromFile<List<MapAsset>>("Maps/chunkAssets.json");
            LoadAssets();
        }

        public void LoadTiles(string filter)
        {
            RemoveAllComponents();
            Filter = filter;
            List<MapChunk> chunks = LoadChunks();
            List<ChunkLayout> layout = new List<ChunkLayout>();
            int x = 0;
            int y = 0;
            foreach (MapChunk chunk in chunks)
            {
                if (x + chunk.Width > _mapItem.Width)
                {
                    x = 0;
                    y += chunk.Height+1;
                }
                layout.Add(new ChunkLayout() { MapChunk = chunk, X = x, Y = y });
                x += chunk.Width +1;
            }
            LoadChunkLayer(layout);
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadBlocks(ChunkLayout chunk)
        {
            int[,] dataLines = chunk.MapChunk.Blocks.Data.ToMapArray(chunk.MapChunk.Width, chunk.MapChunk.Height);
            int RealX = chunk.X;
            int RealY = chunk.Y;
            for (int y = 0; y < chunk.MapChunk.Height; y++)
            {
                for (int x = 0; x < chunk.MapChunk.Width; x++)
                {
                    int assetIndex = dataLines[x, y];
                    if (assetIndex != -1)
                    {
                        Texture2D texture = _blockTexture;
                        MapCell cell = new MapCell($"map-cell-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), 0, 0);
                        cell.Color = Color.Red;
                        cell.Opacity = 0.25f;
                        AddComponent(cell);
                    }
                    RealX++;
                }
                RealX = chunk.X;
                RealY++;
            }
        }

        private void LoadEntrances(ChunkLayout chunk)
        {
            int RealX = chunk.X;
            int RealY = chunk.Y;
            for (int y = 0; y < chunk.MapChunk.Height; y++)
            {
                for (int x = 0; x < chunk.MapChunk.Width; x++)
                {
                    MapChunkEntrance entrance = chunk.MapChunk.Entrances.FirstOrDefault(c => c.X == x && c.Y == y);
                    if (entrance != null)
                    {
                        Texture2D texture = _blockTexture;
                        MapCell cell = new MapCell($"map-cell-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), 0, 0);
                        cell.Color = Color.Cyan;
                        cell.Opacity = 0.35f;
                        AddComponent(cell);
                    }
                    RealX++;
                }
                RealX = chunk.X;
                RealY++;
            }
        }

        private void LoadSpawns(ChunkLayout chunk)
        {
            int RealX = chunk.X;
            int RealY = chunk.Y;
            for (int y = 0; y < chunk.MapChunk.Height; y++)
            {
                for (int x = 0; x < chunk.MapChunk.Width; x++)
                {
                    MapChunkSpawn spawn = chunk.MapChunk.Spawns.FirstOrDefault(c => c.X == x && c.Y == y);
                    if (spawn != null)
                    {
                        Texture2D texture = _blockTexture;
                        MapCell cell = new MapCell($"map-cell-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), 0, 0);
                        cell.Color = Color.Yellow;
                        cell.Opacity = 0.35f;
                        AddComponent(cell);
                    }
                    RealX++;
                }
                RealX = chunk.X;
                RealY++;
            }
        }

        private void DrawGrid()
        {
            for (int x = 0; x <= _mapItem.Width; x++)
            {
                int x1 = ScreenManager.ScaleX((int)RealPosition.X + (x * _mapItem.Size));
                int y1 = ScreenManager.ScaleY((int)RealPosition.Y);
                int x2 = ScreenManager.ScaleX((int)RealPosition.X + (x * _mapItem.Size));
                int y2 = ScreenManager.ScaleY((int)RealPosition.Y + (_mapItem.Height * _mapItem.Size));
                Primitives2D.DrawLine(Game.SpriteBatch, x1, y1, x2, y2, Color.White);
            }
            for (int y = 0; y <= _mapItem.Height; y++)
            {
                int x1 = ScreenManager.ScaleX((int)RealPosition.X);
                int y1 = ScreenManager.ScaleY((int)RealPosition.Y + (y * _mapItem.Size));
                int x2 = ScreenManager.ScaleX((int)RealPosition.X + (_mapItem.Width * _mapItem.Size));
                int y2 = ScreenManager.ScaleY((int)RealPosition.Y + (y * _mapItem.Size));
                Primitives2D.DrawLine(Game.SpriteBatch, x1, y1, x2, y2, Color.White);
            }
        }

        private void DrawTiles(MapLayer layer, ChunkLayout chunk)
        {
            int[,] dataLines = layer.Data.ToMapArray(chunk.MapChunk.Width, chunk.MapChunk.Height);
            int RealX = chunk.X;
            int RealY = chunk.Y;
            for (int y = 0; y < chunk.MapChunk.Height; y++)
            {
                for (int x = 0; x < chunk.MapChunk.Width; x++)
                {
                    int assetIndex = dataLines[x, y];
                    if (assetIndex != -1)
                    {
                        MapAsset mapAsset = _mapItem.Assets[assetIndex];
                        Texture2D texture = _mapTextures[assetIndex];
                        if (mapAsset.Type == "spritesheet")
                        {
                            AddComponent(new AnimatedCell($"animated-cell-{RealX}-{RealY}", texture, mapAsset.Columns, mapAsset.Rows, mapAsset.Speed, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), mapAsset.OffsetX, mapAsset.OffsetY));
                        }
                        else if (mapAsset.Type == "multi")
                        {
                            AddComponent(new MultiCell($"multi-cell-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell.GetMultiCellType(dataLines, x, y, chunk.MapChunk.Width, chunk.MapChunk.Height)));
                        }
                        else if (mapAsset.Type == "multi2")
                        {
                            AddComponent(new MultiCell2($"multi-cell2-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell2.GetMultiCellType(dataLines, x, y, chunk.MapChunk.Width, chunk.MapChunk.Height)));
                        }
                        else if (mapAsset.Type == "bridge")
                        {
                            AddComponent(new BridgeCell($"bridge-cell-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, BridgeCell.GetMultiCellType(dataLines, x, y, chunk.MapChunk.Width, chunk.MapChunk.Height)));
                        }
                        else
                        {
                            AddComponent(new MapCell($"map-cell-{RealX}-{RealY}", texture, RealX, RealY, (RealX * _mapItem.Size) + (_mapItem.Size / 2), (RealY * _mapItem.Size) + (_mapItem.Size / 2), mapAsset.OffsetX, mapAsset.OffsetY));
                        }
                    }
                    RealX++;
                }
                RealX = chunk.X;
                RealY++;
            }
        }

        private void LoadAssets()
        {
            _mapTextures = new List<Texture2D>();
            foreach (MapAsset asset in _mapItem.Assets)
            {
                _mapTextures.Add(Game.Content.Load<Texture2D>(asset.Path));
            }
        }

        private void LoadChunkLayer(List<ChunkLayout> layout)
        {
            foreach (ChunkLayout chunk in layout)
            {
                foreach (MapLayer layer in chunk.MapChunk.Layers)
                {
                    DrawTiles(layer, chunk);
                }
                if (_forceDebug) LoadBlocks(chunk);
                if (_forceDebug) LoadEntrances(chunk);
                if (_forceDebug) LoadSpawns(chunk);
            }
        }

        private List<MapChunk> LoadChunks()
        {
            List<MapChunk> chunks = new List<MapChunk>();
            foreach (string file in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Maps/Chunks"), Filter, SearchOption.AllDirectories))
            {
                chunks.Add(LoadFromFile<MapChunk>(file));
            }
            return chunks;
        }

        private T LoadFromFile<T>(string filename)
        {
            string content = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename));
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(filename);
                return default(T);
            }
        }

        #endregion Private Methods
    }
}