using DCC.COLAB.Common.AuxiliarEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Turma
    {
        public Turma()
        {
            disciplina = new Disciplina();
            periodo = new Periodo();
            listaProfessores = new List<Usuario>();

        }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public Disciplina disciplina { get; set; }

        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public Periodo periodo { get; set; }

        [DataMember]
        public List<Usuario> listaProfessores { get; set; }

        public String listaProfessoresString
        {
            get
            {
                if (listaProfessores != null && listaProfessores.Count > 0)
                {
                    return String.Join(",", listaProfessores.Select(x=>x.nome).ToList());
                }
                else {
                    return "-";
                }
            }
        }
    }
}
