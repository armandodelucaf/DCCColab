﻿using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCC.COLAB.Business.Entities
{
    [Transactional]
    public class ProvaBusinessFacade : BaseBusinessFacade<IProvaDataAccess>
    {

        public virtual Prova SelecionarProvaPorCodigo(int codigo, int? idUsuario = null)
        {
            try
            {
                Prova prova = dataAccess.SelecionarProvaPorCodigo(codigo);

                TurmaBusinessFacade turmaBF = ObterOutraBusiness<TurmaBusinessFacade>();
                prova.turma = turmaBF.SelecionarTurmaPorCodigo(prova.turma.id);
                prova.temasAssociados = SelecionarTemasPorIdProva(codigo);

                if (idUsuario != null)
                {
                    AvaliacaoUsuario aval = ObterAvaliacaoProva(codigo, (int)idUsuario);
                    prova.avaliacaoLogado = (aval != null ? aval.valor : 0);
                }

                return prova;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirProva(Prova prova)
        {
            try
            {
                TurmaBusinessFacade turmaBF = ObterOutraBusiness<TurmaBusinessFacade>();
                FiltroTurma filtro = new FiltroTurma() { 
                    idDisciplina = prova.turma.disciplina.id,
                    ano = prova.turma.periodo.ano,
                    semestre = prova.turma.periodo.semestre
                };
                Turma resultado = turmaBF.SelecionarTurmasFiltradas(filtro).ToList().FirstOrDefault();

                if (resultado == null)
                {
                    prova.turma.id = turmaBF.InserirTurma(prova.turma);
                }
                else 
                {
                    prova.turma.id = resultado.id;
                }

                prova.id = dataAccess.InserirProva(prova);
                foreach (Tema tema in prova.temasAssociados)
                {
                    InserirTemaProva(tema.id, prova.id);
                }
                return prova.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [RequiresTransaction]
        public virtual void AtualizarProva(Prova prova)
        {
            try
            {
                TurmaBusinessFacade turmaBF = ObterOutraBusiness<TurmaBusinessFacade>();
                FiltroTurma filtro = new FiltroTurma()
                {
                    idDisciplina = prova.turma.disciplina.id,
                    ano = prova.turma.periodo.ano,
                    semestre = prova.turma.periodo.semestre
                };
                Turma resultado = turmaBF.SelecionarTurmasFiltradas(filtro).ToList().FirstOrDefault();

                if (resultado == null)
                {
                    prova.turma.id = turmaBF.InserirTurma(prova.turma);
                }
                else
                {
                    prova.turma.id = resultado.id;
                }

                dataAccess.AtualizarProva(prova);
                
                ExcluirTemaProva(prova.id);
                foreach (Tema tema in prova.temasAssociados)
                {
                    InserirTemaProva(tema.id, prova.id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public virtual int SelecionarQuantidadeProvasFiltradas(FiltroProva filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeProvasFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public virtual List<Prova> SelecionarProvasFiltradas(FiltroProva filtro)
        {
            try
            {
                TurmaBusinessFacade turmaBF = ObterOutraBusiness<TurmaBusinessFacade>();

                List<Prova> listaProvas = dataAccess.SelecionarProvasFiltradas(filtro).ToList();
                for (int i = 0; i < listaProvas.Count; i++)
                {
                    listaProvas[i].turma = turmaBF.SelecionarTurmaPorCodigo(listaProvas[i].turma.id);
                }

                return listaProvas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [RequiresTransaction]
        public virtual void ExcluirProva(int codigo)
        {
            try
            {
                ExcluirTemaProva(codigo);
                dataAccess.ExcluirProva(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<TipoProva> SelecionarTiposProva()
        {
            try
            {
                return dataAccess.SelecionarTiposProva();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<Tema> SelecionarTemasPorIdProva(int id)
        {
            try
            {
                return dataAccess.SelecionarTemasPorIdProva(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void InserirTemaProva(int idTema, int idProva)
        {
            try
            {
                dataAccess.InserirTemaProva(idTema, idProva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirTemaProva(int idProva)
        {
            try
            {
                dataAccess.ExcluirTemaProva(idProva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void SalvarAvaliacaoProva(AvaliacaoUsuario aval)
        {
            try
            {
                dataAccess.SalvarAvaliacaoProva(aval);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual AvaliacaoUsuario ObterAvaliacaoProva(int idProva, int idUsuario)
        {
            try
            {
                return dataAccess.ObterAvaliacaoProva(idProva, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
