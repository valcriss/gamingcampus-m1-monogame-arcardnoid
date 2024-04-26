using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace arcardnoid.Models.Framework.Scenes
{
    public abstract class Component : ComponentContainer
    {
        #region Public Properties

        public RectangleF Bounds
        {
            get
            {
                return Parent == null ? _bounds : new RectangleF(Parent.Position.X + _bounds.X, Parent.Position.Y + _bounds.Y, _bounds.Width, _bounds.Height);
            }
            set
            {
                _bounds = value;
            }
        }

        public Color Color
        {
            get
            {
                return new Color(_color.R, _color.G, _color.B, Opacity * 255);
            }
            set
            {
                _color = value;
            }
        }

        public float Opacity
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

        public Vector2 Position
        {
            get
            {
                return Parent == null ? _position : Parent.Position + _position;
            }
            set
            {
                _position = value;
                _bounds = new RectangleF(_position.X, _position.Y, Bounds.Width, Bounds.Height);
            }
        }

        public float Rotation
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

        public float Scale
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

        protected Component Parent { get; set; } = null;

        #endregion Protected Properties

        #region Private Fields

        private RectangleF _bounds = RectangleF.Empty;

        private Color _color = Color.White;
        private float _opacity = 1.0f;
        private Vector2 _position = Vector2.Zero;
        private float _rotation = 0.0f;
        private float _scale = 1.0f;

        #endregion Private Fields

        #region Public Constructors

        public Component(int x = 0, int y = 0, int width = 0, int height = 0, float rotation = 0, float scale = 1)
        {
            _position = new Vector2(x, y);
            _bounds = new RectangleF(x, y, width, height);
            _rotation = rotation;
            _scale = scale;
        }

        #endregion Public Constructors

        #region Public Methods

        public override Component AddComponent(Component component)
        {
            component.Parent = this;
            return base.AddComponent(component);
        }

        #endregion Public Methods
    }
}