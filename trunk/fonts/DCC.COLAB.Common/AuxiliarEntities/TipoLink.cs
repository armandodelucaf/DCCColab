using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.AuxiliarEntities
{
    [Serializable]
    [DataContract]
    public class TipoLink
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string nome { get; set; }
    }
}
