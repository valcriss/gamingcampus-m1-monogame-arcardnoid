using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IBitmapFont
    {
        #region Public Methods

        void DrawString(string text, Point position, GameColor color, float rotation, float scale);

        Point MeasureString(string text);

        #endregion Public Methods
    }
}