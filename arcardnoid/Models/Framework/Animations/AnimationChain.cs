using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Framework.Animations
{
    public class AnimationChain
    {
        #region Public Properties

        public AnimationState State { get; private set; } = AnimationState.Waiting;

        #endregion Public Properties

        #region Private Fields

        private Animation[] _animations;
        private int _currentAnimationIndex;
        private bool _loop;
        private bool _playOnStart;

        #endregion Private Fields

        #region Public Constructors

        public AnimationChain(Animation[] animations, bool loop, bool playOnStart)
        {
            _animations = animations;
            _currentAnimationIndex = 0;
            _loop = loop;
            _playOnStart = playOnStart;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Play()
        {
            State = AnimationState.Playing;
            _animations[_currentAnimationIndex].Play();
        }

        public void SetComponent(Component component)
        {
            foreach (var animation in _animations)
            {
                animation.SetComponent(component);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (State == AnimationState.Waiting && _playOnStart)
            {
                Play();
            }
            if (State == AnimationState.Playing)
            {
                _animations[_currentAnimationIndex].Update(gameTime);
                if (_animations[_currentAnimationIndex].State == AnimationState.Ended)
                {
                    _currentAnimationIndex++;
                    if (_currentAnimationIndex >= _animations.Length)
                    {
                        if (_loop)
                        {
                            _currentAnimationIndex = 0;
                        }
                        else
                        {
                            State = AnimationState.Ended;
                            return;
                        }
                    }
                    _animations[_currentAnimationIndex].Play();
                }
            }
        }

        #endregion Public Methods
    }
}