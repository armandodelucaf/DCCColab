using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class LinkDataAccess : BaseDataAccess, ILinkDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "AL.nota DESC, TL.nm_Tipo_Link ASC"; }

        public Link SelecionarLinkPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarLinkPorCodigo", parametros, this.RecuperaObjeto).Cast<Link>().FirstOrDefault();
        }
        
        public int InserirLink(Link link)
        {                        
            Hashtable parametrosLink = this.BuildParametrosLink(link);
            int idLink = this.InserirObjetoPorNomeQueryERetornarId("inserirLink", parametrosLink);
            return idLink;
        }

        public void AtualizarLink(Link link)
        {
            Hashtable parametros = this.BuildParametrosLink(link);    
            this.AtualizarObjetoPorNomeQuery("atualizarLink", parametros);
        }

        public int SelecionarQuantidadeLinksFiltrados(FiltroLink filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarLinksFiltrados(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarLinksFiltrados", parametros);
        }

        public List<Link> SelecionarLinksFiltrados(FiltroLink filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarLinksFiltrados(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarLinksFiltrados", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Link>().ToList();
        }

        public void ExcluirLink(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirLink", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoLink> SelecionarTiposLink()
        {
            Hashtable parametros = new Hashtable();
            return this.SelecionarPorNomeQuery("selecionarTiposLink", parametros, this.RecuperaObjetoTipoLink).Cast<TipoLink>().ToList();
        }

        public List<Tema> SelecionarTemasPorIdLink(int id)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID_LINK", id);
            return this.SelecionarPorNomeQuery("selecionarTemasPorIdLink", parametros, this.RecuperaObjetoTema).Cast<Tema>().ToList();
        }

        public void InserirTemaLink(int idTema, int idLink)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_TEMA", idTema);
                parametros.Add("ID_LINK", idLink);
                this.RemoverPorNomeQuery("inserirTemaLink", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirTemaLink(int idLink)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_LINK", idLink);
                this.RemoverPorNomeQuery("excluirTemaLink", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SalvarAvaliacaoLink(AvaliacaoUsuario aval)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_USUARIO", aval.idUsuario);
                parametros.Add("ID_LINK", aval.id);
                parametros.Add("NOTA", aval.valor);
                this.RemoverPorNomeQuery("salvarAvaliacaoLink", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AvaliacaoUsuario ObterAvaliacaoLink(int idLink, int idUsuario)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID_LINK", idLink);
            parametros.Add("ID_USUARIO", idUsuario);
            return this.SelecionarPorNomeQuery("selecionarAvaliacaoLinkPorIds", parametros, this.RecuperaObjetoAvaliacao).Cast<AvaliacaoUsuario>().FirstOrDefault();
        }        
       
        #region RecuperaObjeto

        private Link RecuperaObjeto(MySqlDataReader dr)
        {
            Link link = new Link();

            link.id = CastDB<int>(dr, "id_Link");
            link.titulo = CastDB<string>(dr, "titulo");
            link.url = CastDB<string>(dr, "url");
            link.descricao = CastDB<string>(dr, "descricao");
            link.tipo = new TipoLink() {
                id = CastDB<int>(dr, "id_Tipo_Link"),
                nome = CastDB<string>(dr, "nm_Tipo_Link")
            };
            link.usuario = new Usuario() {
                id = CastDB<int>(dr, "id_Usuario"),
                nome = CastDB<string>(dr, "nm_Usuario")
            };
            link.disciplina = new Disciplina()
            {
                id = CastDB<int>(dr, "id_Disciplina"),
                nome = CastDB<string>(dr, "nm_Disciplina")
            };
            link.recomendadoMonitor = CastDB<bool>(dr, "aval_Monitor");
            link.recomendadoProfessor = CastDB<bool>(dr, "aval_Professor");
            link.avaliacao = CastDB<int>(dr, "nota");
            return link;
        }

        private TipoLink RecuperaObjetoTipoLink(MySqlDataReader dr)
        {
            TipoLink tipoLink = new TipoLink();

            tipoLink.id = CastDB<int>(dr, "id_Tipo_Link");
            tipoLink.nome = CastDB<string>(dr, "nm_Tipo_Link");
            return tipoLink;
        }

        private Tema RecuperaObjetoTema(MySqlDataReader dr)
        {
            Tema tema = new Tema();
            tema.id = CastDB<int>(dr, "id_Tema");
            tema.nome = CastDB<string>(dr, "nm_Tema");
            tema.descricao = CastDB<string>(dr, "ds_Tema");
            tema.disciplina = new Disciplina
            {
                id = CastDB<int>(dr, "id_Disciplina"),
                nome = CastDB<string>(dr, "nm_Disciplina")
            };
            return tema;
        }

        private AvaliacaoUsuario RecuperaObjetoAvaliacao(MySqlDataReader dr)
        {
            AvaliacaoUsuario aval = new AvaliacaoUsuario();

            aval.id = CastDB<int>(dr, "id_Link");
            aval.idUsuario = CastDB<int>(dr, "id_Usuario");
            aval.valor = CastDB<int>(dr, "nota");

            return aval;
        }

        #endregion

        #region BuildParametros
        private Hashtable BuildParametrosLink(Link link)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", link.id);
            parametros.Add("TITULO", link.titulo);            
            parametros.Add("DESCRICAO", link.descricao);
            parametros.Add("URL", link.url);
            parametros.Add("ID_USUARIO", link.usuario.id);
            parametros.Add("ID_DISCIPLINA", link.disciplina.id);
            parametros.Add("ID_TIPO_LINK", link.tipo.id);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarLinksFiltrados(FiltroLink filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            parametros.Add("ID_DISCIPLINA", filtro.idDisciplina);
            parametros.Add("ID_TEMA", filtro.idTema);
            parametros.Add("ID_PROFESSOR", filtro.idProfessor);
            return parametros;
        }

        #endregion
    }
}
