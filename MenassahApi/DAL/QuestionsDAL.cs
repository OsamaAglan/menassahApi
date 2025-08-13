using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class QuestionsDAL : SharedCode, IQuestionsRepo
    {
        public QuestionsDAL(IConfiguration configuration) : base(configuration)
        {
        }


        private DataTable ConvertToDataTable(List<QuestionOptionsDL> options)
        {
            var table = new DataTable();
            // نفس ترتيب وأسماء الأعمدة في SQL Type
            table.Columns.Add("OptionText", typeof(string));
            table.Columns.Add("IsCorrect", typeof(bool));

            if (options != null)
            {
                foreach (var opt in options)
                {
                    table.Rows.Add(opt.OptionText, opt.IsCorrect);
                }
            }

            return table;
        }

        public string Insert(QuestionsDL QuestionsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Questions_Insert", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@TeacherGroupID", QuestionsDL.TeacherGroupID));
            cmd.Parameters.Add(GetParameter("@QuestionText", QuestionsDL.QuestionText));
            cmd.Parameters.Add(GetParameter("@Score", QuestionsDL.Score));
            cmd.Parameters.Add(GetParameter("@QuestionTypeID", QuestionsDL.QuestionTypeID));
            cmd.Parameters.Add(GetParameter("@AskedIn", QuestionsDL.AskedIn));

            // تحويل الـ List ل DataTable وتمريره كـ Structured Parameter
            var dtOptions = ConvertToDataTable(QuestionsDL.Options);
            var pOptions = cmd.Parameters.AddWithValue("@Options", dtOptions);
            pOptions.SqlDbType = SqlDbType.Structured;
            pOptions.TypeName = "QuestionOptionsType"; // لازم يكون نفس اسم الـ Type في SQL

            SqlParameter pID = GetParameterReturnValue("@NewID");
            cmd.Parameters.Add(pID);

            ExecuteNonQuery(cmd);

            return (pID?.Value == null) ? "0" : Convert.ToString(pID.Value);
        }


        //public string Insert(QuestionsDL QuestionsDL)
        //{
        //    SqlCommand cmd = GetCommand("spx_tbl_Questions_Insert", CommandType.StoredProcedure);

        //    cmd.Parameters.Add(GetParameter("@TeacherGroupID", QuestionsDL.TeacherGroupID));
        //    cmd.Parameters.Add(GetParameter("@QuestionText", QuestionsDL.QuestionText));
        //    cmd.Parameters.Add(GetParameter("@Score", QuestionsDL.Score));
        //    cmd.Parameters.Add(GetParameter("@QuestionTypeID", QuestionsDL.QuestionTypeID));
        //    cmd.Parameters.Add(GetParameter("@AskedIn", QuestionsDL.AskedIn));
        //    cmd.Parameters.Add(GetParameter("@Options", QuestionsDL.Options));


        //    SqlParameter pID = GetParameterReturnValue("@NewID");
        //    cmd.Parameters.Add(pID);

        //    ExecuteNonQuery(cmd);
        //    string ID;
        //    if (pID == null || pID.Value == null)
        //    {
        //        ID = "0";
        //    }
        //    else
        //    {
        //        ID = Convert.ToString(pID.Value);
        //    }

        //    return ID;
        //}
        public string Update(QuestionsDL QuestionsDL)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Questions_Update", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@QuestionID", QuestionsDL.QuestionID));

            cmd.Parameters.Add(GetParameter("@TeacherGroupID", QuestionsDL.TeacherGroupID));
            cmd.Parameters.Add(GetParameter("@QuestionText", QuestionsDL.QuestionText));
            cmd.Parameters.Add(GetParameter("@Score", QuestionsDL.Score));
            cmd.Parameters.Add(GetParameter("@QuestionTypeID", QuestionsDL.QuestionTypeID));
            cmd.Parameters.Add(GetParameter("@AskedIn", QuestionsDL.AskedIn));

            // تحويل الـ List ل DataTable وتمريره كـ Structured Parameter
            var dtOptions = ConvertToDataTable(QuestionsDL.Options);
            var pOptions = cmd.Parameters.AddWithValue("@Options", dtOptions);
            pOptions.SqlDbType = SqlDbType.Structured;
            pOptions.TypeName = "QuestionOptionsType"; // لازم يكون نفس اسم الـ Type في SQL

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
        public string Delete(int QuestionID)
        {
            SqlCommand cmd = GetCommand("spx_tbl_Questions_Delete", CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParameter("@QuestionID", QuestionID));


            SqlParameter pID = GetParameterReturnValue("@QuestionID");
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

            SqlCommand cmd = GetCommand("spx_tbl_Questions_GetAll", CommandType.StoredProcedure);
            var ds = GetDataSet(cmd, 999);
            return ds;

        }
        public DataSet GetByID(int QuestionID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Questions_GetByID", CommandType.StoredProcedure);

    cmd.Parameters.Add(GetParameter("@QuestionID", QuestionID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}


        public DataSet GetByGroupID(int GroupID)
{

    SqlCommand cmd = GetCommand("spx_tbl_Questions_GetByGroupID", CommandType.StoredProcedure);

    cmd.Parameters.Add(GetParameter("@TeacherGroupID", GroupID));
    var ds = GetDataSet(cmd, 999);
    return ds;

}



    }
}

