using arcardnoid.Models.Framework.Animations;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class PauseScreen : Component
    {
        private DialogBackground DialogBackground { get; set; }
        private PauseDialog PauseDialog { get; set; }
        private Action OnResume { get; set; }
        private Action OnDebug { get; set; }
        private Action OnQuit { get; set; }

        public PauseScreen(Action onResume, Action onDebug, Action onQuit) : base("PauseScreen")
        {
            OnResume = onResume;
            OnDebug = onDebug;
            OnQuit = onQuit;
        }

        public override void Load()
        {
            base.Load();
            DialogBackground = AddComponent(new DialogBackground());
            DialogBackground.Visible = false;
            PauseDialog = AddComponent(new PauseDialog(OnResumeClicked, OnDebugClicked, OnQuitClicked));
            PauseDialog.Visible = false;
        }

        private void OnResumeClicked()
        {
            Close(OnResume);
        }

        private void OnDebugClicked()
        {
            Close(OnDebug);
        }

        private void OnQuitClicked()
        {
            Close(OnQuit);
        }

        public void Open()
        {
            DialogBackground.Visible = true;
            DialogBackground.AddAnimation<DialogBackground>(new AlphaFadeAnimation("appear", 0.5f, 0f, 0.5f, false, true, Framework.Easing.EaseType.Linear, BackgroundAppearCompleted));
        }

        private void Close(Action callBack)
        {
            PauseDialog.AddAnimations<PauseDialog>(GetDialogHideAnimations(() => BackgroundHideStart(callBack)));
        }

        private void BackgroundAppearCompleted()
        {
            PauseDialog.Visible = true;
            PauseDialog.AddAnimations<PauseDialog>(GetDialogAppearAnimations());
        }

        private void BackgroundHideStart(Action callBack)
        {
            DialogBackground.AddAnimation<DialogBackground>(new AlphaFadeAnimation("appear", 0.5f, 0.5f, 0f, false, true, Framework.Easing.EaseType.Linear, callBack));
        }

        private AnimationChain[] GetDialogAppearAnimations()
        {
            return new AnimationChain[]
           {
                new AnimationChain(new Animation[] { new MoveAnimation("Move", 0.5f, new Vector2(672, 1381), new Vector2(672, 190), false, true, Framework.Easing.EaseType.InOutElastic)}, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade",0.5f,0, 1, false, true, Framework.Easing.EaseType.Linear) }, false, true)
           };
        }

        private AnimationChain[] GetDialogHideAnimations(Action onCompleted)
        {
            return new AnimationChain[]
            {
                new AnimationChain(new Animation[] { new MoveAnimation("Move",0.5f, new Vector2(672, 190), new Vector2(672, 1381), false, true, Framework.Easing.EaseType.OutElastic) }, false, true),
                new AnimationChain(new Animation[] { new AlphaFadeAnimation("Fade", 0.5f,1, 0, false, true, Framework.Easing.EaseType.Linear, onCompleted) }, false, true)
            };
        }
    }
}
