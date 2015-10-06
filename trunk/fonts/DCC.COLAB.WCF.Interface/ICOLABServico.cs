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
        int InserirUsuario(Usuario usuario);

        [OperationContract]
        void AtualizarUsuario(Usuario usuario);

        [OperationContract]
        void ExcluirUsuario(int codigo);

        [OperationContract]
        void AtualizarSenha(Usuario usuario);

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
        int InserirDocumento(Documento documento);

        [OperationContract]
        void AtualizarDocumento(Documento documento);

        [OperationContract]
        int SelecionarQuantidadeDocumentosFiltrados(FiltroDocumento filtro);

        [OperationContract]
        List<Documento> SelecionarDocumentosFiltrados(FiltroDocumento filtro);

        [OperationContract]
        void ExcluirUltimaVersaoDocumento(int codigo);

        [OperationContract]
        void ExcluirDocumento(int codigo);

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
        int InserirDisciplina(Disciplina disciplina);

        [OperationContract]
        void AtualizarDisciplina(Disciplina disciplina);

        [OperationContract]
        void ExcluirDisciplina(int codigo);

        #endregion


        #region Tema

        [OperationContract]
        Tema SelecionarTemaPorCodigo(int codigo);

        [OperationContract]
        int SelecionarQuantidadeTemasFiltrados(FiltroTema filtro);

        [OperationContract]
        List<Tema> SelecionarTemasFiltrados(FiltroTema filtro);

        [OperationContract]
        int InserirTema(Tema usuario);

        [OperationContract]
        void AtualizarTema(Tema usuario);

        [OperationContract]
        void ExcluirTema(int codigo);

        #endregion

        #region Turma

        [OperationContract]
        Turma SelecionarTurmaPorCodigo(int codigo);

        [OperationContract]
        int SelecionarQuantidadeTurmasFiltradas(FiltroTurma filtro);

        [OperationContract]
        List<Turma> SelecionarTurmasFiltradas(FiltroTurma filtro);

        [OperationContract]
        int InserirTurma(Turma turma);

        [OperationContract]
        void AtualizarTurma(Turma turma);

        [OperationContract]
        void ExcluirTurma(int codigo);

        [OperationContract]
        List<Turma> SelecionarTurmasPorIdProfessor(int id);

        #endregion

    }
}
