using System.Runtime.Serialization;
namespace DCC.COLAB.Common.Filtros
{
    public class FiltroLink : FiltroBase
    {
        [DataMember]
        public int? idDisciplina { get; set; }
        
        [DataMember]
        public int? idTema { get; set; }

        [DataMember]
        public int? idProfessor { get; set; }
    }
}
