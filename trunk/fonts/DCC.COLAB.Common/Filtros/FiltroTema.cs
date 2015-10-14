using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Filtros
{
    public class FiltroTema : FiltroBase
    {
        public FiltroTema() { comQtdProvas = false; }

        [DataMember]
        public bool comQtdProvas { get; set; }

        [DataMember]
        public int? idDisciplina { get; set; }
    }
}
