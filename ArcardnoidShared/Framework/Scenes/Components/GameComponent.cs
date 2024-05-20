using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;

namespace ArcardnoidShared.Framework.Scenes.Components
{
    public class GameComponent : GameComponentContainer
    {
        #region Public Properties

        public virtual Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }

        public virtual GameColor Color
        {
            get
            {
                return _color.UpdateOpacity((int)(Opacity * 255));
            }
            set
            {
                _color = value;
            }
        }

        public bool HasAnimations => Animations.Count > 0;

        public virtual float Opacity
        {
            get
            {
                return Parent == null ? _opacity : Parent.Opacity * _opacity;
            }
            set
            {
                _opacity = value;
            }
        }

        public GameComponent? Parent { get; set; }

        public virtual Point Position
        {
            get => Bounds.Position;
            set => Bounds.SetPosition(value);
        }

        public virtual Rectangle RealBounds
        {
            get
            {
                return Parent == null ? _bounds : new Rectangle(Parent.RealBounds.X + _bounds.X, Parent.RealBounds.Y + _bounds.Y, _bounds.Width, _bounds.Height);
            }
        }

        public virtual float Rotation
        {
            get
            {
                return Parent == null ? _rotation : Parent.Rotation + _rotation;
            }
            set
            {
                _rotation = value;
            }
        }

        public virtual float Scale
        {
            get
            {
                return Parent == null ? _scale : Parent.Scale * _scale;
            }
            set
            {
                _scale = value;
            }
        }

        #endregion Public Properties

        #region Protected Properties

        protected List<AnimationChain> Animations { get; set; } = new List<AnimationChain>();

        #endregion Protected Properties

        #region Private Fields

        private Rectangle _bounds = Rectangle.Empty;
        private GameColor _color = GameColor.White;
        private float _opacity = 1.0f;
        private float _rotation = 0.0f;
        private float _scale = 1.0f;

        #endregion Private Fields

        #region Public Constructors

        public GameComponent(int x = 0, int y = 0, int width = 0, int height = 0, float rotation = 0, float scale = 1)
        {
            _bounds = new Rectangle(x, y, width, height);
            _rotation = rotation;
            _scale = scale;
        }

        #endregion Public Constructors

        #region Public Methods

        public T AddAnimation<T>(Animation animation) where T : GameComponent
        {
            AnimationChain chain = new AnimationChain(new Animation[] { animation }, animation.Loop, animation.PlayOnStart);
            chain.SetComponent(this);
            Animations.Add(chain);
            return (T)this;
        }

        public T AddAnimations<T>(AnimationChain[] animationChains) where T : GameComponent
        {
            foreach (var chain in animationChains)
            {
                chain.SetComponent(this);
                Animations.Add(chain);
            }
            return (T)this;
        }

        public T AddAnimations<T>(AnimationChain animations) where T : GameComponent
        {
            animations.SetComponent(this);
            Animations.Add(animations);
            return (T)this;
        }

        public override void Update(float delta)
        {
            if (!Enabled) return;
            base.Update(delta);
            AnimationChain[] animations = Animations.ToArray();
            foreach (var animation in animations)
            {
                animation.Update(delta);
            }
            Animations.RemoveAll(a => a.State == AnimationState.Ended);
        }

        #endregion Public Methods
    }
}