using arcardnoid.Models.Content.Components.MainMenu;
using arcardnoid.Models.Framework;
using arcardnoid.Models.Framework.Animations;
using arcardnoid.Models.Framework.Components.Profiler;
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
            AddComponent(new MainMenuBackground());
            AddComponent(new Components.UI.AnimatedTitleBand("ui/bandeau", "fonts/band", "ArCardNoid : The Battle of the Bouncing Balls", 0.05f, 960, 100, Color.FromNonPremultiplied(75, 30, 0, 255)));
            AddComponent(new MainGameMenu(672, 316)).AddAnimation(new MoveAnimation("MainGameMenuMove", 1f, new Vector2(672, 1081), new Vector2(672, 316), false, true));
            if (BaseGame.DebugMode) AddComponent(new ProfilerComponent());
        }

        #endregion Public Constructors
    }
}