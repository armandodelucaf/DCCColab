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
        public int InserirUsuario(Usuario usuario)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            return usuarioBusinessFacade.InserirUsuario(usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarUsuario(Usuario usuario)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            usuarioBusinessFacade.AtualizarUsuario(usuario);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirUsuario(int codigo)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            usuarioBusinessFacade.ExcluirUsuario(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarSenha(Usuario usuario)
        {
            UsuarioBusinessFacade usuarioBusinessFacade = BusinessFactory.GetInstance().Get<UsuarioBusinessFacade>();
            usuarioBusinessFacade.AtualizarSenha(usuario);
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

        #endregion

        #region Prova
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Prova SelecionarProvaPorCodigo(int codigo)
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            return provaBusinessFacade.SelecionarProvaPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirProva(Prova prova)
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            return provaBusinessFacade.InserirProva(prova);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarProva(Prova prova)
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            provaBusinessFacade.AtualizarProva(prova);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeProvasFiltradas(FiltroProva filtro)
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            return provaBusinessFacade.SelecionarQuantidadeProvasFiltradas(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Prova> SelecionarProvasFiltradas(FiltroProva filtro)
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            return provaBusinessFacade.SelecionarProvasFiltradas(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirProva(int codigo)
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            provaBusinessFacade.ExcluirProva(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<TipoProva> SelecionarTiposProva()
        {
            ProvaBusinessFacade provaBusinessFacade = BusinessFactory.GetInstance().Get<ProvaBusinessFacade>();
            return provaBusinessFacade.SelecionarTiposProva();
        }
        
        #endregion

        #region Disciplina

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Disciplina SelecionarDisciplinaPorCodigo(int codigo)
        {
            DisciplinaBusinessFacade disciplinaBusinessFacade = BusinessFactory.GetInstance().Get<DisciplinaBusinessFacade>();
            return disciplinaBusinessFacade.SelecionarDisciplinaPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            DisciplinaBusinessFacade disciplinaBusinessFacade = BusinessFactory.GetInstance().Get<DisciplinaBusinessFacade>();
            return disciplinaBusinessFacade.SelecionarQuantidadeDisciplinasFiltradas(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Disciplina> SelecionarDisciplinasFiltradas(FiltroDisciplina filtro)
        {
            DisciplinaBusinessFacade disciplinaBusinessFacade = BusinessFactory.GetInstance().Get<DisciplinaBusinessFacade>();
            return disciplinaBusinessFacade.SelecionarDisciplinasFiltradas(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirDisciplina(Disciplina disciplina)
        {
            DisciplinaBusinessFacade disciplinaBusinessFacade = BusinessFactory.GetInstance().Get<DisciplinaBusinessFacade>();
            return disciplinaBusinessFacade.InserirDisciplina(disciplina);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarDisciplina(Disciplina disciplina)
        {
            DisciplinaBusinessFacade disciplinaBusinessFacade = BusinessFactory.GetInstance().Get<DisciplinaBusinessFacade>();
            disciplinaBusinessFacade.AtualizarDisciplina(disciplina);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirDisciplina(int codigo)
        {
            DisciplinaBusinessFacade disciplinaBusinessFacade = BusinessFactory.GetInstance().Get<DisciplinaBusinessFacade>();
            disciplinaBusinessFacade.ExcluirDisciplina(codigo);
        }

        #endregion

        #region Tema
        
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Tema SelecionarTemaPorCodigo(int codigo)
        {
            TemaBusinessFacade temaBusinessFacade = BusinessFactory.GetInstance().Get<TemaBusinessFacade>();
            return temaBusinessFacade.SelecionarTemaPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeTemasFiltrados(FiltroTema filtro)
        {
            TemaBusinessFacade temaBusinessFacade = BusinessFactory.GetInstance().Get<TemaBusinessFacade>();
            return temaBusinessFacade.SelecionarQuantidadeTemasFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Tema> SelecionarTemasFiltrados(FiltroTema filtro)
        {
            TemaBusinessFacade temaBusinessFacade = BusinessFactory.GetInstance().Get<TemaBusinessFacade>();
            return temaBusinessFacade.SelecionarTemasFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirTema(Tema tema)
        {
            TemaBusinessFacade temaBusinessFacade = BusinessFactory.GetInstance().Get<TemaBusinessFacade>();
            return temaBusinessFacade.InserirTema(tema);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarTema(Tema tema)
        {
            TemaBusinessFacade temaBusinessFacade = BusinessFactory.GetInstance().Get<TemaBusinessFacade>();
            temaBusinessFacade.AtualizarTema(tema);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirTema(int codigo)
        {
            TemaBusinessFacade temaBusinessFacade = BusinessFactory.GetInstance().Get<TemaBusinessFacade>();
            temaBusinessFacade.ExcluirTema(codigo);
        }

        #endregion

        #region Turma

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Turma SelecionarTurmaPorCodigo(int codigo)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            return turmaBusinessFacade.SelecionarTurmaPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeTurmasFiltradas(FiltroTurma filtro)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            return turmaBusinessFacade.SelecionarQuantidadeTurmasFiltradas(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Turma> SelecionarTurmasFiltradas(FiltroTurma filtro)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            return turmaBusinessFacade.SelecionarTurmasFiltradas(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirTurma(Turma turma)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            return turmaBusinessFacade.InserirTurma(turma);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarTurma(Turma turma)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            turmaBusinessFacade.AtualizarTurma(turma);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirTurma(int codigo)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            turmaBusinessFacade.ExcluirTurma(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Turma> SelecionarTurmasPorIdProfessor(int id)
        {
            TurmaBusinessFacade turmaBusinessFacade = BusinessFactory.GetInstance().Get<TurmaBusinessFacade>();
            return turmaBusinessFacade.SelecionarTurmasPorIdProfessor(id);
        }

        #endregion

        #region Link
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public Link SelecionarLinkPorCodigo(int codigo)
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            return linkBusinessFacade.SelecionarLinkPorCodigo(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int InserirLink(Link link)
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            return linkBusinessFacade.InserirLink(link);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void AtualizarLink(Link link)
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            linkBusinessFacade.AtualizarLink(link);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public int SelecionarQuantidadeLinksFiltrados(FiltroLink filtro)
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            return linkBusinessFacade.SelecionarQuantidadeLinksFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<Link> SelecionarLinksFiltrados(FiltroLink filtro)
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            return linkBusinessFacade.SelecionarLinksFiltrados(filtro);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public void ExcluirLink(int codigo)
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            linkBusinessFacade.ExcluirLink(codigo);
        }

        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public List<TipoLink> SelecionarTiposLink()
        {
            LinkBusinessFacade linkBusinessFacade = BusinessFactory.GetInstance().Get<LinkBusinessFacade>();
            return linkBusinessFacade.SelecionarTiposLink();
        }

        #endregion
    }
}
