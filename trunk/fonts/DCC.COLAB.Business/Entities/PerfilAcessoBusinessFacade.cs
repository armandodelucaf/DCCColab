using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                perfil.funcionalidadesPermitidas = SelecionarFuncionalidadesAcaoAssociadasPerfil(new FiltroFuncionalidadeAcao() { codigoPerfil = perfil.codigo });

                return perfil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [RequiresTransaction]
        public virtual int InserirPerfilAcesso(PerfilAcesso perfilAcesso, string usuario)
        {
            try
            {
                return dataAccess.InserirPerfilAcesso(perfilAcesso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        [RequiresTransaction]
        public virtual void AtualizarPerfilAcesso(PerfilAcesso perfilAcesso, string usuario)
        {
            try
            {
                dataAccess.AtualizarPerfilAcesso(perfilAcesso);
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
        
        [RequiresTransaction]
        public virtual void ExcluirPerfilAcesso(int codigoPerfil, string usuario)
        {
            try
            {
                dataAccess.ExcluirPerfilAcesso(codigoPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<FuncionalidadeAcao> SelecionarFuncionalidadesAcoesFiltradas(FiltroFuncionalidadeAcao filtro)
        {
            try
            {
                return dataAccess.SelecionarFuncionalidadesAcoesFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<FuncionalidadeAcao> SelecionarFuncionalidadesAcaoAssociadasPerfil(FiltroFuncionalidadeAcao filtro)
        {
            try
            {
                return dataAccess.SelecionarFuncionalidadesAcaoAssociadasPerfil(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void MudarStatusPerfilAcesso(int codigoPerfil, bool status, string usuario)
        {
            try
            {
                dataAccess.MudarStatusPerfilAcesso(codigoPerfil, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
