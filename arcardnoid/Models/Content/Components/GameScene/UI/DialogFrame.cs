using arcardnoid.Models.Content.Components.Map.Models;
using arcardnoid.Models.Content.Components.UI;
using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.GameScene.UI
{
    public class DialogFrame : Component
    {
        private Dictionary<PlayerFaceExpression, Rectangle> _playerFacesRect;
        private PlayerFaceExpression _currentExpression = PlayerFaceExpression.Happy;
        private DialogCollection _dialogs;
        private FastRandom _fastRandom;
        private EncounterDialog _currentDialog = null;
        private int _currentDialogStep = 0;
        private Dictionary<string, Component> _actorFace;

        private Image PlayerFaceBack { get; set; }
        private Image OponentFaceBack { get; set; }

        private BitmapText PlayerText { get; set; }
        private BitmapText OponentText { get; set; }

        private ImagePart PlayerFace { get; set; }

        private Action Callback { get; set; }

        public DialogFrame() : base("DialogFrame")
        {
            _fastRandom = new FastRandom((DateTime.Now.Second + 1000));
        }

        public override void Load()
        {
            base.Load();

            _dialogs = LoadDialogs();

            AddComponent(new Frame("dialogFrame", "ui/banner", 100, 760, 1720, 320));
            AddComponent(new Frame("pageFrame", "ui/page", 170, 830, 1580, 188));
            PlayerFaceBack = AddComponent(new Image("dialogIcon", "map/units/face-back", 270, 925));
            OponentFaceBack = AddComponent(new Image("dialogIcon", "map/units/face-back", 1650, 925));
            PlayerText = AddComponent(new BitmapText("playerText", "fonts/band", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec nunc et tellus sollicitudin porttitor non id turpis. Nunc nec faucibus est.", 370, 920, TextHorizontalAlign.Left, TextVerticalAlign.Center, Color.Black));
            OponentText = AddComponent(new BitmapText("playerText", "fonts/band", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec nunc et tellus sollicitudin porttitor non id turpis. Nunc nec faucibus est.", 1550, 920, TextHorizontalAlign.Right, TextVerticalAlign.Center, Color.Black));
            _playerFacesRect = new Dictionary<PlayerFaceExpression, Rectangle>
            {
                { PlayerFaceExpression.None, new Rectangle(0, 0, 144, 144) },
                { PlayerFaceExpression.Happy, new Rectangle(144, 0, 144, 144) },
                { PlayerFaceExpression.Shocked, new Rectangle(144*2, 0, 144, 144) },
                { PlayerFaceExpression.Sad, new Rectangle(144*3, 0, 144, 144) },
            };

            Point actorFacePosition = new Point(1650, 923);

            _actorFace = new Dictionary<string, Component>
            {
                {"Archer",AddComponent(new Image("Archer","map/units/archer-face",actorFacePosition.X,actorFacePosition.Y)) },
                {"Warrior",AddComponent(new Image("Warrior","map/units/warrior-face",actorFacePosition.X,actorFacePosition.Y)) },
                {"Torch",AddComponent(new Image("Torch","map/units/torch-face",actorFacePosition.X,actorFacePosition.Y)) },
                {"Tnt",AddComponent(new Image("Tnt","map/units/tnt-face",actorFacePosition.X,actorFacePosition.Y)) },
                {"Sheep",AddComponent(new Image("Tnt","map/units/sheep-face",actorFacePosition.X,actorFacePosition.Y)) }
            };

            foreach (var item in _actorFace)
            {
                item.Value.Visible = false;
            }

            PlayerFace = AddComponent(new ImagePart("playerFace", "map/units/player-faces", 269, 924, _playerFacesRect[_currentExpression]));
            PlayerText.Visible = false;
            OponentFaceBack.Visible = false;
            Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_currentDialog == null) return;
            // Affiche du step en cours.
            DialogStep step = _currentDialog.Steps[_currentDialogStep];
            if(step.Actor == "Player")
            {
                OponentFaceBack.Visible = false;
                OponentText.Visible = false;
                PlayerFaceBack.Visible = true;
                foreach (var item in _actorFace)
                {
                    item.Value.Visible = false;
                }
                PlayerFace.SetSourceRect(_playerFacesRect[(PlayerFaceExpression)Enum.Parse(typeof(PlayerFaceExpression), step.Face)]);
                PlayerFace.Visible = true;
                PlayerText.SetText(step.Text);
                PlayerText.Visible = true;
            }
            else
            {
                PlayerFaceBack.Visible = false;
                PlayerFace.Visible = false;
                PlayerText.Visible = false;
                OponentFaceBack.Visible = true;
                OponentText.SetText(step.Text);
                OponentText.Visible = true;
                _actorFace[step.Actor].Visible = true;
            }

            GameKeyboard.GetState();
            if (GameKeyboard.HasBeenPressed(Keys.Space))
            {
                _currentDialogStep++;
                if(_currentDialogStep >= _currentDialog.Steps.Count)
                {
                    _currentDialog = null;
                    _currentDialogStep = 0;
                    Visible = false;
                    Callback?.Invoke();
                }
            }
        }

        public void ShowDialog(EncounterType encounterType, Action callback)
        {
            Callback = callback;
            EncounterDialogCollection items = _dialogs.EncounterDialogs.FirstOrDefault(c => c.Type == encounterType);
            _currentDialog = items.EncounterDialogs[_fastRandom.Next(items.EncounterDialogs.Count-1)];
            _currentDialogStep = 0;
            Visible = true;
        }

        private DialogCollection LoadDialogs()
        {
            string filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Maps/Dialogs/dialogs.json");
            return JsonConvert.DeserializeObject<DialogCollection>(File.ReadAllText(filename));
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
