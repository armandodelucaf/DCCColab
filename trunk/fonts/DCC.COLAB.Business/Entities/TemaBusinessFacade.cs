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
                List<Tema> listaTemas = dataAccess.SelecionarTemasFiltrados(filtro).ToList();
                if (filtro.comQtdProvas && listaTemas != null)
                {
                    ProvaBusinessFacade provaBF = ObterOutraBusiness<ProvaBusinessFacade>();
                    LinkBusinessFacade linkBF = ObterOutraBusiness<LinkBusinessFacade>();

                    foreach (Tema tema in listaTemas)
                    {
                        tema.qtdProvas = provaBF.SelecionarQuantidadeProvasFiltradas(new FiltroProva() { idTema = tema.id, idProfessor = filtro.idProfessor });
                        tema.qtdLinks = linkBF.SelecionarQuantidadeLinksFiltrados(new FiltroLink() { idTema = tema.id, idProfessor = filtro.idProfessor });
                    }
                }
                return listaTemas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirTema(Tema tema)
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
        public virtual void AtualizarTema(Tema tema)
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
        public virtual void ExcluirTema(int codigo)
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
