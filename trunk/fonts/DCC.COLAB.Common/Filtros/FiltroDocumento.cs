using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Filtros
{
    public class FiltroDocumento : FiltroBase
    {
        public FiltroDocumento() { codigos = new List<int>(); }

        public List<int> codigos { get; set; }
    }
}
