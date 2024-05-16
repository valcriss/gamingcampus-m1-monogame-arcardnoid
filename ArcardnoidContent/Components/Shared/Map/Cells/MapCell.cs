using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.Shared.Map.Cells
{
    public class MapCell : GameComponent
    {
        #region Protected Properties

        protected Rectangle DrawBounds { get; set; } = Rectangle.Empty;
        protected Point DrawOrigin { get; set; } = Point.Zero;
        protected int GridX { get; set; }
        protected int GridY { get; set; }

        protected Rectangle ImageBounds { get; set; } = Rectangle.Empty;
        protected int OffsetX { get; set; }
        protected int OffsetY { get; set; }
        protected Point Origin { get; set; } = Point.Zero;
        protected ITexture Texture2D { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public MapCell(ITexture texture, int x, int y, int realX, int realY, int offsetX, int offsetY) : base(realX, realY)
        {
            GridX = x;
            GridY = y;
            OffsetX = offsetX;
            OffsetY = offsetY;
            Texture2D = texture;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            Texture2D.DrawTexture(DrawBounds, ImageBounds, Color, Rotation, Origin);
            base.Draw();
        }

        public override void Load()
        {
            Bounds = new Rectangle(Bounds.X, Bounds.Y, Texture2D.Width, Texture2D.Height);
            ImageBounds = new Rectangle(0, 0, Texture2D.Width, Texture2D.Height);
            Origin = new Point(Texture2D.Width / 2, Texture2D.Height / 2);
            DrawOrigin = new Point(0, 0);
            UpdateRenderBounds();
            base.Load();
        }

        #endregion Public Methods

        #region Protected Methods

        protected void UpdateRenderBounds()
        {
            DrawBounds = new Rectangle(RealBounds.X - DrawOrigin.X + OffsetX, RealBounds.Y - DrawOrigin.Y + OffsetY, RealBounds.Width, RealBounds.Height);
        }

        #endregion Protected Methods
    }
}