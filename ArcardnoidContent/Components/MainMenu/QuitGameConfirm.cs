using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.MainMenu
{
    public class QuitGameConfirm : GameComponent
    {
        private Action OnCancel { get; set; }
        private Action OnConfirm { get; set; }

        public QuitGameConfirm(Action onConfirm, Action onCancel) : base(672, 1081)
        {
            OnConfirm = onConfirm;
            OnCancel = onCancel;
        }

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Frame("ui/banner", 0, 0, 576, 384));
            AddGameComponent(new BitmapText("fonts/band", "Voulez-vous vraiment quitter ?", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            AddGameComponent(new Button("Confirmer", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnConfirm, 80, 145, 410, 64));
            AddGameComponent(new Button("Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnCancel, 80, 225, 410, 64));
        }
    }
}
