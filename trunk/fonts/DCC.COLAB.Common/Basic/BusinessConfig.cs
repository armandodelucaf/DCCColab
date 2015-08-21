using System;
using System.Configuration;

namespace DCC.COLAB.Common.Basic
{
    public class BusinessConfig
    {
        //Configurações Gerais
        private static string _stringDeConexaoBD;
        //private static string _applicationPath;

        public static string StringDeConexaoBD
        {
            get
            {
                if (string.IsNullOrEmpty(_stringDeConexaoBD))
                    _stringDeConexaoBD = ConfigurationManager.AppSettings["stringDeConexaoBD"];
                return _stringDeConexaoBD;
            }
        }
    }
}

