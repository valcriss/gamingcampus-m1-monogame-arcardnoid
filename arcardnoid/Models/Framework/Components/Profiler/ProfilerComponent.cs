using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using System.Linq;

namespace arcardnoid.Models.Framework.Components.Profiler
{
    public class ProfilerComponent : Component
    {
        #region Private Fields

        private BitmapFont _bitmapFont;
        private ProfilerItem[] _items;
        private double elapsedTime;
        private string fps = "FPS: 0";
        private double frameCount;
        private Rectangle ProfilerRectangle;

        #endregion Private Fields

        #region Public Constructors

        public ProfilerComponent() : base("Profiler", 20, 20)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            Game.SpriteBatch.FillRectangle(ProfilerRectangle, Color.FromNonPremultiplied(0, 0, 0, 128));
            Game.SpriteBatch.DrawRectangle(ProfilerRectangle, Color.White);
            int y = 30;
            int x = 40;
            Game.SpriteBatch.DrawString(_bitmapFont, fps, new Vector2(x, y), Color.White);
            foreach (ProfilerItem item in _items)
            {
                y += 20;
                Game.SpriteBatch.DrawString(_bitmapFont, item.Name, new Vector2(x, y), Color.White);
                Game.SpriteBatch.DrawString(_bitmapFont, item.Value.ToString(), new Vector2(x + 300, y), Color.White);
            }
            base.Draw();
        }

        public override void Load()
        {
            base.Load();
            _bitmapFont = Game.Content.Load<BitmapFont>("fonts/ken");
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            frameCount++;
            if (elapsedTime >= 1.0)
            {
                elapsedTime = 0;
                fps = $"FPS: {frameCount}";
                frameCount = 0;
            }
            _items = ProfilerCollection.Items.Where(c => c.Value > 0.05).OrderByDescending(c => c.Value).Take(20).ToArray();
            ProfilerRectangle = new Rectangle(20, 20, 400, 40 + (_items.Length * 20));

            base.Update(gameTime);
        }

        #endregion Public Methods
    }
}