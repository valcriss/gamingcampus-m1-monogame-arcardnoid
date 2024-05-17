using arcardnoid.Models.Implementations;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using MonoGame.Extended.BitmapFonts;

namespace arcardnoid.Models.Services
{
    public class BitmapFontService : IBitmapFontService
    {
        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BitmapFontService(ArCardNoidGame game)
        {
            Game = game;
        }

        #endregion Public Constructors

        #region Public Methods

        public IBitmapFont Load(string assetName)
        {
            BitmapFont font = Game.Content.Load<BitmapFont>(assetName);
            return new MonoGameBitmapFont(Game, font);
        }

        #endregion Public Methods
    }
}