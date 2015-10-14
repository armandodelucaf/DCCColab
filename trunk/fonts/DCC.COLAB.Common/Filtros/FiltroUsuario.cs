using System.Runtime.Serialization;
namespace DCC.COLAB.Common.Filtros
{
    public class FiltroUsuario : FiltroBase
    {
        public FiltroUsuario() { comQtdProvas = false; }

        [DataMember]
        public int? idPerfil { get; set; }

        [DataMember]
        public bool comQtdProvas { get; set; }
    }
}
