using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    public class ParametersFrame : Component
    {
        #region Private Properties

        private Progress MusicVolume { get; set; }
        private Action OnClose { get; set; }
        private Progress SoundVolume { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public ParametersFrame(Action onClose) : base("ParametersFrame", 672, 1081)
        {
            OnClose = onClose;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            int height = 428;
            AddComponent(new Frame("frame", "ui/banner", -195, 0, 960, height));
            AddComponent(new BitmapText("txtTitle", "fonts/band", "Parametres", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));

            int y = 165;
            int x = -115;
            AddComponent(new BitmapText("txtTitle", "fonts/band", "Volume musique:", x, y, TextHorizontalAlign.Left, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));
            MusicVolume = AddComponent(new Progress(Name + "Volume", "ui/progress/progress-normal", "ui/progress/progress-hover", "ui/progress/progress-pressed", 0.5f, x + 295, y - 25, 6));

            y += 80;
            AddComponent(new BitmapText("txtTitle", "fonts/band", "Volume effets:", x, y, TextHorizontalAlign.Left, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));
            SoundVolume = AddComponent(new Progress(Name + "Volume", "ui/progress/progress-normal", "ui/progress/progress-hover", "ui/progress/progress-pressed", 0.5f, x + 295, y - 25, 6));

            AddComponent(new Button("btnParametres", "Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnClose, 490, height - 124, 210, 64));
        }

        #endregion Public Methods
    }
}