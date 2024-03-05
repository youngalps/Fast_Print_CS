using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using Fast_Print.Classes;
using Microsoft.VisualBasic.Logging;
using static System.Net.WebRequestMethods;

//TODO: Make the Adobe Acrobat Open before printing the PDF
//TODO: Add a loading bar for the Data viewGrid 
//TODO Delete Non-Conforming rows from the Data viewGrid
//TODO: Tell the user what part number was not found
//TODO: Make a readme file 

namespace Fast_Print;

public partial class Main : Form
{
    private const string ClipboardSeparator = ";";
    private readonly List<string> _clipboardList = [];
    private readonly List<FileInfo> _collectedFiles = [];


    private DirectoryInfo _gedDir;

    public Main()
    {
        InitializeComponent();
    }


    private void Btn_SelectFile_Click(object sender, EventArgs e)
    {
        OpenCsvFileBrowserDialog();
    }


    private void button4_Click(object sender, EventArgs e)
    {
      

    }


    


    private async void Btn_PrintFiles_Click(object sender, EventArgs e)
    {
        if (FileGridView.RowCount > 0)
            await PrintSelectedPdf();
        else
            MessageBox.Show(@"No Files Selected", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ClearGrid_Btn(object sender, EventArgs e)
    {
        FileGridView.Rows.Clear();
    }

    private void Settings_Click_Btn(object sender, EventArgs e)
    {
        // Open the settings form
        var settings = new Settings(this);
        settings.ShowDialog();
    }

    private async Task<Task> PrintSelectedPdf()
    {
        foreach (DataGridViewRow row in FileGridView.Rows)
            if (!string.IsNullOrEmpty(row.Cells[1].Value?.ToString()) && FileGridView.RowCount > 1)
            {
                var index = row.Index;
                var fullPath = row.Cells[0].Value.ToString(); // ex "C:\Users\Public\Documents\PartNumberRevision.pdf"
                if (fullPath == null) continue;
                var folderRelativePath =
                    fullPath[..(fullPath.LastIndexOf('\\') + 1)]; // eX "C:\Users\Public\Documents\"
                var partNumber = row.Cells[1].Value.ToString(); // Get the part number // ex "123456"
                var revision = row.Cells[2].Value.ToString(); // Get the revision //  ex "A"

                if (revision != null && partNumber != null)
                {
                    var pdfFilePath = new StringBuilder().Append(folderRelativePath)
                        .Append(partNumber)
                        .Append(revision)
                        .ToString();
                    var printer = new PdfPrinter(FileGridView);
                    printer.AddToPrintQueue(pdfFilePath,index);
                }
            }

        return Task.CompletedTask;
    }


    private void PopulateGridView(int index, string partNo, List<string> matchedFiles, FileInfo collectedFiles,string shopOrder)
    {

        try
        {
            Invoke(new MethodInvoker(() =>
            {
                // 0 - File Path
                // 1 - SO
                // 2 - Part Number
                // 3 - Revision
                // 4 - Print Status
                // 5 - Index

                // Add the collected files to the grid view
                FileGridView.Rows[index].Cells[0].Value = collectedFiles.FullName;
                FileGridView.Rows[index].Cells[1].Value = shopOrder;
                FileGridView.Rows[index].Cells[2].Value = partNo; // get the part number
                FileGridView.Rows[index].Cells[5].Value = index; 

                var revisionComboBoxCell = (DataGridViewComboBoxCell)FileGridView.Rows[index].Cells[3];

                foreach (var file in matchedFiles.Where(file => file != null))
                    revisionComboBoxCell.Items.Add(file);
                revisionComboBoxCell.Value = revisionComboBoxCell.Items[^1];
            }));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), @"Error Populating Grid View");
        }
    }

    private async void LoadCsvFileData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        try
        {
            var records = csv.GetRecords<CsvRecords>().ToList();
            var processedRecordsTasks = records.Select((record, index) =>
                ProcessRecordAsync(record, index)).ToList();

            var processedRecords = await Task.WhenAll(processedRecordsTasks);

            foreach (var (Record, Order, Files) in processedRecords.OrderBy(x => x.Order))
            {
                Invoke((MethodInvoker)(() => PopulateGridViewForRecord(Record, Files)));
            }
        }
        catch (CsvHelper.HeaderValidationException ex)
        {
            MessageBox.Show($"CSV Header Validation Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error Loading CSV File: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    
    private async Task<(CsvRecords Record, int Order, List<FileInfo> Files)> ProcessRecordAsync(CsvRecords record, int order)
    {
        var files = await Task.Run(() => _gedDir.EnumerateFiles("*.PDF", SearchOption.AllDirectories)
            .Where(file => file.Name.StartsWith(record.PartNumber)).ToList());

        return (record, order, files);
    }

  

    private void OpenCsvFileBrowserDialog()
    {
        using var openFileDialog = new OpenFileDialog();
        openFileDialog.Title = @"Select CSV File";
        openFileDialog.InitialDirectory = Properties.Settings.Default.SettingExcelPath;
        openFileDialog.Filter = @"CSV Files|*.csv";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
            LoadCsvFileData(openFileDialog.FileName);
        else
            MessageBox.Show(@"File Not Selected", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static void CopyToClipboardList(List<string> shopOrder) //, string clipboardContent)
    {
        try
        {
            var clipboardContent = new StringBuilder();
            foreach (var orders in
                     shopOrder) clipboardContent.Append(orders).Append(ClipboardSeparator); // Set Your value here

            // add the shop order to the combined shop order
            Clipboard.SetText(clipboardContent.ToString());
        }
        catch (Exception)
        {
            MessageBox.Show(@"Error Copying to Clipboard", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Main_Load_(object sender, EventArgs e)
    {
        _gedDir = new DirectoryInfo(Properties.Settings.Default.SettingDrawingPath);
    }

    public void RefreshFswLocation()
    {
        fsw.EndInit();
        fsw.Path = Properties.Settings.Default.SettingDrawingPath;
        fsw.BeginInit();
        _gedDir = new DirectoryInfo(Properties.Settings.Default.SettingDrawingPath);
    } // Refresh the FileSystemWatcher location

    private void fsw_Changed(object sender, FileSystemEventArgs e)
    {
        _gedDir.Refresh();
    }

    private void FileGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void PopulateGridViewForRecord(CsvRecords record, List<FileInfo> files)
    {
        var index = FileGridView.Rows.Add();
        PopulateGridView(index, record.PartNumber, files.Select(f => f.Name).ToList(), files.FirstOrDefault(), record.ShopOrder);
    }
}