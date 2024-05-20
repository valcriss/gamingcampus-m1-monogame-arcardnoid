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
    public class StaticMap : GameComponent
    {
        #region Private Properties


        #endregion Private Properties

        #region Protected Fields

        protected MapItem _mapItem;
        protected List<ITexture> _mapTextures;

        #endregion Protected Fields

        #region Private Fields

        private bool _debug;
        private string _mapAsset;

        #endregion Private Fields

        #region Public Constructors

        public StaticMap(string mapAsset, int x, int y, bool debug = false) : base(x, y)
        {
            _mapAsset = mapAsset;
            _mapTextures = new List<ITexture>();
            _debug = debug;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            if (_debug)
            {
                DrawGrid();
            }
        }

        public override void Load()
        {
            base.Load();
            string mapContent = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _mapAsset));
            _mapItem = JsonConvert.DeserializeObject<MapItem>(mapContent);
            foreach (MapAsset asset in _mapItem.Assets)
            {
                _mapTextures.Add(GameServiceProvider.GetService<ITextureService>().Load(asset.Path));
            }
            foreach (MapLayer layer in _mapItem.Layers)
            {
                int[,] dataLines = layer.Data.ToMapArray(_mapItem.Width, _mapItem.Height);
                for (int y = 0; y < _mapItem.Height; y++)
                {
                    for (int x = 0; x < _mapItem.Width; x++)
                    {
                        int assetIndex = dataLines[x, y];
                        if (assetIndex != -1)
                        {
                            MapAsset mapAsset = _mapItem.Assets[assetIndex];
                            ITexture texture = _mapTextures[assetIndex];
                            if (mapAsset.Type == "spritesheet")
                            {
                                AddGameComponent(new AnimatedCell(texture, mapAsset.Columns, mapAsset.Rows, mapAsset.Speed, mapAsset.DelayMin, mapAsset.DelayMax, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), mapAsset.OffsetX, mapAsset.OffsetY));
                            }
                            else if (mapAsset.Type == "multi")
                            {
                                AddGameComponent(new MultiCell(texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell.GetMultiCellType(dataLines, x, y, _mapItem.Width, _mapItem.Height)));
                            }
                            else if (mapAsset.Type == "multi2")
                            {
                                AddGameComponent(new MultiCell2(texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell2.GetMultiCellType(dataLines, x, y, _mapItem.Width, _mapItem.Height)));
                            }
                            else if (mapAsset.Type == "bridge")
                            {
                                AddGameComponent(new BridgeCell(texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, BridgeCell.GetMultiCellType(dataLines, x, y, _mapItem.Width, _mapItem.Height)));
                            }
                            else
                            {
                                AddGameComponent(new MapCell(texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), mapAsset.OffsetX, mapAsset.OffsetY));
                            }
                        }
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void DrawGrid()
        {
            for (int x = 0; x <= _mapItem.Width; x++)
            {
                int x1 = ScreenManager.ScaleX((int)RealBounds.X + (x * _mapItem.Size));
                int y1 = ScreenManager.ScaleY((int)RealBounds.Y);
                int x2 = ScreenManager.ScaleX((int)RealBounds.X + (x * _mapItem.Size));
                int y2 = ScreenManager.ScaleY((int)RealBounds.Y + (_mapItem.Height * _mapItem.Size));
                GameServiceProvider.GetService<IPrimitives2D>().DrawLine(x1, y1, x2, y2, GameColor.White);
            }
            for (int y = 0; y <= _mapItem.Height; y++)
            {
                int x1 = ScreenManager.ScaleX((int)RealBounds.X);
                int y1 = ScreenManager.ScaleY((int)RealBounds.Y + (y * _mapItem.Size));
                int x2 = ScreenManager.ScaleX((int)RealBounds.X + (_mapItem.Width * _mapItem.Size));
                int y2 = ScreenManager.ScaleY((int)RealBounds.Y + (y * _mapItem.Size));
                GameServiceProvider.GetService<IPrimitives2D>().DrawLine(x1, y1, x2, y2, GameColor.White);
            }
        }

        #endregion Private Methods
    }
}