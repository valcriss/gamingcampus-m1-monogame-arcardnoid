using arcardnoid.Models.Framework;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace arcardnoid.Models.Content.Components.Map
{
    public class GameMap : Component
    {
        #region Private Fields

        private string _mapAsset;
        private MapItem _mapItem;
        private List<Texture2D> _mapTextures;

        #endregion Private Fields

        #region Public Constructors

        public GameMap(string mapAsset, int x, int y) : base("GameMap", x, y)
        {
            _mapAsset = mapAsset;
            _mapTextures = new List<Texture2D>();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            if (BaseGame.DebugMode)
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
                _mapTextures.Add(Game.Content.Load<Texture2D>(asset.Path));
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
                            Texture2D texture = _mapTextures[assetIndex];
                            if (mapAsset.Type == "spritesheet")
                            {
                                AddComponent(new AnimatedCell($"animated-cell-{x}-{y}", texture, mapAsset.Columns, mapAsset.Rows, mapAsset.Speed, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), mapAsset.OffsetX, mapAsset.OffsetY));
                            }
                            else if (mapAsset.Type == "multi")
                            {
                                AddComponent(new MultiCell($"multi-cell-{x}-{y}", texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell.GetMultiCellType(dataLines, x, y, _mapItem.Width, _mapItem.Height)));
                            }
                            else if (mapAsset.Type == "multi2")
                            {
                                AddComponent(new MultiCell2($"multi-cell2-{x}-{y}", texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), _mapItem.Size, mapAsset.OffsetX, mapAsset.OffsetY, MultiCell2.GetMultiCellType(dataLines, x, y, _mapItem.Width, _mapItem.Height)));
                            }
                            else
                            {
                                AddComponent(new MapCell($"map-cell-{x}-{y}", texture, x, y, (x * _mapItem.Size) + (_mapItem.Size / 2), (y * _mapItem.Size) + (_mapItem.Size / 2), mapAsset.OffsetX, mapAsset.OffsetY));
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
                Primitives2D.DrawLine(Game.SpriteBatch, (int)RealPosition.X + (x * _mapItem.Size), (int)RealPosition.Y, (int)RealPosition.X + (x * _mapItem.Size), (int)RealPosition.Y + (_mapItem.Height * _mapItem.Size), Color.White);
            }
            for (int y = 0; y <= _mapItem.Height; y++)
            {
                Primitives2D.DrawLine(Game.SpriteBatch, (int)RealPosition.X, (int)RealPosition.Y + (y * _mapItem.Size), (int)RealPosition.X + (_mapItem.Width * _mapItem.Size), (int)RealPosition.Y + (y * _mapItem.Size), Color.White);
            }
        }

        #endregion Private Methods
    }
}