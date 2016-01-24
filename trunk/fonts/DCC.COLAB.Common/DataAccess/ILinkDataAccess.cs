using DCC.COLAB.Common.AuxiliarEntities;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface ILinkDataAccess : IBaseDataAccess
    {
        Link SelecionarLinkPorCodigo(int codigo);

        int InserirLink(Link prova);

        void AtualizarLink(Link prova);

        int SelecionarQuantidadeLinksFiltrados(FiltroLink filtro);

        List<Link> SelecionarLinksFiltrados(FiltroLink filtro);

        void ExcluirLink(int codigo);


        List<TipoLink> SelecionarTiposLink();


        List<Tema> SelecionarTemasPorIdLink(int id);

        void InserirTemaLink(int idTema, int idLink);

        void ExcluirTemaLink(int idLink);


        void SalvarAvaliacaoLink(AvaliacaoUsuario aval);

        AvaliacaoUsuario ObterAvaliacaoLink(int idLink, int idUsuario);

    }
}
