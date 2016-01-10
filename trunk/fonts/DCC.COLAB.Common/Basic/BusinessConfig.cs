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

        private static int _idTipoLinkArtigoTexto;
        private static int _idTipoLinkApostilaLivro;
        private static int _idTipoLinkImagem;
        private static int _idTipoLinkVideo;

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

        public static int IdTipoLinkApostilaLivro
        {
            get
            {
                if (_idTipoLinkApostilaLivro == 0)
                    _idTipoLinkApostilaLivro = Convert.ToInt32(ConfigurationManager.AppSettings["idTipoLinkApostilaLivro"]);
                return _idTipoLinkApostilaLivro;
            }
        }

        public static int IdTipoLinkArtigoTexto
        {
            get
            {
                if (_idTipoLinkArtigoTexto == 0)
                    _idTipoLinkArtigoTexto = Convert.ToInt32(ConfigurationManager.AppSettings["idTipoLinkArtigoTexto"]);
                return _idTipoLinkArtigoTexto;
            }
        }

        public static int IdTipoLinkImagem
        {
            get
            {
                if (_idTipoLinkImagem == 0)
                    _idTipoLinkImagem = Convert.ToInt32(ConfigurationManager.AppSettings["idTipoLinkImagem"]);
                return _idTipoLinkImagem;
            }
        }

        public static int IdTipoLinkVideo
        {
            get
            {
                if (_idTipoLinkVideo == 0)
                    _idTipoLinkVideo = Convert.ToInt32(ConfigurationManager.AppSettings["idTipoLinkVideo"]);
                return _idTipoLinkVideo;
            }
        }
    }
}

