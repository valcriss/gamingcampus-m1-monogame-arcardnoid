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
            MainGameMenu main = AddComponent(new MainGameMenu(672, 316)).AddAnimations<MainGameMenu>(new AnimationChain(new Animation[]
            {
                new MoveAnimation("MainGameMenuMove", 3f, new Vector2(672, 1181), new Vector2(672, 316), false, true, Framework.Easing.EaseType.InOutElastic),
            }, false, true)).AddAnimations<MainGameMenu>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation("MainGameMenuFade", 3f,0, 1, false, true, Framework.Easing.EaseType.Linear),
            }, false, true));
            if (BaseGame.DebugMode) AddComponent(new ProfilerComponent());
        }

        #endregion Public Constructors
    }
}