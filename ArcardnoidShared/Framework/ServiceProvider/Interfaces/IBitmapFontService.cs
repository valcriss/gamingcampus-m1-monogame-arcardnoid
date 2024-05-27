using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IBitmapFontService
    {
        #region Public Methods

        IBitmapFont Load(BitmapFontType fontType);

        #endregion Public Methods
    }
}