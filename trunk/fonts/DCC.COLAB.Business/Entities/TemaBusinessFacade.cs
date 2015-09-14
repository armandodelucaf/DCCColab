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
    public class TemaBusinessFacade : BaseBusinessFacade<ITemaDataAccess>
    {
        
        public virtual Tema SelecionarTemaPorCodigo(int codigo)
        {
            try
            {
                Tema tema = dataAccess.SelecionarTemaPorCodigo(codigo);
                return tema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual int SelecionarQuantidadeTemasFiltrados(FiltroTema filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeTemasFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual List<Tema> SelecionarTemasFiltrados(FiltroTema filtro)
        {
            try
            {
                return dataAccess.SelecionarTemasFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirTema(Tema tema, string temaLogado)
        {
            try
            {
                return dataAccess.InserirTema(tema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void AtualizarTema(Tema tema, string temaLogado)
        {
            try
            {
                dataAccess.AtualizarTema(tema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirTema(int codigo, string temaLogado)
        {
            try
            {
                dataAccess.ExcluirTema(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
