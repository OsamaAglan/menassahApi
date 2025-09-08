using MenassahApi.DL;
using MenassahApi.Repo;
using System.Data;
using System.Data.SqlClient;

namespace Menassah.Shared
{
    public class StudentAnswersDAL : SharedCode, IStudentAnswersRepo
    {
        public StudentAnswersDAL(IConfiguration configuration) : base(configuration) { }

        // تحويل القائمة إلى DataTable مطابق لتعريف TVP
        private DataTable ConvertAnswersToDataTable(List<StudentAnswerDL> answers)
        {
            var dt = new DataTable();
            dt.Columns.Add("QuestionID", typeof(int));
            dt.Columns.Add("AnswerText", typeof(string));
            dt.Columns.Add("OptionIDs", typeof(string));

            if (answers != null)
            {
                foreach (var a in answers)
                {
                    dt.Rows.Add(a.QuestionID, a.AnswerText ?? (object)DBNull.Value, a.OptionIDs ?? (object)DBNull.Value);
                }
            }

            return dt;
        }

        public string InsertSingle(StudentAnswerDL answer)
        {
            // خيار: يمكنك إما استدعاء نفس الإجراء batch مع صف واحد، أو عمل stored proc منفصل.
            var list = new List<StudentAnswerDL> { answer };
            var res = InsertBatch(0, list); // هنا تحتاج StudentID فعلي، لكن controller قد يستخدم InsertBatch دائماً
            return res;
        }

        public string InsertBatch(int studentId, List<StudentAnswerDL> answers)
        {
            try
            {
                using (var cmd = GetCommand("spx_StudentAnswers_InsertBatch_TVP", CommandType.StoredProcedure))
                {
                    cmd.Parameters.Add(GetParameter("@StudentID", studentId));

                    var dt = ConvertAnswersToDataTable(answers);
                    var p = cmd.Parameters.AddWithValue("@Answers", dt);
                    p.SqlDbType = SqlDbType.Structured;
                    p.TypeName = "dbo.StudentAnswerTableType";

                    ExecuteNonQuery(cmd);
                    return "1";
                }
            }
            catch (Exception ex)
            {
                // سجّل أو أعِد الخطأ كما تفضل
                throw;
            }
        }
    }
}
