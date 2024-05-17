using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.MainMenu.Frames
{
    public class ParametersFrame : GameComponent
    {
        #region Private Properties

        private Progress MusicVolume { get; set; }
        private Action OnClose { get; set; }
        private Progress SoundVolume { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public ParametersFrame(Action onClose) : base(672, 1081)
        {
            OnClose = onClose;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            int height = 428;
            AddGameComponent(new Frame("ui/banner", -195, 0, 960, height));
            AddGameComponent(new BitmapText("fonts/band", "Parametres", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));

            int y = 165;
            int x = -115;
            AddGameComponent(new BitmapText("fonts/band", "Volume musique:", x, y, TextHorizontalAlign.Left, TextVerticalAlign.Center, GameColor.Black));
            MusicVolume = AddGameComponent(new Progress("ui/progress/progress-normal", "ui/progress/progress-hover", "ui/progress/progress-pressed", 0.5f, x + 295, y - 25, 6));

            y += 80;
            AddGameComponent(new BitmapText("fonts/band", "Volume effets:", x, y, TextHorizontalAlign.Left, TextVerticalAlign.Center, GameColor.Black));
            SoundVolume = AddGameComponent(new Progress("ui/progress/progress-normal", "ui/progress/progress-hover", "ui/progress/progress-pressed", 0.5f, x + 295, y - 25, 6));

            AddGameComponent(new Button("Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnClose, 490, height - 124, 210, 64));
        }

        #endregion Public Methods
    }
}