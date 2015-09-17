using System.Runtime.Serialization;
namespace DCC.COLAB.Common.Filtros
{
    public class FiltroUsuario : FiltroBase
    {
        [DataMember]
        public int? idPerfil { get; set; }
    }
}
