namespace ArcardnoidShared.Framework.Tools
{
    public static class MathTools
    {
        #region Public Methods

        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        #endregion Public Methods
    }
}