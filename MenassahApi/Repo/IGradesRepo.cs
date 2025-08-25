using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IGradesRepo

    {
        public string Insert(GradesDL GradesDL);
        public string Update(GradesDL GradesDL);
        public string Delete(int GradeID);
        public DataSet GetAll();
        public DataSet GetByID(int GradeID);


    }
}
