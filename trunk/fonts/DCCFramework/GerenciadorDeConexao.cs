using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COBFramework
{
    public class GerenciadorDeConexao : IConexaoManager
    {
        private IDbConnection dbConnection;
        private IDbTransaction dbTransaction;
        public const string DEFAULT_QUERY_FILE_KEY = "Default";
        private string m_queryFileKey;

        public IDbConnection Connection
        {
            get
            {
                return this.dbConnection;
            }
        }

        public string QueryFileKey
        {
            get
            {
                return this.m_queryFileKey;
            }
            set
            {
                this.m_queryFileKey = value;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return this.dbTransaction;
            }
        }

        public GerenciadorDeConexao()
        {
            this.m_queryFileKey = "Default";
            this.dbConnection = ConexaoDataAccess.GetInstancia().ConnectDatabase();
            this.dbTransaction = null;
        }

        public GerenciadorDeConexao(string queryFileKey)
        {
            this.m_queryFileKey = "Default";
            this.dbConnection = ConexaoDataAccess.GetInstancia().ConnectDatabase();
            this.dbTransaction = null;
            this.QueryFileKey = queryFileKey;
        }

        public void AssignSharedTransaction(ref IDbTransaction sharedTransaction)
        {
            if ((this.Connection == null) || !this.Connection.Equals(sharedTransaction.Connection))
            {
                if ((this.Connection != null) && (this.Connection.State == ConnectionState.Open))
                {
                    this.Connection.Close();
                }
                this.dbConnection = sharedTransaction.Connection;
                this.dbTransaction = sharedTransaction;
            }
        }

        public void BeginTransaction()
        {
            if (this.dbConnection != null)
            {
                this.dbTransaction = this.dbConnection.BeginTransaction();
            }
        }

        public void Close()
        {
            if ((this.dbConnection != null) && (this.dbConnection.State != ConnectionState.Closed))
            {
                this.dbConnection.Close();
                this.dbConnection.Dispose();
            }
        }

        public void Commit()
        {
            if (this.dbTransaction != null)
            {
                this.dbTransaction.Commit();
            }
        }

        public void Open()
        {
            if (this.dbConnection == null)
            {
                this.dbConnection = ConexaoDataAccess.GetInstancia().ConnectDatabase();
                this.dbConnection.Open();
            }
            else if (this.dbConnection.State != ConnectionState.Open)
            {
                this.Close();
                this.dbConnection = ConexaoDataAccess.GetInstancia().ConnectDatabase();
                this.dbConnection.Open();
            }
        }

        public void Rollback()
        {
            if (this.dbTransaction != null)
            {
                this.dbTransaction.Rollback();
            }
        }

    }
}
