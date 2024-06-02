using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GamePlay.Cards;
using ArcardnoidContent.Components.GameScene.Cards;
using ArcardnoidContent.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class ObtainDialog : GameComponent
    {
        #region Private Fields

        private Card _card;
        private ObtainType _obtainType;
        private Action _onClose;
        private IRandom _random;

        #endregion Private Fields

        #region Public Constructors

        public ObtainDialog(IRandom random, ObtainType obtainType, Action onClose)
        {
            _random = random;
            _obtainType = obtainType;
            _onClose = onClose;
            _card = GetObtainedCard();
            AddGameComponent(new DialogBackground(0.75f));
            AddGameComponent(new CardImage(_card, 704, 191));
            AddGameComponent(new AnimatedTitleBand(TextureType.UI_BANDEAU, BitmapFontType.Default, "Vous avez obtenu", 0.05f, 960, 180, new GameColor(75, 30, 0)));
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(float delta)
        {
            base.Update(delta);
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
            if (mouseService.IsMouseLeftButtonPressed())
            {
                IGamePlay gamePlay = GameServiceProvider.GetService<IGamePlay>();
                if (_card.CardType == CardType.Money)
                {
                    gamePlay.AddGold((int)_card.CardParam);
                }
                else
                {
                    gamePlay.AddCard(_card);
                }
                _onClose();
                this.InnerUnload();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private Card GetObtainedCard()
        {
            return GameServiceProvider.GetService<ICardProviderService>().GetRandomCard(_random, _obtainType == ObtainType.START);
        }

        #endregion Private Methods
    }
}