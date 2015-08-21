using System;
using System.Collections.Generic;
using System.Text;

namespace DCC.COLAB.Business.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransactionalAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresTransactionAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresConnectionAttribute : Attribute
    {
    }
}
