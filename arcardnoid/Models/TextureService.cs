using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class TextureService : ITextureService
    {
        private ArCardNoidGame Game { get; set; }

        public TextureService(ArCardNoidGame game)
        {
            Game = game;
        }

        public ITexture Load(string assetName)
        {
            Texture2D texture = Game.Content.Load<Texture2D>(assetName);
            return new MonoGameTexture(Game, texture);
        }
    }
}
