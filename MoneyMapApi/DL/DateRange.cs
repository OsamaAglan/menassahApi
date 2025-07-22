namespace MenassahApi.DL
{
    public partial class DateRange
    {
        public DateRange()
        {
            Date1 = DateTime.Now;
            Date2 = DateTime.Now;
        }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public string ClientId { get; set; }

    }
}
