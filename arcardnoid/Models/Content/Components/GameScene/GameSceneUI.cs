using arcardnoid.Models.Content.Components.GameScene.UI;
using arcardnoid.Models.Framework.Components.Images;
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
            AddComponent(new Image("coin-background", "ui/header", 300, 55));
            AddComponent(new HeartUI(120, 17));
            AddComponent(new HeartUI(195, 17));
            AddComponent(new CoinAmountUI(300, 5));
        }

        #endregion Public Methods
    }
}