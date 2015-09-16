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
    public class DisciplinaBusinessFacade : BaseBusinessFacade<IDisciplinaDataAccess>
    {
        
        public virtual Disciplina SelecionarDisciplinaPorCodigo(int codigo)
        {
            try
            {
                Disciplina disciplina = dataAccess.SelecionarDisciplinaPorCodigo(codigo);
                return disciplina;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual int SelecionarQuantidadeDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeDisciplinasFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual List<Disciplina> SelecionarDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            try
            {
                return dataAccess.SelecionarDisciplinasFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirDisciplina(Disciplina disciplina)
        {
            try
            {
                return dataAccess.InserirDisciplina(disciplina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void AtualizarDisciplina(Disciplina disciplina)
        {
            try
            {
                dataAccess.AtualizarDisciplina(disciplina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirDisciplina(int codigo)
        {
            try
            {
                dataAccess.ExcluirDisciplina(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
