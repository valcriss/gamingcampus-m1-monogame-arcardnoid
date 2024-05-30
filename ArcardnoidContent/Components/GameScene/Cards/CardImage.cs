using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GamePlay.Cards;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;

namespace ArcardnoidContent.Components.GameScene.Cards
{
    public class CardImage : MouseInterractComponent
    {
        #region Public Properties

        public Card Card { get; set; }

        #endregion Public Properties

        #region Private Properties

        private Action<CardImage>? CardClicked { get; set; }
        private IGamePlay GamePlay { get; set; } = GameServiceProvider.GetService<IGamePlay>();
        private Image? Image { get; set; }

        private bool Used { get; set; } = false;

        #endregion Private Properties

        #region Private Fields

        private bool _small;

        #endregion Private Fields

        #region Public Constructors

        public CardImage(Card card, int x, int y, bool small = false, Action<CardImage>? cardClicked = null) : base(null, x, y, small ? 212 : 512, small ? 297 : 718)
        {
            _small = small;
            Card = card;
            CardClicked = cardClicked;
            this.OnClick = OnCardClicked;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            Image = AddGameComponent(new Image(_small ? Card.CardSmallTexture : Card.CardTexture, _small ? 106 : 256, _small ? 148 : 359));
            AddGameComponent(new Primitive2D((primitive) =>
            {
                primitive.DrawRectangle(RealBounds, GameColor.Red, 2f);
            }));
        }

        public void OnCardClicked()
        {
            if (!_small || !IsAvailable()) return;
            Used = true;
            CardClicked?.Invoke(this);
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Image?.UpdateRenderBounds();
            UpdateAvailable();
        }

        #endregion Public Methods

        #region Internal Methods

        internal void UpdateAvailable()
        {
            if (!_small) return;
            if (IsAvailable())
            {
                Opacity = 1f;
            }
            else
            {
                Opacity = 0.5f;
            }

            if (InterractState == MouseInterractState.Hover)
            {
                Image.Color = GameColor.Red;
            }
            else
            {
                Image.Color = GameColor.White;
            }
        }

        #endregion Internal Methods

        #region Private Methods

        private bool IsAvailable()
        {
            return !Used && (Card.MaxUnits == 0 || GamePlay.GetUnits() <= Card.MaxUnits);
        }

        #endregion Private Methods
    }
}