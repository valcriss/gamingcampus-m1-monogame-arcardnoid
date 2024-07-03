using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GameScene;
using ArcardnoidContent.Components.GameScene.Battle;
using ArcardnoidContent.Components.GameScene.Character;
using ArcardnoidContent.Components.GameScene.Dialogs;
using ArcardnoidContent.Components.GameScene.SubScreens;
using ArcardnoidContent.Components.GameScene.UI;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidContent.Components.Shared.Map.Models;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Scenes
{
    public class GameScene : Scene
    {
        #region Private Properties

        private static IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        private BattleContainer? BattleContainer { get; set; }
        private Cursor? Cursor { get; set; }
        private DialogFrame? DialogFrame { get; set; }
        private GameMapBackground? GameMapBackground { get; set; }
        private GameSceneUI? GameSceneUI { get; set; }
        private LoadingScreen? LoadingScreen { get; set; }
        private GameSceneState LoadingState { get; set; } = GameSceneState.None;
        private Task? LoadingTask { get; set; }
        private MainCharacter? MainCharacter { get; set; }
        private MapGenerator? MapGenerator { get; set; }
        private PauseScreen? PauseScreen { get; set; }
        private IRandom Random { get; set; }
        private RandomMap? RandomMap { get; set; }
        private int Seed { get; set; }
        private BitmapText? SeedText { get; set; }

        #endregion Private Properties

        #region Private Fields

        private bool _battleStarted = false;
        private bool _debug = false;

        #endregion Private Fields

        #region Public Constructors

        public GameScene(int seed = 1)
        {
            Random = GameServiceProvider.GetService<IRandomService>().GetRandom(seed);
            GameServiceProvider.GetService<IGamePlay>().Reset();
            BackgroundColor = new GameColor(71, 171, 169, 255);
            Seed = seed;
            MapGenerator = new MapGenerator(Random, Seed);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            GameMapBackground = AddGameComponent(new GameMapBackground());
            SeedText = AddGameComponent(new BitmapText(BitmapFontType.Regular, $"Graine : {Seed}", 10, 1050, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
            LoadingScreen = AddGameComponent(new LoadingScreen());
            BattleContainer = AddGameComponent(new BattleContainer());
            BattleContainer.Visible = BattleContainer.Enabled = false;
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
                    MapGenerator?.GenerateMap();
                });
            }

            if (LoadingTask != null && LoadingTask.IsCompleted && LoadingState == GameSceneState.Loading)
            {
                PostLoadMap();
                LoadingScreen?.Close();
                LoadingState = GameSceneState.Loaded;
            }

            if (LoadingState == GameSceneState.Loaded)
            {
                if (keyboard.HasBeenPressed("Escape"))
                {
                    if (RandomMap != null) RandomMap.Enabled = false;
                    if (MainCharacter != null) MainCharacter.Enabled = false;
                    if (BattleContainer != null) BattleContainer.Enabled = false;
                    MoveToFront(PauseScreen);
                    MoveToFront(Cursor);
                    if (PauseScreen != null) PauseScreen?.Open();
                }
            }

            GamePlay.Update(delta);
        }

        #endregion Public Methods

        #region Private Methods

        private static GameComponent AddHideAnimation<T>(GameComponent component, float duration = 0.5f) where T : GameComponent
        {
            return component.AddAnimation<T>(new AlphaFadeAnimation(duration, 1, 0, false, true, EaseType.Linear, () =>
            {
                component.Enabled = false;
                component.Visible = false;
            }));
        }

        private static GameComponent AddShowAnimation<T>(GameComponent component, float duration = 0.5f) where T : GameComponent
        {
            component.Enabled = true;
            component.Visible = true;
            return component.AddAnimation<T>(new AlphaFadeAnimation(duration, 0, 1, false, true, EaseType.Linear, () =>
            {
            }));
        }

        private void EndBattle(bool victory, Point cell)
        {
            if (MapGenerator == null || MapGenerator.MapHypothesis == null) return;
            HideBattleContainer();
            if (victory)
            {
                AnimatedCell? animatedCell = RandomMap?.GetActorCell(cell);
                if (animatedCell == null || animatedCell.TextureAsset == null) return;
                int gridX = animatedCell.GridX;
                int gridY = animatedCell.GridY;
                AnimatedCell corpse = AddGameComponent(new AnimatedCell(BattleField.LoadAssetTexture(TextureType.MAP_UNITS_DEAD_1), 7, 1, 80, 0, 0, gridX, gridY, MapGenerator.MapHypothesis.PositionX + gridX * 64, MapGenerator.MapHypothesis.PositionY + gridY * 64, 32, 16, false));
                RandomMap?.ReplaceActorCell(animatedCell.TextureAsset, corpse, cell);
                OpenObtainDialog(ObtainType.RANDOM);
                this.MoveToFront(MainCharacter);
                this.MoveToFront(Cursor);
            }
            else
            {
                GamePlay.RemoveHeart(1);
                // TODO: Gerer le fait que si il n'y a plus de vie on perd le jeu.
            }
        }

        private void HideBattleContainer()
        {
            if (BattleContainer != null) AddHideAnimation<BattleContainer>(BattleContainer);
            if (RandomMap != null) AddShowAnimation<RandomMap>(RandomMap);
            if (MainCharacter != null) AddShowAnimation<MainCharacter>(MainCharacter);
            if (GameSceneUI != null) AddShowAnimation<GameSceneUI>(GameSceneUI);
            if (SeedText != null) AddShowAnimation<BitmapText>(SeedText);
        }

        private void OnDebug()
        {
            OnResume();
            _debug = !_debug;
            RandomMap?.SetDebug(_debug);
            MainCharacter?.SetDebug(_debug);
            BattleContainer?.SetDebug(_debug);
        }

        private void OnEncounter(EncounterType type, Point cell, double distanceFromStart)
        {
            System.Diagnostics.Debug.WriteLine("Encounter : " + type + " at " + distanceFromStart + " from start");
            if (type == EncounterType.Meat && GameServiceProvider.GetService<IGamePlay>().GetHeart() == 2) return;
            DialogFrame?.ShowDialog(type, (encouterDialog) => { OnEncounterDialogEnds(encouterDialog, type, cell, distanceFromStart); });
        }

        private void OnEncounterDialogEnds(EncounterDialog encounterDialog, EncounterType type, Point cell, double distanceFromStart)
        {
            switch (type)
            {
                case EncounterType.Gold:
                    RandomMap?.ClearCell(TextureType.MAP_UNITS_GOLD, cell);
                    GameServiceProvider.GetService<IGamePlay>().AddGold(encounterDialog.Gold);
                    break;

                case EncounterType.Meat:
                    RandomMap?.ClearCell(TextureType.MAP_UNITS_MEAT, cell);
                    GameServiceProvider.GetService<IGamePlay>().AddHeart(1);
                    break;

                case EncounterType.Archer:
                case EncounterType.Warrior:
                case EncounterType.Torch:
                case EncounterType.Tnt:
                    StartBattle(type, cell, distanceFromStart);
                    break;
            }
        }

        private void OnMapClicked(Point point)
        {
            if (PauseScreen != null && PauseScreen.IsOpened() || DialogFrame != null && DialogFrame.IsOpened())
            {
                return;
            }
            MainCharacter?.SetCurrentPath(RandomMap?.GetPath((int)MainCharacter.CurrentCell.X, (int)MainCharacter.CurrentCell.Y, (int)point.X, (int)point.Y));
        }

        private void OnQuit()
        {
            GameServiceProvider.GetService<IScenesManager>().SwitchScene(this, new MainMenu());
        }

        private void OnResume()
        {
            if (RandomMap != null) RandomMap.Enabled = true;
            if (MainCharacter != null) MainCharacter.Enabled = true;
            if (BattleContainer != null) BattleContainer.Enabled = true;
        }

        private void OpenObtainDialog(ObtainType obtainType = ObtainType.RANDOM)
        {
            AddGameComponent(new ObtainDialog(Random, obtainType, StartingObtain))
                .AddAnimation<ObtainDialog>(new MoveAnimation(0.5f, new Point(-1920, 0), new Point(0, 0), false, true, EaseType.Linear))
                .AddAnimation<ObtainDialog>(new AlphaFadeAnimation(0.5f, 0, 1f, false, true, EaseType.Linear));
            this.MoveToFront(Cursor);
        }

        private void PauseMap()
        {
            if (RandomMap != null) RandomMap.Enabled = false;
            if (MainCharacter != null) MainCharacter.Enabled = false;
            if (GameSceneUI != null) GameSceneUI.Enabled = false;
        }

        private void PostLoadMap()
        {
            if (MapGenerator?.MapHypothesis == null) return;
            RandomMap = AddGameComponent(new RandomMap(MapGenerator.MapHypothesis, false));
            RandomMap.OnMapClickedEvent += OnMapClicked;
            MainCharacter = AddGameComponent(new MainCharacter(RandomMap, MapGenerator.MapHypothesis));
            MainCharacter.OnEncounter += OnEncounter;
            GameSceneUI = AddGameComponent(new GameSceneUI());
            DialogFrame = AddGameComponent(new DialogFrame());
            PauseScreen = AddGameComponent(new PauseScreen(OnResume, OnDebug, OnQuit));
            Cursor = AddGameComponent(new Cursor(TextureType.UI_CURSOR, new Point(12, 16)));
            StartingObtain();
        }

        private void ResumeMap()
        {
            if (RandomMap != null) RandomMap.Enabled = true;
            if (MainCharacter != null) MainCharacter.Enabled = true;
            if (GameSceneUI != null) GameSceneUI.Enabled = true;
        }

        private void ShowBattleContainer()
        {
            if (RandomMap != null) AddHideAnimation<RandomMap>(RandomMap);
            if (MainCharacter != null) AddHideAnimation<MainCharacter>(MainCharacter);
            if (GameSceneUI != null) AddHideAnimation<GameSceneUI>(GameSceneUI);
            if (SeedText != null) AddHideAnimation<BitmapText>(SeedText);
            if (BattleContainer != null) AddShowAnimation<BattleContainer>(BattleContainer);
        }

        private void StartBattle(EncounterType type, Point cell, double distanceFromStart)
        {
            ShowBattleContainer();
            System.Diagnostics.Debug.WriteLine("Start battle with " + type + " at " + distanceFromStart + " from start");
            this.MoveToFront(BattleContainer);
            this.MoveToFront(Cursor);
            if (RandomMap != null) BattleContainer?.Show(RandomMap.GetGroundType(cell), type, distanceFromStart, cell, EndBattle);
        }

        private void StartingObtain()
        {
            System.Diagnostics.Debug.WriteLine("Starting obtain");

            if (GamePlay.GetCards().Count < 2)
            {
                PauseMap();
                System.Diagnostics.Debug.WriteLine("Need cards");
                OpenObtainDialog(ObtainType.START);
            }
            else
            {
                ResumeMap();
            }
        }

        #endregion Private Methods
    }
}