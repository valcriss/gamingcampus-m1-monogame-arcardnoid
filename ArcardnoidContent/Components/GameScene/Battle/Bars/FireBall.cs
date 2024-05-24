using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Particles;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.Battle.Bars
{
    public class FireBall : GameComponent
    {
        #region Public Properties

        public bool Attached { get; set; } = true;

        #endregion Public Properties

        #region Private Properties

        private Image BallImage { get; set; }
        private BattleFaction Faction { get; set; }
        private ParticuleEmitter ParticuleEmitter { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public FireBall(BattleFaction faction) : base()
        {
            Faction = faction;
            if (this.Faction == BattleFaction.Player)
                this.Bounds.SetPosition(new Point(960, 990));
            else
                this.Bounds.SetPosition(new Point(960, 90));
            GameColor red = GameColor.Red;
            GameColor yellow = GameColor.Yellow;
            GameColor blue = GameColor.Blue;
            GameColor lightBlue = GameColor.LightBlue;
            GameColor white = GameColor.White;
            ParticleData data = new ParticleData("particles/particleWhite_4", this.Faction == BattleFaction.Player ? red : blue, this.Faction == BattleFaction.Player ? yellow : lightBlue, .05f, .05f, 1, 1, 0, 360, 22, 22, 1, 200, 0.016f);
            ParticuleEmitter = AddGameComponent(new ParticuleEmitter(data, true));
            BallImage = AddGameComponent(new Image(faction == BattleFaction.Player ? "battle/player-ball" : "battle/oponent-ball", 0, 0));

            ParticuleEmitter.Start();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(float delta)
        {
            base.Update(delta);
            ParticuleEmitter.UpdateDirectionAndPosition(new Point(0, 1), this.RealBounds.Position);
        }

        #endregion Public Methods
    }
}