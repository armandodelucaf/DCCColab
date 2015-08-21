using System;
using System.Collections.Generic;

namespace DCC.COLAB.Common.AuxiliarEntities
{
    public class Paginacao
    {
        public int draw {get; set;}

        public int recordsTotal {get; set;}
        
        public int recordsFiltered {get; set;}
        
        public List<Object> data { get; set; }
    }
}
