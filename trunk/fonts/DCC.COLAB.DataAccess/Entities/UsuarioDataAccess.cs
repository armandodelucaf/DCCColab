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
    public class UsuarioDataAccess : BaseDataAccess, IUsuarioDataAccess
    {
        protected override string ObterOrdenacaoDefault() { return "U.nm_Usuario ASC"; }

        public Usuario SelecionarUsuarioPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarUsuarioPorCodigo", parametros, this.RecuperaObjeto).Cast<Usuario>().FirstOrDefault();
        }

        public int SelecionarQuantidadeUsuariosFiltrados(FiltroUsuario filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarUsuariosFiltrados(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarUsuariosFiltrados", parametros);
        }

        public List<Usuario> SelecionarUsuariosFiltrados(FiltroUsuario filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarUsuariosFiltrados(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarUsuariosFiltrados", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Usuario>().ToList();
        }

        public int InserirUsuario(Usuario usuario)
        {
            Hashtable parametrosUsuario = this.BuildParametrosInserirUsuario(usuario);
            return this.InserirObjetoPorNomeQueryERetornarId("inserirUsuario", parametrosUsuario);
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            Hashtable parametros = this.BuildParametrosInserirUsuario(usuario);
            this.AtualizarObjetoPorNomeQuery("atualizarUsuario", parametros);
        }

        public void ExcluirUsuario(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario ValidarUsuarioSenha(string login, string senha)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("LOGIN", login);
                parametros.Add("SENHA", senha);
                Usuario usuario = this.SelecionarPorNomeQuery("validaUsuarioSenha", parametros, this.RecuperaObjeto).Cast<Usuario>().FirstOrDefault();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarSenha(Usuario usuario)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", usuario.id);
                parametros.Add("SENHA", usuario.senha);

                this.AtualizarObjetoPorNomeQuery("atualizarSenhaUsuario", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Recupera Objeto

        private Usuario RecuperaObjeto(MySqlDataReader dr)
        {
            Usuario usuario = new Usuario();
            usuario.id = CastDB<int>(dr, "id_Usuario");
            usuario.nome = CastDB<string>(dr, "nm_Usuario");
            usuario.email = CastDB<string>(dr, "email");
            usuario.idFacebook = CastDB<int>(dr, "id_Facebook");
            usuario.perfilAcesso = new PerfilAcesso() {
                id = CastDB<int>(dr, "id_Perfil_Acesso"),
                nome = CastDB<string>(dr, "nm_Perfil_Acesso"),
                perfilModerador = CastDB<bool>(dr, "moderador")
            };
            return usuario;
        }

        #endregion

        #region Build Parametros

        private Hashtable BuildParametrosInserirUsuario(Usuario usuario)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", usuario.id);
            parametros.Add("NOME", usuario.nome);
            parametros.Add("EMAIL", usuario.email);
            parametros.Add("ID_PERFIL", usuario.perfilAcesso.id);
            parametros.Add("SENHA", usuario.senha);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarUsuariosFiltrados(FiltroUsuario filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            return parametros;
        }
        #endregion

    }
}
