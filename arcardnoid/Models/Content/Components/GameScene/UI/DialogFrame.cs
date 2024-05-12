using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene.UI
{
    public class DialogFrame : Component
    {
        private Dictionary<PlayerFaceExpression, Rectangle> _playerFacesRect;
        private PlayerFaceExpression _currentExpression = PlayerFaceExpression.Happy;
        public DialogFrame() : base("DialogFrame")
        {

        }

        public override void Load()
        {
            base.Load();

            AddComponent(new Frame("dialogFrame", "ui/banner", 100, 760, 1720, 320));
            AddComponent(new Frame("pageFrame", "ui/page", 170, 830, 1580, 188));
            AddComponent(new Image("dialogIcon", "map/units/face-back", 270, 925));

            _playerFacesRect = new Dictionary<PlayerFaceExpression, Rectangle>
            {
                { PlayerFaceExpression.None, new Rectangle(0, 0, 144, 144) },
                { PlayerFaceExpression.Happy, new Rectangle(144, 0, 144, 144) },
                { PlayerFaceExpression.Shocked, new Rectangle(144*2, 0, 144, 144) },
                { PlayerFaceExpression.Sad, new Rectangle(144*3, 0, 144, 144) },
            };

            AddComponent(new ImagePart("playerFace", "map/units/player-faces", 269, 924, _playerFacesRect[_currentExpression]));
        }
    }

    public enum PlayerFaceExpression
    {
        None = 0,
        Happy = 1,
        Shocked = 2,
        Sad = 3,
    }
}
