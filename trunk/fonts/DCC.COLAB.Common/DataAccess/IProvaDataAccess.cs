using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IProvaDataAccess : IBaseDataAccess
    {
        Prova SelecionarProvaPorCodigo(int codigo);

        int InserirProva(Prova prova);

        void AtualizarProva(Prova prova);

        int SelecionarQuantidadeProvasFiltradas(FiltroProva filtro);

        List<Prova> SelecionarProvasFiltradas(FiltroProva filtro);

        void ExcluirProva(int codigo);


        List<TipoProva> SelecionarTiposProva();


        List<Tema> SelecionarTemasPorIdProva(int id);

        void InserirTemaProva(int idTema, int idProva);

        void ExcluirTemaProva(int idProva);

    }
}
