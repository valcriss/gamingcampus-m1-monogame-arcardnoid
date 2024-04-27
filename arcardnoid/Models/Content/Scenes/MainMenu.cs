using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Content.Scenes
{
    public class MainMenu : Scene
    {
        #region Public Constructors

        public MainMenu()
        {
            BackgroundColor = Color.FromNonPremultiplied(71, 171, 169, 255);
            AddComponent(new Components.MainMenu.MainMenuBackground());
            AddComponent(new Components.UI.AnimatedTitleBand("ui/bandeau", "fonts/band", "ArCardNoid", 0.09f, 960, 100,Color.FromNonPremultiplied(75, 30, 0,255)));
        }

        #endregion Public Constructors
    }
}