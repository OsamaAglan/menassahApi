using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class RolesDAL : SharedCode, IRolesRepo
    {
        public RolesDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(RolesDL RolesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Roles_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@RoleName", RolesDL.RoleName));



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
        public string Update(RolesDL RolesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Roles_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@RoleID", RolesDL.RoleID));
            cmd.Parameters.Add(GetParameter("@RoleName", RolesDL.RoleName));


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
        public string Delete(int RoleID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Roles_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@RoleID", RoleID));


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

            SqlCommand cmd = GetCommand("spx_tbl_Roles_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int RoleID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Roles_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@RoleID", RoleID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

