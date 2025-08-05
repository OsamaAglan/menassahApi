using System.Data.Common;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace Menassah.Repo
{
    public interface IBaseDAL
    {
        SqlCommand GetCommand(string commandText, CommandType commandType);
        SqlParameter GetParameter(string parameter, object value);
        int ExecuteNonQuery(SqlCommand Sqlcomm);
        object ExecuteScalar(SqlCommand cmd);
        DbDataReader ExecuteDataReader(SqlCommand cmd);

        DataSet GetDataSet(SqlCommand cmd);
    }
}
