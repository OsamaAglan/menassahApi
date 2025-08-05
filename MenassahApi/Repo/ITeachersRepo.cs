using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ITeachersRepo

    {
        public string Insert(TeachersDL IM2OrdersDL);
        public string Update(TeachersDL IM2OrdersDL);
        public string Delete(int TeacherID);
        public DataSet GetAll();
        public DataSet GetDashboard();
        public DataSet GetByID(int TeacherID);
        public DataSet GetGroupGrowth(int TeacherID);


    }
}
