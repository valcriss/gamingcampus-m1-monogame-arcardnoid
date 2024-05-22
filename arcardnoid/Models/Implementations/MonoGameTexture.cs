using arcardnoid.Models.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Implementations
{
    public class MonoGameTexture : ITexture
    {
        #region Public Properties

        public string AssetPath => _assetPath;
        public int Height => _texture.Height;
        public int Width => _texture.Width;

        #endregion Public Properties

        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Private Fields

        private readonly string _assetPath;
        private readonly Texture2D _texture;

        #endregion Private Fields

        #region Public Constructors

        public MonoGameTexture(ArCardNoidGame game, Texture2D texture, string assetPath)
        {
            _assetPath = assetPath;
            _texture = texture;
            Game = game;
        }

        #endregion Public Constructors

        #region Public Methods

        public void DrawTexture(ArcardnoidShared.Framework.Drawing.Rectangle destination, ArcardnoidShared.Framework.Drawing.Rectangle source, GameColor color, float rotation, ArcardnoidShared.Framework.Drawing.Point origin)
        {
            ArcardnoidShared.Framework.Drawing.Rectangle sourceRect = source ?? new ArcardnoidShared.Framework.Drawing.Rectangle(0, 0, _texture.Width, _texture.Height);
            Game.SpriteBatch.Draw(_texture, ScreenManager.Scale(destination).ToXnaRectangle().ToRectangle(), sourceRect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), MathHelper.ToRadians(rotation), origin.ToVector2(), SpriteEffects.None, 0);
        }

        #endregion Public Methods
    }
}