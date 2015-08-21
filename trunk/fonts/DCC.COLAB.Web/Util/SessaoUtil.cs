using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Basic;
using DCC.COLAB.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCC.COLAB.Web.Util
{
    public class SessaoUtil
    {
        public static System.Nullable<bool> UsuarioEstaAutenticado
        {
            get {
                if (System.Web.HttpContext.Current.Session == null)
                {
                    return false;
                }
                else
                {
                    return (System.Nullable<bool>)System.Web.HttpContext.Current.Session[ConstantsEnum.CHAVE_SESSAO_AUTENTICADO];
                }
            }
            set { System.Web.HttpContext.Current.Session[ConstantsEnum.CHAVE_SESSAO_AUTENTICADO] = value; }
        }

        public static Usuario Usuario
        {
            get {
                if (System.Web.HttpContext.Current.Session == null || System.Web.HttpContext.Current.Session.IsNewSession)
                {
                    return null;
                }
                else
                {
                    return (Usuario)System.Web.HttpContext.Current.Session[ConstantsEnum.CHAVE_SESSAO_USUARIO];
                }
            }
            set { System.Web.HttpContext.Current.Session[ConstantsEnum.CHAVE_SESSAO_USUARIO] = value; }
        }

        private static string Login
        {
            get { return (string)System.Web.HttpContext.Current.Session[ConstantsEnum.CHAVE_SESSAO_USUARIO_LOGIN]; }
            set { System.Web.HttpContext.Current.Session[ConstantsEnum.CHAVE_SESSAO_USUARIO_LOGIN] = value; }
        }

        static internal void AlterarVariavelSessaoUsuario(Usuario usuario)
        {
            Usuario = usuario;

            if (usuario != null)
            {
                Login = usuario.email;
            }
            else {
                Login = "-";
            }
        }

        public static string UsuarioLogin
        {
            get
            {
                try
                {
                    return Login;
                }
                catch (Exception)
                {
                    return "-";
                }
            }
        }

        static internal void RemoverVariaveisSessao()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        static internal void RemoverSessao()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }
    }
}