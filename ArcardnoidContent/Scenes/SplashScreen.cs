using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Scenes
{
    public class SplashScreen : Scene
    {
        #region Public Constructors

        public SplashScreen()
        {
            BackgroundColor = new GameColor(127, 178, 255);
            AddGameComponent(new BitmapText("Fonts/title-font", "Daniel Silvestre", 960, 700, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            AddGameComponent(new BitmapText("Fonts/subtitle-font", "Programmation avancée C# et Monogame", 960, 850, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            AddGameComponent(new SpriteSheetImage("logo/logo", 6, 5, 0.03f, 960, 400));
            AddGameComponent(new TimeOutAction(4, ChangeScene));
        }

        private void ChangeScene()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new MainMenu());
        }

        #endregion Public Constructors
    }
}