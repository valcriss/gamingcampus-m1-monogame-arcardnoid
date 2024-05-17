using ArcardnoidContent.Components.GameScene.Dialogs;
using ArcardnoidContent.Components.GameScene.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.SubScreens
{
    public class PauseScreen : GameComponent
    {
        #region Private Properties

        private DialogBackground DialogBackground { get; set; }
        private Action OnDebug { get; set; }
        private Action OnQuit { get; set; }
        private Action OnResume { get; set; }
        private PauseMenu PauseDialog { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public PauseScreen(Action onResume, Action onDebug, Action onQuit) : base()
        {
            OnResume = onResume;
            OnDebug = onDebug;
            OnQuit = onQuit;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            DialogBackground = AddGameComponent(new DialogBackground());
            DialogBackground.Visible = false;
            PauseDialog = AddGameComponent(new PauseMenu(OnResumeClicked, OnDebugClicked, OnQuitClicked));
            PauseDialog.Visible = false;
        }

        public void Open()
        {
            DialogBackground.Visible = true;
            DialogBackground.AddAnimation<DialogBackground>(new AlphaFadeAnimation(0.5f, 0f, 0.5f, false, true, EaseType.Linear, BackgroundAppearCompleted));
        }

        #endregion Public Methods

        #region Internal Methods

        internal bool IsOpened()
        {
            return PauseDialog.Visible;
        }

        #endregion Internal Methods

        #region Private Methods

        private void BackgroundAppearCompleted()
        {
            PauseDialog.Visible = true;
            PauseDialog.AddAnimations<PauseMenu>(GetDialogAppearAnimations());
        }

        private void BackgroundHideStart(Action callBack)
        {
            DialogBackground.AddAnimation<DialogBackground>(new AlphaFadeAnimation(0.5f, 0.5f, 0f, false, true, EaseType.Linear, callBack));
        }

        private void Close(Action callBack)
        {
            PauseDialog.AddAnimations<PauseMenu>(GetDialogHideAnimations(() => BackgroundHideStart(() => { PauseDialog.Visible = false; callBack(); })));
        }

        private AnimationChain[] GetDialogAppearAnimations()
        {
            return new AnimationChain[]
           {
                new AnimationChain(new Animation[] { new MoveAnimation(0.5f, new Point(672, 1381), new Point(672, 190), false, true, EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation(0.5f,0, 1, false, true, EaseType.Linear) }, false, true)
           };
        }

        private AnimationChain[] GetDialogHideAnimations(Action onCompleted)
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation(0.5f, new Point(672, 190), new Point(672, 1381), false, true, EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation(0.5f,1, 0, false, true, EaseType.Linear, onCompleted) }, false, true)
            };
        }

        private void OnDebugClicked()
        {
            Close(OnDebug);
        }

        private void OnQuitClicked()
        {
            Close(OnQuit);
        }

        private void OnResumeClicked()
        {
            Close(OnResume);
        }

        #endregion Private Methods
    }
}