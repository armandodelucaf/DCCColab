using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.SQLServer;
using System.Collections.Generic;

namespace DCC.COLAB.Common.DataAccess
{
    public interface INotificacaoDataAccess : IBaseDataAccess
    {
        Notificacao SelecionarNotificacaoPorCodigo(int codigo);

        int SelecionarQuantidadeNotificacoesFiltradas(FiltroNotificacao filtro);

        List<Notificacao> SelecionarNotificacoesFiltradas(FiltroNotificacao filtro);

        int InserirNotificacao(Notificacao notificacao);
                
    }
}
