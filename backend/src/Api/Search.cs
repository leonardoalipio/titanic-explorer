using TitanicExplorer.Business;

namespace TitanicExplorer.Api
{
    public class Search
    {
        public int? Survived { get; set; }
        public int? Class { get; set; }
        public int? Sex { get; set; }
        public ESex? ESex { get; set; }
        public int? Age { get; set; }
        public decimal? Fare { get; set; }
    }
}