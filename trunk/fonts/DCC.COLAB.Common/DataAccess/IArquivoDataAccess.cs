using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.DataAccess
{
    public interface IArquivoDataAccess : IBaseDataAccess
    {
        Arquivo SelecionarArquivoPorCodigo(int codigo);

        int InserirArquivo(Arquivo arquivo);

        void AtualizarArquivo(Arquivo arquivo);

        void ExcluirArquivo(int codigo);
    }
}
