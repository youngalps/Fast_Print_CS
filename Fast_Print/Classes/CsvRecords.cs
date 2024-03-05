using CsvHelper.Configuration.Attributes;

namespace Fast_Print.Classes;

public class CsvRecords : ParserMethods
{
    private readonly ParserMethods parser = new();

    private string _partNumber;

    // name of column
    [Name("Part No")]
    public string PartNumber
    {
        // Check if the part number is a string
        get => parser.StringChecker(_partNumber);
        set => _partNumber = value;
    }


    // name of Shop Order
    [Name("SO No")] public string ShopOrder { get; set; }
}