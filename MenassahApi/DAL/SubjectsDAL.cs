using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class SubjectsDAL : SharedCode, ISubjectsRepo
    {
        public SubjectsDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(SubjectsDL SubjectsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Subjects_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@SubjectName", SubjectsDL.SubjectName));



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
        public string Update(SubjectsDL SubjectsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Subjects_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@SubjectID", SubjectsDL.SubjectId));
            cmd.Parameters.Add(GetParameter("@SubjectName", SubjectsDL.SubjectName));


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
        public string Delete(int SubjectID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Subjects_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@SubjectID", SubjectID));


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

            SqlCommand cmd = GetCommand("spx_tbl_Subjects_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int SubjectID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Subjects_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@SubjectID", SubjectID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

