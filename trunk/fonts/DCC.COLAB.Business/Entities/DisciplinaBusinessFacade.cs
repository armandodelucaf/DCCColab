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
                List<Disciplina> listaDisciplinas = dataAccess.SelecionarDisciplinasFiltradas(filtro).ToList();
                if (filtro.comQtdProvas && listaDisciplinas != null)
                {
                    ProvaBusinessFacade provaBF = ObterOutraBusiness<ProvaBusinessFacade>();
                    LinkBusinessFacade linkBF = ObterOutraBusiness<LinkBusinessFacade>();

                    foreach (Disciplina disciplina in listaDisciplinas)
                    {
                        disciplina.qtdProvas = provaBF.SelecionarQuantidadeProvasFiltradas(new FiltroProva() { idDisciplina = disciplina.id });
                        disciplina.qtdLinks = linkBF.SelecionarQuantidadeLinksFiltrados(new FiltroLink() { idDisciplina = disciplina.id });
                    }
                }
                return listaDisciplinas;
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
