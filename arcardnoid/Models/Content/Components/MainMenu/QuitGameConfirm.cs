using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    internal class QuitGameConfirm : Component
    {
        #region Private Properties

        private Action OnCancel { get; set; }
        private Action OnConfirm { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public QuitGameConfirm(Action onConfirm, Action onCancel) : base("QuitGameConfirm", 672, 1081)
        {
            OnConfirm = onConfirm;
            OnCancel = onCancel;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new Frame("frame", "ui/banner", 0, 0, 576, 384));

            AddComponent(new BitmapText("txtTitle", "fonts/band", "Voulez-vous vraiment quitter ?", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));
            int buttonStartY = 145;
            AddComponent(new Button("btnPlay", "Confirmer", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnConfirm, 80, buttonStartY, 410, 64));
            AddComponent(new Button("btnParametres", "Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnCancel, 80, buttonStartY + (80), 410, 64));
        }

        #endregion Public Methods
    }
}