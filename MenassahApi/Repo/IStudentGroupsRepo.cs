using System.Data;
using MenassahApi.DL;

namespace MenassahApi.Repo
{
    public partial interface IStudentGroupsRepo

    {
        public string Insert(StudentGroupsDL StudentGroupsDL);
        public string UpdateStatuses(List<StudentStatusUpdate> updates);
        
        public string Delete(int StudentGroupID);
        public DataSet GetAll();
        public DataSet GetByID(int StudentGroupID);
        public DataSet GetBystudentID(int StudentID, int Term);
        public DataSet GetByGradeID(int GradeID,int Term);
        public DataSet GetBySubjectID(int SubjectID, int Term);


    }
}
