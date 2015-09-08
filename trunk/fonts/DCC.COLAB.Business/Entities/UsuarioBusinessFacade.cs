using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Business.Entities
{
    [Transactional]
    public class UsuarioBusinessFacade : BaseBusinessFacade<IUsuarioDataAccess>
    {
        
        public virtual Usuario SelecionarUsuarioPorCodigo(int codigo)
        {
            try
            {
                Usuario usuario = dataAccess.SelecionarUsuarioPorCodigo(codigo);
                PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = ObterOutraBusiness<PerfilAcessoBusinessFacade>();
                usuario.perfilAcesso = perfilAcessoBusinessFacade.SelecionarPerfilPorCodigo(usuario.perfilAcesso.id);
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual int SelecionarQuantidadeUsuariosFiltrados(FiltroUsuario filtro)
        {
            try
            {
                return dataAccess.SelecionarQuantidadeUsuariosFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual List<Usuario> SelecionarUsuariosFiltrados(FiltroUsuario filtro)
        {
            try
            {
                return dataAccess.SelecionarUsuariosFiltrados(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual int InserirUsuario(Usuario usuario, string usuarioLogado)
        {
            try
            {
                return dataAccess.InserirUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void AtualizarUsuario(Usuario usuario, string usuarioLogado)
        {
            try
            {
                dataAccess.AtualizarUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirUsuario(int codigo, string usuarioLogado)
        {
            try
            {
                dataAccess.ExcluirUsuario(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public virtual string RecuperarSenha(Usuario usuario)
        {
            try
            {
                return dataAccess.RecuperarSenha(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual void AtualizarSenha(Usuario usuario, string usuarioLogado)
        {
            try
            {
                Usuario registro = SelecionarUsuarioPorCodigo(usuario.id);
                registro.senha = usuario.senha;
                dataAccess.AtualizarSenha(registro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Usuario ValidarUsuarioSenha(string login, string senha)
        {
            try
            {
                return dataAccess.ValidarUsuarioSenha(login, senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
