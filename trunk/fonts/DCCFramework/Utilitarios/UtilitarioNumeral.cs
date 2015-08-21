using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DCCFramework.Atributos;

namespace DCCFramework.Utilitarios
{
    public static class UtilitarioNumeral
    {
        public static string ObterTamanhoEmBytes(double tamanho)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };

            int order = 0;
            while (tamanho >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                tamanho = tamanho / 1024;
            }

            string result = String.Format("{0:0.##}{1}", tamanho, sizes[order]);

            return result;
        }
    }
}
