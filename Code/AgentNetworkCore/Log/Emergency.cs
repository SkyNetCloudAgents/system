using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Log
{
    using log4net;

    static class Emergency
    {
        #region Fields

        private static readonly object _locker = new object();

        private static bool _initialized = false;

        private static ILog _auditTrail, _console;

        #endregion

        #region Methods

        private static void Initialize()
        {
            lock (_locker)
            {
                if (!_initialized)
                {
                    _initialized = true;
                    string name = string.Format("(Emergency) {0}", Guid.NewGuid().ToString("N"));
                    _auditTrail = Initializer.CreateAuditTrail(name);
                    _console = Initializer.CreateConsole(name);
                }
            }
        }

        private static void Safe(Action action)
        {
            try
            {
                action();
            }
            catch
            {
            }
        }

        public static void LogError(string error)
        {
            Initialize();
            Safe(() => _auditTrail.Error(error));
            Safe(() => _console.Error(error));
        }

        public static void LogInfo(string info)
        {
            Initialize();
            Safe(() => _auditTrail.Info(info));
            Safe(() => _console.Info(info));
        }


        #endregion
    }
}
