using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ITeacherGroupsRepo

    {
        public string Insert(TeacherGroupsDL IM2OrdersDL);
        public string Update(TeacherGroupsDL IM2OrdersDL);
        public string Delete(int TeacherGroupID);
        public DataSet GetAll();
        public DataSet GetByID(int TeacherGroupID);
        public DataSet GetByTeacherID(int TeacherID);



    }
}
