using arcardnoid.Models.Content.Components.GameScene.UI;
using arcardnoid.Models.Framework.Scenes;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class GameSceneUI : Component
    {
        #region Public Constructors

        public GameSceneUI() : base("GameSceneUI")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new CoinAmountUI(200, 20));
        }

        #endregion Public Methods
    }
}