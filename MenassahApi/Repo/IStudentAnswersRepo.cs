using MenassahApi.DL;
using System.Collections.Generic;

namespace MenassahApi.Repo
{
    public partial interface IStudentAnswersRepo
    {
        string InsertSingle(StudentAnswerDL answer); // اختياري
        string InsertBatch(int studentId, List<StudentAnswerDL> answers);
    }
}
