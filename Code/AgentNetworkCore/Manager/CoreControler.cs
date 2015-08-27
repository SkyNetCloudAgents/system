using SkyNet.Global;
using SkyNet.Manager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Manager
{
    public sealed class CoreControler : IDisposable
    {
        #region Fields

        private readonly Guid _coreId;

        private readonly ICoreControllerService _service;

        private bool _initialized = false;

        #endregion

        #region Constructors

        internal CoreControler(Guid coreId)
        {
            _coreId = coreId;
            var instanceContext = new InstanceContext(new CoreControllerCallbackService(this));
            var factory = new DuplexChannelFactory<ICoreControllerService>(instanceContext,
                new NetNamedPipeBinding(), Addresses.CorePipeAddress(CoreId).ToString());
            _service = factory.CreateChannel();
        }

        #endregion

        #region Events

        internal event Action<CoreControler> OnDispose;

        #endregion

        #region Properties

        internal Guid CoreId { get { return _coreId; } }

        #endregion

        #region Methods

        public void Initialize(CoreConfiguration configuration)
        {
            if(!_initialized)
            {
                _service.Initialize(configuration);
                _initialized = true;
            }
        }

        public void Dispose()
        {
            if(OnDispose != null)
            {
                OnDispose(this);
            }
        }

        public override string ToString()
        {
            return CoreId.ToString("N");
        }

        #endregion
    }
}
