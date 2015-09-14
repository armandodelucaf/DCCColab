using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IDisciplinaDataAccess : IBaseDataAccess
    {
        Disciplina SelecionarDisciplinaPorCodigo(int codigo);

        int SelecionarQuantidadeDisciplinasFiltradas(FiltroDisciplina filtro);

        List<Disciplina> SelecionarDisciplinasFiltradas(FiltroDisciplina filtro);

        int InserirDisciplina(Disciplina disciplina);

        void AtualizarDisciplina(Disciplina disciplina);

        void ExcluirDisciplina(int codigo);
    }
}
