using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene.Battle.Bars
{
    public class BattleBar : GameComponent
    {
        #region Protected Properties

        protected virtual Point AnimationOffset { get; } = Point.Zero;
        protected virtual Point BarPosition { get; } = Point.Zero;
        protected BattleFaction Faction { get; }
        protected Rectangle GameBounds { get; set; } = Rectangle.Empty;
        protected virtual float Speed { get; set; } = 200;
        protected virtual int Width { get; set; } = 10;

        #endregion Protected Properties

        #region Private Fields

        private ITexture? barTexture;

        private Rectangle centerBar = Rectangle.Empty;
        private Rectangle drawCenterBar = Rectangle.Empty;
        private Rectangle drawLeftBar = Rectangle.Empty;
        private Rectangle drawRightBar = Rectangle.Empty;
        private Rectangle leftBar = Rectangle.Empty;
        private Rectangle rightBar = Rectangle.Empty;

        #endregion Private Fields

        #region Public Constructors

        public BattleBar(BattleFaction faction, Rectangle gameBounds)
        {
            Faction = faction;
            GameBounds = gameBounds;
            this.Bounds.SetPosition(this.BarPosition);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            barTexture?.DrawTexture(drawLeftBar, leftBar, Color, 0, Point.Zero);
            barTexture?.DrawTexture(drawCenterBar, centerBar, Color, 0, Point.Zero);
            barTexture?.DrawTexture(drawRightBar, rightBar, Color, 0, Point.Zero);
        }

        public override void Load()
        {
            base.Load();
            barTexture = GameServiceProvider.GetService<ITextureService>().Load(Faction == BattleFaction.Player ? "battle/bar-player" : "battle/bar-opponent");
            leftBar = new Rectangle(0, 0, 16, 32);
            rightBar = new Rectangle(32, 0, 16, 32);
            centerBar = new Rectangle(16, 0, 16, 32);
        }

        public virtual T Show<T>(Action? onAnimationCompleted = null) where T : BattleBar
        {
            return AddAnimation<T>(new MoveAnimation(1.5f, new Point(BarPosition.X, BarPosition.Y + AnimationOffset.Y), new Point(BarPosition.X, BarPosition.Y), false, true, EaseType.InOutBounce, () =>
            {
                onAnimationCompleted?.Invoke();
                this.Bounds.SetPosition(BarPosition);
            }));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            int barWidth = Width * (int)centerBar.Width;
            drawLeftBar = new Rectangle(RealBounds.X - ((barWidth / 2)) - leftBar.Width, RealBounds.Y, leftBar.Width, leftBar.Height);
            drawRightBar = new Rectangle(RealBounds.X + ((barWidth / 2)), RealBounds.Y, rightBar.Width, rightBar.Height);
            drawCenterBar = new Rectangle(drawLeftBar.X + drawLeftBar.Width, RealBounds.Y, barWidth, centerBar.Height);
        }

        #endregion Public Methods
    }
}