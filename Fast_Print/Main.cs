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

    //private DataGridView FileGridView { get; set; }

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


    private void PopulateGridView(int index, string partNo, List<string> matchedFiles, FileInfo collectedFiles)
    {

        try
        {
            Invoke(new MethodInvoker(() =>
            {
                // Add the collected files to the grid view
                FileGridView.Rows[index].Cells[0].Value = collectedFiles.FullName;
                FileGridView.Rows[index].Cells[1].Value = partNo;
                FileGridView.Rows[index].Cells[4].Value = index;

                var revisionComboBoxCell = (DataGridViewComboBoxCell)FileGridView.Rows[index].Cells[2];

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

    private void LoadCsvFileData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        try
        {
            var records = csv.GetRecords<CsvRecords>();

            foreach (var record in records)
            {
                _clipboardList.Add(record.ShopOrder);

                var threads = new Thread(Start);
                threads.SetApartmentState(ApartmentState.STA); // Set the thread to STA
                threads.Start();


                // Modified to collect threads for each record processing
                async void Start() => await CollectRecord(record);
            }

            CopyToClipboardList(_clipboardList);
        }
        catch (Exception)
        {
            MessageBox.Show(@"Error Loading CSV File", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        MessageBox.Show(@"Shop Order's Copied", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //Activate();
    }

    private async Task CollectRecord(CsvRecords record)
    {
        try
        {
            var files = await Task.Run(() =>
                _gedDir.GetFiles($"{record.PartNumber}*.PDF", SearchOption.AllDirectories));
            var matchedFiles = new List<string>(); // List of matched files


            foreach (var file in files)
            {
                _collectedFiles.Add(file);
                var partNumberIndex = file.Name.IndexOf(record.PartNumber, StringComparison.OrdinalIgnoreCase);
                if (partNumberIndex != -1)
                    matchedFiles.Add(file.Name[(partNumberIndex + record.PartNumber.Length)..]);
            }

            int index;

            Invoke(new MethodInvoker(() =>
            {
                index = FileGridView.Rows.Add();
                PopulateGridView(index, record.PartNumber, matchedFiles, files.Length > 0 ? files[0] : null);
            }));
        }
        catch (Exception ex)
        {
            Invoke(new MethodInvoker(() => { MessageBox.Show(ex.ToString(), @"Error Collecting Files"); }));
        }
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

 
}