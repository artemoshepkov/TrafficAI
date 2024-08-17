using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> _services;

        static ServiceLocator()
        {
            var assetProvider = new AssetProvider();
            var roadFactory = new RoadFactory(assetProvider);

            _services = new Dictionary<Type, IService>()
            {
                { typeof(AssetProvider), assetProvider },
                { typeof(RoadFactory), roadFactory },
            };
        }

        public static T GetService<T>() where T : class, IService => _services[typeof(T)] as T;
    }
}
