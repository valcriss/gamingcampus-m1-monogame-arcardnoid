﻿using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.MainMenu.Frames
{
    public class SeedFrame : GameComponent
    {
        #region Private Properties

        private Input? Input { get; set; }
        private Action OnCancel { get; set; }
        private Action OnConfirm { get; set; }
        private IRandom Random { get; set; } = GameServiceProvider.GetService<IRandomService>().GetRandom();

        #endregion Private Properties

        #region Public Constructors

        public SeedFrame(Action onConfirm, Action onCancel) : base(672, 1081)
        {
            OnConfirm = onConfirm;
            OnCancel = onCancel;
        }

        #endregion Public Constructors

        #region Public Methods

        public int GetSeed()
        {
            if (Input == null) return 0;
            return int.Parse(Input.GetValue());
        }

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Frame(TextureType.UI_BANNER, 0, 0, 576, 512));
            AddGameComponent(new BitmapText(BitmapFontType.Default, "Graine de la partie", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            Input = AddGameComponent(new Input(TextureType.UI_INPUT, "", 95, 140, 4));
            AddGameComponent(new Button("Confirmer", TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL, TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER, TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED, OnConfirm, 80, 300, 410, 64));
            AddGameComponent(new Button("Annuler", TextureType.UI_BUTTONS_BUTTON_RED_NORMAL, TextureType.UI_BUTTONS_BUTTON_RED_HOVER, TextureType.UI_BUTTONS_BUTTON_RED_PRESSED, OnCancel, 80, 370, 410, 64));
            NewSeed();
        }

        public void NewSeed()
        {
            string seed = Random.Next(100000, 999999).ToString();
            //seed = 123456.ToString();
            Input?.SetValue(seed);
        }

        #endregion Public Methods
    }
}