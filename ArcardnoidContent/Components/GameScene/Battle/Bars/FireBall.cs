using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Particles;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Battle.Bars
{
    public class FireBall : GameComponent
    {
        #region Public Properties

        public bool Attached { get; set; } = true;
        public bool CanCollide => DateTime.Now - CollideTime > TimeSpan.FromSeconds(_collideTimeOut);

        public float Speed { get; set; } = 500;

        #endregion Public Properties

        #region Private Properties

        private float Angle { get; set; }
        private Image BallImage { get; set; }
        private DateTime CollideTime { get; set; }
        private Point Direction { get; set; } = Point.Zero;
        private BattleFaction Faction { get; set; }
        private ParticuleEmitter ParticuleEmitter { get; set; }

        #endregion Private Properties

        #region Private Fields

        private float _collideTimeOut = 0.5f;

        #endregion Private Fields

        #region Public Constructors

        public FireBall(BattleFaction faction) : base(0, 0, 32, 32)
        {
            Faction = faction;
            this.Bounds.SetPosition(GetStartPosition());
            GameColor red = GameColor.Red;
            GameColor yellow = GameColor.Yellow;
            GameColor blue = GameColor.Blue;
            GameColor lightBlue = GameColor.LightBlue;
            GameColor white = GameColor.White;
            ParticleData data = new ParticleData("particles/particleWhite_4", this.Faction == BattleFaction.Player ? red : blue, this.Faction == BattleFaction.Player ? yellow : lightBlue, .05f, .05f, 1, 1, 0, 360, 22, 22, 1, 200, 0.016f);
            ParticuleEmitter = AddGameComponent(new ParticuleEmitter(data, true));
            BallImage = AddGameComponent(new Image(faction == BattleFaction.Player ? "battle/player-ball" : "battle/oponent-ball", 16, 16));

            ParticuleEmitter.Start();
        }

        #endregion Public Constructors

        #region Public Methods

        public void ColideWithPlane(CollidingPlane planeVector, bool wall)
        {
            if (planeVector == CollidingPlane.None) return;
            if (!wall)
            {
                CollideTime = DateTime.Now;
                _collideTimeOut = 0.5f;
            }
            Direction = Direction.Reflect(planeVector == CollidingPlane.Horizontal ? new Point(0, 1) : new Point(1, 0)).Normalize();
        }

        public void Reset()
        {
            this.Bounds.SetPosition(GetStartPosition());
            this.Attached = true;
        }

        public void Shoot(float angle)
        {
            CollideTime = DateTime.Now;
            _collideTimeOut = 1f;
            Direction = MathTools.VectorFromAngle(angle).Normalize();
            Direction.Y = Math.Min(-0.1f, Direction.Y);
            Angle = angle;
            Attached = false;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (!Attached)
                this.Bounds.SetPosition(this.Bounds.Position + Direction * Speed * delta);

            ParticuleEmitter.UpdateDirectionAndPosition(new Point(0, 1), this.RealBounds.Position + new Point(16, 16));
            BallImage.UpdateRenderBounds();
        }

        #endregion Public Methods

        #region Internal Methods

        internal void ForcePosition(Rectangle bounds, Point barPosition)
        {
            Point difference = bounds.Position - barPosition;
            this.Bounds.SetPosition(GetStartPosition() + difference);
        }

        #endregion Internal Methods

        #region Private Methods

        private Point GetStartPosition()
        {
            if (this.Faction == BattleFaction.Player)
                return new Point(944, 974);
            else
                return new Point(944, 74);
        }

        #endregion Private Methods
    }
}