using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Basic;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Notificacao
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String descricao { get; set; }

        [DataMember]        
        public Usuario usuario { get; set; }

        [DataMember]
        public EnumConteudo tipoConteudo { get; set; }

        [DataMember]
        public int idConteudo { get; set; }

        [DataMember]
        public Prova prova { get; set; }

        [DataMember]
        public Link link { get; set; }

        [DataMember]
        public DateTime data { get; set; }

        public String dataString
        {
            get
            {
                return (data != null ? data.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) : "-");
            }
        }

        public String conteudoString
        {
            get
            {
                if (tipoConteudo == EnumConteudo.prova)
                {
                    return (prova != null ? prova.tipo.nome + " - " + prova.turma.tableString : "");
                }
                else if (tipoConteudo == EnumConteudo.link)
                {
                    return (link != null ? link.tipo.nome + " - " + link.titulo + " (" + link.disciplina.nome + ")" : "");
                }

                return "";
            }
        }

    }
}

