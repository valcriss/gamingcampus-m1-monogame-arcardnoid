using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.Components.Images
{
    public class ImagePart : Image
    {
        #region Private Properties

        private Rectangle? SourceRect { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public ImagePart(string spriteAsset, int x, int y, Rectangle? sourceRect) : base(spriteAsset, x, y)
        {
            SourceRect = sourceRect;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            Bounds = new Rectangle(Bounds.X, Bounds.Y, SourceRect == null ? 0 : SourceRect.Width, SourceRect == null ? 0 : SourceRect.Height);
            ImageBounds = SourceRect ?? Rectangle.Empty;
            Origin = SourceRect == null ? Point.Zero : new Point(SourceRect.Width / 2, SourceRect.Height / 2);
            UpdateRenderBounds();
        }

        public void SetSourceRect(Rectangle sourceRect)
        {
            SourceRect = sourceRect;
            ImageBounds = SourceRect;
            Origin = new Point(SourceRect.Width / 2, SourceRect.Height / 2);
            Bounds = new Rectangle(Bounds.X, Bounds.Y, SourceRect.Width, SourceRect.Height);
            UpdateRenderBounds();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void UpdateRenderBounds()
        {
            DrawBounds = new Rectangle(RealBounds.X - DrawOrigin.X, RealBounds.Y - DrawOrigin.Y, ImageBounds.Width, ImageBounds.Height);
        }

        #endregion Protected Methods
    }
}