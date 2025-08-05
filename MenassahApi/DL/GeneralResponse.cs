namespace MenassahApi.DL
{
    public partial class GeneralResponse
    {
        public GeneralResponse()
        {
            ID = "0";
            Message = "";
        }
        public string ID { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public object Summary { get; set; }

    }

}
