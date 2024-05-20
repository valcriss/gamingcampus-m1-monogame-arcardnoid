using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.Shared.Map
{
    public class AnimatedStaticMap : StaticMap
    {
        #region Private Fields

        private float _delay = 0f;

        private string[] ANIMATION_TILES_PATH = new string[]
                {
            "map/deco/05",
            "map/deco/04",
            "map/deco/10",
            "map/deco/11",
        };

        #endregion Private Fields

        #region Public Constructors

        public AnimatedStaticMap(string mapAsset, int x, int y, bool debug = false) : base(mapAsset, x, y, debug)
        {
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
            return this;
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