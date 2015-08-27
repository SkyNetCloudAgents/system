using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Manager.Contracts
{
    interface ICoreControllerCallbackService
    {
        [OperationContract(IsOneWay = true)]
        void Log(string text);
    }
}
