using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCC.COLAB.Business.Entities
{
    [Transactional]
    public class UsuarioLogadoBusinessFacade : BaseBusinessFacade<IUsuarioLogadoDataAccess>
    {
        
        public virtual UsuarioLogado ValidarUsuarioSenha(string login, string senha)
        {
            try
            {
                return dataAccess.ValidarUsuarioSenha(login, senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual UsuarioLogado ObterDadosUsuarioLogado(int idParticipante)
        {
            try
            {
                return dataAccess.ObterDadosUsuarioLogado(idParticipante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}