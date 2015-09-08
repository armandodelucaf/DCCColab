using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System.Collections.Generic;
using System.ServiceModel;

namespace DCC.COLAB.WCF.Interface
{
    [ServiceContract]
    public interface ICOLABServico
    {
        #region Usuario

        [OperationContract]
        Usuario ValidarUsuarioSenha(string login, string senha);

        [OperationContract]
        Usuario SelecionarUsuarioPorCodigo(int codigo);

        [OperationContract]
        int SelecionarQuantidadeUsuariosFiltrados(FiltroUsuario filtro);

        [OperationContract]
        List<Usuario> SelecionarUsuariosFiltrados(FiltroUsuario filtro);

        [OperationContract]
        int InserirUsuario(Usuario usuario, string usuarioLogado);

        [OperationContract]
        void AtualizarUsuario(Usuario usuario, string usuarioLogado);

        [OperationContract]
        void ExcluirUsuario(int codigo, string usuarioLogado);
        
        [OperationContract]
        string RecuperarSenha(Usuario usuario);

        [OperationContract]
        void AtualizarSenha(Usuario usuario, string usuarioLogado);

        #endregion

        #region PerfisAcesso

        [OperationContract]
        PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil);

        [OperationContract]
        int InserirPerfilAcesso(PerfilAcesso perfil, string usuario);

        [OperationContract]
        void AtualizarPerfilAcesso(PerfilAcesso perfil, string usuario);

        [OperationContract]
        int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro);

        [OperationContract]
        List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro);

        [OperationContract]
        void ExcluirPerfilAcesso(int codigoPerfil, string usuario);

        #endregion

        #region Arquivo

        [OperationContract]
        int InserirArquivo(Arquivo arquivo);

        [OperationContract]
        void ExcluirArquivo(int codigo);

        #endregion

        #region Documento

        [OperationContract]
        Documento SelecionarDocumentoPorCodigo(int codigo);

        [OperationContract]
        int InserirDocumento(Documento documento, string usuario);

        [OperationContract]
        void AtualizarDocumento(Documento documento, string usuario);

        [OperationContract]
        int SelecionarQuantidadeDocumentosFiltrados(FiltroDocumento filtro);

        [OperationContract]
        List<Documento> SelecionarDocumentosFiltrados(FiltroDocumento filtro);

        [OperationContract]
        void ExcluirUltimaVersaoDocumento(int codigo, string usuario);

        [OperationContract]
        void ExcluirDocumento(int codigo, string usuario);

        [OperationContract]
        Arquivo SelecionarArquivoDoDocumento(int codigo);

        #endregion

    }
}
