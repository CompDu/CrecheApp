using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLibrary.BusinessLogic
{
    public class SqlDataAccess:IDataAccess
    {
        private readonly IConfiguration configuration;

       
        public SqlDataAccess(IConfiguration config)
        {
            configuration = config;
        }

        public List<T> LoadData<T,U>(string Procedure,U parameter)
        {
            string connectionStringName = configuration.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionStringName)) {
                return connection.Query<T>(Procedure,parameter,commandType:CommandType.StoredProcedure).ToList();
            }
        }

        public int SaveData<U>(string Procedure,U parameter)
        {
            string connectionStringName = configuration.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionStringName)) {
                return connection.Execute(Procedure, parameter, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
