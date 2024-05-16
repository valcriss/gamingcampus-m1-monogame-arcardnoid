using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IMouseService
    {
        #region Public Methods

        Point GetMousePosition();

        bool IsMouseLeftButtonPressed();

        void Update();

        #endregion Public Methods
    }
}