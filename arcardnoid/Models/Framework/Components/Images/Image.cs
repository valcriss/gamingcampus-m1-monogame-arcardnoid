using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Framework.Components.Images
{
    public class Image : Component
    {
        #region Public Properties

        public override Vector2 Position
        { get => base.Position; set { base.Position = value; UpdateRenderBounds(); } }

        #endregion Public Properties

        #region Protected Properties

        protected RectangleF DrawBounds { get; set; }
        protected Vector2 DrawOrigin { get; set; }
        protected Rectangle ImageBounds { get; set; }
        protected Vector2 Origin { get; set; }
        protected Texture2D Sprite { get; set; }

        #endregion Protected Properties

        #region Private Fields

        private string _spriteAsset;

        #endregion Private Fields

        #region Public Constructors

        public Image(string name, string spriteAsset, int x, int y) : base(name, x, y)
        {
            _spriteAsset = spriteAsset;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            Game.SpriteBatch.Draw(Sprite, ScreenManager.Scale(DrawBounds.ToRectangle()), ImageBounds, Color, MathHelper.ToRadians(Rotation), Origin, SpriteEffects.None, 0);
            base.Draw();
        }

        public override void Load()
        {
            Sprite = Game.Content.Load<Texture2D>(_spriteAsset);
            Bounds = new RectangleF(Bounds.X, Bounds.Y, Sprite.Width, Sprite.Height);
            ImageBounds = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            DrawOrigin = new Vector2(0, 0);
            UpdateRenderBounds();
            base.Load();
        }

        #endregion Public Methods

        #region Protected Methods

        protected void UpdateRenderBounds()
        {
            DrawBounds = new RectangleF(RealBounds.X - DrawOrigin.X, RealBounds.Y - DrawOrigin.Y, RealBounds.Width, RealBounds.Height);
        }

        #endregion Protected Methods
    }
}