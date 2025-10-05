using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ILessonsRepo

    {
        public string Insert(LessonsDL LessonsDL);
        public string Update(LessonsDL IM2OrdLessonsDLersDL);
        public string Delete(int LessonID);
        public DataSet GetByID(int LessonID);
        public DataSet GetByGroupID(int LessonID);


    }
}
