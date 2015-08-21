using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Filtros
{
    public class FiltroUsuario : FiltroBase
    {
        public FiltroUsuario() { codigos = new List<int>(); }

        public List<int> codigos {get; set;}
    }
}
