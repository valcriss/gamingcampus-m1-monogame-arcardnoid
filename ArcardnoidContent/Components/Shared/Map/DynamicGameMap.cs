using arcardnoid.Models.Framework.Tools;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Models;
using ArcardnoidContent.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using Newtonsoft.Json;
using System.Reflection;

namespace ArcardnoidContent.Components.Shared.Map
{
    public class DynamicGameMap : GameComponent
    {
        #region Private Properties

        private string Filter { get; set; }


        #endregion Private Properties

        #region Private Fields

        private ITexture _blockTexture;
        private bool _forceDebug;
        private MapItem _mapItem;
        private List<ITexture> _mapTextures = new List<ITexture>();

        #endregion Private Fields

        #region Public Constructors

        public DynamicGameMap(bool forceDebug = false) : base(28, 28)
        {
            _forceDebug = forceDebug;
        }

        #endregion Public Constructors

        #region Public Methods

        public static T LoadFromFile<T>(string filename) where T : new()
        {
            string content = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename));
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(filename);
                return default;
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (_forceDebug)
            {
                DrawGrid();
            }
        }

        public override void Load()
        {
            base.Load();
            _blockTexture = GameServiceProvider.GetService<ITextureService>().Load("map/halt");
            _mapItem = new MapItem() { Width = 29, Height = 16, Size = 64 };
            _mapItem.Assets = LoadFromFile<List<MapAsset>>("Maps/chunkAssets.json");
            LoadAssets();
        }

        public void LoadTiles(string filter)
        {
            RemoveAllGameComponents();
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
                    y += chunk.Height + 1;
                }
                layout.Add(new ChunkLayout() { MapChunk = chunk, X = x, Y = y });
                x += chunk.Width + 1;
            }
            LoadChunkLayer(layout);
        }

        public void SetForceDebug(bool forceDebug)
        {
            _forceDebug = forceDebug;
        }

        #endregion Public Methods

        #region Private Methods

        private void DrawGrid()
        {
            for (int x = 0; x <= _mapItem.Width; x++)
            {
                int x1 = ScreenManager.ScaleX((int)RealBounds.X + x * _mapItem.Size);
                int y1 = ScreenManager.ScaleY((int)RealBounds.Y);
                int x2 = ScreenManager.ScaleX((int)RealBounds.X + x * _mapItem.Size);
                int y2 = ScreenManager.ScaleY((int)RealBounds.Y + _mapItem.Height * _mapItem.Size);
                GameServiceProvider.GetService<IPrimitives2D>().DrawLine(x1, y1, x2, y2, GameColor.White);
            }
            for (int y = 0; y <= _mapItem.Height; y++)
            {
                int x1 = ScreenManager.ScaleX((int)RealBounds.X);
                int y1 = ScreenManager.ScaleY((int)RealBounds.Y + y * _mapItem.Size);
                int x2 = ScreenManager.ScaleX((int)RealBounds.X + _mapItem.Width * _mapItem.Size);
                int y2 = ScreenManager.ScaleY((int)RealBounds.Y + y * _mapItem.Size);
                GameServiceProvider.GetService<IPrimitives2D>().DrawLine(x1, y1, x2, y2, GameColor.White);
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
                        ITexture texture = _mapTextures[assetIndex];
                        if (mapAsset.Type == "spritesheet")
                        {
                            AddGameComponent(new AnimatedCell(texture, mapAsset.Columns, mapAsset.Rows, mapAsset.Speed, mapAsset.DelayMin, mapAsset.DelayMax, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, mapAsset.OffsetX, mapAsset.OffsetY));
                        }
                        else if (mapAsset.Type == "multi")
                        {
                            AddGameComponent(new MultiCell(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell.GetMultiCellType(dataLines, x, y, chunk.MapChunk.Width, chunk.MapChunk.Height)));
                        }
                        else if (mapAsset.Type == "multi2")
                        {
                            AddGameComponent(new MultiCell2(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell2.GetMultiCellType(dataLines, x, y, chunk.MapChunk.Width, chunk.MapChunk.Height)));
                        }
                        else if (mapAsset.Type == "bridge")
                        {
                            AddGameComponent(new BridgeCell(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, BridgeCell.GetMultiCellType(dataLines, x, y, chunk.MapChunk.Width, chunk.MapChunk.Height)));
                        }
                        else
                        {
                            AddGameComponent(new MapCell(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, mapAsset.OffsetX, mapAsset.OffsetY));
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
            _mapTextures = new List<ITexture>();
            foreach (MapAsset asset in _mapItem.Assets)
            {
                _mapTextures.Add(GameServiceProvider.GetService<ITextureService>().Load(asset.Path));
            }
        }

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
                        ITexture texture = _blockTexture;
                        MapCell cell = new MapCell(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, 0, 0);
                        cell.Color = GameColor.Red;
                        cell.Opacity = 0.25f;
                        AddGameComponent(cell);
                    }
                    RealX++;
                }
                RealX = chunk.X;
                RealY++;
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

        private void LoadEntrances(ChunkLayout chunk)
        {
            int RealX = chunk.X;
            int RealY = chunk.Y;
            for (int y = 0; y < chunk.MapChunk.Height; y++)
            {
                for (int x = 0; x < chunk.MapChunk.Width; x++)
                {
                    int count = chunk.MapChunk.Entrances.Count(c => c.X == x && c.Y == y);

                    if (count >= 1)
                    {
                        MapChunkEntrance entrance = chunk.MapChunk.Entrances.FirstOrDefault(c => c.X == x && c.Y == y);
                        ITexture texture = _blockTexture;
                        MapCell cell = new MapCell(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, 0, 0);
                        cell.Color = GameColor.Cyan;
                        cell.Opacity = 0.35f;
                        AddGameComponent(cell);
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
                    int count = chunk.MapChunk.Spawns.Count(c => c.X == x && c.Y == y);

                    if (count >= 1)
                    {
                        MapChunkSpawn spawn = chunk.MapChunk.Spawns.FirstOrDefault(c => c.X == x && c.Y == y);
                        ITexture texture = _blockTexture;
                        MapCell cell = new MapCell(texture, RealX, RealY, RealX * _mapItem.Size + _mapItem.Size / 2, RealY * _mapItem.Size + _mapItem.Size / 2, 0, 0);
                        cell.Color = GameColor.Yellow;
                        cell.Opacity = 0.35f;
                        AddGameComponent(cell);
                    }
                    RealX++;
                }
                RealX = chunk.X;
                RealY++;
            }
        }

        #endregion Private Methods
    }
}