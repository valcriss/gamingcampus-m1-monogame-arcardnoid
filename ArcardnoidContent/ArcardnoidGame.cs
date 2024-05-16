using ArcardnoidContent.Scenes;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent
{
    public class ArcardnoidGame : IGameService
    {
        private IScenesManager _scenesManager;

        public ArcardnoidGame()
        {
            _scenesManager = GameServiceProvider.GetService<IScenesManager>();
        }

        public event Action? OnGameExit;

        public void DrawGame()
        {
            _scenesManager.Draw();
        }

        public void InitializeGame()
        {
            GameServiceProvider.GetService<IScreenManager>().SetSize(new Point(1920, 1080));
        }

        public void LoadGameContent()
        {
            _scenesManager.AddScene(new GameScene());
        }

        public void ExitGame()
        {
            OnGameExit?.Invoke();
        }

        public void UpdateGame(float delta)
        {
            _scenesManager.Update(delta);
        }
    }
}
