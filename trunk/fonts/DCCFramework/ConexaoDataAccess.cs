using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MySql.Data.MySqlClient;

namespace DCCFramework
{
    public class ConexaoDataAccess
    {
        private string stringDeConexaoComBD;
        private static ConexaoDataAccess instancia;

        public static ConexaoDataAccess GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new ConexaoDataAccess();
            }
            return instancia;
        }

        protected ConexaoDataAccess()
        {
            this.ConfigurarConexaoBD();
        }

        protected void ConfigurarConexaoBD()
        {
            try
            {
                this.stringDeConexaoComBD = ConfigurationManager.AppSettings["stringDeConexaoBD"];
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar a string de conexão com banco de dados");
            }
        }

        public MySqlConnection ConnectDatabase()
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection(this.stringDeConexaoComBD);
                return sqlConnection;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao conectar na base de dados");
            }
        }

    }


}
