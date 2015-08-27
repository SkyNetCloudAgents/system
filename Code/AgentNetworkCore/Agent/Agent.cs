using SkyNet.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Agent
{
    public abstract class Agent : IIdentification
    {
        #region Fields

        private readonly Guid _id = Guid.NewGuid();

        #endregion

        #region Properties

        public Guid Id
        {
            get { return _id; }
        }

        #endregion

        #region Methods

        #endregion
    }
}
