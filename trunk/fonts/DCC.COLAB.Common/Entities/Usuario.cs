using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.COLAB.Common.Basic;
using System.Runtime.Serialization;
using DCC.COLAB.Common.AuxiliarEntities;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Usuario
    {
        public Usuario()
        {
            perfilAcesso = new PerfilAcesso();
        }

        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public string nome { get; set; }

        [DataMember]
        public string senha { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public PerfilAcesso perfilAcesso { get; set; }
    }
}
