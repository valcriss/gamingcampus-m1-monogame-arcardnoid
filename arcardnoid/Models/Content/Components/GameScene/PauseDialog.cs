using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;

namespace arcardnoid.Models.Content.Components.GameScene
{
    public class PauseDialog : Component
    {
        private Action OnResume { get; set; }
        private Action OnDebug { get; set; }
        private Action OnQuit { get; set; }

        public PauseDialog(Action onResume,Action onDebug,Action onQuit) : base("PauseDialog", 672, 1081)
        {
            OnResume = onResume;
            OnDebug = onDebug;
            OnQuit = onQuit;
        }

        public override void Load()
        {
            base.Load();
            AddComponent(new Frame("frame", "ui/banner", 0, 0, 576, 448));
            AddComponent(new BitmapText("txtTitle", "fonts/band", "Jeu en pause", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));
            int buttonStartY = 150;
            AddComponent(new Button("btnResume", "Reprendre", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnResume, 80, buttonStartY, 410, 64));
            AddComponent(new Button("btnDebug", "Debug", "ui/buttons/button-blue-normal", "ui/buttons/button-blue-hover", "ui/buttons/button-blue-pressed", OnDebug, 80, buttonStartY + (70), 410, 64));
            AddComponent(new Button("btnQuit", "Quitter", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnQuit, 80, buttonStartY + (70 * 2), 410, 64));
        }
    }
}
