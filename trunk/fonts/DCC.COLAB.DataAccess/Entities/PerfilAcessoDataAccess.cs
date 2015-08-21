using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCCFramework;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
//using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class PerfilAcessoDataAccess : BaseDataAccess, IPerfilAcessoDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "PA.nome ASC"; }

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
                foreach (var funcionalidadeAcao in perfilAcesso.funcionalidadesPermitidas)
                {
                    Hashtable parametrosPermissao = this.BuildParametrosFuncionalidadesAcaoInserir(funcionalidadeAcao, idPerfil);
                    this.InserirObjetoPorNomeQuery("adicionarFuncionalidadeAcaoAoPerfilAcesso", parametrosPermissao);
                }
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
                parametros.Add("CODIGO", perfilAcesso.codigo);
                this.AtualizarObjetoPorNomeQuery("atualizarPerfilAcesso", parametros);

                Hashtable parametrosFuncionalidades = new Hashtable();
                parametros.Add("ID", perfilAcesso.codigo);
                this.RemoverPorNomeQuery("removerFuncionalidadesDoPerfilAcesso", parametros);

                foreach (var funcionalidadeAcao in perfilAcesso.funcionalidadesPermitidas)
                {
                    Hashtable parametrosPermissao = this.BuildParametrosFuncionalidadesAcaoInserir(funcionalidadeAcao, perfilAcesso.codigo);
                    this.InserirObjetoPorNomeQuery("adicionarFuncionalidadeAcaoAoPerfilAcesso", parametrosPermissao);
                }
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
                this.RemoverPorNomeQuery("removerFuncionalidadesDoPerfilAcesso", parametros);
                this.RemoverPorNomeQuery("excluirPerfilAcesso", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FuncionalidadeAcao> SelecionarFuncionalidadesAcoesFiltradas(FiltroFuncionalidadeAcao filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return this.SelecionarPorNomeQuery("selecionarFuncionalidadesAcaoFiltradas", parametros, this.RecuperaFuncionalidadeAcao).Cast<FuncionalidadeAcao>().ToList();
        }

        public List<FuncionalidadeAcao> SelecionarFuncionalidadesAcaoAssociadasPerfil(FiltroFuncionalidadeAcao filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            parametros.Add("ID_PERFIL", filtro.codigoPerfil);
            return this.SelecionarPorNomeQuery("selecionarFuncionalidadesAcaoAssociadasPerfil", parametros, this.RecuperaFuncionalidadeAcao).Cast<FuncionalidadeAcao>().ToList();
        }

        public void MudarStatusPerfilAcesso(int codigoPerfil, bool mudarStatus)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigoPerfil);
            parametros.Add("STATUS", mudarStatus);
            this.AtualizarObjetoPorNomeQuery("mudarStatusPerfilAcesso", parametros);
        }

        #region BuildParametros

        private Hashtable BuildParametrosPerfilAcessoInserir(PerfilAcesso perfilAcesso)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("NOME", perfilAcesso.nome);
            parametros.Add("NECESSITA_MODERACAO", perfilAcesso.moderacaoConteudo);
            parametros.Add("EDITA_APENAS_SEU_CONTEUDO", perfilAcesso.editaApenasConteudoProprio);
            parametros.Add("ATIVO", perfilAcesso.perfilAtivo);
            parametros.Add("MODERADOR", perfilAcesso.perfilModerador);
            parametros.Add("PODE_ADICIONAR_ACERVO", perfilAcesso.permiteAdicaoAcervo);
            return parametros;
        }

        private Hashtable BuildParametrosFuncionalidadesAcaoInserir(FuncionalidadeAcao funcionalidadeAcao, int codigoPerfil)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID_FUNCIONALIDADE_ACAO", funcionalidadeAcao.codigo);
            parametros.Add("ID", codigoPerfil);
            return parametros;
        }

        #endregion

        #region RecuperaObjeto

        public PerfilAcesso RecuperaObjeto(MySqlDataReader dr)
        {
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            perfilAcesso.codigo = CastDB<int>(dr, "id_perfil");
            perfilAcesso.nome = CastDB<string>(dr, "nome");
            perfilAcesso.moderacaoConteudo = CastDB<bool>(dr, "necessita_moderacao_conteudo");
            perfilAcesso.editaApenasConteudoProprio = CastDB<bool>(dr, "edita_apenas_seu_conteudo");
            perfilAcesso.perfilAtivo = CastDB<bool>(dr, "ativo");
            perfilAcesso.perfilModerador = CastDB<bool>(dr, "moderador");
            perfilAcesso.permiteAdicaoAcervo = CastDB<bool>(dr, "pode_adicionar_acervo"); 
            return perfilAcesso;
        }

        public FuncionalidadeAcao RecuperaFuncionalidadeAcao(MySqlDataReader dr)
        {
            FuncionalidadeAcao funcionalidadeAcao = new FuncionalidadeAcao()
            {
                codigo = CastDB<int>(dr, "codigoFuncionalidadeAcao"),
                acao = new Acao()
                {
                    codigo = CastDB<int>(dr, "codigoAcao"),
                    nome = CastDB<string>(dr, "nomeAcao")
                },
                funcionalidade = new Funcionalidade()
                {
                    codigo = CastDB<int>(dr, "codigoFuncionalidade"),
                    nome = CastDB<string>(dr, "nomeFuncionalidade")
                }
            };

            return funcionalidadeAcao;
        }

        #endregion

    }
}
