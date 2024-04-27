using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;

namespace arcardnoid.Models.Framework.Animations
{
    public class Animation
    {
        #region Public Properties

        public bool Loop { get; set; }
        public bool PlayOnStart { get; set; }
        public AnimationState State { get; private set; } = AnimationState.Waiting;

        #endregion Public Properties

        #region Protected Properties

        protected Component Component { get; set; }
        protected float Duration { get; set; }
        protected float ElapsedTime { get; set; }
        protected string Name { get; set; }
        protected float Ratio => ElapsedTime / Duration;

        #endregion Protected Properties

        #region Public Constructors

        public Animation(string name, float duration, bool loop = false, bool playOnStart = false)
        {
            Name = name;
            Duration = duration;
            Loop = loop;
            PlayOnStart = playOnStart;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Play()
        {
            State = AnimationState.Playing;
            ElapsedTime = 0;
        }

        public void SetComponent(Component component)
        {
            Component = component;
        }

        public void Stop()
        {
            State = Loop ? AnimationState.Playing : AnimationState.Ended;
            ElapsedTime = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (State == AnimationState.Waiting && PlayOnStart)
            {
                Play();
            }
            if (State == AnimationState.Playing)
            {
                ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ElapsedTime >= Duration)
                {
                    Stop();
                }
            }
        }

        #endregion Public Methods
    }
}