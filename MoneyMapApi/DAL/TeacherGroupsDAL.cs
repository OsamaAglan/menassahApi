using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class TeacherGroupsDAL : SharedCode, ITeacherGroupsRepo
    {
        public TeacherGroupsDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(TeacherGroupsDL TeacherGroupsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherGroups_Insert", CommandType.StoredProcedure);

                         cmd.Parameters.Add(GetParameter("@GroupName", TeacherGroupsDL.GroupName));
                         cmd.Parameters.Add(GetParameter("@TeacherID", TeacherGroupsDL.TeacherID));
                         cmd.Parameters.Add(GetParameter("@SubjectID", TeacherGroupsDL.SubjectID));
                         cmd.Parameters.Add(GetParameter("@GradeID", TeacherGroupsDL.GradeID));
                         cmd.Parameters.Add(GetParameter("@Term", TeacherGroupsDL.Term));

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
        public string Update(TeacherGroupsDL TeacherGroupsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherGroups_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherSubjectID", TeacherGroupsDL.TeacherGroupID));
            cmd.Parameters.Add(GetParameter("@GroupName", TeacherGroupsDL.GroupName));
            cmd.Parameters.Add(GetParameter("@TeacherID", TeacherGroupsDL.TeacherID));
            cmd.Parameters.Add(GetParameter("@SubjectID", TeacherGroupsDL.SubjectID));
            cmd.Parameters.Add(GetParameter("@GradeID", TeacherGroupsDL.GradeID));
            cmd.Parameters.Add(GetParameter("@Term", TeacherGroupsDL.Term));


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
        public string Delete(int TeacherGroupID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_TeacherGroups_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherGroupID", TeacherGroupID));


            SqlParameter pID = GetParameterReturnValue("@TeacherGroupID");
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

            SqlCommand cmd = GetCommand("spx_tbl_TeacherGroups_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByTeacherID(int TeacherID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherGroups_GetByTeacherID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetByID(int TeacherGroupID)
{

    SqlCommand cmd = GetCommand("spx_tbl_TeacherGroups_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherGroupID", TeacherGroupID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

