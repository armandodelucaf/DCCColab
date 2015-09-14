using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IUsuarioDataAccess : IBaseDataAccess
    {
        Usuario SelecionarUsuarioPorCodigo(int codigo);

        int SelecionarQuantidadeUsuariosFiltrados(FiltroUsuario filtro);

        List<Usuario> SelecionarUsuariosFiltrados(FiltroUsuario filtro);

        int InserirUsuario(Usuario usuario);

        void AtualizarUsuario(Usuario usuario);

        void ExcluirUsuario(int codigo);

        void AtualizarSenha(Usuario usuario);

        Usuario ValidarUsuarioSenha(string login, string senha);
    }
}
