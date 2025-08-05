using Menassah.Repo;
using System.Data.Common;
using System.Data;
using MenassahApi.DL;
using System.Data.SqlClient;

namespace Menassah.DAL
{
    public class BaseDAL : IBaseDAL
    {

        public BaseDAL()
        {
        }


        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(Shared.Main.ConnectionString);

            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
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

        public SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        public SqlParameter GetParameterReturnValue(string parameter)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, SqlDbType.Int); ;

            parameterObject.Direction = ParameterDirection.ReturnValue;


            parameterObject.Value = 0;

            return parameterObject;
        }

        public int ExecuteNonQuery(string sql)
        {
            int returnValue = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(sql);
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

        public object ExecuteScalar(SqlCommand cmd)
        {
            object returnValue = null;

            try
            {
                using SqlConnection connection = this.GetConnection();
                cmd.Connection = connection;
                returnValue = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                //LogException("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        public DbDataReader ExecuteDataReader(SqlCommand cmd)
        {
            DbDataReader ds;

            cmd.Connection = GetConnection();
            ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return ds;
        }
        public DataSet GetDataSet(SqlCommand cmd)
        {
            int CompanyID = 0;
            return GetDataSet(cmd, CompanyID);

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

        public List<MenassahApi.DL.MainDL> GetBaseList(string ProcedureName, PageParam pageParam, params SqlParameter[] P)
        {

            if (pageParam == null)
            {
                pageParam = new PageParam();
            }
            SqlCommand cmd = GetCommand(ProcedureName, CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@PageSize", pageParam.PageSize));
            cmd.Parameters.Add(GetParameter("@PageNumber", pageParam.PageNumber));

            for (int i = 0; i < P.Length; i++)
            {
                cmd.Parameters.Add(GetParameter(P[i].ParameterName, P[i].Value));
            }

            List<MainDL> lstMainDL = new List<MainDL>();
            MainDL objMainDL;
            DbDataReader rdrMain;
            rdrMain = ExecuteDataReader(cmd);

            using (rdrMain)
            {
                if (rdrMain != null && rdrMain.HasRows)
                {
                    while (rdrMain.Read())
                    {
                        objMainDL = new MainDL();
                        MapReaderToMainDL(rdrMain, objMainDL);
                        lstMainDL.Add(objMainDL);
                    }
                }
            }
            rdrMain.Dispose();
            return lstMainDL;
        }

        public bool MapReaderToMainDL(DbDataReader rdrMainDL, MenassahApi.DL.MainDL oClients)
        {
            if (rdrMainDL["ID"] != DBNull.Value)
                oClients.ID = Convert.ToString(rdrMainDL["ID"]);

            if (rdrMainDL["EnglishName"] != DBNull.Value)
                oClients.EnglishName = Convert.ToString(rdrMainDL["EnglishName"]);

            if (rdrMainDL["ArabicName"] != DBNull.Value)
                oClients.ArabicName = Convert.ToString(rdrMainDL["ArabicName"]);

            return true;
        }



    }
}
