using DCC.COLAB.Common.AuxiliarEntities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Link
    {
        public Link()
        {
            temasAssociados = new List<Tema>();
        }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public Byte[] src { get; set; }

        [DataMember]
        public string mimeType { get; set; }

        [DataMember]
        public string extension { get; set; }

        [DataMember]
        public bool upload { get; set; }

        [DataMember]
        public string titulo { get; set; }

        [DataMember]
        public string descricao { get; set; }

        [DataMember]
        public TipoLink tipo { get; set; }

        [DataMember]
        public Disciplina disciplina { get; set; }

        [DataMember]
        public Usuario usuario { get; set; }

        [DataMember]
        public List<Tema> temasAssociados { get; set; }

        [DataMember]
        public bool recomendadoProfessor { get; set; }

        [DataMember]
        public bool recomendadoMonitor { get; set; }

        [DataMember]
        public int avaliacao { get; set; }

        [DataMember]
        public int avaliacaoLogado { get; set; }
    }
}
