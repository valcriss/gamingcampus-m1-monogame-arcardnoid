namespace arcardnoid.Models.Content.Components.GameScene
{
    public class GameManager
    {
        #region Public Properties

        public static GameManager Instance { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public GameManager()
        {
            Instance = this;
        }

        #endregion Public Constructors
    }
}