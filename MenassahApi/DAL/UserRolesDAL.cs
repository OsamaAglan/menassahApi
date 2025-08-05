using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class UserRolesDAL : SharedCode, IUserRolesRepo
    {
        public UserRolesDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(UserRolesDL UserRolesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_UserRoles_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@UserId", UserRolesDL.UserId));
            cmd.Parameters.Add(GetParameter("@RoleId", UserRolesDL.RoleId));



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
        public string Update(UserRolesDL UserRolesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_UserRoles_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@UserRoleID", UserRolesDL.UserRoleID));
            cmd.Parameters.Add(GetParameter("@UserId", UserRolesDL.UserId));
            cmd.Parameters.Add(GetParameter("@RoleId", UserRolesDL.RoleId));


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
        public string Delete(int UserRoleID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_UserRoles_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@UserRoleID", UserRoleID));


            SqlParameter pID = GetParameterReturnValue("@UserRoleID");
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

            SqlCommand cmd = GetCommand("spx_tbl_UserRoles_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int UserRoleID)
{

    SqlCommand cmd = GetCommand("spx_tbl_UserRoles_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@UserRoleID", UserRoleID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public List<string> GetUserRoles(int UserID)
        {
            List<string> roles = new List<string>();

            SqlCommand cmd = GetCommand("spx_tbl_UserRoles_GetByUserID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@UserID", UserID));
            DataSet ds = GetDataSet(cmd, 999);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["RoleName"] != DBNull.Value)
                    {
                        roles.Add(row["RoleName"].ToString());
                    }
                }
            }

            return roles;
        }

    }
}

