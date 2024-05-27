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
            AddGameComponent(new BitmapText(BitmapFontType.Title, "Daniel Silvestre", 960, 700, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            AddGameComponent(new BitmapText(BitmapFontType.Subtitle, "Programmation avancée C# et Monogame", 960, 850, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            AddGameComponent(new SpriteSheetImage(TextureType.LOGO_LOGO, 6, 5, 0.03f, 960, 400));
            AddGameComponent(new TimeOutAction(4, ChangeScene));
        }

        #endregion Public Constructors

        #region Private Methods

        private void ChangeScene()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new MainMenu());
        }

        #endregion Private Methods
    }
}