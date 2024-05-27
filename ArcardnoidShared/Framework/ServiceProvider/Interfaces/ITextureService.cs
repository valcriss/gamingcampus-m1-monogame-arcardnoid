using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface ITextureService
    {
        #region Public Methods

        ITexture Load(TextureType textureType);

        #endregion Public Methods
    }
}