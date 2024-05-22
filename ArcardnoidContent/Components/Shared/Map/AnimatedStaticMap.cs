using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.Shared.Map
{
    public class AnimatedStaticMap : StaticMap
    {
        #region Private Properties

        private AnimatedStaticMapState AnimationState { get; set; } = AnimatedStaticMapState.WAITING;
        private Action? MapAnimationFinished { get; set; }

        #endregion Private Properties

        #region Private Fields

        private float _delay = 0f;

        private string[] ANIMATION_TILES_PATH = new string[]
                {
            "map/deco/05",
            "map/deco/04",
            "map/deco/10",
            "map/deco/11",
            "map/units/archer-blue-idle",
            "map/units/warrior-blue-idle",
            "map/units/torch-red-idle",
            "map/units/tnt-red-idle",
            "map/units/player-battle"
        };

        #endregion Private Fields

        #region Public Constructors

        public AnimatedStaticMap(string mapAsset, int x, int y, Action? mapAnimationFinished, bool debug = false) : base(mapAsset, x, y, debug)
        {
            MapAnimationFinished = mapAnimationFinished;
        }

        #endregion Public Constructors

        #region Public Methods

        public override T AddGameComponent<T>(T gameComponent)
        {
            gameComponent = base.AddGameComponent(gameComponent);
            if (gameComponent is MapCell tile && ANIMATION_TILES_PATH.Contains(tile.TextureAsset))
            {
                gameComponent.Enabled = false;
                gameComponent.Visible = false;
            }
            return gameComponent;
        }

        public AnimatedStaticMap AddTilesAnimation()
        {
            foreach (GameComponent gameComponent in GameComponents)
            {
                if (gameComponent is MapCell tile && ANIMATION_TILES_PATH.Contains(tile.TextureAsset))
                    AddTileAnimation(gameComponent);
            }
            AnimationState = AnimatedStaticMapState.ANIMATING;
            return this;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (AnimationState == AnimatedStaticMapState.WAITING || AnimationState == AnimatedStaticMapState.FINISHED) return;
            if (AnimationState == AnimatedStaticMapState.ANIMATING)
            {
                if (!GameComponents.Any(c => c.HasAnimations))
                {
                    MapAnimationFinished?.Invoke();
                    AnimationState = AnimatedStaticMapState.FINISHED;
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void AddTileAnimation(GameComponent gameComponent)
        {
            gameComponent.Visible = false;
            gameComponent.Enabled = true;

            gameComponent.AddAnimation<GameComponent>(new MoveAnimation(1f, new Point(gameComponent.Bounds.X, -128), new Point(gameComponent.Bounds.X, gameComponent.Bounds.Y), false, true, EaseType.InOutBounce, null, _delay));
            _delay += 0.03f;
        }

        #endregion Private Methods
    }
}