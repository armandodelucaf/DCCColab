using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Filtros
{
    public class FiltroTema : FiltroBase
    {
        [DataMember]
        public int? idDisciplina { get; set; }
    }
}
