using arcardnoid.Models.Framework.Animations;
using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    public class MainMenuBackground : Component
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

        #region Public Constructors

        public MainMenuBackground() : base("MainMenuBackground")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new Image("mainmenu-left", "mainmenu/left", 444, 540)).AddAnimations< Image>(new AnimationChain(new Animation[]
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

            AddComponent(new Map.GameMap("Maps/menumap.json", 608, 200));

            int[] cloud1PositionY = new int[] { 220, 420, 620, 820, 970 };
            int[] cloud2PositionY = new int[] { 150, 350, 550, 750, 900 };

            for (int i = 0; i < cloud1PositionY.Length; i++)
            {
                int y = cloud1PositionY[i];
                Vector2 from = i % 2 == 0 ? new Vector2(-106, y) : new Vector2(2020, y);
                Vector2 to = i % 2 == 0 ? new Vector2(2020, y) : new Vector2(-106, y);
                float speed = CLOUD_MOVE_SPEED + (i * CLOUD_MOVE_SPEED_OFFSET);
                AddComponent(new Image($"mainmenu-cloud1-{i}", "mainmenu/cloud1", 0, y)).AddAnimation<Image>(new MoveAnimation("cloud1-move", speed, from, to, true, true));
            }

            for (int i = 0; i < cloud2PositionY.Length; i++)
            {
                int y = cloud2PositionY[i];
                Vector2 from = i % 2 == 0 ? new Vector2(-166, y) : new Vector2(2086, y);
                Vector2 to = i % 2 == 0 ? new Vector2(2086, y) : new Vector2(-166, y);
                float speed = CLOUD_MOVE_SPEED + (i * CLOUD_MOVE_SPEED_OFFSET);
                AddComponent(new Image($"mainmenu-cloud2-{i}", "mainmenu/cloud2", 0, y)).AddAnimation<Image>(new MoveAnimation("cloud2-move", speed, from, to, true, true));
            }
        }

        #endregion Public Methods
    }
}