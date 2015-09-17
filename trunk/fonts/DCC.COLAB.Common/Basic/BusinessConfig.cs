using System;
using System.Configuration;

namespace DCC.COLAB.Common.Basic
{
    public class BusinessConfig
    {
        //Configurações Gerais
        private static string _stringDeConexaoBD;

        private static int _idPerfilAdmin;
        private static int _idPerfilProfessor;
        private static int _idPerfilMonitor;
        private static int _idPerfilAluno;

        public static string StringDeConexaoBD
        {
            get
            {
                if (string.IsNullOrEmpty(_stringDeConexaoBD))
                    _stringDeConexaoBD = ConfigurationManager.AppSettings["stringDeConexaoBD"];
                return _stringDeConexaoBD;
            }
        }

        public static int IdPerfilAdmin
        {
            get
            {
                if (_idPerfilAdmin == 0)
                    _idPerfilAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfilAdmin"]);
                return _idPerfilAdmin;
            }
        }

        public static int IdPerfilProfessor
        {
            get
            {
                if (_idPerfilProfessor == 0)
                    _idPerfilProfessor = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfilProfessor"]);
                return _idPerfilProfessor;
            }
        }

        public static int IdPerfilMonitor
        {
            get
            {
                if (_idPerfilMonitor == 0)
                    _idPerfilMonitor = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfilMonitor"]);
                return _idPerfilMonitor;
            }
        }

        public static int IdPerfilAluno
        {
            get
            {
                if (_idPerfilAluno == 0)
                    _idPerfilAluno = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfilAluno"]);
                return _idPerfilAluno;
            }
        }
    }
}

