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

        }

        #endregion Public Constructors
    }
}