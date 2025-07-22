namespace MenassahApi.DL
{
    public class PageParam
    {
        public PageParam()
        {
            PageSize = 0;
            PageNumber = 0;
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
