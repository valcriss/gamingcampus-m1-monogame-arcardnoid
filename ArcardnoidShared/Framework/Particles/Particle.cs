using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Particles
{
    public class Particle
    {
        #region Public Properties

        public bool IsAlive => _isAlive;

        #endregion Public Properties

        #region Private Fields

        private readonly ParticleData _data;
        private Rectangle _bounds;
        private GameColor _color;
        private Point _direction;
        private bool _isAlive;
        private float _lifeTimeLeft;
        private float _opacity;
        private Point _position;
        private float _size;
        private float _speed;

        #endregion Private Fields

        #region Public Constructors

        public Particle(ParticleData data, Point position, Point direction)
        {
            _data = data;
            _position = new Point(position.X, position.Y);
            _bounds = new Rectangle(position.X, position.Y, _data.Bounds.Width, data.Bounds.Height);
            _lifeTimeLeft = data.LifeTime;
            _color = data.ColorStart;
            _size = data.SizeStart;
            _opacity = data.OpacityStart;
            float angle = ParticuleEmitter.Random.Next((int)data.RotationMin, (int)data.RotationMax);
            _speed = ParticuleEmitter.Random.Next((int)data.SpeedMin, (int)data.SpeedMax);
            _direction = direction.Rotate(angle);
            _isAlive = true;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Draw()
        {
            _data.Texture.DrawTexture(_bounds, _data.Bounds, _color, 0, Point.Zero);
        }

        public void Update(float delta)
        {
            _lifeTimeLeft -= delta;
            if (_lifeTimeLeft <= 0)
            {
                _isAlive = false;
                return;
            }
            float lifeTimeRatio = MathTools.Clamp(_lifeTimeLeft / _data.LifeTime, 0, 1);
            _color = GameColor.Lerp(_data.ColorStart, _data.ColorEnd, lifeTimeRatio);
            _opacity = MathTools.Clamp(MathTools.Lerp(_data.OpacityStart, _data.OpacityEnd, lifeTimeRatio), 0, 1);
            _size = MathTools.Lerp(_data.SizeStart, _data.SizeEnd, lifeTimeRatio);
            _position += _direction * _speed * delta;
            _bounds = new Rectangle(_position.X, _position.Y, _data.Bounds.Width, _data.Bounds.Height).Scale(_size);
        }

        #endregion Public Methods
    }
}