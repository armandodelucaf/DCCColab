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
    public class TemaDataAccess : BaseDataAccess, ITemaDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "T.nm_Tema ASC"; }

        public Tema SelecionarTemaPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarTemaPorCodigo", parametros, this.RecuperaObjeto).Cast<Tema>().FirstOrDefault();
        }

        public int SelecionarQuantidadeTemasFiltrados(FiltroTema filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarTemasFiltrados(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarTemasFiltrados", parametros);
        }

        public List<Tema> SelecionarTemasFiltrados(FiltroTema filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarTemasFiltrados(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarTemasFiltrados", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Tema>().ToList();
        }

        public int InserirTema(Tema tema)
        {
            Hashtable parametrosTema = this.BuildParametrosInserirTema(tema);
            return this.InserirObjetoPorNomeQueryERetornarId("inserirTema", parametrosTema);
        }

        public void AtualizarTema(Tema tema)
        {
            Hashtable parametros = this.BuildParametrosInserirTema(tema);
            this.AtualizarObjetoPorNomeQuery("atualizarTema", parametros);
        }

        public void ExcluirTema(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirTema", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Recupera Objeto

        private Tema RecuperaObjeto(MySqlDataReader dr)
        {
            Tema tema = new Tema();
            tema.id = CastDB<int>(dr, "id_Tema");
            tema.nome = CastDB<string>(dr, "nm_Tema");
            tema.descricao = CastDB<string>(dr, "ds_Tema");
            return tema;
        }

        #endregion

        #region Build Parametros

        private Hashtable BuildParametrosInserirTema(Tema tema)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", tema.id);
            parametros.Add("NOME", tema.nome);
            parametros.Add("DESCRICAO", tema.descricao);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarTemasFiltrados(FiltroTema filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return parametros;
        }
        #endregion

    }
}
