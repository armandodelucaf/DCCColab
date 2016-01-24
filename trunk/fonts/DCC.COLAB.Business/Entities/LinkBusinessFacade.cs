using COLAB.DCC.Business.Util;
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
    public class LinkBusinessFacade : BaseBusinessFacade<ILinkDataAccess>
    {

        public virtual Link SelecionarLinkPorCodigo(int codigo, int? idUsuario = null)
        {
            try
            {
                Link link = dataAccess.SelecionarLinkPorCodigo(codigo);
                link.temasAssociados = SelecionarTemasPorIdLink(codigo);
                if (idUsuario != null)
                {
                    AvaliacaoUsuario aval = ObterAvaliacaoLink(codigo, (int)idUsuario);
                    link.avaliacaoLogado = (aval != null ? aval.valor : 0);
                }

                return link;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirLink(Link link)
        {
            try
            {
                link.id = dataAccess.InserirLink(link);
                foreach (Tema tema in link.temasAssociados)
                {
                    InserirTemaLink(tema.id, link.id);
                }
                return link.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [RequiresTransaction]
        public virtual void AtualizarLink(Link link)
        {
            try
            {
                dataAccess.AtualizarLink(link);
                
                ExcluirTemaLink(link.id);
                foreach (Tema tema in link.temasAssociados)
                {
                    InserirTemaLink(tema.id, link.id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public virtual int SelecionarQuantidadeLinksFiltrados(FiltroLink filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeLinksFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public virtual List<Link> SelecionarLinksFiltrados(FiltroLink filtro)
        {
            try
            {
                List<Link> listaLinks = dataAccess.SelecionarLinksFiltrados(filtro).ToList();
                return listaLinks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [RequiresTransaction]
        public virtual void ExcluirLink(int codigo)
        {
            try
            {
                ExcluirTemaLink(codigo);
                dataAccess.ExcluirLink(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<TipoLink> SelecionarTiposLink()
        {
            try
            {
                return dataAccess.SelecionarTiposLink();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<Tema> SelecionarTemasPorIdLink(int id)
        {
            try
            {
                return dataAccess.SelecionarTemasPorIdLink(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void InserirTemaLink(int idTema, int idLink)
        {
            try
            {
                dataAccess.InserirTemaLink(idTema, idLink);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirTemaLink(int idLink)
        {
            try
            {
                dataAccess.ExcluirTemaLink(idLink);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void SalvarAvaliacaoLink(AvaliacaoUsuario aval)
        {
            try
            {
                dataAccess.SalvarAvaliacaoLink(aval);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual AvaliacaoUsuario ObterAvaliacaoLink(int idLink, int idUsuario)
        {
            try
            {
                return dataAccess.ObterAvaliacaoLink(idLink, idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
