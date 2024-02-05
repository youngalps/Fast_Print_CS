using CsvHelper.Configuration.Attributes;

namespace Fast_Print
{
    public class CsvRecords
    {
        // name of column
        [Name("Part No")]
        public string PartNumber { get; set; }

        // name of Shop Order
        [Name("SO No")]
        public string ShopOrder { get; set; }
    }
}