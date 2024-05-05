using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    public class SeedFrame : Component
    {
        #region Private Properties

        private Input Input { get; set; }
        private Action OnCancel { get; set; }
        private Action OnConfirm { get; set; }
        private Random Random { get; set; } = new Random();

        #endregion Private Properties

        #region Public Constructors

        public SeedFrame(Action onConfirm, Action onCancel) : base("QuitGameConfirm", 672, 1081)
        {
            OnConfirm = onConfirm;
            OnCancel = onCancel;
        }

        #endregion Public Constructors

        #region Public Methods

        public int GetSeed()
        {
            return int.Parse(Input.GetValue());
        }

        public override void Load()
        {
            base.Load();
            AddComponent(new Frame("frame", "ui/banner", 0, 0, 576, 512));

            AddComponent(new BitmapText("txtTitle", "fonts/band", "Graine de la partie", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));

            Input = AddComponent(new Input("inputSeed", "ui/input", "", 95, 140, 4));

            int buttonStartY = 300;
            AddComponent(new Button("btnPlay", "Confirmer", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnConfirm, 80, buttonStartY, 410, 64));
            AddComponent(new Button("btnParametres", "Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnCancel, 80, buttonStartY + (70), 410, 64));
            NewSeed();
        }

        public void NewSeed()
        {
            string seed = Random.Next(100000, 999999).ToString();
            //seed = 123456.ToString();
            Input.SetValue(seed);
        }

        #endregion Public Methods
    }
}