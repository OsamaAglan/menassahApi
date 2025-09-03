using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class StudentGroupsDAL : SharedCode, IStudentGroupsRepo
    {
        public StudentGroupsDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(StudentGroupsDL StudentGroupsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@StudentID", StudentGroupsDL.studentId));
            cmd.Parameters.Add(GetParameter("@TeacherGroupID", StudentGroupsDL.teacherGroupId));
            cmd.Parameters.Add(GetParameter("@IsApproved", StudentGroupsDL.isApproved));



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
        public string Update(StudentGroupsDL StudentGroupsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@RoleID", StudentGroupsDL.studentGroupId));
            cmd.Parameters.Add(GetParameter("@StudentID", StudentGroupsDL.studentId));
            cmd.Parameters.Add(GetParameter("@TeacherGroupID", StudentGroupsDL.teacherGroupId));
            cmd.Parameters.Add(GetParameter("@IsApproved", StudentGroupsDL.isApproved));


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
        public string Delete(int StudentGroupID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@StudentGroupID", StudentGroupID));


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

            SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int StudentGroupID)
{

    SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@StudentGroupID", StudentGroupID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetBystudentID(int StudentID, int Term)
{

    SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_GetByStudentID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@StudentID", StudentID));
    cmd.Parameters.Add(GetParameter("@Term", Term));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetByGradeID(int GradeID, int Term)
{

    SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_GetByGradeID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@GradeID", GradeID));
    cmd.Parameters.Add(GetParameter("@Term", Term));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetBySubjectID(int SubjectID, int Term)
{

    SqlCommand cmd = GetCommand("spx_tbl_StudentGroups_GetBySubjectID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@SubjectID", SubjectID));
    cmd.Parameters.Add(GetParameter("@Term", Term));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

