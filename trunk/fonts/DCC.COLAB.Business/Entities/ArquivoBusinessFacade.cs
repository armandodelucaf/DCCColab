using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCC.COLAB.Business.Entities
{
    [Transactional]
    public class ArquivoBusinessFacade : BaseBusinessFacade<IArquivoDataAccess>
    {

        public virtual Arquivo SelecionarArquivoPorCodigo(int codigo)
        {
            try
            {
                return dataAccess.SelecionarArquivoPorCodigo(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [RequiresTransaction]
        public virtual int InserirArquivo(Arquivo imagem)
        {
            try
            {
                imagem.codigo = dataAccess.InserirArquivo(imagem);
                return imagem.codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void AtualizarArquivo(Arquivo imagem)
        {
            try
            {
                dataAccess.AtualizarArquivo(imagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [RequiresTransaction]
        public virtual void ExcluirArquivo(int codigo)
        {
            try
            {
                dataAccess.ExcluirArquivo(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}