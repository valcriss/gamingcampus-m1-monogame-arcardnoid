namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IRandomService
    {
        #region Public Methods

        IRandom GetRandom();

        IRandom GetRandom(int seed);

        #endregion Public Methods
    }
}