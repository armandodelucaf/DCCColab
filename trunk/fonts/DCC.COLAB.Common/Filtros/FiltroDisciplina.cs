using System.Runtime.Serialization;
namespace DCC.COLAB.Common.Filtros
{
    public class FiltroDisciplina : FiltroBase
    {
        public FiltroDisciplina() { comQtdProvas = false; }

        [DataMember]
        public bool comQtdProvas { get; set; }
    }
}
