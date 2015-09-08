using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IPerfilAcessoDataAccess : IBaseDataAccess
    {
        PerfilAcesso SelecionarPerfilPorCodigo(int codigoPerfil);

        int InserirPerfilAcesso(PerfilAcesso perfil);

        void AtualizarPerfilAcesso(PerfilAcesso perfil);

        int SelecionarQuantidadePerfisAcessoFiltrados(FiltroBase filtro);

        List<PerfilAcesso> SelecionarPerfisAcessoFiltrados(FiltroBase filtro);

        void ExcluirPerfilAcesso(int codigoPerfil);
                
    }
}
