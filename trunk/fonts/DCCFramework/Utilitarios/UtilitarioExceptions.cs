using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCFramework.Utilitarios
{
    public class UtilitarioExceptions
    {
        public static DtoException ObterInfoExcecao(Exception ex)
        {
            var info = new DtoException();

            try
            {
                var trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(0);

                for (int i = 0; i < trace.FrameCount; i++)
                {
                    var f = trace.GetFrame(i);
                    if (f.GetFileName() != null)
                    {
                        stackFrame = f;
                        break;
                    }
                }

                if (trace.FrameCount == 0)
                {
                    return info;
                }

                if (stackFrame.GetFileName() == null)
                {
                    info.nomeClasse = ex.TargetSite.ReflectedType.Name;
                    info.nomeMetodo = ex.TargetSite.Name;
                    info.mensagem = ex.Message;
                    info.stackTrace = ex.StackTrace;
                }
                else
                {
                    // Collect data where exception occured
                    string[] splitFile = stackFrame.GetFileName().Split('\\');

                    info.nomeClasse = splitFile.Length > 0 ? splitFile[splitFile.Length - 1].Split('.').First() : string.Empty;
                    info.nomeMetodo = stackFrame.GetMethod().Name;
                    info.mensagem = ex.Message;
                    info.numLinha = stackFrame.GetFileLineNumber();
                    info.stackTrace = ex.StackTrace;
                }
            }
            catch
            {

            }

            return info;
        }
    }

    public class DtoException
    {
        public string nomeClasse { get; set; }
        public string nomeMetodo { get; set; }
        public string mensagem { get; set; }
        public string stackTrace { get; set; }
        public int numLinha { get; set; }

        public override string ToString()
        {
            return string.Format("Classe: {0}, Método: {1}, Linha: {2}, Mensagem: {3}, StackTrace: {4}", nomeClasse, nomeMetodo, numLinha, mensagem, stackTrace);
        }
    }
}
