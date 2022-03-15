namespace Vibbraneo.API.Models
{
    public class ReportsModel
    {
        public string TotalValue { get; set; }
    }

    public class TotalValueByMonth : ReportsModel
    {
        public string CodMonth { get; set; }
        public string DescMonth { get; set; }
        
    }

    public class TotalValueByCategory : ReportsModel
    {
        public string Category { get; set; }
    }
}
