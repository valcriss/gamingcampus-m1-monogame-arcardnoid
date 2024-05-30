using ArcardnoidContent.Components.GamePlay.Cards;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.Cards
{
    public class CardImage : GameComponent
    {
        #region Private Properties

        private Card Card { get; set; }
        private Image? Image { get; set; }

        #endregion Private Properties

        #region Private Fields

        private bool _small;

        #endregion Private Fields

        #region Public Constructors

        public CardImage(Card card, int x, int y, bool small = false) : base(x, y)
        {
            _small = small;
            Card = card;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            Image = AddGameComponent(new Image(_small ? Card.CardSmallTexture : Card.CardTexture, 0, 0));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Image?.UpdateRenderBounds();
        }

        #endregion Public Methods
    }
}