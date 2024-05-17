namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface ITextureService
    {
        #region Public Methods

        ITexture Load(string assetName);

        #endregion Public Methods
    }
}