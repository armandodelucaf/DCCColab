using DCC.COLAB.Business.Entities;
using DCC.COLAB.Business.Util;
using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.WCF.Interface;
using System.Collections.Generic;
using System.ServiceModel;

namespace DCC.COLAB.Business
{
    public class COLABServico : ICOLABServico
    {

        #region Usuario

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Usuario ValidarUsuarioSenha(string login, string senha)
        {
            UsuarioBusinessFacade usuarioLogadoBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            return usuarioLogadoBusinessFacade.ValidarUsuarioSenha(login, senha);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Usuario SelecionarUsuarioPorCodigo(int codigo)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            return usuarioBusinessFacade.SelecionarUsuarioPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeUsuariosFiltrados(FiltroUsuario filtro)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            return usuarioBusinessFacade.SelecionarQuantidadeUsuariosFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Usuario> SelecionarUsuariosFiltrados(FiltroUsuario filtro)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            return usuarioBusinessFacade.SelecionarUsuariosFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirUsuario(Usuario usuario, string usuarioLogado)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            return usuarioBusinessFacade.InserirUsuario(usuario, usuarioLogado);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarUsuario(Usuario usuario, string usuarioLogado)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            usuarioBusinessFacade.AtualizarUsuario(usuario, usuarioLogado);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirUsuario(int codigo, string usuarioLogado)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            usuarioBusinessFacade.ExcluirUsuario(codigo, usuarioLogado);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarSenha(Usuario usuario, string usuarioLogado)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            usuarioBusinessFacade.AtualizarSenha(usuario, usuarioLogado);
        }

        #endregion

        #region PerfilAcesso

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil)
        {
            PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = BusinessFactory.GetInstance().Get<PerfilAcessoBusinessFacade>();
            return perfilAcessoBusinessFacade.SelecionarPerfilPorCodigo(codigoPerfil);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirPerfilAcesso(PerfilAcesso perfil, string usuario)
        {
            PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = BusinessFactory.GetInstance().Get<PerfilAcessoBusinessFacade>();
            return perfilAcessoBusinessFacade.InserirPerfilAcesso(perfil, usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarPerfilAcesso(PerfilAcesso perfil, string usuario)
        {
            PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = BusinessFactory.GetInstance().Get<PerfilAcessoBusinessFacade>();
            perfilAcessoBusinessFacade.AtualizarPerfilAcesso(perfil, usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro)
        {
            PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = BusinessFactory.GetInstance().Get<PerfilAcessoBusinessFacade>();
            return perfilAcessoBusinessFacade.SelecionarQuantidadePerfisAcessoFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro)
        {
            PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = BusinessFactory.GetInstance().Get<PerfilAcessoBusinessFacade>();
            return perfilAcessoBusinessFacade.SelecionarPerfisAcessoFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirPerfilAcesso(int codigoPerfil, string usuario)
        {
            PerfilAcessoBusinessFacade perfilAcessoBusinessFacade = BusinessFactory.GetInstance().Get<PerfilAcessoBusinessFacade>();
            perfilAcessoBusinessFacade.ExcluirPerfilAcesso(codigoPerfil, usuario);
        }

        #endregion

        #region Arquivo

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirArquivo(Arquivo arquivo)
        {
            ArquivoBusinessFacade arquivoBusinessFacade = BusinessFactory.GetInstance().Get<ArquivoBusinessFacade>();
            return arquivoBusinessFacade.InserirArquivo(arquivo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirArquivo(int codigo)
        {
            ArquivoBusinessFacade arquivoBusinessFacade = BusinessFactory.GetInstance().Get<ArquivoBusinessFacade>();
            arquivoBusinessFacade.ExcluirArquivo(codigo);
        }

        #endregion

        #region Documento
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Documento SelecionarDocumentoPorCodigo(int codigo)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            return documentoBusinessFacade.SelecionarDocumentoPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirDocumento(Documento documento, string usuario)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            return documentoBusinessFacade.InserirDocumento(documento, usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarDocumento(Documento documento, string usuario)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            documentoBusinessFacade.AtualizarDocumento(documento, usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeDocumentosFiltrados(FiltroDocumento filtro)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            return documentoBusinessFacade.SelecionarQuantidadeDocumentosFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Documento> SelecionarDocumentosFiltrados(FiltroDocumento filtro)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            return documentoBusinessFacade.SelecionarDocumentosFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirDocumento(int codigo, string usuario)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            documentoBusinessFacade.ExcluirDocumento(codigo, usuario);
        }

        public void ExcluirUltimaVersaoDocumento(int codigo, string usuario)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            documentoBusinessFacade.ExcluirUltimaVersaoDocumento(codigo, usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Arquivo SelecionarArquivoDoDocumento(int codigo)
        {
            DocumentoBusinessFacade documentoBusinessFacade = BusinessFactory.GetInstance().Get<DocumentoBusinessFacade>();
            return documentoBusinessFacade.SelecionarArquivoDoDocumento(codigo);
        }
        #endregion
    }
}
