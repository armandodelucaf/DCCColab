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
    public class DisciplinaDataAccess : BaseDataAccess, IDisciplinaDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "D.nm_Disciplina ASC"; }

        public Disciplina SelecionarDisciplinaPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarDisciplinaPorCodigo", parametros, this.RecuperaObjeto).Cast<Disciplina>().FirstOrDefault();
        }

        public int SelecionarQuantidadeDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarDisciplinasFiltradas(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarDisciplinasFiltradas", parametros);
        }

        public List<Disciplina> SelecionarDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarDisciplinasFiltradas(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarDisciplinasFiltradas", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Disciplina>().ToList();
        }

        public int InserirDisciplina(Disciplina disciplina)
        {
            Hashtable parametrosDisciplina = this.BuildParametrosInserirDisciplina(disciplina);
            return this.InserirObjetoPorNomeQueryERetornarId("inserirDisciplina", parametrosDisciplina);
        }

        public void AtualizarDisciplina(Disciplina disciplina)
        {
            Hashtable parametros = this.BuildParametrosInserirDisciplina(disciplina);
            this.AtualizarObjetoPorNomeQuery("atualizarDisciplina", parametros);
        }

        public void ExcluirDisciplina(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirDisciplina", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Recupera Objeto

        private Disciplina RecuperaObjeto(MySqlDataReader dr)
        {
            Disciplina disciplina = new Disciplina();
            disciplina.id = CastDB<int>(dr, "id_Disciplina");
            disciplina.nome = CastDB<string>(dr, "nm_Disciplina");
            disciplina.periodo = CastDB<int?>(dr, "nu_Periodo");
            disciplina.codigo = CastDB<string>(dr, "cd_Disciplina");
            return disciplina;
        }

        #endregion

        #region Build Parametros

        private Hashtable BuildParametrosInserirDisciplina(Disciplina disciplina)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", disciplina.id);
            parametros.Add("NOME", disciplina.nome.ToUpper());
            parametros.Add("PERIODO", disciplina.periodo);
            parametros.Add("CODIGO_DISCIPLINA", disciplina.codigo.ToUpper());
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return parametros;
        }
        #endregion

    }
}
