using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Usuario
    {
        public Usuario()
        {
            perfilAcesso = new PerfilAcesso();
        }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string nome { get; set; }

        [DataMember]
        public string senha { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string idFacebook { get; set; }

        [DataMember]
        public PerfilAcesso perfilAcesso { get; set; }

        [DataMember]
        public int qtdProvas { get; set; }

        [DataMember]
        public int qtdLinks { get; set; }

        public int qtdConteudo
        {
            get
            {
                return qtdProvas + qtdLinks;
            }
        }
    }
}
