﻿using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;

namespace arcardnoid.Models.Content.Components.GameScene.UI
{
    public class CoinAmountUI : Component
    {
        #region Private Properties

        private int CoinAmount { get; set; } = 100;

        #endregion Private Properties

        #region Public Constructors

        public CoinAmountUI(int x, int y) : base("CoinAmountUI", x, y)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new Image("coin-icon", "ui/coin", 0, 25));
            AddComponent(new BitmapText("coin-amount", "fonts/band", CoinAmount.ToString() + " Or", 42, 17, TextHorizontalAlign.Left, TextVerticalAlign.Top, Microsoft.Xna.Framework.Color.Black));
            AddComponent(new BitmapText("coin-amount", "fonts/band", CoinAmount.ToString() + " Or", 40, 15, TextHorizontalAlign.Left, TextVerticalAlign.Top, Microsoft.Xna.Framework.Color.White));
        }

        #endregion Public Methods
    }
}