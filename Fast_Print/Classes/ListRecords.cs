using System.Drawing;
using System.Windows.Forms;
using CsvHelper.Configuration.Attributes;


// Goal: Create a class that will be used to create a list of records for the DataGridView
namespace Fast_Print.Classes;

internal class ListRecords
{
    private DataGridViewComboBoxColumn _revisions = new();

    // name of column
    [Name("Path")] public string Path { get; set; }

    // name of column
    [Name("Part No")] public string PartNumber { get; set; }

    // name of column
    [Name("Revisions")]
    public DataGridViewComboBoxColumn Revisions
    {
        get
        {
            _revisions.HeaderText = @"Revisions";
            _revisions.Name = "Revisions";
            return _revisions;
        }
        set => _revisions = value;
    }

    // name of column
    [Name("PrintStatus")] public Image PrintStatus { get; set; }
}