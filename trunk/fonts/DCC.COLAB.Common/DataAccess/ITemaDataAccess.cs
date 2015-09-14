using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface ITemaDataAccess : IBaseDataAccess
    {
        Tema SelecionarTemaPorCodigo(int codigo);

        int SelecionarQuantidadeTemasFiltrados(FiltroTema filtro);

        List<Tema> SelecionarTemasFiltrados(FiltroTema filtro);

        int InserirTema(Tema disciplina);

        void AtualizarTema(Tema disciplina);

        void ExcluirTema(int codigo);
    }
}
