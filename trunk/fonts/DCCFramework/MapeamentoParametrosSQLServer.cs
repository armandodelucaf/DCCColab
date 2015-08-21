using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DCCFramework
{
    public class MapeamentoParametrosSQLServer
    {
        
        public void SubstituiParametrosQueriesList(DbCommand command, Hashtable listaDeParametros)
        {
            if(listaDeParametros != null && listaDeParametros.Count >0)
            {
                foreach (DictionaryEntry parametro in listaDeParametros)
	            {
                    String chaveDoParametro = parametro.Key.ToString();
                    Object valorDoParametro = parametro.Value;

                    if (chaveDoParametro == "ORDER_BY" || chaveDoParametro == "ORDER_BY_EXTERNO")
                    {
                        command.CommandText = command.CommandText.Replace("@" + chaveDoParametro, valorDoParametro.ToString());
                    }
                    else if (valorDoParametro is IEnumerable<int> && !(valorDoParametro is Byte[]))
                    {
                        string str = "";
                        List<int> lista = (List<int>)valorDoParametro;
                        str = (lista.Count == 0 ? "NULL" : string.Join(",", lista));

                        command.CommandText = command.CommandText.Replace("@" + chaveDoParametro, str);
                    }
                    else if (valorDoParametro is IEnumerable<string>)
                    {
                        string str = "";
                        List<string> lista = (List<string>)valorDoParametro;
                        str = (lista.Count == 0 ? "NULL" : string.Join(",", lista));

                        command.CommandText = command.CommandText.Replace("@" + chaveDoParametro, str);
                    }
                    else
                    {
                        DbParameter sqlParameter = command.CreateParameter();
                        CriarParametro(sqlParameter, chaveDoParametro, valorDoParametro);
                        command.Parameters.Add(sqlParameter);
                    }
                }
            }
        }

        private void CriarParametro(DbParameter parametro, string chaveDoParametro, Object valorDoParametro)
        {
            parametro.ParameterName = "@" + chaveDoParametro;

 	        if (valorDoParametro == null)
            {
                parametro.Value = DBNull.Value;
            }
            else{

                if (valorDoParametro.GetType().IsEnum)
                {
                    parametro.Value = (int)valorDoParametro;
                }
                else
                {
                    string tipoParametro = valorDoParametro.GetType().Name;
                    switch (tipoParametro)
                    {
                        case ("String"):
                            parametro.Value = valorDoParametro.ToString();
                            break;
                        case ("Double"):
                            parametro.Value = Double.Parse(valorDoParametro.ToString());
                            break;
                        case ("Int32"):
                            parametro.Value = Int32.Parse(valorDoParametro.ToString());
                            break;
                        case ("Boolean"):
                            Boolean valor = Boolean.Parse(valorDoParametro.ToString());
                            parametro.Value = valor == true ? "1" : "0";
                            break;
                        case ("DateTime"):
                            DateTime data = (DateTime)valorDoParametro;
                            //parametro.Value = data.ToString("dd/MM/yyyy HH:mm:ss");
                            //parametro.Value = data.ToString("yyyy-MM-dd HH:mm:ss");
                            parametro.Value = data;
                            break;
                        default:
                            parametro.Value = valorDoParametro;
                            break;
                    }
                }
            }
        }

    }
}
