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

        public GameScene(int seed)
        {
            BackgroundColor = Color.White;
            Seed = seed;
        }

        #endregion Public Constructors
    }
}