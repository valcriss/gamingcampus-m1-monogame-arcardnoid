using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface ITexture
    {
        #region Public Properties

        string AssetPath { get; }
        int Height { get; }
        int Width { get; }

        #endregion Public Properties

        #region Public Methods

        void DrawTexture(Rectangle destination, Rectangle? source, GameColor color, float rotation, Point origin);

        #endregion Public Methods
    }
}