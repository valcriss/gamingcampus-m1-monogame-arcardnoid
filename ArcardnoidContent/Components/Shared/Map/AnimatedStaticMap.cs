using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.Shared.Map
{
    public class AnimatedStaticMap : StaticMap
    {
        #region Private Properties

        private AnimatedStaticMapState AnimationState { get; set; } = AnimatedStaticMapState.WAITING;
        private Action? MapAnimationFinished { get; set; }

        #endregion Private Properties

        #region Private Fields

        private readonly TextureType[] ANIMATION_TILES_PATH = new TextureType[] {
            TextureType.MAP_DECO_05,
            TextureType.MAP_DECO_04,
            TextureType.MAP_DECO_10,
            TextureType.MAP_DECO_11,
            TextureType.MAP_UNITS_ARCHER_BLUE_IDLE,
            TextureType.MAP_UNITS_WARRIOR_BLUE_IDLE,
            TextureType.MAP_UNITS_TORCH_RED_IDLE,
            TextureType.MAP_UNITS_TNT_RED_IDLE,
            TextureType.MAP_UNITS_PLAYER_BATTLE
        };

        private float _delay = 0f;

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
            if (gameComponent is MapCell tile && ANIMATION_TILES_PATH.Count(c => c == tile.TextureAsset) > 0)
            {
                gameComponent.Enabled = false;
                gameComponent.Visible = false;
            }
            return gameComponent;
        }

        public void AddTileAnimation(GameComponent gameComponent)
        {
            gameComponent.Visible = false;
            gameComponent.Enabled = true;

            gameComponent.AddAnimation<GameComponent>(new MoveAnimation(0.5f, new Point(gameComponent.Bounds.X, -128), new Point(gameComponent.Bounds.X, gameComponent.Bounds.Y), false, true, EaseType.InBounce, null, _delay));
            _delay += 0.03f;
        }

        public AnimatedStaticMap AddTilesAnimation()
        {
            foreach (GameComponent gameComponent in GameComponents)
            {
                if (gameComponent is MapCell tile && ANIMATION_TILES_PATH.Count(c => c == tile.TextureAsset) > 0)
                    AddTileAnimation(gameComponent);
            }
            AnimationState = AnimatedStaticMapState.ANIMATING;
            return this;
        }

        public bool IsFree(TextureType texture, int x, int y)
        {
            return !GameComponents.Where(c => c is AnimatedCell && ((AnimatedCell)c).TextureAsset == texture && ((AnimatedCell)c).GridX == x && ((AnimatedCell)c).GridY == y).Any();
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
    }
}