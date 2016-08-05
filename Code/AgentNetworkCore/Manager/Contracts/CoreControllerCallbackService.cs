using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Manager.Contracts
{
    using log4net;

    class CoreControllerCallbackService : ICoreControllerCallbackService
    {
        #region Fields

        private CoreControler _controler;

        #endregion

        #region Constructors

        public CoreControllerCallbackService(CoreControler controller)
        {
            _controler = controller;
        }

        #endregion

        #region Methods

        public void Log(string text)
        {
            Console.WriteLine("Notification: " + text);
        }

        #endregion
    }
}
