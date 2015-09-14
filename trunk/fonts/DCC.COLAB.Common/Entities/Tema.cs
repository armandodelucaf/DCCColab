using System;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Tema
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string nome { get; set; }

        [DataMember]
        public string descricao { get; set; }

        public String resumoDescricao
        {
            get
            {
                if (String.IsNullOrEmpty(descricao))
                {
                    return "-";
                }
                else
                {
                    if (descricao.Length > 100)
                    {
                        return descricao.Substring(0, 97) + "...";
                    }
                    else
                    {
                        return descricao;
                    }
                }
            }
            set { }
        }
    }
}
