using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Framework.Scenes
{
    public abstract class Scene : ComponentContainer
    {
        #region Protected Properties

        protected Color BackgroundColor { get; set; } = Color.White;

        #endregion Protected Properties

        #region Public Methods

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(BackgroundColor);
            base.Draw();
        }

        #endregion Public Methods
    }
}