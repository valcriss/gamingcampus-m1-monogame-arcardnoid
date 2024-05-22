using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class DialogBackground : GameComponent
    {
        #region Private Properties

        private static IPrimitives2D Primitives2D => GameServiceProvider.GetService<IPrimitives2D>();

        #endregion Private Properties

        #region Public Constructors

        public DialogBackground() : base(0, 0, 1920, 1080)
        {
            Color = GameColor.Black;
            Opacity = 0;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            Primitives2D.FillRectangle(Bounds, Color);
        }

        #endregion Public Methods
    }
}