using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene.UI
{
    public class CoinAmountUI : Component
    {
        private int CoinAmount { get; set; } = 0;

        public CoinAmountUI(int x, int y) : base("CoinAmountUI", x, y)
        {

        }

        public override void Load()
        {
            base.Load();
            AddComponent(new Image("coin-icon", "ui/coin", 0, 20));
            AddComponent(new BitmapText("coin-amount", "fonts/band", CoinAmount.ToString() + " Or", 40, 0, TextHorizontalAlign.Left, TextVerticalAlign.Top, Microsoft.Xna.Framework.Color.White));
        }
    }
}
