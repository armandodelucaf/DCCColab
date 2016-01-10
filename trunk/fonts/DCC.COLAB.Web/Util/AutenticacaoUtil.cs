using DCC.COLAB.Common.Entities;
using DCC.COLAB.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCC.COLAB.Web.Util
{
    public class AutenticacaoUtil
    {

        public static Usuario ValidarUsuarioSenha(String login, String senha)
        {
            try
            {
                Usuario usuarioLogado = WCFDispatcher<ICOLABServico>.UseService(u => u.ValidarUsuarioSenha(login, senha));
                return usuarioLogado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Usuario ValidarUsuarioFacebook(String idFacebook)
        {
            try
            {
                Usuario usuarioLogado = WCFDispatcher<ICOLABServico>.UseService(u => u.ValidarUsuarioFacebook(idFacebook));
                return usuarioLogado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}