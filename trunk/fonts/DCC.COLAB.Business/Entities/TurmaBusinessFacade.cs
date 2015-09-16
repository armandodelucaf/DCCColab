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
    public class TurmaBusinessFacade : BaseBusinessFacade<ITurmaDataAccess>
    {
        
        public virtual Turma SelecionarTurmaPorCodigo(int codigo)
        {
            try
            {
                Turma turma = dataAccess.SelecionarTurmaPorCodigo(codigo);
                return turma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual int SelecionarQuantidadeTurmasFiltradas(FiltroTurma filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeTurmasFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual List<Turma> SelecionarTurmasFiltradas(FiltroTurma filtro)
        {
            try
            {
                return dataAccess.SelecionarTurmasFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirTurma(Turma turma)
        {
            try
            {
                return dataAccess.InserirTurma(turma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void AtualizarTurma(Turma turma)
        {
            try
            {
                dataAccess.AtualizarTurma(turma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirTurma(int codigo)
        {
            try
            {
                dataAccess.ExcluirTurma(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
