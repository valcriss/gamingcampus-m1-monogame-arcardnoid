using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class MonoGameBitmapFont : IBitmapFont
    {
        private BitmapFont _bitmapFont;
        private ArCardNoidGame Game { get; set; }

        public MonoGameBitmapFont(ArCardNoidGame game, BitmapFont bitmapFont)
        {
            Game = game;
            _bitmapFont = bitmapFont;
        }

        public ArcardnoidShared.Framework.Drawing.Point MeasureString(string text)
        {
            MonoGame.Extended.Size2 p = _bitmapFont.MeasureString(text);
            return new ArcardnoidShared.Framework.Drawing.Point(p.Width, p.Height);
        }

        public void DrawString(string text, ArcardnoidShared.Framework.Drawing.Point position, GameColor color, float rotation, float scale)
        {
            Game.SpriteBatch.DrawString(_bitmapFont, text, ScreenManager.Scale(position).ToVector2(), color.ToXnaColor(), 0, Vector2.Zero, ScreenManager.Scale(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }
    }
}
