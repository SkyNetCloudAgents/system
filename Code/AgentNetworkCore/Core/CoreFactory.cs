using SkyNet.AppDomains;
using SkyNet.Manager;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkyNet.Core
{
    public static class CoreFactory
    {
        #region Fields

        private static ConcurrentDictionary<Guid, Tuple<AppDomain, CoreInitializer>> _cores = new ConcurrentDictionary<Guid, Tuple<AppDomain, CoreInitializer>>();

        #endregion

        #region Methods

        public static CoreControler Create()
        {
            AppDomain domain = AppDomainFactory.Create("AgentCore");
            Assembly asm = Assembly.GetAssembly(typeof(CoreFactory));

            CoreInitializer initializer = (CoreInitializer)domain.CreateInstanceFromAndUnwrap(
                typeof(CoreInitializer).Assembly.Location, typeof(CoreInitializer).FullName);
            Guid coreId = initializer.Id;
            _cores.AddOrUpdate(coreId, new Tuple<AppDomain, CoreInitializer>(domain, initializer),
                (id, tuple) => tuple);

            CoreControler remoteControl = new CoreControler(coreId);
            remoteControl.OnDispose += Destroy;
            return remoteControl;
        }

        public static void Destroy(CoreControler remoteControl)
        {
            Tuple<AppDomain, CoreInitializer> item = null;

            if (_cores.TryGetValue(remoteControl.CoreId, out item))
            {
                item.Item2.Destroy();
                AppDomain.Unload(item.Item1);
                _cores.TryRemove(remoteControl.CoreId, out item);
            }
        }

        #endregion
    }
}
