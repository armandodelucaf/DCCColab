/// <summary>
/// Classe para desacoplar a dependencia da aplicacao a ferramenta de Log
/// Alterar no futuro para instanciar a ferramenta de Log dinamicamente.
/// 
/// Com o Log4Net a seguinte linha deve ser inserida no AssemblyInfo.cs:
/// [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
/// </summary>

using System;
namespace DCC.COLAB.Common.Basic

{
    public class LogManager
    {
        public static Logger GetLogger<T>()
	    {
		    return new Logger(typeof(T).FullName);
	    }

        public static Logger GetLogger(Type type, string usuario)
        {
            return new Logger(type.FullName);
        }
    }
}
