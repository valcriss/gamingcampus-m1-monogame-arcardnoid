using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Scenes
{
    public class Scene : GameComponentContainer
    {
        #region Protected Properties

        protected GameColor BackgroundColor { get; set; } = GameColor.Black;

        #endregion Protected Properties

        #region Public Methods

        public override void Draw()
        {
            GameServiceProvider.GetService<IScreenManager>().Clear(BackgroundColor);
            base.Draw();
        }

        #endregion Public Methods
    }
}