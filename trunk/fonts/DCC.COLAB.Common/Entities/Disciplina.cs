using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Disciplina
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string nome { get; set; }

        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public int? periodo { get; set; }

        public String periodoString
        {
            get
            {
                if (periodo == null) {
                    return "-";
                }
                else if (periodo == 0)
                {
                    return "Eletiva";
                }
                else
                {
                    return periodo.ToString();
                }
            }
        }
    }
}
