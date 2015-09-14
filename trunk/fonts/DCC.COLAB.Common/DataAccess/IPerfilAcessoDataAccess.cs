using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IPerfilAcessoDataAccess : IBaseDataAccess
    {
        PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil);

        int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro);

        List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro);
                
    }
}
