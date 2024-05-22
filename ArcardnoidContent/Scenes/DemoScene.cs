using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Scenes
{
    public class DemoScene : Scene
    {
        #region Private Properties

        private BitmapText? BitmapText { get; set; }
        private bool Debug { get; set; }
        private DynamicGameMap? DynamicGameMap { get; set; }

        #endregion Private Properties

        #region Private Fields

        private readonly string[] _filters = new string[]
        {
            "22\\0\\1\\*.json",
            "22\\0\\2\\*.json",
            "22\\0\\3\\*.json",
            "23\\0\\1\\*.json",
            "23\\0\\2\\*.json",
            "23\\0\\3\\*.json",
            "32\\0\\1\\*.json",
            "32\\0\\2\\*.json",
            "32\\0\\3\\*.json",
        };

        private int _filterIndex = 0;
        private bool _loaded = false;

        #endregion Private Fields

        #region Public Constructors

        public DemoScene()
        {
            BackgroundColor = new GameColor(71, 171, 169);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            DynamicGameMap = AddGameComponent(new DynamicGameMap());
            AddGameComponent(new Button("Precedent", "ui/buttons/button-yellow-normal", "ui/buttons/button-yellow-hover", "ui/buttons/button-yellow-pressed", OnPrevious, 50, 1080 - 80, 210, 64));
            AddGameComponent(new Button("Suivant", "ui/buttons/button-yellow-normal", "ui/buttons/button-yellow-hover", "ui/buttons/button-yellow-pressed", OnNext, 260, 1080 - 80, 210, 64));
            AddGameComponent(new Button("Retour", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnReturn, 1450, 1080 - 80, 210, 64));
            AddGameComponent(new Button("Debug", "ui/buttons/button-blue-normal", "ui/buttons/button-blue-hover", "ui/buttons/button-blue-pressed", OnDebug, 1920 - (260), 1080 - 80, 210, 64));
            BitmapText = AddGameComponent(new BitmapText("fonts/band", "", 960, 1025, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            AddGameComponent(new Cursor("ui/cursors/01", new Point(12, 16)));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (!_loaded)
            {
                LoadTiles();
                _loaded = true;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadTiles()
        {
            string filter = _filters[_filterIndex];
            DynamicGameMap?.LoadTiles(filter);
            BitmapText?.SetText(filter.Replace("\\", "-")[..(filter.Length - 7)]);
        }

        private void OnDebug()
        {
            if (!_loaded) return;
            Debug = !Debug;
            DynamicGameMap?.SetForceDebug(Debug);
            LoadTiles();
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

        private void OnReturn()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new MainMenu());
        }

        #endregion Private Methods
    }
}