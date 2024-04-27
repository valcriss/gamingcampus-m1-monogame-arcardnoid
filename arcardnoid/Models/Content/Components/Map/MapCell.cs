using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Content.Components.Map
{
    public class MapCell : Component
    {
        #region Protected Properties

        protected RectangleF DrawBounds { get; set; }
        protected Vector2 DrawOrigin { get; set; }
        protected int GridX { get; set; }
        protected int GridY { get; set; }

        protected Rectangle ImageBounds { get; set; }
        protected int OffsetX { get; set; }
        protected int OffsetY { get; set; }
        protected Vector2 Origin { get; set; }
        protected Texture2D Texture2D { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public MapCell(string name, Texture2D texture, int x, int y, int realX, int realY, int offsetX, int offsetY) : base(name, realX, realY)
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
            Game.SpriteBatch.Draw(Texture2D, DrawBounds.ToRectangle(), ImageBounds, Color, MathHelper.ToRadians(Rotation), Origin, SpriteEffects.None, 0);
            base.Draw();
        }

        public override void Load()
        {
            Bounds = new RectangleF(Bounds.X, Bounds.Y, Texture2D.Width, Texture2D.Height);
            ImageBounds = new Rectangle(0, 0, Texture2D.Width, Texture2D.Height);
            Origin = new Vector2(Texture2D.Width / 2, Texture2D.Height / 2);
            DrawOrigin = new Vector2(0, 0);
            UpdateRenderBounds();
            base.Load();
        }

        #endregion Public Methods

        #region Protected Methods

        protected void UpdateRenderBounds()
        {
            DrawBounds = new RectangleF(RealBounds.X - DrawOrigin.X + OffsetX, RealBounds.Y - DrawOrigin.Y + OffsetY, RealBounds.Width, RealBounds.Height);
        }

        #endregion Protected Methods
    }
}