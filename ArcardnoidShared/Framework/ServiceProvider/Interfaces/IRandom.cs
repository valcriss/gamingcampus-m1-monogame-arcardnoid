namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IRandom
    {
        #region Public Methods

        int Next(int minValue, int maxValue);

        int Next(int maxValue);

        int Next();

        #endregion Public Methods
    }
}