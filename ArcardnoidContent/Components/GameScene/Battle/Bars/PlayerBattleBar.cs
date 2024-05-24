using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class PlayerBattleBar : BattleBar
    {
        #region Protected Properties

        protected override Point AnimationOffset => new Point(0, 300);
        protected override Point BarPosition => new Point(960, 1000);

        #endregion Protected Properties

        #region Public Constructors

        public PlayerBattleBar(Rectangle gameBounds) : base(BattleFaction.Player, gameBounds)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual PlayerBattleBar Show(Action? onAnimationCompleted = null)
        {
            return base.Show<PlayerBattleBar>(onAnimationCompleted);
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (HasAnimations)
            {
                return;
            }
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
            Point mousePosition = mouseService.GetMousePosition();
            if (GameBounds.Contains(mousePosition))
            {
                Point position = Bounds.Position;
                if (mousePosition.X < 960)
                {
                    position.X = position.X - Speed * delta;
                }
                else
                {
                    position.X = position.X + Speed * delta;
                }
                position.X = MathTools.Clamp(position.X, GameBounds.X + ((Width * 8) + 16), GameBounds.X + GameBounds.Width - ((Width * 8) + 16));
                Bounds.SetPosition(position);
            }
        }

        #endregion Public Methods
    }
}