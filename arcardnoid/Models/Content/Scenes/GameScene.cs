﻿using arcardnoid.Models.Content.Components.GameScene;
using arcardnoid.Models.Content.Components.Map;
using arcardnoid.Models.Content.Components.Map.Characters;
using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Scenes
{
    public enum GameSceneState
    {
        None,
        Loading,
        Loaded,
    }

    public class GameScene : Scene
    {
        #region Private Properties

        private LoadingScreen LoadingScreen { get; set; }
        private GameSceneState LoadingState { get; set; } = GameSceneState.None;
        private Task LoadingTask { get; set; }
        private MapGenerator MapGenerator { get; set; }
        private PauseScreen PauseScreen { get; set; }
        private RandomMap RandomMap { get; set; }
        private MainCharacter MainCharacter { get; set; }

        private int Seed { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public GameScene(int seed = 123456)
        {
            BackgroundColor = Color.FromNonPremultiplied(71, 171, 169, 255);
            Seed = seed;
            MapGenerator = new MapGenerator(Seed);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new GameMapBackground());
            AddComponent(new BitmapText("seed", "fonts/regular", $"Graine : {Seed}", 10, 1050, TextHorizontalAlign.Left, TextVerticalAlign.Top, Color.White));
            LoadingScreen = AddComponent(new LoadingScreen());            
        }

        private void OnResume()
        {
            RandomMap.Enabled = true;
            MainCharacter.Enabled = true;
        }

        private void OnDebug()
        {
            OnResume();
            RandomMap.ToggleDebug();
        }

        private void OnQuit()
        {
            Game.ScenesManager.SwitchScene(this, new MainMenu());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

            if(LoadingState == GameSceneState.Loaded)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    RandomMap.Enabled = false;
                    MainCharacter.Enabled = false;
                    PauseScreen.Open();
                }
            }
        }

        private void PostLoadMap()
        {
            RandomMap = AddComponent(new RandomMap(MapGenerator.MapHypothesis, false));
            MainCharacter = AddComponent(new MainCharacter(MapGenerator.MapHypothesis));
            AddComponent(new GameSceneUI());
            PauseScreen = AddComponent(new PauseScreen(OnResume, OnDebug, OnQuit));
            AddComponent(new Cursor("cursor", "ui/cursors/01", new Vector2(12, 16)));
        }

        #endregion Public Methods
    }
}