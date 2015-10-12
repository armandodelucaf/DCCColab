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
    public class ProvaDataAccess : BaseDataAccess, IProvaDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "P.titulo ASC"; }

        public Prova SelecionarProvaPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarProvaPorCodigo", parametros, this.RecuperaObjeto).Cast<Prova>().FirstOrDefault();
        }
        
        public int InserirProva(Prova prova)
        {                        
            Hashtable parametrosProva = this.BuildParametrosProva(prova);
            int idProva = this.InserirObjetoPorNomeQueryERetornarId("inserirProva", parametrosProva);
            return idProva;
        }

        public void AtualizarProva(Prova prova)
        {
            Hashtable parametros = this.BuildParametrosProva(prova);    
            this.AtualizarObjetoPorNomeQuery("atualizarProva", parametros);
        }

        public int SelecionarQuantidadeProvasFiltradas(FiltroProva filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarProvasFiltradas(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarProvasFiltradas", parametros);
        }

        public List<Prova> SelecionarProvasFiltradas(FiltroProva filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarProvasFiltradas(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarProvasFiltradas", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Prova>().ToList();
        }

        public void ExcluirProva(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirProva", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoProva> SelecionarTiposProva()
        {
            Hashtable parametros = new Hashtable();
            return this.SelecionarPorNomeQuery("selecionarTiposProva", parametros, this.RecuperaObjetoTipoProva).Cast<TipoProva>().ToList();
        }

        public List<Tema> SelecionarTemasPorIdProva(int id)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID_PROVA", id);
            return this.SelecionarPorNomeQuery("selecionarTemasPorIdProva", parametros, this.RecuperaObjetoTema).Cast<Tema>().ToList();
        }

        public void InserirTemaProva(int idTema, int idProva)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_TEMA", idTema);
                parametros.Add("ID_PROVA", idProva);
                this.RemoverPorNomeQuery("inserirTemaProva", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirTemaProva(int idProva)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID_PROVA", idProva);
                this.RemoverPorNomeQuery("excluirTemaProva", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #region RecuperaObjeto

        private Prova RecuperaObjeto(MySqlDataReader dr)
        {
            Prova prova = new Prova();

            prova.id = CastDB<int>(dr, "id_Prova");
            prova.titulo = CastDB<string>(dr, "titulo");
            prova.src = CastDB<byte[]>(dr, "src");
            prova.descricao = CastDB<string>(dr, "descricao");
            prova.tipo = new TipoProva() {
                id = CastDB<int>(dr, "id_Tipo_Prova"),
                nome = CastDB<string>(dr, "nm_Tipo_Prova")
            };
            prova.usuario = new Usuario() {
                id = CastDB<int>(dr, "id_Usuario"),
                nome = CastDB<string>(dr, "nm_Usuario")
            };
            prova.turma = new Turma()
            {
                id = CastDB<int>(dr, "id_Turma")
            };
            return prova;
        }

        private TipoProva RecuperaObjetoTipoProva(MySqlDataReader dr)
        {
            TipoProva tipoProva = new TipoProva();

            tipoProva.id = CastDB<int>(dr, "id_Tipo_Prova");
            tipoProva.nome = CastDB<string>(dr, "nm_Tipo_Prova");
            return tipoProva;
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

        #endregion

        #region BuildParametros
        private Hashtable BuildParametrosProva(Prova prova)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", prova.id);
            parametros.Add("TITULO", prova.titulo);            
            parametros.Add("DESCRICAO", prova.descricao);
            parametros.Add("SRC", prova.src);
            parametros.Add("ID_USUARIO", prova.usuario.id);
            parametros.Add("ID_TURMA", prova.turma.id);
            parametros.Add("ID_TIPO_PROVA", prova.tipo.id);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarProvasFiltradas(FiltroProva filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return parametros;
        }

        #endregion
    }
}
