using ArcardnoidShared.Framework.Scenes;

namespace ArcardnoidShared.Framework.ServiceProvider
{
    public static class GameServiceProvider
    {
        #region Private Fields

        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        #endregion Private Fields

        #region Public Constructors

        static GameServiceProvider()
        {
            RegisterService(new ScenesManager());
        }

        #endregion Public Constructors

        #region Public Methods

        public static T GetService<T>()
        {
            if (typeof(T).IsInterface == false)
                throw new InvalidOperationException("Service type must be an interface");
            if (!_services.ContainsKey(typeof(T)))
                throw new InvalidOperationException("Service [" + typeof(T).Name + "] not found");
            return (T)_services[typeof(T)];
        }

        public static void RegisterService(object service)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            var interfaces = service.GetType().GetInterfaces();
            if (interfaces.Length == 0)
                throw new InvalidOperationException("Service must implement an interface");
            if (interfaces.Length > 1)
                throw new InvalidOperationException("Service must implement only one interface");
            var interfaceType = interfaces[0];

            _services[interfaceType] = service;
        }

        #endregion Public Methods
    }
}