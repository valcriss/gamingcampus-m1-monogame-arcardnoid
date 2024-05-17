using ArcardnoidContent.Components.GameScene;
using ArcardnoidContent.Components.GameScene.Character;
using ArcardnoidContent.Components.GameScene.Dialogs;
using ArcardnoidContent.Components.GameScene.SubScreens;
using ArcardnoidContent.Components.GameScene.UI;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Models;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Scenes
{
    public class GameScene : Scene
    {
        #region Private Properties

        private DialogFrame DialogFrame { get; set; }
        private LoadingScreen LoadingScreen { get; set; }
        private GameSceneState LoadingState { get; set; } = GameSceneState.None;
        private Task LoadingTask { get; set; }
        private MainCharacter MainCharacter { get; set; }
        private MapGenerator MapGenerator { get; set; }
        private PauseScreen PauseScreen { get; set; }
        private RandomMap RandomMap { get; set; }
        private int Seed { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public GameScene(int seed = 123456)
        {
            BackgroundColor = new GameColor(71, 171, 169, 255);
            Seed = seed;
            MapGenerator = new MapGenerator(Seed);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new GameMapBackground());
            AddGameComponent(new BitmapText("fonts/regular", $"Graine : {Seed}", 10, 1050, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
            LoadingScreen = AddGameComponent(new LoadingScreen());
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            IKeyboardService keyboard = GameServiceProvider.GetService<IKeyboardService>();

            if (LoadingState == GameSceneState.None)
            {
                LoadingState = GameSceneState.Loading;
                LoadingTask = Task.Run(() =>
                {
                    MapGenerator.GenerateMap();
                });
            }

            if (LoadingTask.IsCompleted && LoadingState == GameSceneState.Loading)
            {
                PostLoadMap();
                LoadingScreen.Close();
                LoadingState = GameSceneState.Loaded;
            }

            if (LoadingState == GameSceneState.Loaded)
            {
                if (keyboard.HasBeenPressed("Escape"))
                {
                    RandomMap.Enabled = false;
                    MainCharacter.Enabled = false;
                    PauseScreen.Open();
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void OnDebug()
        {
            OnResume();
            RandomMap.ToggleDebug();
            MainCharacter.ToggleDebug();
        }

        private void OnEncounter(EncounterType type, double distanceFromStart)
        {
            System.Diagnostics.Debug.WriteLine("Encounter : " + type + " at " + distanceFromStart + " from start");
            DialogFrame.ShowDialog(type, null);
        }

        private void OnMapClicked(Point point)
        {
            if (PauseScreen.IsOpened() || DialogFrame.IsOpened())
            {
                return;
            }
            MainCharacter.SetCurrentPath(RandomMap.GetPath((int)MainCharacter.CurrentCell.X, (int)MainCharacter.CurrentCell.Y, (int)point.X, (int)point.Y));
        }

        private void OnQuit()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new MainMenu());
        }

        private void OnResume()
        {
            RandomMap.Enabled = true;
            MainCharacter.Enabled = true;
        }

        private void PostLoadMap()
        {
            RandomMap = AddGameComponent(new RandomMap(MapGenerator.MapHypothesis, false));
            RandomMap.OnMapClickedEvent += OnMapClicked;
            MainCharacter = AddGameComponent(new MainCharacter(RandomMap, MapGenerator.MapHypothesis));
            MainCharacter.OnEncounter += OnEncounter;
            AddGameComponent(new GameSceneUI());
            DialogFrame = AddGameComponent(new DialogFrame());
            PauseScreen = AddGameComponent(new PauseScreen(OnResume, OnDebug, OnQuit));
            AddGameComponent(new Cursor("ui/cursors/01", new Point(12, 16)));
        }

        #endregion Private Methods
    }
}