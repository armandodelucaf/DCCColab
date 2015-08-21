using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DCCFramework.Atributos;

namespace DCCFramework.Utilitarios
{
    public static class UtilitarioEnum
    {
        public static string Descricao(this Enum valor)
        {
            return Descricao((object) valor);
        }

        public static string Descricao(object valor)
        {
            if (valor != null)
            {
                Type tipo = valor.GetType();
                FieldInfo infoCampo = tipo.GetField(valor.ToString());
                DescricaoAttribute[] atributos = infoCampo.GetCustomAttributes(typeof(DescricaoAttribute), false) as DescricaoAttribute[];
                return atributos.Length > 0 ? atributos[0].Descricao : null;
            }
            else {
                return null;
            }
        }

        public static List<T> ObterValores<T>()
        {
            return ((T[])Enum.GetValues(typeof(T))).ToList();
        }

        public static List<int> ObterInteiros<T>()
        {
            return ((int[])Enum.GetValues(typeof(T))).ToList();
        }

        public static List<String> ObterTextos<T>()
        {
            List<String> strs = new List<String>();
            var lista = ObterValores<T>();
            foreach (T valor in lista)
            {
                strs.Add(Descricao(valor));
            }

            return strs;
        }

        public static List<Tuple<int, string>> ObterTuplas<T>()
        {
            List<Tuple<int, string>> strs = new List<Tuple<int, string>>();
            var lista = ObterValores<T>();
            foreach (T valor in lista)
            {
                if (!string.IsNullOrEmpty(Descricao(valor)))
                {
                    strs.Add(new Tuple<int, string>(valor.GetHashCode(), Descricao(valor)));
                }
            }

            return strs;
        }
    }
}
