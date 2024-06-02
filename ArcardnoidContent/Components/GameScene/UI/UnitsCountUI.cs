using ArcardnoidContent.Components.GamePlay;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class UnitsCountUI : GameComponent
    {
        #region Private Properties

        private BitmapText[] BitmapTexts { get; set; } = new BitmapText[2];
        private int UnitsCount { get; set; } = 0;

        #endregion Private Properties

        #region Public Constructors

        public UnitsCountUI(int x, int y) : base(x, y)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            IGamePlay gamePlay = GameServiceProvider.GetService<IGamePlay>();
            gamePlay.UnitsChanged += GamePlay_UnitsChanged;
            UnitsCount = gamePlay.GetUnits();
            AddGameComponent(new Image(TextureType.UI_UNITS, 0, 30));
            BitmapTexts[0] = AddGameComponent(new BitmapText(BitmapFontType.Default, UnitsCount.ToString() + " Soldats", 35, 17, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.Black));
            BitmapTexts[1] = AddGameComponent(new BitmapText(BitmapFontType.Default, UnitsCount.ToString() + " Soldats", 33, 15, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
        }

        #endregion Public Methods

        #region Private Methods

        private void GamePlay_UnitsChanged(int units)
        {
            for (int i = 0; i < BitmapTexts.Length; i++)
            {
                BitmapTexts[i].SetText(units.ToString() + " Soldats");
            }
        }

        #endregion Private Methods
    }
}