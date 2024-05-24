using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Particles
{
    public struct ParticleData
    {
        #region Public Properties

        public Rectangle Bounds { get; set; }
        public GameColor ColorEnd { get; set; } = GameColor.White;
        public GameColor ColorStart { get; set; } = GameColor.White;
        public float CreationDelay { get; set; } = 0.1f;
        public float LifeTime { get; set; } = 2;
        public int MaximalParticles { get; set; } = 100;
        public float OpacityEnd { get; set; } = 1;
        public float OpacityStart { get; set; } = 1;
        public float RotationMax { get; set; } = 0;
        public float RotationMin { get; set; } = 0;
        public float SizeEnd { get; set; } = 1;
        public float SizeStart { get; set; } = 1;
        public float SpeedMax { get; set; } = 0;
        public float SpeedMin { get; set; } = 0;
        public ITexture Texture { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public ParticleData(string asset, GameColor? colorStart = null, GameColor? colorEnd = null, float sizeStart = 1, float sizeEnd = 1, float opacityStart = 1, float opacityEnd = 1, float rotationMin = 0, float rotationMax = 0, float speedMin = 0, float speedMax = 0, float lifeTime = 2, int maximumParticules = 100, float creationDelay = 0.016f)
        {
            Texture = GameServiceProvider.GetService<ITextureService>().Load(asset);
            Bounds = new Rectangle(0, 0, Texture.Width, Texture.Height);
            ColorStart = colorStart ?? GameColor.White;
            ColorEnd = colorEnd ?? GameColor.White;
            SizeStart = sizeStart;
            SizeEnd = sizeEnd;
            OpacityStart = opacityStart;
            OpacityEnd = opacityEnd;
            RotationMin = rotationMin;
            RotationMax = rotationMax;
            SpeedMin = speedMin;
            SpeedMax = speedMax;
            LifeTime = lifeTime;
            MaximalParticles = maximumParticules;
            CreationDelay = creationDelay;
        }

        #endregion Public Constructors
    }
}