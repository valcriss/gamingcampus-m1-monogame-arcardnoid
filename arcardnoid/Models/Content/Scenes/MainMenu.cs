using arcardnoid.Models.Content.Components.MainMenu;
using arcardnoid.Models.Framework;
using arcardnoid.Models.Framework.Animations;
using arcardnoid.Models.Framework.Components.Profiler;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Content.Scenes
{
    public class MainMenu : Scene
    {
        #region Private Properties

        private CreditFrame CreditFrame { get; set; }
        private MainGameMenu MainGameMenu { get; set; }
        private ParametersFrame ParametersFrame { get; set; }
        private QuitGameConfirm QuitGameConfirm { get; set; }

        private SeedFrame SeedFrame { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MainMenu()
        {
            BackgroundColor = Color.FromNonPremultiplied(71, 171, 169, 255);
            AddComponent(new MainMenuBackground());
            AddComponent(new Components.UI.AnimatedTitleBand("ui/bandeau", "fonts/band", "ArCardNoid : La bataille des balles rebondissantes", 0.05f, 960, 100, Color.FromNonPremultiplied(75, 30, 0, 255)));

            MainGameMenu = AddComponent(new MainGameMenu(OnNewGameButtonClicked, OnCreditsButtonClicked, OnParametersButtonClicked, OnQuitButtonClicked)).AddAnimations<MainGameMenu>(GetMainMenuAppearAnimations(3f));
            QuitGameConfirm = AddComponent(new QuitGameConfirm(OnQuitConfirmButtonClicked, OnQuitConfirmCancelClicked));
            CreditFrame = AddComponent(new CreditFrame(OnCreditFrameCancelClicked));
            ParametersFrame = AddComponent(new ParametersFrame(OnParametersFrameCancelClicked));
            SeedFrame = AddComponent(new SeedFrame(OnNewGameConfirmButtonClicked, OnSeedFrameCancelClicked));

            if (BaseGame.DebugMode) AddComponent(new ProfilerComponent());
            AddComponent(new Cursor("cursor", "ui/cursors/01", new Vector2(12, 16)));
        }

        #endregion Public Constructors

        #region Private Methods

        private AnimationChain[] GetMainMenuAppearAnimations(float duration)
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation("Move", duration, new Vector2(672, 1381), new Vector2(672, 316), false, true, Framework.Easing.EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", duration, 0, 1, false, true, Framework.Easing.EaseType.Linear) }, false, true)
            };
        }

        private AnimationChain[] GetMainMenuHideAnimations()
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation("Move", 1f, new Vector2(672, 316), new Vector2(672, 1381), false, true, Framework.Easing.EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", 1f,1, 0, false, true, Framework.Easing.EaseType.Linear) }, false, true)
            };
        }

        private AnimationChain[] GetQuitGameConfirmAppearAnimations()
        {
            return new AnimationChain[]
           {
                new AnimationChain(new Animation[] { new MoveAnimation("Move", 1f, new Vector2(672, 1381), new Vector2(672, 316), false, true, Framework.Easing.EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", 1f,0, 1, false, true, Framework.Easing.EaseType.Linear) }, false, true)
           };
        }

        private AnimationChain[] GetQuitGameConfirmHideAnimations()
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation("Move",1f, new Vector2(672, 316), new Vector2(672, 1381), false, true, Framework.Easing.EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", 1f,1, 0, false, true, Framework.Easing.EaseType.Linear) }, false, true)
            };
        }

        private AnimationChain[] GetSubFrameAppearAnimations()
        {
            return new AnimationChain[]
           {
                new AnimationChain(new Animation[] { new MoveAnimation("Move", 1f, new Vector2(672, 1381), new Vector2(672, 190), false, true, Framework.Easing.EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", 1f,0, 1, false, true, Framework.Easing.EaseType.Linear) }, false, true)
           };
        }

        private AnimationChain[] GetSubFrameHideAnimations()
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation("Move",1f, new Vector2(672, 190), new Vector2(672, 1381), false, true, Framework.Easing.EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", 1f,1, 0, false, true, Framework.Easing.EaseType.Linear) }, false, true)
            };
        }

        private void OnCreditFrameCancelClicked()
        {
            CreditFrame.AddAnimations<CreditFrame>(GetSubFrameHideAnimations());
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuAppearAnimations(1f));
        }

        private void OnCreditsButtonClicked()
        {
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuHideAnimations());
            CreditFrame.AddAnimations<CreditFrame>(GetSubFrameAppearAnimations());
        }

        private void OnNewGameButtonClicked()
        {
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuHideAnimations());
            SeedFrame.AddAnimations<SeedFrame>(GetSubFrameAppearAnimations());
        }

        private void OnNewGameConfirmButtonClicked()
        {
            Game.ScenesManager.SwitchScene(this, new GameScene(SeedFrame.GetSeed()));
        }

        private void OnParametersButtonClicked()
        {
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuHideAnimations());
            ParametersFrame.AddAnimations<ParametersFrame>(GetSubFrameAppearAnimations());
        }

        private void OnParametersFrameCancelClicked()
        {
            ParametersFrame.AddAnimations<ParametersFrame>(GetSubFrameHideAnimations());
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuAppearAnimations(1f));
        }

        private void OnQuitButtonClicked()
        {
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuHideAnimations());
            QuitGameConfirm.AddAnimations<QuitGameConfirm>(GetQuitGameConfirmAppearAnimations());
        }

        private void OnQuitConfirmButtonClicked()
        {
            Game.Exit();
        }

        private void OnQuitConfirmCancelClicked()
        {
            QuitGameConfirm.AddAnimations<QuitGameConfirm>(GetQuitGameConfirmHideAnimations());
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuAppearAnimations(1f));
        }

        private void OnSeedFrameCancelClicked()
        {
            SeedFrame.AddAnimations<SeedFrame>(GetSubFrameHideAnimations());
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuAppearAnimations(1f));
        }

        #endregion Private Methods
    }
}