using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class OponentBattleBar : BattleBar
    {
        #region Protected Properties

        protected override Point AnimationOffset => new Point(0, -300);
        protected override Point BarPosition => new Point(960, 48);

        #endregion Protected Properties

        #region Public Constructors

        public OponentBattleBar(Rectangle gameBounds) : base(BattleFaction.Opponent, gameBounds)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual OponentBattleBar Show(Action? onAnimationCompleted = null)
        {
            return base.Show<OponentBattleBar>(onAnimationCompleted);
        }

        #endregion Public Methods
    }
}