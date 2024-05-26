using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Components.Images
{
    public class Image : GameComponent
    {
        #region Public Properties

        public override Point Position
        {
            get { return base.Bounds.Position; }
            set { base.Position = value; UpdateRenderBounds(); }
        }

        #endregion Public Properties

        #region Protected Properties

        protected Rectangle DrawBounds { get; set; } = Rectangle.Empty;
        protected Point DrawOrigin { get; set; }
        protected Rectangle ImageBounds { get; set; } = Rectangle.Empty;
        protected ITexture? ImageTexture { get; set; }
        protected Point Origin { get; set; }

        #endregion Protected Properties

        #region Private Fields

        private readonly string _imageAsset;

        #endregion Private Fields

        #region Public Constructors

        public Image(string imageAsset, int x, int y) : base(x, y)
        {
            _imageAsset = imageAsset;
            DrawOrigin = Point.Zero;
            Origin = Point.Zero;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            if (ImageTexture == null) return;
            ImageTexture.DrawTexture(ScreenManager.Scale(DrawBounds), ImageBounds, Color, Rotation, Origin);
            base.Draw();
        }

        public override void Load()
        {
            ImageTexture = GameServiceProvider.GetService<ITextureService>().Load(_imageAsset);
            Bounds = new Rectangle(Bounds.X, Bounds.Y, ImageTexture.Width, ImageTexture.Height);
            ImageBounds = new Rectangle(0, 0, ImageTexture.Width, ImageTexture.Height);
            Origin = new Point(ImageTexture.Width / 2, ImageTexture.Height / 2);
            DrawOrigin = new Point(0, 0);
            UpdateRenderBounds();
            base.Load();
        }

        public virtual void UpdateRenderBounds()
        {
            DrawBounds = new Rectangle(RealBounds.X - DrawOrigin.X, RealBounds.Y - DrawOrigin.Y, RealBounds.Width, RealBounds.Height);
        }

        #endregion Public Methods
    }
}