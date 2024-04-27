using Microsoft.Xna.Framework;
using System;

namespace arcardnoid.Models.Framework.Animations
{
    public class MoveAnimation : Animation
    {
        #region Private Fields

        private Vector2 _from;
        private Vector2 _to;

        #endregion Private Fields

        #region Public Constructors

        public MoveAnimation(string name, float duration, Vector2 from, Vector2 to, bool loop = false, bool playOnStart = false) : base(name, duration, loop, playOnStart)
        {
            _from = from;
            _to = to;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (State == AnimationState.Playing)
            {
                float x = MathF.Abs(_from.X - _to.X) > 0.01 ? MathHelper.Lerp(_from.X, _to.X, Ratio) : _to.X;
                float y = MathF.Abs(_from.Y - _to.Y) > 0.01 ? MathHelper.Lerp(_from.Y, _to.Y, Ratio) : _to.Y;
                Vector2 newPosition = new Vector2(x, y);
                Component.Position = newPosition;
            }
        }

        #endregion Public Methods
    }
}