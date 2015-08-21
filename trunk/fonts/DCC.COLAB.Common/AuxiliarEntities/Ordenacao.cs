using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.AuxiliarEntities
{
    [Serializable]
    [DataContract]
    public class Ordenacao
    {
        [DataMember]
        public string coluna { get; set; }

        [DataMember]
        public string ordem { get; set; }
    }
}
