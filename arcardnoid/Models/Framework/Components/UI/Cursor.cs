using arcardnoid.Models.Framework.Components.Images;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace arcardnoid.Models.Framework.Components.UI
{
    public class Cursor : Component
    {
        #region Private Properties

        private string CursorAsset { get; set; }
        private Image CursorImage { get; set; }

        private Vector2 Offset { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Cursor(string name, string cursorAsset, Vector2 offset) : base(name)
        {
            CursorAsset = cursorAsset;
            Offset = offset;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            CursorImage = AddComponent(new Image("CursorImage", CursorAsset, (int)Offset.X, (int)Offset.Y));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            MouseState state = Mouse.GetState();
            CursorImage.Position = new Vector2(state.Position.X + (int)Offset.X, state.Position.Y + (int)Offset.Y);
        }

        #endregion Public Methods
    }
}