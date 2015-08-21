using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Common.Basic;
using DCCFramework.Utilitarios;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace DCC.COLAB.Business.Util
{
    public class BusinessMethodManagementBehavior : IInterceptionBehavior
    {
        private const string GENERAL_CATEGORY = "General";
        private Logger logWriter = LogManager.GetLogger<BusinessMethodManagementBehavior>();

        public BusinessMethodManagementBehavior()
        {
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result = null;
            
            try
            {
                if (input.MethodBase.IsDefined(typeof(RequiresTransactionAttribute), true))
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                    {
                        result = getNext().Invoke(input, getNext);

                        if (result.Exception != null)
                        {
                            throw result.Exception;
                        }

                        scope.Complete();
                    }
                }
                else
                {
                    result = getNext().Invoke(input, getNext);
                    if (result.Exception != null)
                    {
                        throw result.Exception;
                    }
                }
            }
            catch (BusinessException ex)
            {
                if (!ex.LogFeito)
                {
                    logWriter.WriteErrorLog(ex, input.MethodBase, DCC.COLAB.Common.Basic.Util.ObterLoginUsuarioPeloHeaderWCF());
                    ex.LogFeito = true;
                }
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
