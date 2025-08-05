using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ITeacherGradesRepo

    {
        public string Insert(TeacherGradesDL IM2OrdersDL);
        public string Update(TeacherGradesDL IM2OrdersDL);
        public string Delete(int TeacherGradeID);
        public DataSet GetAll();
        public DataSet GetByID(int TeacherGradeID);


    }
}
