using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Filtros
{
    public class FiltroTurma : FiltroBase
    {
        [DataMember]
        public int? idDisciplina { get; set; }

        [DataMember]
        public int? idProfessor { get; set; }

        [DataMember]
        public int? ano { get; set; }

        [DataMember]
        public int? semestre { get; set; }
    }
}
