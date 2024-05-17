using arcardnoid.Models.Implementations;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace arcardnoid.Models.Services
{
    public class TextureService : ITextureService
    {
        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public TextureService(ArCardNoidGame game)
        {
            Game = game;
        }

        #endregion Public Constructors

        #region Public Methods

        public ITexture Load(string assetName)
        {
            Texture2D texture = Game.Content.Load<Texture2D>(assetName);
            return new MonoGameTexture(Game, texture, assetName);
        }

        #endregion Public Methods
    }
}