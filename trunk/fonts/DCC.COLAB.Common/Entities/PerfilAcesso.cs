using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DCC.COLAB.Common.Basic;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class PerfilAcesso
    {
        public PerfilAcesso()
        {
            this.funcionalidadesPermitidas = new List<FuncionalidadeAcao>();
        }

        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public String nome { get; set; }

        [DataMember]        
        public bool moderacaoConteudo { get; set; }

        [DataMember]        
        public bool editaApenasConteudoProprio { get; set; }

        [DataMember]
        public bool perfilAtivo { get; set; }

        [DataMember]
        public IEnumerable<FuncionalidadeAcao> funcionalidadesPermitidas { get; set; }

        [DataMember]        
        public bool perfilModerador { get; set; }

        [DataMember]
        public bool permiteAdicaoAcervo { get; set; }

        public String moderacaoConteudoString
        {
            get
            {
                return this.moderacaoConteudo.ToString(0);
            }
        }

        public String editaApenasConteudoProprioString
        {
            get
            {
                return this.editaApenasConteudoProprio.ToString(0);
            }
        }

        public String perfilAtivoString
        {
            get
            {
                return this.perfilAtivo.ToString(0);
            }
        }

        public String perfilModeradorString
        {
            get
            {
                return this.perfilModerador.ToString(0);
            }
        }

        public String permiteAdicaoAcervoString
        {
            get
            {
                return this.permiteAdicaoAcervo.ToString(0);
            }
        }
    }
}

