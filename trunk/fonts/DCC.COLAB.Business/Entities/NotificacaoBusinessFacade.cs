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
    public class NotificacaoBusinessFacade : BaseBusinessFacade<INotificacaoDataAccess>
    {

        public virtual Notificacao SelecionarNotificacaoPorCodigo(int codigo)
        {
            try
            {
                Notificacao notificacao = dataAccess.SelecionarNotificacaoPorCodigo(codigo);
                if (notificacao.tipoConteudo == EnumConteudo.prova)
                {
                    ProvaBusinessFacade provaBF = ObterOutraBusiness<ProvaBusinessFacade>();
                    notificacao.prova = provaBF.SelecionarProvaPorCodigo(notificacao.idConteudo);
                }
                else if (notificacao.tipoConteudo == EnumConteudo.link)
                {
                    LinkBusinessFacade linkBF = ObterOutraBusiness<LinkBusinessFacade>();
                    notificacao.link = linkBF.SelecionarLinkPorCodigo(notificacao.idConteudo);
                }

                return notificacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirNotificacao(Notificacao notificacao)
        {
            try
            {
                notificacao.id = dataAccess.InserirNotificacao(notificacao);
                return notificacao.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual int SelecionarQuantidadeNotificacoesFiltradas(FiltroNotificacao filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeNotificacoesFiltradas(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public virtual List<Notificacao> SelecionarNotificacoesFiltradas(FiltroNotificacao filtro)
        {
            try
            {
                List<Notificacao> listaNotificacoes = dataAccess.SelecionarNotificacoesFiltradas(filtro).ToList();
                if (listaNotificacoes != null)
                {
                    foreach (Notificacao notificacao in listaNotificacoes)
                    {
                        if (notificacao.tipoConteudo == EnumConteudo.prova)
                        {
                            ProvaBusinessFacade provaBF = ObterOutraBusiness<ProvaBusinessFacade>();
                            notificacao.prova = provaBF.SelecionarProvaPorCodigo(notificacao.idConteudo);
                        }
                        else if (notificacao.tipoConteudo == EnumConteudo.link)
                        {
                            LinkBusinessFacade linkBF = ObterOutraBusiness<LinkBusinessFacade>();
                            notificacao.link = linkBF.SelecionarLinkPorCodigo(notificacao.idConteudo);
                        }
                    }
                }

                return listaNotificacoes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
