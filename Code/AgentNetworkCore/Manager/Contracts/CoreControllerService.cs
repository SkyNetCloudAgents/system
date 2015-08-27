using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SkyNet.Core;

namespace SkyNet.Manager.Contracts
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    class CoreControllerService : ICoreControllerService
    {
        #region Fields

        private readonly Core.Core _core;

        #endregion

        #region Constructors

        public CoreControllerService(Core.Core core)
        {
            _core = core;
        }

        #endregion

        #region Methods

        public void Initialize(CoreConfiguration configuration)
        {
            _core.Initialize(configuration);
        }

        #endregion
    }
}
