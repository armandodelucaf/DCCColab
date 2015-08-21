using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Filtros;
using DCCFramework;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class DocumentoDataAccess : BaseDataAccess, IDocumentoDataAccess
    {

        public Documento SelecionarDocumentoPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarDocumentoPorCodigo", parametros, this.RecuperaObjeto).Cast<Documento>().FirstOrDefault();
        }
        
        public int InserirDocumento(Documento documento)
        {                        
            Hashtable parametrosDocumento = this.BuildParametrosDocumento(documento);
            int idDocumento = this.InserirObjetoPorNomeQueryERetornarId("inserirDocumento", parametrosDocumento);
            return idDocumento;
        }

        public void AtualizarDocumento(Documento documento)
        {
            Hashtable parametros = this.BuildParametrosDocumento(documento);    
            this.AtualizarObjetoPorNomeQuery("atualizarDocumento", parametros);
        }

        public int SelecionarQuantidadeDocumentosFiltrados(FiltroDocumento filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarDocumentosFiltrados(filtro);
            return this.SelecionarQuantidadePorNomeQuery("selecionarDocumentosFiltrados", parametros);
        }

        public List<Documento> SelecionarDocumentosFiltrados(FiltroDocumento filtro)
        {
            Hashtable parametros = this.BuildParametrosSelecionarDocumentosFiltrados(filtro);
            return this.SelecionarFiltradoPorNomeQuery("selecionarDocumentosFiltrados", parametros, filtro.comPaginacao, this.RecuperaObjeto).Cast<Documento>().ToList();
        }

        public void ExcluirDocumento(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirDocumento", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void AdicionarVersaoDocumento(Documento documento)
        {
            if (documento.versao == 0)
            {
                documento.versao = 1;
            }           
            Hashtable parametros = this.BuildParametrosDocumentoVersao(documento);
            this.InserirObjetoPorNomeQuery("adicionarVersaoDocumento", parametros);
        }

        public Documento SelecionarVersaoDocumento(int codDocumento, int versao)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codDocumento);
            parametros.Add("VERSAO", versao);
            return this.SelecionarPorNomeQuery("selecionarVersaoDocumento", parametros, this.RecuperaObjeto).Cast<Documento>().FirstOrDefault();          
        }

        public void ExcluirVersaoDocumento(int codDocumento, int versao)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codDocumento);
            parametros.Add("VERSAO", versao);
            this.InserirObjetoPorNomeQuery("excluirVersaoDocumento", parametros);
        }

        public void ExcluirTodasVersoesDocumento(int codigoDocumento)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigoDocumento);
            this.RemoverPorNomeQuery("excluirTodasVersoesDocumento", parametros);
        }

        #region RecuperaObjeto
        private Documento RecuperaObjeto(MySqlDataReader dr)
        {
            Documento documento = new Documento();

            documento.codigo = CastDB<int>(dr, "id_documento");
            documento.titulo = CastDB<string>(dr, "titulo");
            documento.nome = CastDB<string>(dr, "nome");
            documento.descricao = CastDB<string>(dr, "descricao");
            documento.versao = CastDB<int>(dr, "versao");
            documento.tamanho = CastDB<double>(dr, "tamanho");
            documento.extensao = CastDB<string>(dr, "extensao"); ;
            documento.arquivo = new Arquivo()
            {
                codigo = CastDB<int>(dr, "id_arquivo")
            };
            return documento;
        }
        #endregion

        #region BuildParametros
        private Hashtable BuildParametrosDocumento(Documento documento)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", documento.codigo);
            parametros.Add("TITULO", documento.titulo);            
            parametros.Add("DESCRICAO", documento.descricao);          
            return parametros;
        }

        private Hashtable BuildParametrosDocumentoVersao(Documento documento)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", documento.codigo);        
            parametros.Add("NOME", documento.nome);
            parametros.Add("TAMANHO", documento.tamanho);
            parametros.Add("VERSAO", documento.versao);
            parametros.Add("EXTENSAO", documento.extensao);       
            if (documento.arquivo != null) { parametros.Add("ID_ARQUIVO", documento.arquivo.codigo); }
            return parametros;
        }
        private Hashtable BuildParametrosSelecionarDocumentosFiltrados(FiltroDocumento filtro)
        {
            Hashtable parametros = CriarHashFiltroDefault(filtro);
            parametros.Add("IDS", filtro.codigos);
            return parametros;
        }
        #endregion        
    }
}
