using arcardnoid.Models.Content.Scenes;
using arcardnoid.Models.Framework;

namespace arcardnoid
{
    public class ArCardNoidGame : BaseGame
    {
        #region Public Constructors

        public ArCardNoidGame()
        {
            SetInitialScene(new MainMenu());
        }

        #endregion Public Constructors
    }
}