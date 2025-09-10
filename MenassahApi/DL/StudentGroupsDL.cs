namespace MenassahApi.DL
{
    public class StudentGroupsDL
    {
        public int studentGroupId { get; set; }
        public int studentId { get; set; }
        public int teacherGroupId { get; set; }
        public Boolean isApproved { get; set; }
   

    }
    public class StudentStatusUpdate
    {
        public int StudentGroupID { get; set; }
        public int Status { get; set; }
    }



}
