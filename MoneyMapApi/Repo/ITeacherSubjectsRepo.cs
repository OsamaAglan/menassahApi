using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ITeacherSubjectsRepo

    {
        public string Insert(TeacherSubjectsDL IM2OrdersDL);
        public string Update(TeacherSubjectsDL IM2OrdersDL);
        public string Delete(int TeacherSubjectID);
        public DataSet GetAll();
        public DataSet GetByID(int TeacherSubjectID);
        public DataSet GetByTeacherID(int TeacherID);



    }
}
