namespace MenassahApi.DL
{
    public class StudentAnswersBatchRequest
    {
        public int StudentID { get; set; }
        public List<StudentAnswerDL> Answers { get; set; }
    }
}
