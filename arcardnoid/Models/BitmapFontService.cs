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
    public class BitmapFontService : IBitmapFontService
    {
        private ArCardNoidGame Game { get; set; }

        public BitmapFontService(ArCardNoidGame game)
        {
            Game = game;
        }

        public IBitmapFont Load(string assetName)
        {
            BitmapFont font = Game.Content.Load<BitmapFont>(assetName);
            return new MonoGameBitmapFont(Game, font);
        }
    }
}
