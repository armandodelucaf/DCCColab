using COLAB.DCC.Business.Util;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Business.Entities
{
    [Transactional]
    public class DocumentoBusinessFacade : BaseBusinessFacade<IDocumentoDataAccess>
    {
        
        public virtual Documento SelecionarDocumentoPorCodigo(int codigo)
        {
            return dataAccess.SelecionarDocumentoPorCodigo(codigo);
        }

        [RequiresTransaction]
        public virtual int InserirDocumento(Documento documento, string usuario)
        {
            ArquivoBusinessFacade arquivoBusinessFacade = ObterOutraBusiness<ArquivoBusinessFacade>();
            documento.arquivo.codigo = arquivoBusinessFacade.InserirArquivo(documento.arquivo);           
            int codigo = dataAccess.InserirDocumento(documento);
            documento.codigo = codigo;
            dataAccess.AdicionarVersaoDocumento(documento);
            return codigo;
        }
        
        [RequiresTransaction]
        public virtual void AtualizarDocumento(Documento documento, string usuario)
        {
            Documento antigoRegistro = SelecionarDocumentoPorCodigo(documento.codigo);

            if (antigoRegistro.arquivo.codigo != documento.arquivo.codigo)
            {
                if (antigoRegistro.versao < documento.versao)
                {
                    ArquivoBusinessFacade arquivoBusinessFacade = ObterOutraBusiness<ArquivoBusinessFacade>();
                    documento.arquivo.codigo = arquivoBusinessFacade.InserirArquivo(documento.arquivo);
                    dataAccess.AdicionarVersaoDocumento(documento);
                }
            }

            dataAccess.AtualizarDocumento(documento);
        }

        public virtual int SelecionarQuantidadeDocumentosFiltrados(FiltroDocumento filtro)
        {
            return dataAccess.SelecionarQuantidadeDocumentosFiltrados(filtro);
        }
        
        public virtual List<Documento> SelecionarDocumentosFiltrados(FiltroDocumento filtro)
        {
            return dataAccess.SelecionarDocumentosFiltrados(filtro);
        }

        [RequiresTransaction]
        public virtual void ExcluirDocumento(int codigo, string usuario)
        {
            Documento registro = SelecionarDocumentoPorCodigo(codigo);
            ArquivoBusinessFacade arquivoBusinessFacade = ObterOutraBusiness<ArquivoBusinessFacade>();
            List<int> listaDeArquivos = new List<int>();
            for (int i = 1; i < registro.versao + 1; i++)
            {
                Documento doc = dataAccess.SelecionarVersaoDocumento(registro.codigo, i);
                listaDeArquivos.Add(doc.arquivo.codigo);
            }
            dataAccess.ExcluirTodasVersoesDocumento(codigo);
            dataAccess.ExcluirDocumento(codigo);
            foreach (var arquivo in listaDeArquivos)
            {
                arquivoBusinessFacade.ExcluirArquivo(arquivo);
            }
        }
        
        [RequiresTransaction]
        public virtual void ExcluirUltimaVersaoDocumento(int codigo, string usuario)
        {
            Documento versaoAtual = SelecionarDocumentoPorCodigo(codigo);
            if (versaoAtual.versao > 1)
            {
                ArquivoBusinessFacade arquivoBusinessFacade = ObterOutraBusiness<ArquivoBusinessFacade>();
                dataAccess.ExcluirVersaoDocumento(codigo, versaoAtual.versao);
                arquivoBusinessFacade.ExcluirArquivo(versaoAtual.arquivo.codigo);                
            }
            else
            {
                ExcluirDocumento(versaoAtual.codigo, usuario);
            }
        }

        [RequiresTransaction]
        public virtual Arquivo SelecionarArquivoDoDocumento(int codigo)
        {
            Documento documento = SelecionarDocumentoPorCodigo(codigo);
            ArquivoBusinessFacade arquivoBusinessFacade = ObterOutraBusiness<ArquivoBusinessFacade>();
            return arquivoBusinessFacade.SelecionarArquivoPorCodigo(documento.arquivo.codigo);
        }
    }
}
