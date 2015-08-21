using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCFramework.Atributos
{
    public class DescricaoAttribute : Attribute
    {
        public string Descricao { get; protected set; }

        public DescricaoAttribute (string valor)
        {
            this.Descricao = valor;
        }
    }
}
