using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Framework.Animations
{
    public class AlphaFadeAnimation : Animation
    {
        #region Private Fields

        private float _from;
        private float _to;

        #endregion Private Fields

        #region Public Constructors

        public AlphaFadeAnimation(string name, float duration, float from, float to, bool loop = false, bool playOnStart = false) : base(name, duration, loop, playOnStart)
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
                Component.Opacity = MathHelper.LerpPrecise(_from, _to, Ratio);
            }
        }

        #endregion Public Methods
    }
}