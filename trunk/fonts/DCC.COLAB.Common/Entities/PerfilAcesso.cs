using DCC.COLAB.Common.Basic;
using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class PerfilAcesso
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String nome { get; set; }

        [DataMember]        
        public bool perfilModerador { get; set; }
        
        public String perfilModeradorString
        {
            get
            {
                return this.perfilModerador.ToString(0);
            }
        }
    }
}

