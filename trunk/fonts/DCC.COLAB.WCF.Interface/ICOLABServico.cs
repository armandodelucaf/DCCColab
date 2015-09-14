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
        void AtualizarSenha(Usuario usuario, string usuarioLogado);

        #endregion

        #region PerfisAcesso

        [OperationContract]
        PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil);

        [OperationContract]
        int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro);

        [OperationContract]
        List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro);

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

        #region Disciplina

        [OperationContract]
        Disciplina SelecionarDisciplinaPorCodigo(int codigo);

        [OperationContract]
        int SelecionarQuantidadeDisciplinasFiltradas(FiltroDisciplina filtro);

        [OperationContract]
        List<Disciplina> SelecionarDisciplinasFiltradas(FiltroDisciplina filtro);

        [OperationContract]
        int InserirDisciplina(Disciplina disciplina, string disciplinaLogado);

        [OperationContract]
        void AtualizarDisciplina(Disciplina disciplina, string disciplinaLogado);

        [OperationContract]
        void ExcluirDisciplina(int codigo, string disciplinaLogado);

        #endregion

        #region Tema

        [OperationContract]
        Tema SelecionarTemaPorCodigo(int codigo);

        [OperationContract]
        int SelecionarQuantidadeTemasFiltrados(FiltroTema filtro);

        [OperationContract]
        List<Tema> SelecionarTemasFiltrados(FiltroTema filtro);

        [OperationContract]
        int InserirTema(Tema usuario, string usuarioLogado);

        [OperationContract]
        void AtualizarTema(Tema usuario, string usuarioLogado);

        [OperationContract]
        void ExcluirTema(int codigo, string usuarioLogado);

        #endregion

    }
}
