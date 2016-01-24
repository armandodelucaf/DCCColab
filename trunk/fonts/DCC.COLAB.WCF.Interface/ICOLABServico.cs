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
        Usuario ValidarUsuarioFacebook(string idFacebook);

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

        #region Prova

        [OperationContract]
        Prova SelecionarProvaPorCodigo(int codigo, int? idUsuario = null);

        [OperationContract]
        int InserirProva(Prova prova);

        [OperationContract]
        void AtualizarProva(Prova prova);

        [OperationContract]
        int SelecionarQuantidadeProvasFiltradas(FiltroProva filtro);

        [OperationContract]
        List<Prova> SelecionarProvasFiltradas(FiltroProva filtro);

        [OperationContract]
        void ExcluirProva(int codigo);

        [OperationContract]
        List<TipoProva> SelecionarTiposProva();

        [OperationContract]
        void SalvarAvaliacaoProva(AvaliacaoUsuario aval);

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

        #region Link

        [OperationContract]
        Link SelecionarLinkPorCodigo(int codigo, int? idUsuario = null);

        [OperationContract]
        int InserirLink(Link link);

        [OperationContract]
        void AtualizarLink(Link link);

        [OperationContract]
        int SelecionarQuantidadeLinksFiltrados(FiltroLink filtro);

        [OperationContract]
        List<Link> SelecionarLinksFiltrados(FiltroLink filtro);

        [OperationContract]
        void ExcluirLink(int codigo);

        [OperationContract]
        List<TipoLink> SelecionarTiposLink();

        [OperationContract]
        void SalvarAvaliacaoLink(AvaliacaoUsuario aval);

        #endregion

    }
}
