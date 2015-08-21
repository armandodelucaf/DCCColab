using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DCC.COLAB.Common.Basic
{
    public class Message
    {
        public static string GENERIC_ERROR = "GenericErrorText";

        public const string LOG_SUCCESS_MSG = "LogSucessMessage";
        public const string LOG_ERROR_MSG = "LogErrorMessage";
        public const string LOG_UNKNOWN_ERROR_MSG = "LogUnknownErrorMessage";

        private static Message message;
        private ResourceManager resourceFile;

        private Message()
        {
            resourceFile = new ResourceManager("DCC.COLAB.Common.Basic.Log.Messages", System.Reflection.Assembly.GetExecutingAssembly());
        }

        private static Message GetInstance()
        {
            if (message == null)
            {
                message = new Message();
            }
            return message;
        }

        public static string GetMessage(string key)
        {
            Message message = GetInstance();
            string result = message.resourceFile.GetString(key);
            return result;
        }
    }
}
