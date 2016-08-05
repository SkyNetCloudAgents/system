using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkyNet.Core
{
    using SkyNet.Log;

    internal class CoreInitializer : MarshalByRefObject
    {
        #region Fields

        private Core _core = null;

        #endregion

        #region Constructors

        public CoreInitializer()
        {
            try
            {
                _core = new Core();
            }
            catch (Exception exception)
            {
                Emergency.Log(exception.ToString());
            }
        }

        #endregion

        #region Properties

        public Guid Id { get { return _core == null ? Guid.Empty : _core.Id; } }

        #endregion

        #region Methods

        internal void Destroy()
        {
            if (_core != null)
            {
                _core.Dispose();
                _core = null;
            }
        }

        #endregion
    }
}
