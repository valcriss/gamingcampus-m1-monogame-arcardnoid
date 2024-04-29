using arcardnoid.Models.Content.Components.Map;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Content.Scenes
{
    public class GameScene : Scene
    {
        #region Private Properties

        private int Seed { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public GameScene(int seed = 123456)
        {
            BackgroundColor = Color.FromNonPremultiplied(71, 171, 169, 255);
            Seed = seed;
        }

        public override void Load()
        {
            base.Load();
            AddComponent(new DynamicGameMap(true));
        }

        #endregion Public Constructors
    }
}