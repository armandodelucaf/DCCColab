using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.COLAB.Common.Basic;
using System.Runtime.Serialization;

namespace DCC.COLAB.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Arquivo
    {
        [DataMember]
        public int codigo { get; set; }

        [DataMember]
        public Byte[] binario{ get; set; }
    }
}
