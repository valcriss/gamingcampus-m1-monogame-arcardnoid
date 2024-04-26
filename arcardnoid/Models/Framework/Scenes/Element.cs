using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Framework.Scenes
{
    public abstract class Element
    {
        #region Public Properties

        public ElementState State { get; set; }

        #endregion Public Properties

        #region Protected Properties

        protected BaseGame Game { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public Element()
        {
            State = ElementState.None;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual void Draw()
        {
        }

        public virtual void Load()
        {
        }

        public void SetGame(BaseGame game)
        {
            Game = game;
        }

        public virtual void Unload()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        #endregion Public Methods
    }
}