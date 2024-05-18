using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleContainer : GameComponent
    {
        private BattleSide LeftBattleSide { get; set; }
        private BattleSide RightBattleSide { get; set; }

        public BattleContainer()
        {
            LeftBattleSide = AddGameComponent(new BattleSide(0, 0));
            RightBattleSide = AddGameComponent(new BattleSide(1920 / 2, 0));
        }

        public void Show()
        {
            AddShowAnimation(LeftBattleSide, true);
            AddShowAnimation(RightBattleSide, false);
        }

        private void AddShowAnimation(BattleSide side, bool left)
        {
            side.AddAnimation<BattleSide>(new MoveAnimation(1.5f, new Point(left ? -1920 / 2 : 1920, 0), new Point(left ? 0 : 1920 / 2, 0), false, true, EaseType.InBounce));
        }
    }
}
