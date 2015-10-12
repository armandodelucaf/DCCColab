using DCC.COLAB.Common.AuxiliarEntities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Prova
    {
        public Prova()
        {
            temasAssociados = new List<Tema>();
        }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public Byte[] src { get; set; }

        [DataMember]
        public string titulo { get; set; }

        [DataMember]
        public string descricao { get; set; }

        [DataMember]
        public TipoProva tipo { get; set; }

        [DataMember]
        public Usuario usuario { get; set; }

        [DataMember]
        public Turma turma { get; set; }

        [DataMember]
        public List<Tema> temasAssociados { get; set; }

    }
}
