using ArcardnoidContent.Components.MainMenu;
using ArcardnoidContent.Components.UI;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Scenes
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
            BackgroundColor = new GameColor(71, 171, 169);
            AddGameComponent(new MainMenuBackground());
            AddGameComponent(new AnimatedTitleBand("ui/bandeau", "fonts/band", "ArCardNoid : La bataille des balles rebondissantes", 0.05f, 960, 100, new GameColor(75, 30, 0)));
            MainGameMenu = AddGameComponent(new MainGameMenu(OnNewGameButtonClicked, OnCreditsButtonClicked, OnParametersButtonClicked, OnDemoButtonClicked, OnQuitButtonClicked)).AddAnimations<MainGameMenu>(GetMainMenuAppearAnimations(3f));
            QuitGameConfirm = AddGameComponent(new QuitGameConfirm(OnQuitConfirmButtonClicked, OnQuitConfirmCancelClicked));
            CreditFrame = AddGameComponent(new CreditFrame(OnCreditFrameCancelClicked));
            ParametersFrame = AddGameComponent(new ParametersFrame(OnParametersFrameCancelClicked));
            SeedFrame = AddGameComponent(new SeedFrame(OnNewGameConfirmButtonClicked, OnSeedFrameCancelClicked));

            AddGameComponent(new Cursor("ui/cursors/01", new Point(12, 16)));
        }

        #endregion Public Constructors

        #region Private Methods

        private AnimationChain[] GetMainMenuAppearAnimations(float duration)
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation( duration, new Point(672, 1381), new Point(672, 316), false, true, EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation( duration, 0, 1, false, true, EaseType.Linear) }, false, true)
            };
        }

        private AnimationChain[] GetMainMenuHideAnimations()
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation( 1f, new Point(672, 316), new Point(672, 1381), false, true, EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation( 1f,1, 0, false, true, EaseType.Linear) }, false, true)
            };
        }

        private AnimationChain[] GetQuitGameConfirmAppearAnimations()
        {
            return new AnimationChain[]
           {
                new AnimationChain(new Animation[] { new MoveAnimation(1f, new Point(672, 1381), new Point(672, 316), false, true, EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation( 1f,0, 1, false, true, EaseType.Linear) }, false, true)
           };
        }

        private AnimationChain[] GetQuitGameConfirmHideAnimations()
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation(1f, new Point(672, 316), new Point(672, 1381), false, true, EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation( 1f,1, 0, false, true, EaseType.Linear) }, false, true)
            };
        }

        private AnimationChain[] GetSubFrameAppearAnimations()
        {
            return new AnimationChain[]
           {
                new AnimationChain(new Animation[] { new MoveAnimation( 1f, new Point(672, 1381), new Point(672, 190), false, true, EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation(1f,0, 1, false, true, EaseType.Linear) }, false, true)
           };
        }

        private AnimationChain[] GetSubFrameHideAnimations()
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation(1f, new Point(672, 190), new Point(672, 1381), false, true, EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation( 1f,1, 0, false, true, EaseType.Linear) }, false, true)
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

        private void OnDemoButtonClicked()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new DemoScene());
        }

        private void OnNewGameButtonClicked()
        {
            SeedFrame.NewSeed();
            MainGameMenu.AddAnimations<MainGameMenu>(GetMainMenuHideAnimations());
            SeedFrame.AddAnimations<SeedFrame>(GetSubFrameAppearAnimations());
        }

        private void OnNewGameConfirmButtonClicked()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new GameScene(SeedFrame.GetSeed()));
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
            GameServiceProvider.GetService<IGameService>().ExitGame();
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