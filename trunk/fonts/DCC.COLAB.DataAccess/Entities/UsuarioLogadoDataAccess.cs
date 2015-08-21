using DCCFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
//using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.AuxiliarEntities;
using MySql.Data.MySqlClient;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class UsuarioLogadoDataAccess : DCCFramework.DataAccess, IUsuarioLogadoDataAccess
    {

        public UsuarioLogado ValidarUsuarioSenha(string login, string senha)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("LOGIN", login);
                parametros.Add("SENHA", senha);
                UsuarioLogado usuario = this.SelecionarPorNomeQuery("validaUsuarioSenha", parametros, this.RecuperaObjeto).Cast<UsuarioLogado>().FirstOrDefault();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuarioLogado ObterDadosUsuarioLogado(int idParticipante)
        {
            UsuarioLogado usuario = new UsuarioLogado();
            //usuario.idParticipante = 40000125;
            //usuario.nome = "Renato";
            //usuario.email = "renato.araujo@cob.org.br";
            //usuario.ativo = true;
            //usuario.necessitaTrocaSenha = false;
            return usuario;
        }

        public UsuarioLogado RecuperaObjeto(MySqlDataReader dr)
        {
            UsuarioLogado usuarioLogado = new UsuarioLogado();
            usuarioLogado.idParticipante = CastDB<int>(dr, "id_Participante");
            usuarioLogado.necessitaTrocaSenha = CastDB<bool>(dr, "fl_Troca_Senha");
            usuarioLogado.ativo = CastDB<bool>(dr, "fl_Ativo");
            return usuarioLogado;
        }

    }

}
