using System.Runtime.Serialization;
namespace DCC.COLAB.Common.Filtros
{
    public class FiltroNotificacao : FiltroBase
    {
        [DataMember]
        public int? idReferencia { get; set; }

        [DataMember]
        public int? tipoReferencia { get; set; }

        [DataMember]
        public int? idUsuario { get; set; }
    }
}
