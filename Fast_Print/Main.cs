using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using Fast_Print.Classes;
using System.Collections.Concurrent;
using System.Text;


namespace Fast_Print;

public partial class Main : Form
{


    #region Objects

    private DirectoryInfo _gedDir;
    private PdfPrinter _pdfPrinter;
    private UIComponets _uiComponets;
    private FileWatcher _fileWatcher;

    #endregion

    #region Main
    public Main()
    {
         
        InitializeComponent();
       
        _fileWatcher= new FileWatcher();
    }
    private void Main_Load_(object sender, EventArgs e)
    {
        _gedDir = new DirectoryInfo(Properties.Settings.Default.SettingDrawingPath);
    }

    

    #endregion
    
    #region Buttons
    private void Btn_SelectFile_Click(object sender, EventArgs e)
    {
        string excelFile = UIComponets.OpenCsvFileBrowserDialog();
        if (excelFile != null)
        {
            LoadCsvFileData(excelFile);
        }

        else
        {
            {
                MessageBox.Show(@"File Not Selected", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //
        }
    }

    private async void Btn_PrintFiles_Click(object sender, EventArgs e)
    {
        if (FileGridView.RowCount > 0)
        {
            var printer = new PdfPrinter(FileGridView);
            await printer.PrintSelectedPdfAsync();
        }

        else
        {
            MessageBox.Show(@"No Files Selected", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearGrid_Btn(object sender, EventArgs e)
    {
        FileGridView.Rows.Clear();
    }

    private void Settings_Click_Btn(object sender, EventArgs e)
    {
        // Open the settings form
        var settings = new Settings(this, _fileWatcher);
        settings.ShowDialog();
    }

    private void Btn_CopyClipBoard(object sender, EventArgs e)
    {
        if (FileGridView.RowCount > 0)
        {
            var sb = new StringBuilder();
            foreach (DataGridViewRow row in FileGridView.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    sb.Append(row.Cells[0].Value.ToString());
                    sb.Append(Environment.NewLine);
                }
            }

            Clipboard.SetText(sb.ToString());
            MessageBox.Show(@"Files Copied to Clipboard", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show(@"No Files Selected", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }

    private void Btn_StopPrinting(object sender, EventArgs e)
    {
        _pdfPrinter.CancelPrinting();
    }

    #endregion
    
    #region FileData
    private async void LoadCsvFileData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        try
        {
            var records = csv.GetRecords<CsvRecords>().ToList();

            // Get all the PDF files in the GED directory
            var fileEntries = _gedDir.EnumerateFiles("*.PDF", SearchOption.AllDirectories).ToList();

            // Process each record in parallel
            var processedRecordsTasks = records.Select((record, index) =>
            {
                var files = fileEntries.Where(file => file.Name.StartsWith(record.PartNumber)).ToList();
                return Task.FromResult((Record: record, Order: index, Files: files));
            });
            // Wait for all the records to be processed
            var processedRecords = await Task.WhenAll(processedRecordsTasks);

            // Using BeginInvoke to batch UI updates and minimize cross-thread operations
            BeginInvoke(new MethodInvoker(() =>
            {
                foreach (var (Record, Order, Files) in processedRecords.OrderBy(x => x.Order))
                {
                    PopulateGridViewForRecord(Record, Files);
                }
            }));
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
    public void fsw_Changed(object sender, FileSystemEventArgs e)
    {
        _gedDir.Refresh();
    }

    public void RefreshFswLocation()
    {

        fsw.EndInit();
        fsw.Path = Properties.Settings.Default.SettingDrawingPath;
        fsw.BeginInit();
        _gedDir = new DirectoryInfo(Properties.Settings.Default.SettingDrawingPath);
    } // Refresh the FileSystemWatcher location
    

    #endregion

    #region DataGrid
    private void PopulateGridViewForRecord(CsvRecords record, List<FileInfo> files)
    {
        var index = FileGridView.Rows.Add();
        PopulateGridView(index, record.PartNumber, files.Select(f => f.Name).ToList(), files.FirstOrDefault(), record.ShopOrder);
    }

    private void PopulateGridView(int index, string partNo, List<string> matchedFiles, FileInfo collectedFiles, string shopOrder)
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


                var revisionComboBoxCell = (DataGridViewComboBoxCell)FileGridView.Rows[index].Cells[3];

                foreach (var file in matchedFiles.Where(file => file != null)) // Loop through the matched files
                    // Get the revision from the file name
                {
                        string rev; // Initialize variable to store the revision
                        rev = file.Substring(file.LastIndexOf('-') + 1); // Get the revision from the file name
                        revisionComboBoxCell.Items.Add(rev);// Add the file to the combo box
                        revisionComboBoxCell.Value = revisionComboBoxCell.Items[^1]; // Set the default value to the last item in the list
                    
                    
                }





            }));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), @"Error Populating Grid View");
        }
    }
    

    #endregion

  

   

    
}

