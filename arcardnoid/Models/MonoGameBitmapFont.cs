using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;

namespace arcardnoid.Models
{
    public class MonoGameBitmapFont : IBitmapFont
    {
        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Private Fields

        private BitmapFont _bitmapFont;

        #endregion Private Fields

        #region Public Constructors

        public MonoGameBitmapFont(ArCardNoidGame game, BitmapFont bitmapFont)
        {
            Game = game;
            _bitmapFont = bitmapFont;
        }

        #endregion Public Constructors

        #region Public Methods

        public void DrawString(string text, ArcardnoidShared.Framework.Drawing.Point position, GameColor color, float rotation, float scale)
        {
            Game.SpriteBatch.DrawString(_bitmapFont, text, ScreenManager.Scale(position).ToVector2(), color.ToXnaColor(), 0, Vector2.Zero, ScreenManager.Scale(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }

        public ArcardnoidShared.Framework.Drawing.Point MeasureString(string text)
        {
            MonoGame.Extended.Size2 p = _bitmapFont.MeasureString(text);
            return new ArcardnoidShared.Framework.Drawing.Point(p.Width, p.Height);
        }

        #endregion Public Methods
    }
}