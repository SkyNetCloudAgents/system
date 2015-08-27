using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkyNet.Core
{
    internal class CoreInitializer : MarshalByRefObject
    {
        #region Fields

        private Core _core = null;

        #endregion

        #region Constructors

        public CoreInitializer()
        {
            _core = new Core();
        }

        #endregion

        #region Properties

        public Guid Id { get { return _core.Id; } }

        #endregion

        #region Methods

        internal void Destroy()
        {
            _core.Dispose();
            _core = null;
        }

        #endregion
    }
}
