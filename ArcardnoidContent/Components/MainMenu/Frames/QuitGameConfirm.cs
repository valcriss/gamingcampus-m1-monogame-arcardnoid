using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.MainMenu.Frames
{
    public class QuitGameConfirm : GameComponent
    {
        #region Private Properties

        private Action OnCancel { get; set; }
        private Action OnConfirm { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public QuitGameConfirm(Action onConfirm, Action onCancel) : base(672, 1081)
        {
            OnConfirm = onConfirm;
            OnCancel = onCancel;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Frame(TextureType.UI_BANNER, 0, 0, 576, 384));
            AddGameComponent(new BitmapText(BitmapFontType.Default, "Voulez-vous vraiment quitter ?", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            AddGameComponent(new Button("Confirmer", TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL, TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER, TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED, OnConfirm, 80, 145, 410, 64));
            AddGameComponent(new Button("Annuler", TextureType.UI_BUTTONS_BUTTON_RED_NORMAL, TextureType.UI_BUTTONS_BUTTON_RED_HOVER, TextureType.UI_BUTTONS_BUTTON_RED_PRESSED, OnCancel, 80, 225, 410, 64));
        }

        #endregion Public Methods
    }
}