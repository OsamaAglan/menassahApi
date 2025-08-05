using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class TeacherSubjectsDAL : SharedCode, ITeacherSubjectsRepo
    {
        public TeacherSubjectsDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(TeacherSubjectsDL TeacherSubjectsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherSubjects_Insert", CommandType.StoredProcedure);

                         cmd.Parameters.Add(GetParameter("@TeacherID", TeacherSubjectsDL.TeacherID));
                         cmd.Parameters.Add(GetParameter("@SubjectID", TeacherSubjectsDL.SubjectID));
                         cmd.Parameters.Add(GetParameter("@GradeID", TeacherSubjectsDL.GradeID));

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
        public string Update(TeacherSubjectsDL TeacherSubjectsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherSubjects_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherSubjectID", TeacherSubjectsDL.TeacherSubjectID));
            cmd.Parameters.Add(GetParameter("@TeacherID", TeacherSubjectsDL.TeacherID));
            cmd.Parameters.Add(GetParameter("@SubjectID", TeacherSubjectsDL.SubjectID));
            cmd.Parameters.Add(GetParameter("@GradeID", TeacherSubjectsDL.GradeID));


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
        public string Delete(int TeacherSubjectID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherSubjects_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherSubjectID", TeacherSubjectID));


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

            SqlCommand cmd = GetCommand("spx_tbl_TeacherSubjects_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByTeacherID(int TeacherID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherSubjects_GetByTeacherID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetByID(int TeacherSubjectID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherSubjects_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherSubjectID", TeacherSubjectID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

