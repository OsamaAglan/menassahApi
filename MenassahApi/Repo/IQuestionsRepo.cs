using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IQuestionsRepo

    {
        public string Insert(QuestionsDL IM2OrdersDL);
        public string Update(QuestionsDL IM2OrdersDL);
        public string Delete(int QuestionID);
        public DataSet GetAll();
        public DataSet GetByID(int QuestionID);
        public DataSet GetByGroupID(int GroupID);


    }
}
