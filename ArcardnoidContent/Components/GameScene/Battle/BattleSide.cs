using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Scenes.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleSide : GameComponent
    {
        private Frame Frame { get; set; }
        private StaticMap StaticMap { get; set; }

        public BattleSide(int x,int y): base(x, y, 1920 / 2, 952)
        {
            Frame = AddGameComponent(new Frame("ui/banner", 0, 0, 1920 / 2, 952));
            StaticMap = AddGameComponent(new StaticMap("Maps/grassmap.json", 64, 60, true));
        }
    }
}
