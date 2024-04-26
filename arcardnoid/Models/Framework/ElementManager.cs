namespace arcardnoid.Models.Framework
{
    public class ElementManager
    {
        #region Protected Properties

        protected BaseGame Game { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public ElementManager(BaseGame game)
        {
            Game = game;
        }

        #endregion Public Constructors
    }
}