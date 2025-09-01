using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class TeachersDAL : SharedCode, ITeachersRepo
    {
        public TeachersDAL(IConfiguration configuration) : base(configuration)
        {
        }

        public string Insert(TeachersDL TeachersDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Teachers_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherName", TeachersDL.TeacherName));
            cmd.Parameters.Add(GetParameter("@Notes", TeachersDL.Notes));
            cmd.Parameters.Add(GetParameter("@PhoneNumber", TeachersDL.PhoneNumber));
            cmd.Parameters.Add(GetParameter("@Email", TeachersDL.Email));
            cmd.Parameters.Add(GetParameter("@CityID", TeachersDL.CityID));
            cmd.Parameters.Add(GetParameter("@Address", TeachersDL.Address));
            cmd.Parameters.Add(GetParameter("@Gender", TeachersDL.Gender));
            cmd.Parameters.Add(GetParameter("@Bio", TeachersDL.Bio));
            //cmd.Parameters.Add(GetParameter("@profilePicture", TeachersDL.profilePicture));



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
        public string Update(TeachersDL TeachersDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Teachers_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherID", TeachersDL.TeacherID));
            cmd.Parameters.Add(GetParameter("@TeacherName", TeachersDL.TeacherName));
            cmd.Parameters.Add(GetParameter("@Notes", TeachersDL.Notes));
            cmd.Parameters.Add(GetParameter("@PhoneNumber", TeachersDL.PhoneNumber));
            cmd.Parameters.Add(GetParameter("@Email", TeachersDL.Email));
            cmd.Parameters.Add(GetParameter("@CityID", TeachersDL.CityID));
            cmd.Parameters.Add(GetParameter("@Address", TeachersDL.Address));
            cmd.Parameters.Add(GetParameter("@Gender", TeachersDL.Gender));
            cmd.Parameters.Add(GetParameter("@Bio", TeachersDL.Bio));
            //cmd.Parameters.Add(GetParameter("@profilePicture", TeachersDL.profilePicture));


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
        public string Delete(int TeacherID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Teachers_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));


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

            SqlCommand cmd = GetCommand("spx_tbl_Teachers_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }

        public DataSet GetDashboard()
        {

            SqlCommand cmd = GetCommand("spx_Teacher_Dashboard", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }

        public DataSet GetByID(int TeacherID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Teachers_GetByID", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}
        public DataSet GetGroupGrowth(int TeacherID)
{

    SqlCommand cmd = GetCommand("spx_Teacher_GroupGrowth", CommandType.StoredProcedure);
    cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}

    }
}

