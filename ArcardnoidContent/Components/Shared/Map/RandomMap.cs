using arcardnoid.Models.Framework.Tools;
using ArcardnoidContent.Components.GameScene;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
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
    public class RandomMap : GameComponent
    {
        #region Public Events

        public event Action<Point> OnMapClickedEvent;

        #endregion Public Events

        #region Public Properties

        public MapHypothesis MapHypothesis { get; set; }
        public int MouseX { get; set; }
        public int MouseY { get; set; }

        #endregion Public Properties

        #region Private Properties

        private IPrimitives2D Primitives2D => GameServiceProvider.GetService<IPrimitives2D>();

        #endregion Private Properties

        #region Private Fields

        private ITexture _blockTexture;
        private DateTime _clickTime = DateTime.Now;
        private bool _forceDebug;
        private MapItem _mapItem;
        private List<ITexture> _mapTextures = new List<ITexture>();

        #endregion Private Fields

        #region Public Constructors

        public RandomMap(MapHypothesis mapHypotesis, bool forceDebug = false) : base(mapHypotesis.PositionX, mapHypotesis.PositionY, mapHypotesis.Width * 64, mapHypotesis.Height * 64)
        {
            _forceDebug = forceDebug;
            MapHypothesis = mapHypotesis;
        }

        #endregion Public Constructors

        #region Public Methods

        public static T LoadFromFile<T>(string filename) where T : class
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
                return null;
            }
        }

        public void ClearCell(string asset, Point cell)
        {
            MapHypothesis.FinalChunk.ClearActor((int)cell.X, (int)cell.Y);
            foreach (GameComponent component in GameComponents)
            {
                if (component is MapCell mapCell && mapCell.TextureAsset == asset && mapCell.GridX == cell.X && mapCell.GridY == cell.Y)
                {
                    component.InnerUnload();
                    return;
                }
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

        public List<Point>? GetPath(int playerPositionX, int playerPositionY, int x, int y)
        {
            Point source = new Point(playerPositionX, playerPositionY);
            Point destination = new Point(x, y);
            if (!isValid(source) || !isValid(destination)) return null;
            List<Point> done = new List<Point>();
            List<Point> path = new List<Point>
            {
                source
            };
            Queue<List<Point>> queue = new Queue<List<Point>>();
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                List<Point> currentPath = queue.Dequeue();
                Point currentPoint = currentPath.Last();
                if (currentPoint == destination) return currentPath;
                foreach (Point point in GetNeighbors(currentPoint).OrderBy(c => c.Distance(destination)))
                {
                    if (currentPath.Contains(point) || done.Contains(point)) continue;
                    done.Add(point);
                    List<Point> newPath = new List<Point>(currentPath)
                    {
                        point
                    };
                    queue.Enqueue(newPath);
                }
            }
            return null;
        }

        public override void Load()
        {
            base.Load();
            _blockTexture = GameServiceProvider.GetService<ITextureService>().Load("map/halt");
            _mapItem = new MapItem() { Width = MapHypothesis.Width, Height = MapHypothesis.Height, Size = 64 };
            _mapItem.Assets = LoadFromFile<List<MapAsset>>("Maps/chunkAssets.json");
            LoadAssets();
            LoadTiles();
        }

        public void LoadTiles()
        {
            RemoveAllGameComponents();
            LoadChunkLayer(new ChunkLayout() { MapChunk = MapHypothesis.FinalChunk, X = 0, Y = 0 });
        }

        public void ToggleDebug()
        {
            _forceDebug = !_forceDebug;
            LoadTiles();
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
            Point mousePosition = ScreenManager.UIScale(mouseService.GetMousePosition());
            if (Bounds.Contains(mousePosition))
            {
                MouseX = (int)((mousePosition.X - Bounds.X) / _mapItem.Size);
                MouseY = (int)((mousePosition.Y - Bounds.Y) / _mapItem.Size);
                if (mouseService.IsMouseLeftButtonPressed() && DateTime.Now.Subtract(_clickTime).TotalMilliseconds > 200)
                {
                    _clickTime = DateTime.Now;
                    OnMapClickedEvent?.Invoke(new Point(MouseX, MouseY));
                }
            }
            else
            {
                MouseX = -1;
                MouseY = -1;
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal GroundType GetGroundType(Point cell)
        {
            MapLayer terrainLayer1 = MapHypothesis.FinalChunk.Layers.FirstOrDefault(c => c.Name == "Terrain Layer 1");
            string value = terrainLayer1.GetLayerData((int)cell.X, (int)cell.Y);
            if (value != string.Empty) { return int.Parse(value) == 1 ? GroundType.Sand : GroundType.Grass; }
            MapLayer terrainLayer2 = MapHypothesis.FinalChunk.Layers.FirstOrDefault(c => c.Name == "Terrain Layer 2");
            string value2 = terrainLayer2.GetLayerData((int)cell.X, (int)cell.Y);
            if (value2 != string.Empty) { return int.Parse(value2) == 1 ? GroundType.Sand : GroundType.Grass; }
            return GroundType.Grass;
        }

        #endregion Internal Methods

        #region Private Methods

        private void DrawGrid()
        {
            for (int x = 0; x <= _mapItem.Width; x++)
            {
                int x1 = ScreenManager.ScaleX((int)RealBounds.X + x * _mapItem.Size);
                int y1 = ScreenManager.ScaleY((int)RealBounds.Y);
                int x2 = ScreenManager.ScaleX((int)RealBounds.X + x * _mapItem.Size);
                int y2 = ScreenManager.ScaleY((int)RealBounds.Y + _mapItem.Height * _mapItem.Size);
                Primitives2D.DrawLine(x1, y1, x2, y2, GameColor.White);
            }
            for (int y = 0; y <= _mapItem.Height; y++)
            {
                int x1 = ScreenManager.ScaleX((int)RealBounds.X);
                int y1 = ScreenManager.ScaleY((int)RealBounds.Y + y * _mapItem.Size);
                int x2 = ScreenManager.ScaleX((int)RealBounds.X + _mapItem.Width * _mapItem.Size);
                int y2 = ScreenManager.ScaleY((int)RealBounds.Y + y * _mapItem.Size);
                Primitives2D.DrawLine(x1, y1, x2, y2, GameColor.White);
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

        private List<Point> GetNeighbors(Point point)
        {
            List<Point> neighbors = new List<Point>();
            if (isValid(new Point(point.X - 1, point.Y))) neighbors.Add(new Point(point.X - 1, point.Y));
            if (isValid(new Point(point.X + 1, point.Y))) neighbors.Add(new Point(point.X + 1, point.Y));
            if (isValid(new Point(point.X, point.Y - 1))) neighbors.Add(new Point(point.X, point.Y - 1));
            if (isValid(new Point(point.X, point.Y + 1))) neighbors.Add(new Point(point.X, point.Y + 1));
            return neighbors;
        }

        private bool isValid(Point point)
        {
            if (!(point.X >= 0 && point.X < _mapItem.Width && point.Y >= 0 && point.Y < _mapItem.Height)) return false;
            return MapHypothesis.FinalChunk.Blocks.IsEmpty((int)point.X, (int)point.Y);
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

        private void LoadChunkLayer(ChunkLayout layout)
        {
            foreach (MapLayer layer in layout.MapChunk.Layers)
            {
                DrawTiles(layer, layout);
            }
            if (_forceDebug) LoadBlocks(layout);
            if (_forceDebug) LoadEntrances(layout);
            if (_forceDebug) LoadSpawns(layout);
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