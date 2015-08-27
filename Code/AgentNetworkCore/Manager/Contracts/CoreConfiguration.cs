using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.Manager.Contracts
{
    [DataContract]
    public sealed class CoreConfiguration
    {
        #region Fields

        public int _portStart, _portEnd;

        #endregion

        #region Constructors

        public CoreConfiguration()
        {
            _portStart = 10000;
            _portEnd = 20000;
        }

        #endregion

        #region Properties

        [DataMember]
        public int PortStart
        {
            get
            {
                return _portStart;
            }
            set
            {
                if(value <= _portEnd)
                {
                    _portStart = value;
                }
            }
        }

        [DataMember]
        public int PortEnd
        {
            get
            {
                return _portEnd;
            }
            set
            {
                if(value >= _portStart)
                {
                    _portEnd = value;
                }
            }
        }

        [DataMember]
        public string Name { get; set; }

        #endregion
    }
}
