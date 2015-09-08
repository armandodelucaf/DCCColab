using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DCC.COLAB.Common.Basic;
using System.Threading.Tasks;

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

