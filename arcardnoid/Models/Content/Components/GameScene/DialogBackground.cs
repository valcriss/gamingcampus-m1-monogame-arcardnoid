using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class DialogBackground : Component
    {

        public DialogBackground() : base("DialogBackground", 0, 0, 1920, 1080)
        {
            Color = Microsoft.Xna.Framework.Color.Black;
            Opacity = 0;
        }

        public override void Draw()
        {
            base.Draw();
            Primitives2D.FillRectangle(Game.SpriteBatch, Bounds.ToRectangle(), Color);
        }
    }
}
