﻿using ArcardnoidContent.Components.GamePlay;
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

        private BattleContainer? BattleContainer { get; set; }
        private DialogFrame? DialogFrame { get; set; }
        private GameMapBackground? GameMapBackground { get; set; }
        private GameSceneUI? GameSceneUI { get; set; }
        private LoadingScreen? LoadingScreen { get; set; }
        private GameSceneState LoadingState { get; set; } = GameSceneState.None;
        private Task? LoadingTask { get; set; }
        private MainCharacter? MainCharacter { get; set; }
        private MapGenerator? MapGenerator { get; set; }
        private PauseScreen? PauseScreen { get; set; }
        private RandomMap? RandomMap { get; set; }
        private int Seed { get; set; }
        private BitmapText? SeedText { get; set; }

        #endregion Private Properties

        #region Private Fields

        private bool _battleStarted = false;

        #endregion Private Fields

        #region Public Constructors

        public GameScene(int seed = 1)
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
            GameMapBackground = AddGameComponent(new GameMapBackground());
            SeedText = AddGameComponent(new BitmapText("fonts/regular", $"Graine : {Seed}", 10, 1050, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
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
                    if (PauseScreen != null) PauseScreen?.Open();
                }
            }
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
            if (BattleContainer != null) AddHideAnimation<BattleContainer>(BattleContainer);
            if (RandomMap != null) AddShowAnimation<RandomMap>(RandomMap);
            if (MainCharacter != null) AddShowAnimation<MainCharacter>(MainCharacter);
            if (GameSceneUI != null) AddShowAnimation<GameSceneUI>(GameSceneUI);
            if (SeedText != null) AddShowAnimation<BitmapText>(SeedText);
            if (victory)
            {
                AnimatedCell? animatedCell = RandomMap?.GetActorCell(cell);
                if (animatedCell == null) return;
                int gridX = animatedCell.GridX;
                int gridY = animatedCell.GridY;
                AnimatedCell corpse = AddGameComponent(new AnimatedCell(BattleField.LoadAssetTexture("map/units/dead-1"), 7, 1, 80, 0, 0, gridX, gridY, MapGenerator.MapHypothesis.PositionX + gridX * 64, MapGenerator.MapHypothesis.PositionY + gridY * 64, 32, 16, false));
                RandomMap?.ReplaceActorCell(animatedCell.TextureAsset, corpse, cell);
                this.MoveToFront<MainCharacter>(MainCharacter);
            }
        }

        private void OnDebug()
        {
            OnResume();
            RandomMap?.ToggleDebug();
            MainCharacter?.ToggleDebug();
            BattleContainer?.ToggleDebug();
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
                    RandomMap?.ClearCell(MapCell.GOLD_ASSET, cell);
                    GameServiceProvider.GetService<IGamePlay>().AddGold(encounterDialog.Gold);
                    break;

                case EncounterType.Meat:
                    RandomMap?.ClearCell(MapCell.MEAT_ASSET, cell);
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
            AddGameComponent(new Cursor("ui/cursors/01", new Point(12, 16)));
        }

        private void StartBattle(EncounterType type, Point cell, double distanceFromStart)
        {
            if (RandomMap != null) AddHideAnimation<RandomMap>(RandomMap);
            if (MainCharacter != null) AddHideAnimation<MainCharacter>(MainCharacter);
            if (GameSceneUI != null) AddHideAnimation<GameSceneUI>(GameSceneUI);
            if (SeedText != null) AddHideAnimation<BitmapText>(SeedText);
            if (BattleContainer != null) AddShowAnimation<BattleContainer>(BattleContainer);
            System.Diagnostics.Debug.WriteLine("Start battle with " + type + " at " + distanceFromStart + " from start");
            this.MoveToFront(BattleContainer);
            if (RandomMap != null) BattleContainer?.Show(RandomMap.GetGroundType(cell), type, distanceFromStart, cell, EndBattle);
        }

        #endregion Private Methods
    }
}