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
    public class NotificacaoDataAccess : BaseDataAccess, INotificacaoDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "N.dt_Notificacao DESC"; }

        public Notificacao SelecionarNotificacaoPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarNotificacaoPorCodigo", parametros, this.RecuperaObjeto).Cast<Notificacao>().FirstOrDefault();
        }

        public int SelecionarQuantidadeNotificacoesFiltradas(FiltroNotificacao filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarNotificacoesFiltradas(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarNotificacoesFiltradas", parametros);
        }

        public List<Notificacao> SelecionarNotificacoesFiltradas(FiltroNotificacao filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarNotificacoesFiltradas(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarNotificacoesFiltradas", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Notificacao>().ToList();
        }

        public int InserirNotificacao(Notificacao notificacao)
        {
            Hashtable parametros = this.BuildParametrosNotificacao(notificacao);
            int id = this.InserirObjetoPorNomeQueryERetornarId("inserirNotificacao", parametros);
            return id;
        }

        #region RecuperaObjeto

        public Notificacao RecuperaObjeto(MySqlDataReader dr)
        {
            Notificacao notificacao = new Notificacao();
            notificacao.id = CastDB<int>(dr, "id_Notificacao");
            notificacao.descricao = CastDB<string>(dr, "txt_Notificacao");
            notificacao.usuario = new Usuario()
            {
                id = CastDB<int>(dr, "id_Usuario"),
                nome = CastDB<string>(dr, "nm_Usuario")
            };
            notificacao.idConteudo = CastDB<int>(dr, "id_Referencia");
            notificacao.tipoConteudo = (EnumConteudo)CastDB<int>(dr, "tipo_Referencia");
            notificacao.data = CastDB<DateTime>(dr, "dt_Notificacao");
            return notificacao;
        }

        #endregion

        #region BuildParametros

        private Hashtable BuildParametrosNotificacao(Notificacao notificacao)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("TXT_NOTIFICACAO", notificacao.descricao);
            parametros.Add("ID_USUARIO", notificacao.usuario.id);
            parametros.Add("ID_REFERENCIA", notificacao.idConteudo);
            parametros.Add("TIPO_REFERENCIA", (int)notificacao.tipoConteudo);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarNotificacoesFiltradas(FiltroNotificacao filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            parametros.Add("ID_REFERENCIA", filtro.idReferencia);
            parametros.Add("TIPO_REFERENCIA", filtro.tipoReferencia);
            parametros.Add("ID_USUARIO", filtro.idUsuario);
            return parametros;
        }

        #endregion

    }
}
