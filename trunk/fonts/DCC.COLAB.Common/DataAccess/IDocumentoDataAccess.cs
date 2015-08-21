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
    public interface IDocumentoDataAccess : IBaseDataAccess
    {
        Documento SelecionarDocumentoPorCodigo(int codigo);

        int InserirDocumento(Documento documento);

        void AtualizarDocumento(Documento documento);

        int SelecionarQuantidadeDocumentosFiltrados(FiltroDocumento filtro);

        List<Documento> SelecionarDocumentosFiltrados(FiltroDocumento filtro);

        void ExcluirDocumento(int codigo);

        void AdicionarVersaoDocumento(Documento documento);

        Documento SelecionarVersaoDocumento(int codDocumento, int versao);

        void ExcluirVersaoDocumento(int codDocumento, int versao);

        void ExcluirTodasVersoesDocumento(int codigoDocumento);

    }
}
