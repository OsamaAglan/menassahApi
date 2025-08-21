using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MenassahApi.DL
{
    public class DocumentsDL
    {
        public int documentId { get; set; }
        public int beneficiaryId { get; set; }
        public int documentTypeId { get; set; }

        [BindNever]
        public string? filePath { get; set; }
        public IFormFile File { get; set; }



    }

}
