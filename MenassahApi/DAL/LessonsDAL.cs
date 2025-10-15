using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class LessonsDAL : SharedCode, ILessonsRepo
    {
        public LessonsDAL(IConfiguration configuration) : base(configuration)
        {
        }


        private DataTable ConvertToDataTable(List<LessonDtlsDL> Dtls)
        {
            var table = new DataTable();
            // نفس ترتيب وأسماء الأعمدة في SQL Type
                        table.Columns.Add("ContentTitle", typeof(string));
            table.Columns.Add("ContentDescription", typeof(string));
            table.Columns.Add("ContentURL", typeof(string));
table.Columns.Add("ContentTypeID", typeof(int));
            table.Columns.Add("ContentOrder", typeof(int));


            if (Dtls != null)
            {
                foreach (var opt in Dtls)
                {
                    table.Rows.Add( opt.ContentTitle,opt.ContentDescription,  opt.ContentURL,opt.ContentTypeID, opt.ContentOrder);
                }
            }




            return table;
        }

        public string Insert(LessonsDL LessonsDL)
        {
            SqlCommand cmd = GetCommand("spx_Lessons_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherGroupID", LessonsDL.TeacherGroupID));
            cmd.Parameters.Add(GetParameter("@Title", LessonsDL.Title));
            cmd.Parameters.Add(GetParameter("@IsFree", LessonsDL.IsFree));
            cmd.Parameters.Add(GetParameter("@LessonOrder", LessonsDL.LessonOrder));
            cmd.Parameters.Add(GetParameter("@LessonImagePath", LessonsDL.LessonImagePath));

            // تحويل الـ List ل DataTable وتمريره كـ Structured Parameter
            var dtDtls = ConvertToDataTable(LessonsDL.LessonDtls);
            var pDtls = cmd.Parameters.AddWithValue("@LessonDtls", dtDtls);
            pDtls.SqlDbType = SqlDbType.Structured;
            pDtls.TypeName = "LessonDetailsType"; // لازم يكون نفس اسم الـ Type في SQL

            SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);

            return (pID?.Value == null) ? "0" : Convert.ToString(pID.Value);
        }



        public string Update(LessonsDL LessonsDL)
        {
            SqlCommand cmd = GetCommand("spx_Lessons_Update", CommandType.StoredProcedure);
            cmd.Parameters.Add(GetParameter("@LessonID", LessonsDL.LessonID));
            cmd.Parameters.Add(GetParameter("@TeacherGroupID", LessonsDL.TeacherGroupID));
            cmd.Parameters.Add(GetParameter("@Title", LessonsDL.Title));
            cmd.Parameters.Add(GetParameter("@IsFree", LessonsDL.IsFree));
            cmd.Parameters.Add(GetParameter("@LessonOrder", LessonsDL.LessonOrder));
            cmd.Parameters.Add(GetParameter("@LessonImagePath", LessonsDL.LessonImagePath));


            // تحويل الـ List ل DataTable وتمريره كـ Structured Parameter
            var dtDtls = ConvertToDataTable(LessonsDL.LessonDtls);
            var pDtls = cmd.Parameters.AddWithValue("@LessonDtls", dtDtls);
            pDtls.SqlDbType = SqlDbType.Structured;
            pDtls.TypeName = "LessonDetailsType"; // لازم يكون نفس اسم الـ Type في SQL

            SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);

            return (pID?.Value == null) ? "0" : Convert.ToString(pID.Value);



            //string ID;
            //if (pID == null || pID.Value == null)
            //{
            //    ID = "0";
            //}
            //else
            //{
            //    ID = Convert.ToString(pID.Value);
            //}

            //return ID;


        }
        public string Delete(int LessonID)
        {
            SqlCommand cmd = GetCommand("spx_Lessons_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@LessonID", LessonID));


            SqlParameter pID = GetParameterReturnValue("@LessonID");
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

        public DataSet GetByID(int LessonID)
        {

            SqlCommand cmd = GetCommand("spx_Lessons_GetByID", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@LessonID", LessonID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet ContentTypeGetAll()
        {

            SqlCommand cmd = GetCommand("spx_LessoneContentType_GetAll", CommandType.StoredProcedure);

            var ds = GetDataSet(cmd, 999);
            return ds;

        }


        public DataSet GetByGroupID(int GroupID)
        {

            SqlCommand cmd = GetCommand("spx_tbl_Lessons_GetByGroupID", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherGroupID", GroupID));
            var ds = GetDataSet(cmd, 999);
            return ds;

        }



    }
}

