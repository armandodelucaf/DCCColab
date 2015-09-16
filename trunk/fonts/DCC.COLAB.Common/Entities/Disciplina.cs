﻿using System;
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
        public int periodoRecomendado { get; set; }

        public String periodoRecomendadoString
        {
            get
            {
                if (periodoRecomendado == 0)
                {
                    return "Eletiva";
                }
                else
                {
                    return periodoRecomendado.ToString();
                }
            }
        }
    }
}