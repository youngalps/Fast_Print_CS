using CsvHelper.Configuration.Attributes;

namespace Fast_Print.Classes;

public class CsvRecords: ParserMethods
{

    ParserMethods parser = new ParserMethods();
    private string partNumber;


   



    // name of column
    [Name("Part No")] 
    public string PartNumber
    {
        get => parser.StringChecker(partNumber);
        set => partNumber = value;
    }


    // name of Shop Order
    [Name("SO No")] public string ShopOrder { get; set; }


    [Name("Planned Start Date")]
    public string PlannedStartDate { get; set; }





}