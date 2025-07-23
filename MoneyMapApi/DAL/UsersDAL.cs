using DocumentFormat.OpenXml.Spreadsheet;
using Menassah;
using MenassahApi.DL;
using Menassah.Shared;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class UsersDAL : SharedCode, IUsersRepo
    {
        public UsersDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(UsersDL UsersDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Users_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@UserName", UsersDL.UserName));
            cmd.Parameters.Add(GetParameter("@PasswordHash", UsersDL.PasswordHash));
            cmd.Parameters.Add(GetParameter("@IsActive", UsersDL.IsActive));


            SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);
            string ID;
            if (pID == null || pID.Value == null)
            {
                ID = "0";
            }
            else
            {
                ID = Convert.ToString(pID.Value);
            }

            return ID;
        }
        public string Update(UsersDL UsersDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Users_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@UserID", UsersDL.UserID));
            cmd.Parameters.Add(GetParameter("@UserName", UsersDL.UserName));
            cmd.Parameters.Add(GetParameter("@PasswordHash", UsersDL.PasswordHash));
            cmd.Parameters.Add(GetParameter("@IsActive", UsersDL.IsActive));

            SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);
            string ID;
            if (pID == null || pID.Value == null)
            {
                ID = "0";
            }
            else
            {
                ID = Convert.ToString(pID.Value);
            }

            return ID;
        }
        public string Delete(int UserID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Users_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@UserID", UserID));


            SqlParameter pID = GetParameterReturnValue("@BindHdrID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);
            string ID;
            if (pID == null || pID.Value == null)
            {
                ID = "0";
            }
            else
            {
                ID = Convert.ToString(pID.Value);
            }

            return ID;
        }
        public DataSet GetAll()
        {

            SqlCommand cmd = GetCommand("spx_tbl_Users_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet Login(UsersDL UsersDL)
{

    SqlCommand cmd = GetCommand("spx_tbl_Users_Login", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@UserName", UsersDL.UserName));
            cmd.Parameters.Add(GetParameter("@PasswordHash", UsersDL.PasswordHash));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetByID(int UserID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Users_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@UserID", UserID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

