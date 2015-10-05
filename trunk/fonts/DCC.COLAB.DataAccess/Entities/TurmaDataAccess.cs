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
    public class TurmaDataAccess : BaseDataAccess, ITurmaDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "D.nm_Disciplina ASC, T.nu_Ano DESC, T.nu_Semestre DESC"; }

        public Turma SelecionarTurmaPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarTurmaPorCodigo", parametros, this.RecuperaObjeto).Cast<Turma>().FirstOrDefault();
        }

        public int SelecionarQuantidadeTurmasFiltradas(FiltroTurma filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarTurmasFiltradas(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarTurmasFiltradas", parametros);
        }

        public List<Turma> SelecionarTurmasFiltradas(FiltroTurma filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarTurmasFiltradas(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarTurmasFiltradas", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Turma>().ToList();
        }

        public int InserirTurma(Turma turma)
        {
            Hashtable parametrosTurma = this.BuildParametrosInserirTurma(turma);
            return this.InserirObjetoPorNomeQueryERetornarId("inserirTurma", parametrosTurma);
        }

        public void AtualizarTurma(Turma turma)
        {
            Hashtable parametros = this.BuildParametrosInserirTurma(turma);
            this.AtualizarObjetoPorNomeQuery("atualizarTurma", parametros);
        }

        public void ExcluirTurma(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirTurma", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> SelecionarProfessoresPorIdTurma(int id)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID_TURMA", id);
            return this.SelecionarPorNomeQuery("selecionarProfessoresPorIdTurma", parametros, this.RecuperaObjetoUsuario).Cast<Usuario>().ToList();
        
        }

        public List<Turma> SelecionarTurmasPorIdProfessor(int id)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID_PROFESSOR", id);
            return this.SelecionarPorNomeQuery("selecionarTurmasPorIdProfessor", parametros, this.RecuperaObjeto).Cast<Turma>().ToList();

        }

        public void InserirProfessorTurma(int idTurma, int idProfessor)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_TURMA", idTurma);
                parametros.Add("ID_PROFESSOR", idProfessor);
                this.RemoverPorNomeQuery("inserirProfessorTurma", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirProfessorTurma(int idTurma)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_TURMA", idTurma);
                this.RemoverPorNomeQuery("excluirProfessorTurma", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Recupera Objeto

        private Turma RecuperaObjeto(MySqlDataReader dr)
        {
            Turma turma = new Turma();
            turma.id = CastDB<int>(dr, "id_Turma");
            turma.codigo = CastDB<string>(dr, "cd_Turma");
            turma.disciplina = new Disciplina()
            {
                id = CastDB<int>(dr, "id_Disciplina"),
                nome = CastDB<string>(dr, "nm_Disciplina")
            };
            turma.periodo = new Periodo()
            {
                ano = CastDB<int>(dr, "nu_Ano"),
                semestre = CastDB<int>(dr, "nu_Semestre")
            };
            return turma;
        }

        private Usuario RecuperaObjetoUsuario(MySqlDataReader dr)
        {
            Usuario usuario = new Usuario();
            usuario.id = CastDB<int>(dr, "id_Usuario");
            usuario.nome = CastDB<string>(dr, "nm_Usuario");
            return usuario;
        }

        #endregion

        #region Build Parametros

        private Hashtable BuildParametrosInserirTurma(Turma turma)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", turma.id);
            parametros.Add("CODIGO", turma.codigo.ToUpper());
            parametros.Add("ANO", turma.periodo.ano);
            parametros.Add("SEMESTRE", turma.periodo.semestre);
            parametros.Add("DISCIPLINA", turma.disciplina.id);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarTurmasFiltradas(FiltroTurma filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return parametros;
        }
        #endregion

    }
}
