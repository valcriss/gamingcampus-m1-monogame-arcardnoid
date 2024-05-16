using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class GraphicsManager : IScreenManager
    {
        private ArCardNoidGame Game { get; set; }
        public GraphicsManager(ArCardNoidGame game)
        {
            Game = game;
        }

        public void Clear(GameColor color)
        {
            Game.Graphics.GraphicsDevice.Clear(color.ToXnaColor());
        }

        public Point GetSize()
        {
            return new Point(Game.Graphics.PreferredBackBufferWidth, Game.Graphics.PreferredBackBufferHeight);
        }

        public void SetSize(Point size)
        {
            Game.Graphics.PreferredBackBufferWidth = (int)size.X;
            Game.Graphics.PreferredBackBufferHeight = (int)size.Y;
            Game.Graphics.ApplyChanges();
        }
    }
}
