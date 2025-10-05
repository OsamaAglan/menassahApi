using MenassahApi.DL;

namespace MenassahApi.DL
{
    public class LessonsDL
    {
        public int  LessonID { get; set; }
        public int TeacherGroupID  { get; set; }
        public int  LessonOrder { get; set; }
        public string Title  { get; set; }
        public DateTime UploadDate  { get; set; }
        public Boolean  IsFree { get; set; }


       public List<LessonDtlsDL> LessonDtls { get; set; }
 }

    }

 
    