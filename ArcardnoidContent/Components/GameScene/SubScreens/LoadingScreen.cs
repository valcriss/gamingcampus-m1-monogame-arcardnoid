using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene.SubScreens
{
    public class LoadingScreen : GameComponent
    {
        #region Private Properties

        private BitmapText BitmapText { get; set; } = null;
        private IPrimitives2D Primitives2D => GameServiceProvider.GetService<IPrimitives2D>();

        #endregion Private Properties

        #region Private Fields

        private const double _messageChangeTime = 1;
        private double _elapsedTime = 0;

        private string[] _loadingMessages = new string[]
        {
            "Les architectes sont encore en train de dessiner",
            "Les artistes sont en train de peindre",
            "Nous construisons les batiments aussi vite que possible",
            "Les developpeurs sont en train de coder",
            "Veuillez patienter pendant que les petits lutins dessinent votre carte",
            "Le serveur est alimente par un citron et deux electrodes.",
            "Les hamsters sont en train de courir dans leur roue",
            "Nous testons votre patience",
            "Suis le lapin blanc",
            "Nous sommes en train de construire votre monde",
            "Pourquoi ne commandes-tu pas un sandwich ?",
            "Pendant que le satellite se met en position",
            "C'est toujours plus rapide que tu ne pourrais le dessiner",
            "La derniere fois que j'ai essaye, le singe n'a pas survecu. Esperons que cela fonctionnera mieux cette fois.",
            "All your bases are belong to us",
            "Les licornes sont au bout de ce chemin, je le promets."
        };

        private int _messageIndex = 0;
        private IRandom _messageRandom;

        #endregion Private Fields

        #region Public Constructors

        public LoadingScreen() : base()
        {
            _messageRandom = GameServiceProvider.GetService<IRandomService>().GetRandom(DateTime.Now.Second + 1000);
            Color = GameColor.Black;
            _messageIndex = _messageRandom.Next(0, _loadingMessages.Length - 1);
        }

        #endregion Public Constructors

        #region Public Methods

        public void Close()
        {
            BitmapText.InnerUnload();
            AddAnimation<LoadingScreen>(new AlphaFadeAnimation(1f, 1, 0, false, true, EaseType.Linear, () => { InnerUnload(); }));
        }

        public override void Draw()
        {
            Primitives2D.FillRectangle(new Rectangle(0, 0, 1920, 1080), Color);
            base.Draw();
        }

        public override void Load()
        {
            base.Load();
            BitmapText = AddGameComponent(new BitmapText("fonts/band", _loadingMessages[_messageIndex], 1920 / 2, 1080 / 2, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.White));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            _elapsedTime += delta;
            if (_elapsedTime >= _messageChangeTime)
            {
                _messageIndex = _messageRandom.Next(0, _loadingMessages.Length - 1);
                BitmapText.SetText(_loadingMessages[_messageIndex]);
                _elapsedTime = 0;
            }
        }

        #endregion Public Methods
    }
}