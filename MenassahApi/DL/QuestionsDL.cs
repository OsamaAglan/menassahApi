namespace MenassahApi.DL
{
    public class QuestionsDL
    {
        public int QuestionID { get; set; }
        public int TeacherGroupID { get; set; }
        public string QuestionText { get; set; }
        public int Score { get; set; }
        public int QuestionTypeID { get; set; }
        public string AskedIn { get; set; }
       public List<QuestionOptionsDL> Options { get; set; }
 }

    }
