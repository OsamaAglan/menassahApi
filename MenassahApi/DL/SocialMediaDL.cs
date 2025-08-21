namespace MenassahApi.DL
{
    public class SocialMediaDL
    {
        public int SocialID { get; set; }
        public int TeacherID { get; set; }
        public string PlatformName { get; set; } = string.Empty;
        public string ProfileLink { get; set; } = string.Empty;
        public bool IsMain { get; set; } = true;

    }

}
