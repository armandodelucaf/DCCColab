using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COBFramework
{
    public interface IConexaoManager
    {
        void AssignSharedTransaction(ref IDbTransaction sharedTransaction);
        void BeginTransaction();
        void Close();
        void Commit();
        void Open();
        void Rollback();

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
