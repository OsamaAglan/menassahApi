using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MenassahApi.DL
{
    public class UploadsDL
    {
        public int  uploadId { get; set; }
        public int teacherId { get; set; }
        public int groupId { get; set; }
        public string  uploadType { get; set; }
        public Boolean  isPublic { get; set; }

        [BindNever]
        public string? filePath { get; set; }
        public IFormFile File { get; set; }



    }

}
