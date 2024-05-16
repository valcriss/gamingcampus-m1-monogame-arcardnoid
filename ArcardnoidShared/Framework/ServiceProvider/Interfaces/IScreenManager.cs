using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IScreenManager
    {
        #region Public Methods

        void Clear(GameColor color);

        void SetSize(Point size);

        Point GetSize();

        #endregion Public Methods
    }
}