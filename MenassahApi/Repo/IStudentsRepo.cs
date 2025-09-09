using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IStudentsRepo

    {
        public string Insert(StudentsDL IM2OrdersDL);
        public string Update(StudentsDL IM2OrdersDL);
        public string Delete(int StudentID);
        public DataSet GetAll();
        public DataSet GetByID(int StudentID);
        public DataSet GetByTeacherID(int TeacherID);


    }
}
