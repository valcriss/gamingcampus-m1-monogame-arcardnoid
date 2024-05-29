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

        #region Public Constructors

        public CardImage(Card card, int x, int y) : base(x, y)
        {
            Card = card;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            Image = AddGameComponent(new Image(Card.CardTexture, 0, 0));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Image?.UpdateRenderBounds();
        }

        #endregion Public Methods
    }
}