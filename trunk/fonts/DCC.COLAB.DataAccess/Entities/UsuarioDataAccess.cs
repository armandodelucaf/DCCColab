﻿using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCCFramework;
using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DCC.COLAB.Common.AuxiliarEntities;
using MySql.Data.MySqlClient;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class UsuarioDataAccess : BaseDataAccess, IUsuarioDataAccess
    {

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
            this.AtualizarObjetoPorNomeQuery("atualizarPerfilUsuario", parametros);
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

        public string RecuperarSenha(Usuario usuario)
        {
            try
            {
                Hashtable parametros = this.BuildParametrosInserirUsuario(usuario);
                usuario.senha = this.SelecionarCampoQuery("obterSenhaParticipante", parametros).ToArray().FirstOrDefault().ToString();
                return this.montaCorpoEmail(usuario);               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string montaCorpoEmail(Usuario usuario)
        {
            StringBuilder html = new StringBuilder();
            string hr = "<hr style='color:#00AEED; background-color:#00AEED; height:1px; border:0;'/>";
            string br = "<br />";

            html.Append("<h3 style='color: #00AEED; font-family: sans-serif; margin-bottom: 0;'>Recuperação de Senha</h3>" + hr + br);
            html.Append("Conforme foi solicitado através do sistema, segue abaixo o seu login e sua senha de acesso ao COLAB do DCC." + br + br);
            html.Append("Login:   " + usuario.email + br);
            html.Append("Senha:   " + usuario.senha + br + br);
            html.Append("Atenciosamente," + br);
            html.Append("Administração");

            return html.ToString();
        }

        public void AtualizarSenha(Usuario usuario)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                string query = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery("atualizarSenhaPessoaFisicaConta");
                query = query.Replace("@SENHA", usuario.senha);
                
                this.AtualizarObjetoPorQueryString(query, parametros);
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
            usuario.codigo = CastDB<int>(dr, "id_usuario");
            usuario.perfilAcesso.codigo = CastDB<int>(dr, "id_perfil");
            usuario.email = CastDB<string>(dr, "ds_login");
            usuario.nome = CastDB<string>(dr, "nm_participante");
            return usuario;
        }

        #endregion

        #region Build Parametros

        private Hashtable BuildParametrosInserirUsuario(Usuario usuario)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", usuario.codigo);
            parametros.Add("NOME", usuario.nome);
            parametros.Add("EMAIL", usuario.email);
            parametros.Add("ID_PERFIL", usuario.perfilAcesso.codigo);
            return parametros;
        }

        private Hashtable BuildParametrosSelecionarUsuariosFiltrados(FiltroUsuario filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            parametros.Add("IDS", filtro.codigos);
            return parametros;
        }
        #endregion

    }
}
