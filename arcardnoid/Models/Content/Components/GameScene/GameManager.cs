using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class GameManager
    {
        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            Instance = this;
        }
    }
}
