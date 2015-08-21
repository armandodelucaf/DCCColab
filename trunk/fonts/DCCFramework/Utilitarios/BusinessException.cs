using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DCCFramework.Utilitarios
{
    [Serializable]
    public class BusinessException : BaseException
    {
        public BusinessException() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception inner) : base(message, inner) { }
        public bool LogFeito { get; set; }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
