using arcardnoid.Models.Framework.Tools;

namespace arcardnoid.Models.Framework.Configuration
{
    public class GameConfiguration : SaveAndLoad<GameConfiguration>
    {
        #region Public Properties

        public int ResolutionHeight { get; set; } = 1080;

        public int ResolutionWidth { get; set; } = 1920;

        #endregion Public Properties

        #region Public Constructors

        public GameConfiguration() : base("configuration.json")
        {
        }

        #endregion Public Constructors
    }
}