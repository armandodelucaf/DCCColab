using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DCCFramework
{
    public class CommandFactory
    {
        static CommandFactory()
        {

        }

        public static DbCommand GetCommand(Database db, string commandText)
        {
            DbCommand cmd = db.GetSqlStringCommand(commandText);
            cmd.CommandText = commandText;
            return cmd;
        }

    }
}
