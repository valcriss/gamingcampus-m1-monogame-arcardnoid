using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Drawing;

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