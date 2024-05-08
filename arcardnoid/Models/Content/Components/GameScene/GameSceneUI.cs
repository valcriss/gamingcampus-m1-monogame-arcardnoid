using arcardnoid.Models.Content.Components.GameScene.UI;
using arcardnoid.Models.Framework.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class GameSceneUI : Component
    {
        public GameSceneUI():base("GameSceneUI")
        {

        }

        public override void Load()
        {
            base.Load();
            AddComponent(new CoinAmountUI(200,20));
        }
    }
}
