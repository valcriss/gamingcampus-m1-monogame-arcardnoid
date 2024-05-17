using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.MainMenu
{
    public class MainMenuBackground : GameComponent
    {
        #region Private Fields

        private const float BORDERS_FADE_DURATION = 2;
        private const float BORDERS_MOVE_DURATION = 2;
        private const float BORDERS_MOVE_OFFSET = 6;
        private const float BORDERS_OPACITY_MAX = 1;
        private const float BORDERS_OPACITY_MIN = 0.6f;
        private const float CLOUD_MOVE_SPEED = 40;
        private const float CLOUD_MOVE_SPEED_OFFSET = 5;

        #endregion Private Fields

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Image("mainmenu/left", 444, 540)).AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MIN,BORDERS_OPACITY_MAX),
                new AlphaFadeAnimation( BORDERS_FADE_DURATION,BORDERS_OPACITY_MAX,BORDERS_OPACITY_MIN)
            }, true, true))
            .AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(444-BORDERS_MOVE_OFFSET,540),new Point(444,540)),
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(444,540),new Point(444-BORDERS_MOVE_OFFSET,540))
            }, true, true));

            AddGameComponent(new Image("mainmenu/right", 1476, 540)).AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MIN,BORDERS_OPACITY_MAX),
                new AlphaFadeAnimation(BORDERS_FADE_DURATION,BORDERS_OPACITY_MAX,BORDERS_OPACITY_MIN)
            }, true, true))
            .AddAnimations<Image>(new AnimationChain(new Animation[]
            {
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(1476+BORDERS_MOVE_OFFSET,540),new Point(1476,540)),
                new MoveAnimation(BORDERS_MOVE_DURATION,new Point(1476,540),new Point(1476+BORDERS_MOVE_OFFSET,540))
            }, true, true));

            AddGameComponent(new StaticMap("Maps/menumap.json", 608, 200));

            int[] cloud1PositionY = new int[] { 220, 420, 620, 820, 970 };
            int[] cloud2PositionY = new int[] { 150, 350, 550, 750, 900 };

            for (int i = 0; i < cloud1PositionY.Length; i++)
            {
                int y = cloud1PositionY[i];
                Point from = i % 2 == 0 ? new Point(-106, y) : new Point(2020, y);
                Point to = i % 2 == 0 ? new Point(2020, y) : new Point(-106, y);
                float speed = CLOUD_MOVE_SPEED + (i * CLOUD_MOVE_SPEED_OFFSET);
                AddGameComponent(new Image("mainmenu/cloud1", 0, y)).AddAnimation<Image>(new MoveAnimation(speed, from, to, true, true));
            }

            for (int i = 0; i < cloud2PositionY.Length; i++)
            {
                int y = cloud2PositionY[i];
                Point from = i % 2 == 0 ? new Point(-166, y) : new Point(2086, y);
                Point to = i % 2 == 0 ? new Point(2086, y) : new Point(-166, y);
                float speed = CLOUD_MOVE_SPEED + (i * CLOUD_MOVE_SPEED_OFFSET);
                AddGameComponent(new Image("mainmenu/cloud2", 0, y)).AddAnimation<Image>(new MoveAnimation(speed, from, to, true, true));
            }
        }

        #endregion Public Methods
    }
}