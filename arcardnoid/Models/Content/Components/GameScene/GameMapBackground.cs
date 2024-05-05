using arcardnoid.Models.Framework.Animations;
using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class GameMapBackground : Component
    {
        #region Private Fields

        private const float BORDERS_FADE_DURATION = 2;
        private const float BORDERS_MOVE_DURATION = 2;
        private const float BORDERS_MOVE_OFFSET = 6;
        private const float BORDERS_OPACITY_MAX = 1;
        private const float BORDERS_OPACITY_MIN = 0.6f;

        #endregion Private Fields

        #region Public Constructors

        public GameMapBackground() : base("GameMapBackground")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new Image("mainmenu-left", "mainmenu/left", 444, 540)).AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation("left-fade-up",BORDERS_FADE_DURATION,BORDERS_OPACITY_MIN,BORDERS_OPACITY_MAX),
                new AlphaFadeAnimation("left-fade-down", BORDERS_FADE_DURATION,BORDERS_OPACITY_MAX,BORDERS_OPACITY_MIN)
            }, true, true))
            .AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new MoveAnimation("left-move-left",BORDERS_MOVE_DURATION,new Vector2(444-BORDERS_MOVE_OFFSET,540),new Vector2(444,540)),
                new MoveAnimation("left-move-right",BORDERS_MOVE_DURATION,new Vector2(444,540),new Vector2(444-BORDERS_MOVE_OFFSET,540))
            }, true, true));

            AddComponent(new Image("mainmenu-right", "mainmenu/right", 1476, 540)).AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation("right-fade-up",BORDERS_FADE_DURATION,BORDERS_OPACITY_MIN,BORDERS_OPACITY_MAX),
                new AlphaFadeAnimation("right-fade-down",BORDERS_FADE_DURATION,BORDERS_OPACITY_MAX,BORDERS_OPACITY_MIN)
            }, true, true))
            .AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new MoveAnimation("right-move-right",BORDERS_MOVE_DURATION,new Vector2(1476+BORDERS_MOVE_OFFSET,540),new Vector2(1476,540)),
                new MoveAnimation("right-move-left",BORDERS_MOVE_DURATION,new Vector2(1476,540),new Vector2(1476+BORDERS_MOVE_OFFSET,540))
            }, true, true));
        }

        #endregion Public Methods
    }
}