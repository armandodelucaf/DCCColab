/// <summary>
/// Classe para desacoplar a dependencia da aplicacao a ferramenta de Log
/// Alterar no futuro para instanciar a ferramenta de Log dinamicamente.
/// </summary>

using log4net;
using System;
using System.Collections;
using System.Text;
using DCCFramework.Utilitarios;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net.Appender;
using System.Data;
using log4net.Layout;

namespace DCC.COLAB.Common.Basic
{
    public class Logger
    {
        #region "Variáveis Reservadas"

        private string classe = string.Empty;
        private static ILog _log;

        #endregion

        #region "Construtores"

        public Logger(string type)
        {
            classe = type;
            _log = log4net.LogManager.GetLogger(classe);
        }

        #endregion

        #region "Métodos Públicos"

        public void Debug(string message)
        {
            _log.Debug(string.Format("{0}: {1}", classe, message));
        }

        public void Info(string message)
        {
            _log.Info(string.Format("{0}: {1}", classe, message));
        }

        public void Fatal(string message)
        {
            _log.Fatal(string.Format("{0}: {1}", classe, message));
        }

        public void Warn(string message)
        {
            _log.Warn(string.Format("{0}: {1}", classe, message));
        }

        public void Fatal(string message, Exception exe)
        {
            _log.Fatal(string.Format("{0}: {1}", classe, message), exe);
        }

        public void Erro(string message)
        {
            _log.Error(string.Format("{0}: {1}", classe, message));
        }

        public void Erro(string message, Exception exe)
        {
            _log.Error(string.Format("{0}: {1}", classe, message), exe);
        }

        public void ErroSql(string queryString, ArrayList paramsList, BaseException exe)
        {
            string @params = "[";
            int contador = 0;

            if ((paramsList == null))
            {
                @params = "";
            }
            else
            {
                while (contador < paramsList.Count)
                {
                    if ((paramsList[contador] == null))
                    {
                        @params = @params + "null";
                    }
                    else
                    {
                        @params = @params + paramsList[contador].ToString();
                    }
                    if ((contador != paramsList.Count - 1))
                    {
                        @params = @params + ", ";
                    }
                    contador = contador + 1;
                }
                @params = @params + "]";
            }
            Erro("Erro ao executar query: " + queryString + " " + @params, exe);
        }

        public void ErroSql(string queryString, Hashtable paramsList, BaseException exe)
        {
            string @params = "[";
            int contador = 0;

            if ((paramsList == null))
            {
                @params = "";
            }
            else
            {
                foreach (string parametro in paramsList.Keys)
                {
                    if ((paramsList[parametro] == null))
                    {
                        @params = @params + parametro + " = null";
                    }
                    else
                    {
                        @params = @params + parametro + " = " + paramsList[parametro].ToString();
                    }
                    if ((contador != paramsList.Count - 1))
                    {
                        @params = @params + ", ";
                    }
                    contador += 1;
                }
                @params = @params + "]";
            }
            Erro("Erro ao executar query: " + queryString + " " + @params, exe);
        }

        public string CreateErrorException(Exception exception)
        {
            StringBuilder result = new StringBuilder();
            string newLine = Environment.NewLine;

            result.Append("Message: ");
            result.Append(exception.Message);
            result.Append(" ");
            result.Append(exception.GetType().ToString());
            result.Append(newLine);

            if ((!string.IsNullOrEmpty(exception.StackTrace)))
            {
                result.Append("StackTrace:");
                result.Append(newLine);
                result.Append(exception.StackTrace);
                result.Append(newLine);
            }

            if (exception.InnerException != null)
            {
                result.Append("Inner Exception:");
                result.Append(newLine);
                result.Append(CreateErrorException(exception.InnerException));
                result.Append(newLine);
            }

            return result.ToString();
        }

        public bool IsLoggingEnabled()
        {
            return _log.IsDebugEnabled || _log.IsErrorEnabled || _log.IsInfoEnabled || _log.IsWarnEnabled;
        }

        public bool IsDebugEnabled()
        {
            return _log.IsDebugEnabled;
        }

        public bool IsInfoEnabled()
        {
            return _log.IsInfoEnabled;
        }

        public bool IsWarnEnabled()
        {
            return _log.IsWarnEnabled;
        }

        public bool IsFatalEnabled()
        {
            return _log.IsFatalEnabled;
        }

        public void WriteErrorLog(Exception ex, string usuario)
        {
            if (this.IsLoggingEnabled())
            {
                DtoException dto = UtilitarioExceptions.ObterInfoExcecao(ex);

                string message = string.Format(Message.GetMessage(Message.LOG_ERROR_MSG), dto.nomeClasse, dto.nomeMetodo, dto.mensagem + " : " + dto.stackTrace);
                AddCustomParameters(usuario, dto.nomeClasse, dto.nomeMetodo, dto.mensagem, dto.stackTrace);
                this.Erro(message);
            }
        }

        public void WriteErrorLog(Exception ex, MethodBase methodInfo, string usuario)
        {
            if (this.IsLoggingEnabled())
            {
                string message = string.Format(Message.GetMessage(Message.LOG_ERROR_MSG), methodInfo.DeclaringType.Name, methodInfo.Name, ex.Message + " : " + ex.StackTrace);
                AddCustomParameters(usuario, methodInfo.DeclaringType.Name, methodInfo.Name, ex.Message, ex.StackTrace);
                this.Erro(message);
            }
        }

        public void WriteInfoLog(MethodBase methodInfo, string usuario)
        {
            if (this.IsLoggingEnabled())
            {
                string message__1 = String.Format(Message.GetMessage(Message.LOG_SUCCESS_MSG), methodInfo.DeclaringType.Name, methodInfo.Name);
                AddCustomParameters(usuario, methodInfo.DeclaringType.Name, methodInfo.Name);
                this.Info(message__1);
            }
        }

        private void AddCustomParameters(string usuario, string classe, string metodo, string mensagem = null, string stacktrace = null) 
        {
            log4net.LogicalThreadContext.Properties["usuario"] = usuario;
            log4net.LogicalThreadContext.Properties["classe"] = classe;
            log4net.LogicalThreadContext.Properties["metodo"] = metodo;
            log4net.LogicalThreadContext.Properties["mensagem"] = mensagem;
            log4net.LogicalThreadContext.Properties["stacktrace"] = stacktrace;
        }

        #endregion
    }
}
