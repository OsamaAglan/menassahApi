using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class TeacherGradesDAL : SharedCode, ITeacherGradesRepo
    {
        public TeacherGradesDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(TeacherGradesDL TeacherGradesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherGrades_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherID", TeacherGradesDL.TeacherID));
            cmd.Parameters.Add(GetParameter("@GradeID", TeacherGradesDL.GradeID));

          


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
        public string Update(TeacherGradesDL TeacherGradesDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherGrades_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherGradeID", TeacherGradesDL.TeacherGradeID));
            cmd.Parameters.Add(GetParameter("@TeacherID", TeacherGradesDL.TeacherID));
            cmd.Parameters.Add(GetParameter("@GradeID", TeacherGradesDL.GradeID));


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
        public string Delete(int TeacherGradeID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherGrades_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherGradeID", TeacherGradeID));


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

            SqlCommand cmd = GetCommand("spx_tbl_TeacherGrades_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int TeacherGradeID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherGrades_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherGradeID", TeacherGradeID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

