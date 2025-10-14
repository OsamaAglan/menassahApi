using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MenassahApi.DL
{
    public class UploadsDL
    {
        public int  UploadId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public int LessonID { get; set; }
        public string  UploadType { get; set; }
        public Boolean  IsPublic { get; set; }

        [BindNever]
        public string? FilePath { get; set; }
        public IFormFile File { get; set; }



    }

}
