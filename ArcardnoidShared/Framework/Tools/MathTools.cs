namespace ArcardnoidShared.Framework.Tools
{
    public static class MathTools
    {
        #region Public Methods

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        #endregion Public Methods
    }
}