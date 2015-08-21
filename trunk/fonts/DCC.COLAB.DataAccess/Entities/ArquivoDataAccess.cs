using DCCFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
//using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Entities;
using DCC.COLAB.Common.Basic;
using MySql.Data.MySqlClient;

namespace DCC.COLAB.DataAccess.SQLServer.Entities
{
    public class ArquivoDataAccess : DCCFramework.DataAccess, IArquivoDataAccess
    {

        public Arquivo SelecionarArquivoPorCodigo(int codigo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("ID", codigo);
            return this.SelecionarPorNomeQuery("selecionarArquivoPorCodigo", parametros, this.RecuperaObjeto).Cast<Arquivo>().FirstOrDefault();
        }

        public int InserirArquivo(Arquivo arquivo)
        {
            try
            {
                Hashtable parametros = this.BuildParametrosArquivoInserir(arquivo);
                return this.InserirObjetoPorNomeQueryERetornarId("inserirArquivo", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarArquivo(Arquivo arquivo)
        {
            try
            {
                Hashtable parametros = this.BuildParametrosArquivoInserir(arquivo);
                parametros.Add("CODIGO", arquivo.codigo);
                this.AtualizarObjetoPorNomeQuery("atualizarArquivo", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirArquivo(int codigo)
        {
            try
            {
                Hashtable parametros = new Hashtable();
                parametros.Add("ID", codigo);
                this.RemoverPorNomeQuery("excluirArquivo", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region BuildParametros

        private Hashtable BuildParametrosArquivoInserir(Arquivo arquivo)
        {
            Hashtable parametros = new Hashtable();
            parametros.Add("BINARIO", arquivo.binario);
            return parametros;
        }

        #endregion

        #region RecuperaObjeto

        public Arquivo RecuperaObjeto(MySqlDataReader dr)
        {
            Arquivo arquivo = new Arquivo();
            arquivo.codigo = CastDB<int>(dr, "id_arquivo");
            arquivo.binario = CastDB<byte[]>(dr, "binario");
            
            return arquivo;
        }

        #endregion
    }
}
