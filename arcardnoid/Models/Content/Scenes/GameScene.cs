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

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            MapGenerator mapGenerator = new MapGenerator(Seed);
            mapGenerator.GenerateMap();
            AddComponent(new RandomMap(mapGenerator.MapHypotesis, false));
        }

        #endregion Public Methods
    }
}