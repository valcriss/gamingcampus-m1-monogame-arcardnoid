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
        #region Public Properties

        public override Point BarPosition => new Point(960, 1000);

        #endregion Public Properties

        #region Protected Properties

        protected override Point AnimationOffset => new Point(0, 300);

        #endregion Protected Properties

        #region Public Constructors

        public PlayerBattleBar(Rectangle gameBounds) : base(BattleFaction.Player, gameBounds)
        {
            GamePlay.PlayerSpeedChanged += (speed, duration) => CurrentSpeed = Speed * speed;
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
            IKeyboardService keyboardService = GameServiceProvider.GetService<IKeyboardService>();
            if (keyboardService.IsKeyDown("Q") || keyboardService.IsKeyDown("D"))
            {
                float direction = keyboardService.IsKeyDown("Q") ? -1 : 1;
                Point position = Bounds.Position;
                position.X = position.X + (CurrentSpeed * delta) * direction;
                position.X = MathTools.Clamp(position.X, GameBounds.X + ((Width * 8) + 16), GameBounds.X + GameBounds.Width - ((Width * 8) + 16));
                Bounds.SetPosition(position);
            }
        }

        #endregion Public Methods
    }
}