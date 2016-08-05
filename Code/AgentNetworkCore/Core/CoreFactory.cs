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
    using log4net;

    public static class CoreFactory
    {
        #region Fields

        private static ConcurrentDictionary<Guid, CoreFactoryItem> _cores = new ConcurrentDictionary<Guid, CoreFactoryItem>();

        #endregion

        #region Methods

        public static CoreControler Create()
        {
            AppDomain domain = AppDomainFactory.Create("AgentCore");
            Assembly asm = Assembly.GetAssembly(typeof(CoreFactory));

            CoreInitializer initializer = (CoreInitializer)domain.CreateInstanceFromAndUnwrap(
                typeof(CoreInitializer).Assembly.Location, typeof(CoreInitializer).FullName);
            Guid coreId = initializer.Id;
            if (coreId == Guid.Empty)
            {
                AppDomain.Unload(domain);
                throw new Exception("Error in CoreInitializer");
            }
            _cores.AddOrUpdate(coreId, new CoreFactoryItem(domain, initializer, null),
                (id, tuple) => tuple);

            CoreControler remoteControl = new CoreControler(coreId);
            remoteControl.OnDispose += Destroy;
            return remoteControl;
        }

        public static void Destroy(CoreControler remoteControl)
        {
            CoreFactoryItem item = null;

            if (_cores.TryGetValue(remoteControl.CoreId, out item))
            {
                item.Initializer.Destroy();
                AppDomain.Unload(item.Domain);
                _cores.TryRemove(remoteControl.CoreId, out item);
            }
        }

        #endregion
    }
}
