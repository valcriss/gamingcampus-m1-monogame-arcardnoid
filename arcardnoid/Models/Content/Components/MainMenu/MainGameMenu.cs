using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    public class MainGameMenu : Component
    {
        public MainGameMenu(int x,int y) : base("MainGameMenu", x, y)
        {
        }

        public override void Load()
        {
            base.Load();
            AddComponent(new Frame("frame","ui/banner", 0, 0, 576, 448));
        }
    }
    
}
