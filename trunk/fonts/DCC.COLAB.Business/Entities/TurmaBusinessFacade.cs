using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;

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
                turma.listaProfessores = this.SelecionarProfessoresPorIdTurma(codigo);
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
                List<Turma> lista = dataAccess.SelecionarTurmasFiltradas(filtro).ToList();
                if (lista != null) { 
                    foreach (Turma turma in lista) {
                        turma.listaProfessores = this.SelecionarProfessoresPorIdTurma(turma.id);
                    }
                }
                return lista;
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
                int idTurma = dataAccess.InserirTurma(turma);
                foreach (Usuario professor in turma.listaProfessores) {
                    this.InserirProfessorTurma(idTurma, professor.id);
                }
                return idTurma;
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

                this.ExcluirProfessorTurma(turma.id);
                foreach (Usuario professor in turma.listaProfessores)
                {
                    this.InserirProfessorTurma(turma.id, professor.id);
                }
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
                this.ExcluirProfessorTurma(codigo);
                dataAccess.ExcluirTurma(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<Usuario> SelecionarProfessoresPorIdTurma(int id)
        {
            try
            {
                return dataAccess.SelecionarProfessoresPorIdTurma(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void InserirProfessorTurma(int idTurma, int idProfessor)
        {
            try
            {
                dataAccess.InserirProfessorTurma(idTurma, idProfessor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirProfessorTurma(int idTurma)
        {
            try
            {
                dataAccess.ExcluirProfessorTurma(idTurma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
