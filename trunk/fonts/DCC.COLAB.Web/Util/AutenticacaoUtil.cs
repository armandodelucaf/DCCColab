using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCC.COLAB.Web.Util
{
    public class AutenticacaoUtil
    {

        public static UsuarioLogado ValidarUsuarioSenha(String login, String senha)
        {
            try
            {
                UsuarioLogado usuarioLogado = WCFDispatcher<ICOLABServico>.UseService(u => u.ValidarUsuarioSenha(login, senha));
                return usuarioLogado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}