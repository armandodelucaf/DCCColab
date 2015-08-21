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
    public class Acao
    {
        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public String nome { get; set; }
    }
}
