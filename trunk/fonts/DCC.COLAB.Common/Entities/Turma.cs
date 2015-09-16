using DCC.COLAB.Common.AuxiliarEntities;
using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Turma
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public Disciplina disciplina { get; set; }

        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public Periodo periodo { get; set; }
    }
}
