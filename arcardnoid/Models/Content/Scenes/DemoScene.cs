using arcardnoid.Models.Content.Components.Map;
using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace arcardnoid.Models.Content.Scenes
{
    internal class DemoScene : Scene
    {
        #region Public Constructors

        private int _filterIndex = 0;

        private string[] _filters = new string[]
        {
            "22\\0\\1\\*.json",
            "22\\0\\2\\*.json",
            "23\\0\\1\\*.json",
            /*
            "chunk-450-1-*",
            "chunk-450-2-*",
            "chunk-450-3-*",
            "chunk-451-1-*",
            "chunk-451-2-*",
            */
        };

        private bool _loaded = false;

        private DynamicGameMap DynamicGameMap { get; set; }
        private BitmapText BitmapText { get; set; }

        private bool Debug { get; set; }

        public DemoScene()
        {
            BackgroundColor = Color.FromNonPremultiplied(71, 171, 169, 255);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            DynamicGameMap = AddComponent(new DynamicGameMap());
            AddComponent(new Button("btnParametres", "Precedent", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnPrevious, 50, 1080 - 80, 210, 64));
            AddComponent(new Button("btnParametres", "Debug", "ui/buttons/button-blue-normal", "ui/buttons/button-blue-hover", "ui/buttons/button-blue-pressed", OnDebug, 1920 - (210 + 50), 1080 - 80, 210, 64));
            AddComponent(new Button("btnParametres", "Suivant", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnNext, 260, 1080 - 80, 210, 64));
            BitmapText = AddComponent(new BitmapText("txt", "fonts/band", "", 960, 1025, TextHorizontalAlign.Center, TextVerticalAlign.Center, Microsoft.Xna.Framework.Color.Black));
            AddComponent(new Cursor("cursor", "ui/cursors/01", new Vector2(12, 16)));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!_loaded)
            {
                LoadTiles();
                _loaded = true;
            }
        }

        private void LoadTiles()
        {
            string filter = _filters[_filterIndex];
            DynamicGameMap.LoadTiles(filter);
            BitmapText.SetText(filter.Replace("\\","-").Substring(0, filter.Length -7));
        }

        private void OnNext()
        {
            if (!_loaded) return;
            _filterIndex++;
            if (_filterIndex >= _filters.Length)
            {
                _filterIndex = 0;
            }
            LoadTiles();
        }

        private void OnPrevious()
        {
            if (!_loaded) return;
            _filterIndex--;
            if (_filterIndex < 0)
            {
                _filterIndex = _filters.Length - 1;
            }
            LoadTiles();
        }

        private void OnDebug()
        {
            if (!_loaded) return;
            Debug = !Debug;
            DynamicGameMap.SetForceDebug(Debug);
            LoadTiles();
        }


        #endregion Public Methods
    }
}
