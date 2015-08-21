using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DCCFramework
{
    public abstract class DataAccess
    {
        protected readonly Database database;
        public delegate object RecuperaObjetoDelegate(MySqlDataReader dr);

        protected DataAccess()
         {
             database = DatabaseFactory.CreateDatabase();
         }

        protected DataAccess(string databaseService)
         {
             database = DatabaseFactory.CreateDatabase(databaseService);
         }

        protected int SelecionarQuantidadePorNomeQuery(string nomeQuery, Hashtable parametros)
        {
            int quantidade = 0;
            ArrayList result = new ArrayList();
            DbCommand command = null;
            IDataReader dr = null;

            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                string queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery);
                string queryCount = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery("querySelecionarCount").ToUpper();
                
                queryCount = queryCount.Replace("@MINHAQUERY@", queryString);
                queryCount = queryCount.Replace("ORDER BY", "").Replace("@ORDER_BY", "");

               //// var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryCount);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                dr = database.ExecuteReader(command);
                var sqlDataReader = ((RefCountingDataReader)dr).InnerReader as MySqlDataReader;
                
                while (sqlDataReader.Read())
                {
                    quantidade = Int32.Parse(sqlDataReader[0].ToString()); 
                }

                return quantidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bool flag = dr != null;
                if (flag)
                {
                    dr.Close();
                    dr.Dispose();
                }
                flag = (command != null);
                if (flag)
                {
                    command.Dispose();
                    
                }
            }
        }

        protected ArrayList SelecionarCampoQuery(string nomeQuery, Hashtable parametros)
        {
            ArrayList result = new ArrayList();
            DbCommand command = null;
            IDataReader dr = null;

            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                String queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery).ToUpper();
               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                dr = database.ExecuteReader(command);
                var sqlDataReader = ((RefCountingDataReader)dr).InnerReader as MySqlDataReader;

                while (sqlDataReader.Read())
                {
                    result.Add(sqlDataReader[0].ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bool flag = dr != null;
                if (flag)
                {
                    dr.Close();
                    dr.Dispose();
                }
                flag = (command != null);
                if (flag)
                {
                    command.Dispose();
                }
            }
        }

        protected ArrayList SelecionarFiltradoPorNomeQuery(string nomeQuery, Hashtable parametros, bool comPaginacao, DataAccess.RecuperaObjetoDelegate recuperaFunction)
        {
            if (comPaginacao)
            {
                return SelecionarPaginadoPorNomeQuery(nomeQuery, parametros, recuperaFunction);
            }
            else
            {
                return SelecionarPorNomeQuery(nomeQuery, parametros, recuperaFunction);
            }
        }

        private ArrayList SelecionarPaginadoPorNomeQuery(string nomeQuery, Hashtable parametros, DataAccess.RecuperaObjetoDelegate recuperaFunction)
        {
            ArrayList result = new ArrayList();
            DbCommand command = null;
            IDataReader dr = null;

            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                String query = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery).ToUpper();
                query = query.Replace("ORDER BY", "").Replace("@ORDER_BY", "");

                string queryPaginacao = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery("querySelecionarPaginado").ToUpper();
                queryPaginacao = queryPaginacao.Replace("@MINHAQUERY@", query);

               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryPaginacao);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                dr = database.ExecuteReader(command);
                var sqlDataReader = ((RefCountingDataReader)dr).InnerReader as MySqlDataReader;
                
                while (dr.Read())
                {
                    Object obj = recuperaFunction(sqlDataReader);
                    result.Add(RuntimeHelpers.GetObjectValue(obj));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bool flag = dr != null;
                if (flag)
                {
                    dr.Close();
                    dr.Dispose();
                }
                flag = (command != null);
                if (flag)
                {
                    command.Dispose();
                }
            }
        }

        protected ArrayList SelecionarPorNomeQuery(string nomeQuery, Hashtable parametros, DataAccess.RecuperaObjetoDelegate recuperaFunction)
        {
            ArrayList result = new ArrayList();
            DbCommand command = null;
            IDataReader dr = null;

            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                String queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery);
               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                dr = database.ExecuteReader(command);
                var sqlDataReader = ((RefCountingDataReader)dr).InnerReader as MySqlDataReader;

                while (dr.Read())
                {
                    Object obj = recuperaFunction(sqlDataReader);
                    result.Add(RuntimeHelpers.GetObjectValue(obj));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bool flag = dr != null;
                if (flag)
                {
                    dr.Close();
                    dr.Dispose();
                }
                flag = (command != null);
                if (flag)
                {
                    command.Dispose();
                }
            }
        }

        protected void RemoverPorNomeQuery(string nomeQuery, Hashtable parametros)
        {
            DbCommand command = null;
            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                String queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery);
               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                database.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
        }

        protected void InserirObjetoPorNomeQuery(string nomeQuery, Hashtable parametros)
        {
            DbCommand command = null;
            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                var queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery);
               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                database.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
        }

        protected int InserirObjetoPorNomeQueryERetornarId(string nomeQuery, Hashtable parametros)
        {
            DbCommand command = null;
            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                var queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery);
                var queryComRetornoId = string.Format("BEGIN {0} SELECT SCOPE_IDENTITY() END", queryString);
               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryComRetornoId);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                var retorno = database.ExecuteScalar(command);
                return (retorno is DBNull ? 0 : Convert.ToInt32(retorno));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
        }

        protected void AtualizarObjetoPorNomeQuery(string nomeQuery, Hashtable parametros)
        {
            DbCommand command = null;
            try
            {
                if ((nomeQuery == null) | (nomeQuery.Trim().Length == 0))
                {
                    throw new ArgumentNullException("nomeQuery", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

                var queryString = GerenciadorDoArquivoDeQueries.Instancia.ObterQuery(nomeQuery);
               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                database.ExecuteScalar(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
        }

        protected void AtualizarObjetoPorQueryString(string queryString, Hashtable parametros)
        {
            DbCommand command = null;
            try
            {
                if ((queryString == null) | (queryString.Trim().Length == 0))
                {
                    throw new ArgumentNullException("queryString", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                database.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
        }

        protected void InserirObjetoPorQueryString(string queryString, Hashtable parametros)
        {
            DbCommand command = null;
            try
            {
                if ((queryString == null) | (queryString.Trim().Length == 0))
                {
                    throw new ArgumentNullException("queryString", "Argumento n\x00e3o pode ser nulo ou vazio.");
                }

               // var database = DatabaseFactory.CreateDatabase();
                command = CommandFactory.GetCommand(database, queryString);

                MapeamentoParametrosSQLServer mapper = new MapeamentoParametrosSQLServer();
                mapper.SubstituiParametrosQueriesList(command, parametros);

                database.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
        }

        protected static bool TemColuna(IDataRecord dr, string nomeColuna)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(nomeColuna, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        protected static T CastDB<T>(IDataRecord dr, string nomeColuna)
        {
            if (TemColuna(dr, nomeColuna))
            {
                if (!(dr[nomeColuna] is DBNull))
                {
                    if (typeof(T).IsEnum)
                    {
                        return (T)Enum.Parse(typeof(T), dr[nomeColuna].ToString());
                    }
                    else if (typeof(T).IsGenericType) {
                        if (typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>) && Nullable.GetUnderlyingType(typeof(T)).IsEnum)
                        {
                            Type I = Nullable.GetUnderlyingType(typeof(T));
                            return (T)Enum.Parse(I, dr[nomeColuna].ToString());
                        }
                    }

                    return (T)Convert.ChangeType(dr[nomeColuna], typeof(T));
                }
            }
            return default(T);
        }
    }
}
