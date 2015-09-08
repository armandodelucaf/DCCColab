using DCCFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using DCC.COLAB.Common.DataAccess;
using DCC.COLAB.Common.Basic;
using DCC.COLAB.Common.Filtros;
using DCC.COLAB.Common.AuxiliarEntities;
using System.Data;

namespace DCC.COLAB.DataAccess.SQLServer
{
    public class BaseDataAccess : DCCFramework.DataAccess
    {
        protected virtual string ObterOrdenacaoDefault() { return "nome ASC";  }

        protected Hashtable CriarHashFiltroDefault(FiltroBase filtro)
        {
            string ordenacao = AcrescentarOrdenacao(filtro);

            Hashtable parametros = new Hashtable();
            parametros.Add("FILTRO", filtro.filtroGenerico);
            parametros.Add("REGISTRO_INICIAL", filtro.registroInicial);
            parametros.Add("QTD_A_RETORNAR", filtro.quantidadeARetornar);
            parametros.Add("ORDER_BY", ordenacao);
            //parametros.Add("ORDER_BY_EXTERNO", RetirarReferenciaTabela(ordenacao));

            return parametros;
        }

        private string AcrescentarOrdenacao(FiltroBase filtro) {
            if (filtro.listaOrdenacao != null && filtro.listaOrdenacao.Count > 0)
            {
                string retorno = "";

                for (int i = 0; i < filtro.listaOrdenacao.Count; i++)
                {
                    if (i > 0) {
                        retorno += ", ";
                    }

                    retorno += filtro.listaOrdenacao[i].coluna + " " + filtro.listaOrdenacao[i].ordem;
                }

                return retorno;
            }
            else {
                return ObterOrdenacaoDefault();
            }
        }

        //private string RetirarReferenciaTabela(string ordenacao) {
        //    List<string> lista = ordenacao.Split(new Char [] {','}).ToList();

        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        int indicePonto = lista[i].IndexOf('.');
        //        if (indicePonto >= 0)
        //        {
        //            lista[i] = lista[i].Substring(indicePonto+1);
        //        }
        //    }

        //    return string.Join(", ", lista.ToArray());
        //}
    }
}
