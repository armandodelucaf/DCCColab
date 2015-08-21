using DCC.COLAB.Common.AuxiliarEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace DCC.COLAB.Common.Basic
{
    public class Util
    {
        public static string ObterLoginUsuarioPeloHeaderWCF() {
           try
           {
               return OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("usuario", "ns");
           }
           catch (Exception)
           {
               return string.Empty;
           }
        }
    }
}