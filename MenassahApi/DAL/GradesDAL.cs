using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class GradesDAL : SharedCode, IGradesRepo
    {
        public GradesDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(GradesDL GradesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Grades_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@GradeName", GradesDL.GradeName));
            cmd.Parameters.Add(GetParameter("@StageID", GradesDL.StageId));



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
        public string Update(GradesDL GradesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Grades_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@GradeID", GradesDL.GradeId));
            cmd.Parameters.Add(GetParameter("@GradeName", GradesDL.GradeName));
            cmd.Parameters.Add(GetParameter("@StageID", GradesDL.StageId));


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
        public string Delete(int GradeID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Grades_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@GradeID", GradeID));


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

            SqlCommand cmd = GetCommand("spx_tbl_Grades_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int GradeID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Grades_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@GradeID", GradeID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

