using ArcardnoidContent.Components.GameScene.UI;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Newtonsoft.Json;
using System.Reflection;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class DialogFrame : GameComponent
    {
        #region Private Properties

        private Action<EncounterDialog>? Callback { get; set; }
        private Image? OponentFaceBack { get; set; }
        private BitmapText? OponentText { get; set; }
        private ImagePart? PlayerFace { get; set; }
        private Image? PlayerFaceBack { get; set; }
        private BitmapText? PlayerText { get; set; }

        #endregion Private Properties

        #region Private Fields

        private readonly PlayerFaceExpression _currentExpression = PlayerFaceExpression.Happy;
        private readonly IRandom _fastRandom;
        private Dictionary<string, GameComponent> _actorFace = new();
        private EncounterDialog? _currentDialog = null;
        private int _currentDialogStep = 0;
        private DialogCollection? _dialogs;
        private Dictionary<PlayerFaceExpression, Rectangle> _playerFacesRect = new();

        #endregion Private Fields

        #region Public Constructors

        public DialogFrame() : base()
        {
            _fastRandom = GameServiceProvider.GetService<IRandomService>().GetRandom(DateTime.Now.Second + 1000);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();

            _dialogs = LoadDialogs();

            AddGameComponent(new Frame("ui/banner", 100, 760, 1720, 320));
            AddGameComponent(new Frame("ui/page", 170, 830, 1580, 188));
            PlayerFaceBack = AddGameComponent(new Image("map/units/face-back", 270, 925));
            OponentFaceBack = AddGameComponent(new Image("map/units/face-back", 1650, 925));
            PlayerText = AddGameComponent(new BitmapText("fonts/band", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec nunc et tellus sollicitudin porttitor non id turpis. Nunc nec faucibus est.", 370, 920, TextHorizontalAlign.Left, TextVerticalAlign.Center, GameColor.Black));
            OponentText = AddGameComponent(new BitmapText("fonts/band", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec nunc et tellus sollicitudin porttitor non id turpis. Nunc nec faucibus est.", 1550, 920, TextHorizontalAlign.Right, TextVerticalAlign.Center, GameColor.Black));
            _playerFacesRect = new Dictionary<PlayerFaceExpression, Rectangle>
            {
                { PlayerFaceExpression.None, new Rectangle(0, 0, 144, 144) },
                { PlayerFaceExpression.Happy, new Rectangle(144, 0, 144, 144) },
                { PlayerFaceExpression.Shocked, new Rectangle(144*2, 0, 144, 144) },
                { PlayerFaceExpression.Sad, new Rectangle(144*3, 0, 144, 144) },
            };

            Point actorFacePosition = new(1650, 923);

            _actorFace = new Dictionary<string, GameComponent>
            {
                {"Archer",AddGameComponent(new Image("map/units/archer-face",(int)actorFacePosition.X,(int) actorFacePosition.Y)) },
                {"Warrior",AddGameComponent(new Image("map/units/warrior-face",(int) actorFacePosition.X,(int) actorFacePosition.Y)) },
                {"Torch",AddGameComponent(new Image("map/units/torch-face",(int) actorFacePosition.X,(int) actorFacePosition.Y)) },
                {"Tnt",AddGameComponent(new Image("map/units/tnt-face",(int) actorFacePosition.X,(int) actorFacePosition.Y)) },
                {"Sheep",AddGameComponent(new Image("map/units/sheep-face",(int) actorFacePosition.X,(int) actorFacePosition.Y)) }
            };

            foreach (var item in _actorFace)
            {
                item.Value.Visible = false;
            }

            PlayerFace = AddGameComponent(new ImagePart("map/units/player-faces", 269, 924, _playerFacesRect[_currentExpression]));
            PlayerText.Visible = false;
            OponentFaceBack.Visible = false;
            Visible = false;
        }

        public void ShowDialog(EncounterType encounterType, Action<EncounterDialog> callback)
        {
            Callback = callback;
            if (_dialogs == null) return;
            EncounterDialogCollection? items = _dialogs.EncounterDialogs.FirstOrDefault(c => c.Type == encounterType);
            _currentDialog = items?.EncounterDialogs[_fastRandom.Next(items.EncounterDialogs.Count - 1)];
            _currentDialogStep = 0;
            Visible = true;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (_currentDialog == null || OponentFaceBack == null || OponentText == null || PlayerFaceBack == null || PlayerFace == null || PlayerText == null) return;
            // Affiche du step en cours.
            DialogStep step = _currentDialog.Steps[_currentDialogStep];
            if (step.Text == null || step.Actor == null || (step.Actor == "Player" && step.Face == null)) return;
            if (step.Actor == "Player")
            {
                OponentFaceBack.Visible = false;
                OponentText.Visible = false;
                PlayerFaceBack.Visible = true;
                foreach (var item in _actorFace)
                {
                    item.Value.Visible = false;
                }
                if (step.Face != null)
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

            IKeyboardService keyboard = GameServiceProvider.GetService<IKeyboardService>();
            IMouseService mouse = GameServiceProvider.GetService<IMouseService>();
            if (keyboard.HasBeenPressed("Space") || mouse.IsMouseLeftButtonPressed())
            {
                _currentDialogStep++;
                if (_currentDialogStep >= _currentDialog.Steps.Count)
                {
                    Callback?.Invoke(_currentDialog);
                    _currentDialog = null;
                    _currentDialogStep = 0;
                    Visible = false;
                }
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal bool IsOpened()
        {
            return Visible;
        }

        #endregion Internal Methods

        #region Private Methods

        private static DialogCollection? LoadDialogs()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            string? directory = Path.GetDirectoryName(location) ?? throw new Exception("Base Directory not found");
            string filename = Path.Combine(directory, "Maps/Dialogs/dialogs.json");
            return JsonConvert.DeserializeObject<DialogCollection>(File.ReadAllText(filename));
        }

        #endregion Private Methods
    }
}