using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Global
{
    static class Addresses
    {
        #region Properties

        public static Uri CorePipeBase
        {
            get { return new Uri("net.pipe://localhost"); }
        }

        #endregion

        #region Methods

        public static Uri CorePipeAddress(Guid coreId)
        {
            return new Uri(string.Format("{0}/SkyNetCore{1}", CorePipeBase, coreId));
        }

        #endregion
    }
}
