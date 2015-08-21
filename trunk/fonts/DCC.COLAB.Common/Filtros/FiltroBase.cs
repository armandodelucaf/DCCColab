using DCC.COLAB.Common.AuxiliarEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Filtros
{
    [Serializable]
    [DataContract]
    public class FiltroBase
    {
        public FiltroBase() { comPaginacao = false; listaOrdenacao = new List<Ordenacao>(); }

        [DataMember]
        public bool comPaginacao { get; set; }

        [DataMember]
        public int? registroInicial { get; set; }

        [DataMember]
        public int? quantidadeARetornar { get; set; }

        [DataMember]
        public string filtroGenerico { get; set; }

        [DataMember]
        public List<Ordenacao> listaOrdenacao { get; set; }

    }
}
