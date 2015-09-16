using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface ITurmaDataAccess : IBaseDataAccess
    {
        Turma SelecionarTurmaPorCodigo(int codigo);

        int SelecionarQuantidadeTurmasFiltradas(FiltroTurma filtro);

        List<Turma> SelecionarTurmasFiltradas(FiltroTurma filtro);

        int InserirTurma(Turma disciplina);

        void AtualizarTurma(Turma disciplina);

        void ExcluirTurma(int codigo);
    }
}
