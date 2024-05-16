using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class MonoGameTexture : ITexture
    {
        private Texture2D _texture;
        private ArCardNoidGame Game { get; set; }

        public MonoGameTexture(ArCardNoidGame game, Texture2D texture)
        {
            _texture = texture;
            Game = game;
        }

        public int Height => _texture.Height;

        public int Width => _texture.Width;

        public void DrawTexture(ArcardnoidShared.Framework.Drawing.Rectangle destination, ArcardnoidShared.Framework.Drawing.Rectangle? source, GameColor color, float rotation, ArcardnoidShared.Framework.Drawing.Point origin)
        {
            ArcardnoidShared.Framework.Drawing.Rectangle sourceRect = source != null ? source : new ArcardnoidShared.Framework.Drawing.Rectangle(0, 0, _texture.Width, _texture.Height);
            Game.SpriteBatch.Draw(_texture, ScreenManager.Scale(destination).ToXnaRectangle().ToRectangle(), sourceRect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), MathHelper.ToRadians(rotation), origin.ToVector2(), SpriteEffects.None, 0);
        }
    }
}
