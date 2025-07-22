using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Menassah;

namespace Menassah.Shared
{
   

    public class SharedCode
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SharedCode(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }
        public DataSet GetDataSet(SqlCommand cmd, int CompanyID)
        {
            DataSet ds = new DataSet();

            using SqlConnection connection = GetConnection();
            cmd.Connection = connection;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(ds);

            return ds;

        }

        public SqlParameter GetParameterReturnValue(string parameter)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, SqlDbType.Int); ;

            parameterObject.Direction = ParameterDirection.ReturnValue;


            parameterObject.Value = 0;

            return parameterObject;
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int returnValue = 0;

            try
            {
                using SqlConnection connection = GetConnection();
                cmd.Connection = connection;
                returnValue = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //LogException("Failed to ExecuteNonQuery for " + procedureName, ex, parameters);
                return 0;
            }

            return returnValue;
        }


        public SqlCommand GetCommand(string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText)
            {
                CommandType = commandType
            };
            return command;
        }

        public SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value)
            {
                Direction = ParameterDirection.Input
            };
            return parameterObject;
        }



    }
}
