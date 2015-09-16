using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.AuxiliarEntities
{
    [Serializable]
    [DataContract]
    public class Periodo
    {
        [DataMember]
        public int ano { get; set; }

        [DataMember]
        public int semestre { get; set; }

        public String tableString
        {
            get
            {
                return ano + "/" + semestre;
            }
        }
    }
}
