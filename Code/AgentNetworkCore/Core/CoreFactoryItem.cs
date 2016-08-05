using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Core
{
    using log4net;

    sealed class CoreFactoryItem
    {
        #region Constructors

        public CoreFactoryItem(AppDomain domain, CoreInitializer initializer, ILog logger)
        {
            Domain = domain;
            Initializer = initializer;
            Logger = logger;
        }

        #endregion

        #region Properties

        public AppDomain Domain { get; private set; }

        public CoreInitializer Initializer { get; private set; }

        public ILog Logger { get; private set; }

        #endregion
    }
}
