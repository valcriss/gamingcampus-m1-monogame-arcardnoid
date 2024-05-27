using arcardnoid.Models.Implementations;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using MonoGame.Extended.BitmapFonts;
using System.ComponentModel;

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

        public IBitmapFont Load(BitmapFontType fontType)
        {
            BitmapFont font = Game.Content.Load<BitmapFont>(GetAssetName(fontType));
            return new MonoGameBitmapFont(Game, font);
        }

        #endregion Public Methods

        #region Private Methods

        private string GetAssetName(BitmapFontType fontType)
        {
            return fontType switch
            {
                BitmapFontType.Default => "fonts/band",
                BitmapFontType.Title => "fonts/title-font",
                BitmapFontType.Subtitle => "fonts/subtitle-font",
                BitmapFontType.Simple => "fonts/ken",
                BitmapFontType.Regular => "fonts/regular",
                _ => throw new InvalidEnumArgumentException(),
            };
        }

        #endregion Private Methods
    }
}