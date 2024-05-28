using ArcardnoidContent.Components.UI;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class ObtainDialog : GameComponent
    {
        #region Private Fields

        private ObtainType _obtainType;
        private Action _onClose;

        #endregion Private Fields

        #region Public Constructors

        public ObtainDialog(ObtainType obtainType, Action onClose)
        {
            _obtainType = obtainType;
            _onClose = onClose;
            AddGameComponent(new DialogBackground(0.75f));
            AddGameComponent(new Frame(TextureType.UI_BANNER, 610, 220, 700, 640));
            AddGameComponent(new AnimatedTitleBand(TextureType.UI_BANDEAU, BitmapFontType.Default, "Vous avez obtenu ceci", 0.05f, 960, 330, new GameColor(75, 30, 0)));
        }

        #endregion Public Constructors
    }
}