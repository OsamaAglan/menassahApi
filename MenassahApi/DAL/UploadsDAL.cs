using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class UploadsDAL : SharedCode, IUploadsRepo
    {
        public UploadsDAL(IConfiguration configuration) : base(configuration)
        {
        }
        public string Insert(UploadsDL uploadsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Uploads_Insert", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@teacherId", uploadsDL.teacherId));
            cmd.Parameters.Add(GetParameter("@groupId", uploadsDL.groupId));
            cmd.Parameters.Add(GetParameter("@uploadType", uploadsDL.uploadType));
            cmd.Parameters.Add(GetParameter("@filePath", uploadsDL.filePath));
            cmd.Parameters.Add(GetParameter("@isPublic", uploadsDL.isPublic));

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
        public string Update(UploadsDL uploadsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Uploads_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@uploadId", uploadsDL.uploadId));
            cmd.Parameters.Add(GetParameter("@teacherId", uploadsDL.teacherId));
            cmd.Parameters.Add(GetParameter("@groupId", uploadsDL.groupId));
            cmd.Parameters.Add(GetParameter("@uploadType", uploadsDL.uploadType));
            cmd.Parameters.Add(GetParameter("@filePath", uploadsDL.filePath));



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
        public string Delete(int UploadID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Uploads_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@uploadId", UploadID));


            SqlParameter pID = GetParameterReturnValue("@UploadID");
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

            SqlCommand cmd = GetCommand("spx_tbl_Uploads_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int UploadID)
        {

            SqlCommand cmd = GetCommand("spx_tbl_Uploads_GetByID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@uploadId", UploadID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByTeacherID(int TeacherID)
        {

            SqlCommand cmd = GetCommand("spx_tbl_Uploads_GetByTeacherID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@TeacherID", TeacherID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }

        public DataSet GetByGroupID(int GroupID)
        {

            SqlCommand cmd = GetCommand("spx_tbl_Uploads_GetByGroupID", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@GroupID", GroupID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }


    }
}

