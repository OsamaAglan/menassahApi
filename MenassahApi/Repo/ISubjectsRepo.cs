using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface ISubjectsRepo

    {
        public string Insert(SubjectsDL SubjectsDL);
        public string Update(SubjectsDL SubjectsDL);
        public string Delete(int SubjectID);
        public DataSet GetAll();
        public DataSet GetByID(int SubjectID);


    }
}
