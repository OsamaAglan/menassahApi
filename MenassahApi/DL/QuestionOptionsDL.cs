namespace MenassahApi.DL
{
    public class QuestionOptionsDL
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
