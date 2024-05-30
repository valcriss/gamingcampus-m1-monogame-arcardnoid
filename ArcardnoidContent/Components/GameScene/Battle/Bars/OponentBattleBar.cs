using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class OponentBattleBar : BattleBar
    {
        #region Public Properties

        public override Point BarPosition => new Point(960, 48);

        #endregion Public Properties

        #region Protected Properties

        protected override Point AnimationOffset => new Point(0, -300);

        #endregion Protected Properties

        #region Public Constructors

        public OponentBattleBar(Rectangle gameBounds) : base(BattleFaction.Opponent, gameBounds)
        {
            GamePlay.OponentSpeedChanged += (speed, duration) => CurrentSpeed = Speed * speed;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual OponentBattleBar Show(Action? onAnimationCompleted = null)
        {
            return base.Show<OponentBattleBar>(onAnimationCompleted);
        }

        public void UpdateBarPosition(float delta, FireBall fireball)
        {
            if (HasAnimations)
            {
                return;
            }

            Point position = Bounds.Position;
            if (fireball.RealBounds.X < position.X)
            {
                position.X = position.X + (CurrentSpeed * delta) * -1;
            }
            else if (fireball.RealBounds.X > position.X)
            {
                position.X = position.X + (CurrentSpeed * delta) * 1;
            }

            position.X = MathTools.Clamp(position.X, GameBounds.X + ((Width * 8) + 16), GameBounds.X + GameBounds.Width - ((Width * 8) + 16));
            Bounds.SetPosition(position);
        }

        #endregion Public Methods
    }
}