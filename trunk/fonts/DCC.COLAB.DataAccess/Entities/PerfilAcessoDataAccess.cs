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
    public class PerfilAcessoDataAccess : BaseDataAccess, IPerfilAcessoDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "PA.nm_Perfil_Acesso ASC"; }

        public PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigoPerfil);
            return this.SelecionarPorNomeQuery("selecionarPerfilAcessoPorCodigo", parametros, this.RecuperaObjeto).Cast<PerfilAcesso>().FirstOrDefault();
        }

        public int InserirPerfilAcesso(PerfilAcesso perfilAcesso)
        {
            try
            {
                Hashtable parametros = this.BuildParametrosPerfilAcessoInserir(perfilAcesso);
                int idPerfil = this.InserirObjetoPorNomeQueryERetornarId("inserirPerfilAcesso", parametros);
                return idPerfil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarPerfilAcesso(PerfilAcesso perfilAcesso)
        {
            try
            {
                Hashtable parametros = this.BuildParametrosPerfilAcessoInserir(perfilAcesso);
                parametros.Add("CODIGO", perfilAcesso.id);
                this.AtualizarObjetoPorNomeQuery("atualizarPerfilAcesso", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarPerfisAcessoFiltrados", parametros);
        }

        public List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarPerfisAcessoFiltrados", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<PerfilAcesso>().ToList();
        }


        public void ExcluirPerfilAcesso(int codigoPerfil)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigoPerfil);
                this.RemoverPorNomeQuery("excluirPerfilAcesso", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region BuildParametros

        private Hashtable BuildParametrosPerfilAcessoInserir(PerfilAcesso perfilAcesso)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("NOME", perfilAcesso.nome);
            parametros.Add("MODERADOR", perfilAcesso.perfilModerador);
            return parametros;
        }

        #endregion

        #region RecuperaObjeto

        public PerfilAcesso RecuperaObjeto(MySqlDataReader dr)
        {
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            perfilAcesso.id = CastDB<int>(dr, "id_Perfil_Acesso");
            perfilAcesso.nome = CastDB<string>(dr, "nm_Perfil_Acesso");
            perfilAcesso.perfilModerador = CastDB<bool>(dr, "moderador");
            return perfilAcesso;
        }

        #endregion

    }
}
