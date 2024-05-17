using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IScreenManager
    {
        #region Public Methods

        void Clear(GameColor color);

        Point GetSize();

        void SetSize(Point size);

        #endregion Public Methods
    }
}