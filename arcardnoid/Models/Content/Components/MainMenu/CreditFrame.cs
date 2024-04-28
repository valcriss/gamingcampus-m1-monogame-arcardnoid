using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;
using System.Collections.Generic;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    public class CreditFrame : Component
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

        public CreditFrame(Action onClose) : base("CreditFrame", 672, 1081)
        {
            OnClose = onClose;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            int height = 684;
            AddComponent(new Frame("frame", "ui/banner", -195, 0, 960, height));
            AddComponent(new BitmapText("txtTitle", "fonts/band", "Credits", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));
            int y = 115;
            int categoryX = -90;
            int categoryContentX = categoryX + 20;
            foreach (var item in Content)
            {
                if (item.Value == null) continue;
                AddComponent(new BitmapText("txt" + item.Key, "fonts/band", item.Key, categoryX, y, TextHorizontalAlign.Left, TextVerticalAlign.Top, Microsoft.Xna.Framework.Color.Black));
                var bitmap = AddComponent(new BitmapText("txt" + item.Key + "Content", "fonts/ken", item.Value, categoryContentX, y + 45, TextHorizontalAlign.Left, TextVerticalAlign.Top, Microsoft.Xna.Framework.Color.Black));
                y += ((item.Value.Split('\n').Length - 1) * 22) + 70;
            }
            AddComponent(new Button("btnParametres", "Annuler", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnClose, 490, height - 124, 210, 64));
        }

        #endregion Public Methods
    }
}