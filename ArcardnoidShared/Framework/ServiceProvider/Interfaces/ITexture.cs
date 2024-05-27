using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface ITexture
    {
        #region Public Properties

        int Height { get; }
        TextureType TextureType { get; }
        int Width { get; }

        #endregion Public Properties

        #region Public Methods

        void DrawTexture(Rectangle destination, Rectangle? source, GameColor color, float rotation, Point origin);

        #endregion Public Methods
    }
}