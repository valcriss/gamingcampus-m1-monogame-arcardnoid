using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;


namespace ArcardnoidContent.Components.GameScene.UI
{
    public class DialogBackground : GameComponent
    {
        private IPrimitives2D Primitives2D => GameServiceProvider.GetService<IPrimitives2D>();

        public DialogBackground() : base(0, 0, 1920, 1080)
        {
            Color = GameColor.Black;
            Opacity = 0;
        }

        public override void Draw()
        {
            base.Draw();
            Primitives2D.FillRectangle(Bounds, Color);
        }
    }
}
