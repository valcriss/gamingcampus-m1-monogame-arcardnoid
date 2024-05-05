﻿using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Content.Scenes
{
    public class SplashScreen : Scene
    {
        #region Private Fields

        private int _duration = 4;
        private double _time = 0;

        #endregion Private Fields

        #region Public Constructors

        public SplashScreen(int duration = 4)
        {
            BackgroundColor = Color.FromNonPremultiplied(127, 178, 255, 255);
            AddComponent(new BitmapText("Titre", "Fonts/title-font", "Daniel Silvestre", 960, 700, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            AddComponent(new BitmapText("Sous-titre", "Fonts/subtitle-font", "Programmation avancée C# et Monogame", 960, 850, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            AddComponent(new SpriteSheetImage("logo", "logo/logo", 6, 5, 30, 960, 400));
            _duration = duration;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.TotalSeconds;
            if (_time > _duration)
            {
                Game.ScenesManager.SwitchScene(this, new MainMenu());
            }
            base.Update(gameTime);
        }

        #endregion Public Methods
    }
}