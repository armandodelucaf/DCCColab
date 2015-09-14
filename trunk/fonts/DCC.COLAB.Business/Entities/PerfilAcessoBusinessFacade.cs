using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System;
using System.Collections.Generic;

namespace DCC.COLAB.Business.Entities
{
    [Transactional]
    public class PerfilAcessoBusinessFacade : BaseBusinessFacade<IPerfilAcessoDataAccess>
    {

        public virtual PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil)
        {
            try
            {
                PerfilAcesso perfil = dataAccess.SelecionarPerfilPorCodigo(codigoPerfil);
                return perfil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadePerfisAcessoFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro)
        {
            try
            {
                return dataAccess.SelecionarPerfisAcessoFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
