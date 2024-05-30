using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Particles;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Battle.Bars
{
    public class FireBall : GameComponent
    {
        #region Public Properties

        public bool Attached { get; set; } = true;
        public bool CanCollide => DateTime.Now - CollideTime > TimeSpan.FromSeconds(_collideTimeOut);

        #endregion Public Properties

        #region Protected Properties

        protected virtual float CurrentSpeed { get; set; } = 300;
        protected float Speed { get; set; } = 500;

        #endregion Protected Properties

        #region Private Properties

        private float Angle { get; set; }
        private Image BallImage { get; set; }
        private DateTime CollideTime { get; set; }
        private Point Direction { get; set; } = Point.Zero;
        private BattleFaction Faction { get; set; }
        private IGamePlay GamePlay { get; set; } = GameServiceProvider.GetService<IGamePlay>();
        private ParticleData ParticleData { get; set; }
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
            ParticleData = new ParticleData(TextureType.PARTICLES_PARTICLEWHITE_4, this.Faction == BattleFaction.Player ? red : blue, this.Faction == BattleFaction.Player ? yellow : lightBlue, .05f, .05f, 1, 1, 0, 360, 22, 22, 1, 200, 0.016f);
            ParticuleEmitter = AddGameComponent(new ParticuleEmitter(ParticleData, true));
            BallImage = AddGameComponent(new Image(faction == BattleFaction.Player ? TextureType.BATTLE_PLAYER_BALL : TextureType.BATTLE_OPONENT_BALL, 16, 16));
            if (faction == BattleFaction.Player)
                GamePlay.PlayerSpeedChanged += SpeedUpdated;
            else
                GamePlay.OponentSpeedChanged += SpeedUpdated;
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
                _collideTimeOut = 0.1f;
            }
            Direction = Direction.Reflect(planeVector == CollidingPlane.Horizontal ? new Point(0, 1) : new Point(1, 0)).Normalize();
        }

        public void Reset()
        {
            this.Bounds.SetPosition(GetStartPosition());
            this.Attached = true;
        }

        public void Shoot(float angle, BattleFaction faction)
        {
            CollideTime = DateTime.Now;
            _collideTimeOut = 1f;
            Direction = MathTools.VectorFromAngle(angle).Normalize();
            Direction.Y = (faction == BattleFaction.Player) ? (float)Math.Min(-0.1, Direction.Y) : (float)Math.Max(0.1, Direction.Y);
            Angle = angle;
            Attached = false;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (!Attached)
                this.Bounds.SetPosition(this.Bounds.Position + Direction * CurrentSpeed * delta);

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

        private void SpeedUpdated(float speed, float duration)
        {
            GameColor red = GameColor.Red;
            GameColor yellow = GameColor.Yellow;
            GameColor blue = GameColor.Blue;
            GameColor lightBlue = GameColor.LightBlue;
            GameColor white = GameColor.White;
            GameColor green = GameColor.Green;
            GameColor cyan = GameColor.Cyan;
            GameColor purple = GameColor.Purple;
            CurrentSpeed = Speed * speed;
            if (speed > 1)
            {
                _collideTimeOut = 0.05f;
                ParticuleEmitter.UpdateParticuleEmitterData(new ParticleData(TextureType.PARTICLES_PARTICLEWHITE_4, this.Faction == BattleFaction.Player ? purple : blue, this.Faction == BattleFaction.Player ? cyan : lightBlue, .1f, .1f, 1, 1, 0, 360, 22, 22, 1.5f, 200, 0.008f));
            }
            else
            {
                _collideTimeOut = 0.1f;
                ParticuleEmitter.UpdateParticuleEmitterData(ParticleData);
            }
        }

        #endregion Private Methods
    }
}