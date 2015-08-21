using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class FuncionalidadeAcao
    {
        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public Funcionalidade funcionalidade { get; set; }

        [DataMember]
        public Acao acao { get; set; }

    }
}
