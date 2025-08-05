using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class StudentsDAL : SharedCode, IStudentsRepo
    {
        public StudentsDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(StudentsDL StudentsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Students_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@StudentName", StudentsDL.StudentName));
            cmd.Parameters.Add(GetParameter("@GradeLevel", StudentsDL.GradeLevel));
            cmd.Parameters.Add(GetParameter("@GuardianID", StudentsDL.GuardianID));
            cmd.Parameters.Add(GetParameter("@Notes", StudentsDL.Notes));
            cmd.Parameters.Add(GetParameter("@UserID", StudentsDL.UserID));
            cmd.Parameters.Add(GetParameter("@PhoneNumber", StudentsDL.PhoneNumber));
            cmd.Parameters.Add(GetParameter("@Email", StudentsDL.Email));
            cmd.Parameters.Add(GetParameter("@CityID", StudentsDL.CityID));
            cmd.Parameters.Add(GetParameter("@Address", StudentsDL.Address));
            cmd.Parameters.Add(GetParameter("@Gender", StudentsDL.Gender));


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
        public string Update(StudentsDL StudentsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Students_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@StudentID", StudentsDL.StudentID));
            cmd.Parameters.Add(GetParameter("@StudentName", StudentsDL.StudentName));
            cmd.Parameters.Add(GetParameter("@GradeLevel", StudentsDL.GradeLevel));
            cmd.Parameters.Add(GetParameter("@GuardianID", StudentsDL.GuardianID));
            cmd.Parameters.Add(GetParameter("@Notes", StudentsDL.Notes));
            cmd.Parameters.Add(GetParameter("@UserID", StudentsDL.UserID));
            cmd.Parameters.Add(GetParameter("@PhoneNumber", StudentsDL.PhoneNumber));
            cmd.Parameters.Add(GetParameter("@Email", StudentsDL.Email));
            cmd.Parameters.Add(GetParameter("@CityID", StudentsDL.CityID));
            cmd.Parameters.Add(GetParameter("@Address", StudentsDL.Address));
            cmd.Parameters.Add(GetParameter("@Gender", StudentsDL.Gender));


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
        public string Delete(int StudentID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Students_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@StudentID", StudentID));


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

            SqlCommand cmd = GetCommand("spx_tbl_Students_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int StudentID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Students_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@StudentID", StudentID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

