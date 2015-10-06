using DCCFramework.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Documento
    {
        public Documento()
        {
            arquivo = new Arquivo();
        }

        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public string titulo { get; set; }

        [DataMember]
        public string nome {get; set;}

        [DataMember]
        public string descricao { get; set; }

        [DataMember]
        public Arquivo arquivo { get; set; }

        [DataMember]
        public double tamanho { get; set; }

        [DataMember]
        public int versao { get; set; }

        [DataMember]
        public string extensao { get; set; }

        public string tamanhoString
        {
            get
            {
                return UtilitarioNumeral.ObterTamanhoEmBytes(tamanho);
            }
            private set { }
        }

        public object id { get; set; }
    }

    public class DocumentoComparador : IEqualityComparer<Documento>
    {

        public bool Equals(Documento x, Documento y) {
            return x.codigo.Equals(y.codigo);
        }

        public int GetHashCode(Documento obj)
        {
            return obj.codigo.GetHashCode();
        }

    }
}
