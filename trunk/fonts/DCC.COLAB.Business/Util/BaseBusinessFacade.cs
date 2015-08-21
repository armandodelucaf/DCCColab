using DCC.COLAB.Business.Entities;
using DCC.COLAB.Business.Infrastructure;
using DCC.COLAB.Business.Util;
using DCC.COLAB.Common.SQLServer;
using DCC.COLAB.DataAccess.SQLServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COLAB.DCC.Business.Util
{
    public abstract class BaseBusinessFacade<T> where T : IBaseDataAccess
    {
        protected T dataAccess;

        protected BaseBusinessFacade()
        {
            dataAccess = ContainerManager.Instance.Get<T>();
        }

        protected T ObterOutraBusiness<T>()
        {
            return BusinessFactory.GetInstance().Get<T>();
        }
    }
}
