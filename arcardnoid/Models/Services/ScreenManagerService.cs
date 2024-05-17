using arcardnoid.Models.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace arcardnoid.Models.Services
{
    public class ScreenManagerService : IScreenManager
    {
        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public ScreenManagerService(ArCardNoidGame game)
        {
            Game = game;
        }

        #endregion Public Constructors

        #region Public Methods

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

        #endregion Public Methods
    }
}