using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class AvaliacaoUsuario
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public int idUsuario { get; set; }

        [DataMember]
        public int valor { get; set; }
    }
}
