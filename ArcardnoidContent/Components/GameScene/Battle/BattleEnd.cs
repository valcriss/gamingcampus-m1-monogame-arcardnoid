using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleEnd : GameComponent
    {
        #region Private Properties

        private float Duration { get; set; } = 7f;
        private Image? Image { get; set; }
        private Action? OnScreenEnd { get; set; } = null;

        #endregion Private Properties

        #region Public Methods

        public void Show(bool victory, float duration = 7f, Action? onScreenEnd = null)
        {
            Duration = duration;
            OnScreenEnd = onScreenEnd;
            AddGameComponent(new Primitive2D((primitive) =>
            {
                primitive.FillRectangle(new Rectangle(0, 0, 1920, 1080), new GameColor(0, 0, 0, 128));
            }));
            Image = AddGameComponent(new Image(victory ? "ui/victory" : "ui/defeated", 0, 0));
            Image.AddAnimation<Image>(new AlphaFadeAnimation(2, 0, 1, false, true, EaseType.Linear));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (Image == null)
            {
                return;
            }
            Image.Bounds = new Rectangle(960 - Image.Bounds.Width / 2, 540 - Image.Bounds.Height / 2, Image.Bounds.Width, Image.Bounds.Height);
            Duration -= deltaTime;
            if (Duration <= 0)
            {
                OnScreenEnd?.Invoke();
            }
        }

        #endregion Public Methods
    }
}