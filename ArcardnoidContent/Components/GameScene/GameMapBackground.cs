using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene
{
    public class GameMapBackground : GameComponent
    {
        #region Private Fields

        private const float BORDERS_FADE_DURATION = 2;
        private const float BORDERS_MOVE_DURATION = 2;
        private const float BORDERS_MOVE_OFFSET = 6;
        private const float BORDERS_OPACITY_MAX = 1;
        private const float BORDERS_OPACITY_MIN = 0.6f;

        #endregion Private Fields

        #region Public Constructors

        public GameMapBackground() : base()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Image(TextureType.MAINMENU_LEFT, 444, 540)).AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MIN,BORDERS_OPACITY_MAX),
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MAX,BORDERS_OPACITY_MIN)
            }, true, true))
            .AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(444-BORDERS_MOVE_OFFSET,540),new Point(444,540)),
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(444,540),new Point(444-BORDERS_MOVE_OFFSET,540))
            }, true, true));

            AddGameComponent(new Image(TextureType.MAINMENU_RIGHT, 1476, 540)).AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MIN,BORDERS_OPACITY_MAX),
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MAX,BORDERS_OPACITY_MIN)
            }, true, true))
            .AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(1476+BORDERS_MOVE_OFFSET,540),new Point(1476,540)),
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(1476,540),new Point(1476+BORDERS_MOVE_OFFSET,540))
            }, true, true));
        }

        #endregion Public Methods
    }
}