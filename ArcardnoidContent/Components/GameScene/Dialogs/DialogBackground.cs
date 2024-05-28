using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class DialogBackground : GameComponent
    {
        #region Public Constructors

        public DialogBackground(float opacity = 0) : base(0, 0, 1920, 1080)
        {
            Color = GameColor.Black;
            Opacity = opacity;
            AddGameComponent(new Primitive2D((primitive) =>
            {
                primitive.FillRectangle(Bounds, Color);
            }));
        }

        #endregion Public Constructors
    }
}