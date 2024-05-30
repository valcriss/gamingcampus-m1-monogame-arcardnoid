using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Particles
{
    public class ParticuleEmitter : GameComponent
    {
        #region Public Properties

        public static IRandom Random { get; set; } = GameServiceProvider.GetService<IRandomService>().GetRandom();

        #endregion Public Properties

        #region Private Fields

        private Point _direction = Point.Zero;
        private bool _enabled = false;
        private List<Particle> _particles = new List<Particle>();
        private ParticleData _particuleData;
        private Point _position = Point.Zero;
        private float _timeSinceLastCreation = 0;

        #endregion Private Fields

        #region Public Constructors

        public ParticuleEmitter(ParticleData particleData, bool enable = false) : base()
        {
            _particuleData = particleData;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            Particle[] particles = _particles.ToArray();
            foreach (var particle in particles)
            {
                particle.Draw();
            }
        }

        public void Start()
        {
            _enabled = true;
        }

        public void Stop()
        {
            _enabled = false;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            _timeSinceLastCreation += delta;
            if (_enabled && _timeSinceLastCreation >= _particuleData.CreationDelay)
            {
                _timeSinceLastCreation = 0;
                if (_particles.Count < _particuleData.MaximalParticles)
                {
                    _particles.Add(new Particle(_particuleData, _position, _direction));
                }
            }
            Particle[] particles = _particles.ToArray();
            foreach (var particle in particles)
            {
                particle.Update(delta);
            }
            _particles.RemoveAll(p => !p.IsAlive);
        }

        public void UpdateDirectionAndPosition(Point direction, Point position)
        {
            _direction = direction;
            _position = position;
        }

        public void UpdateParticuleEmitterData(ParticleData particleData)
        {
            _particuleData = particleData;
        }

        #endregion Public Methods
    }
}