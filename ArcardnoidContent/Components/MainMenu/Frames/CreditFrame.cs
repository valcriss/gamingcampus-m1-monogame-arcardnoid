using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.MainMenu.Frames
{
    public class CreditFrame : GameComponent
    {
        #region Private Properties

        private Dictionary<string, string> Content { get; set; } = new Dictionary<string, string>()
        {
            {"Assets Graphique","Tiny Swords by Pixel Frog (https://pixelfrog-assets.itch.io/tiny-swords)" },
            {"Assets Audio",null},
            {"Snippets Code","Primitives2D by DoogeJ (https://github.com/DoogeJ/MonoGame.Primitives2D)\nEasingFunctions by Kryzarel (https://gist.github.com/Kryzarel)"},
            {"Logiciels","Krita (https://krita.org/fr/)\nInkscape (https://inkscape.org/fr/)"},
            {"Remerciements",null},
        };

        private Action OnClose { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public CreditFrame(Action onClose) : base(672, 1081)
        {
            OnClose = onClose;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            int height = 684;
            AddGameComponent(new Frame("ui/banner", -195, 0, 960, height));
            AddGameComponent(new BitmapText("fonts/band", "Credits", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            int y = 115;
            int categoryX = -90;
            int categoryContentX = categoryX + 20;
            foreach (var item in Content)
            {
                if (item.Value == null) continue;
                AddGameComponent(new BitmapText("fonts/band", item.Key, categoryX, y, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.Black));
                AddGameComponent(new BitmapText("fonts/ken", item.Value, categoryContentX, y + 45, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.Black));
                y += (item.Value.Split('\n').Length - 1) * 22 + 70;
            }
            AddGameComponent(new Button("Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnClose, 490, height - 124, 210, 64));
        }

        #endregion Public Methods
    }
}