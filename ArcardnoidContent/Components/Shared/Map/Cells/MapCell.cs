using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.Shared.Map.Cells
{
    public class MapCell : GameComponent
    {
        #region Public Properties

        public int GridX { get; set; }
        public int GridY { get; set; }
        public TextureType? TextureAsset => Texture2D?.TextureType;

        #endregion Public Properties

        #region Protected Properties

        protected Rectangle DrawBounds { get; set; } = Rectangle.Empty;
        protected Point DrawOrigin { get; set; } = Point.Zero;
        protected Rectangle ImageBounds { get; set; } = Rectangle.Empty;
        protected int OffsetX { get; set; }
        protected int OffsetY { get; set; }
        protected Point Origin { get; set; } = Point.Zero;
        protected ITexture Texture2D { get; set; }

        #endregion Protected Properties

        #region Public Fields

        public static string ARCHER_ASSET = "map/units/archer-blue-idle";
        public static string GOLD_ASSET = "map/units/gold";
        public static string MEAT_ASSET = "map/units/meat";

        public static string SHEEP_ASSET = "map/units/sheep-idle";
        public static string TNT_ASSET = "map/units/tnt-red-idle";
        public static string TORCH_ASSET = "map/units/torch-red-idle";
        public static string WARRIOR_ASSET = "map/units/warrior-blue-idle";

        #endregion Public Fields

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

        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateRenderBounds();
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