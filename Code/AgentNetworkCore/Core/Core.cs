using SkyNet.Global;
using SkyNet.Manager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Core
{
    using log4net;

    using SkyNet.Log;

    internal sealed partial class Core : IIdentification, IDisposable
    {
        #region Fields

        private readonly Guid _id = Guid.NewGuid();

        private ServiceHost _controlerService = null;

        private ICoreControllerCallbackService _callbackService = null;

        #endregion

        #region Constructors

        internal Core()
        {
            Emergency.LogInfo("Core created in " + ToString());
            InitializeService();
        }

        #endregion

        #region Properties

        public Guid Id { get { return _id; } }

        public Uri ListenAddress { get; private set; }

        #endregion

        #region Methods

        private void InitializeService()
        {
            ListenAddress = new Uri(string.Format("{0}AgentCore_{1}", Addresses.CorePipeBase,Id.ToString("N")));
            _controlerService = new ServiceHost(new CoreControllerService(this), ListenAddress);
            _controlerService.AddServiceEndpoint(typeof(ICoreControllerService), new NetNamedPipeBinding(),
                Addresses.CorePipeAddress(Id));
            _controlerService.Open();
        }

        internal void Initialize(CoreConfiguration configuration)
        {
            this.InitializeDependencyInjection(configuration);
            _callbackService = OperationContext.Current.GetCallbackChannel<ICoreControllerCallbackService>();
            _callbackService.Log("Recieved data");
            Container.GetInstance<ILog>().Info(configuration.Name);
        }

        public void Dispose()
        {
            try
            {
                _controlerService.Close();
            }
            finally
            {
                _controlerService = null;
            }
            _callbackService = null;
            Container.GetInstance<ILog>().InfoFormat("Core destroyed in {0}", ToString());
        }

        public override string ToString()
        {
            return AppDomain.CurrentDomain.ToString();
        }

        #endregion
    }
}
