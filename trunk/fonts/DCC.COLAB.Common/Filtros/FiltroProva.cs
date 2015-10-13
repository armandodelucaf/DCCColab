using System.Runtime.Serialization;
namespace DCC.COLAB.Common.Filtros
{
    public class FiltroProva : FiltroBase
    {
        [DataMember]
        public int? idDisciplina { get; set; }

        [DataMember]
        public int? idProfessor { get; set; }
    }
}
