

using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration.Attributes;






namespace Fast_Print
{


    public partial class Form1 : Form
    {
        // need a way to check if the user has selected a file before printing.
        private bool canPrint = false;




        // TODO: Make this a user input (buttons most likely
        private const string DefaultFolderPath = @"C:\Users\gedmsinv\OneDrive - GED Integrated Solutions\Desktop\DWG";
        private const string ClipboardSeparator = ";";

        public Form1()
        {
            InitializeComponent();


        }





        private void Btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenCsvFileBrowserDialog();
            if (FileGridView.RowCount > 0)
            {
                canPrint = true;
            }
            else
            {
                canPrint = false;
            }

        } // Open CSV File Button

        private void Btn_PrintFiles_Click(object sender, EventArgs e)
        {
            if (canPrint == true)
            {
                PrintSelectedPDF();
            }
            else
            {
                ShowErrorMessageBox("No Files Selected", null);
            }

        }


        private async void PrintSelectedPDF()
        {
            foreach (DataGridViewRow row in FileGridView.Rows)
            {
                //Get the selected part number and revision
                if (!String.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    if (FileGridView.RowCount > 1)
                    {

                        {
                            string folderRelativePath = row.Cells[0].Value.ToString();
                            string partNumber = row.Cells[1].Value.ToString();
                            string revision = row.Cells[2].Value.ToString();

                            string pdfFilePath = DefaultFolderPath + folderRelativePath + partNumber + revision; // TODO Make PathCombine Work


                            PdfPrinter printer = new PdfPrinter();
                            printer.AddToPrintQueue(pdfFilePath);

                            await Task.Delay(1000);
                        }
                    }
            }
        } // Get the users desired PDF and Send it to print


      

      






        public void ValidatePartNumbers(string partNumbers)
        {
            ParserMethods parserMethods = new ParserMethods();
            parserMethods.RemoveTrailingDashAndNumber(partNumbers);

        }
        public void PopulateGridView(int index, string partNumber, string[] SplitPath, string[] filePathArray) // Populate the DataGridView with the file paths and part numbers
        {

            // Populate the DataGridView
            FileGridView.Rows[index].Cells[0].Value = SplitPath[0]; // Set the folder path
            FileGridView.Rows[index].Cells[1].Value = partNumber; // Set the part number
            DataGridViewComboBoxCell revisionComboBoxCell = (DataGridViewComboBoxCell)FileGridView.Rows[index].Cells[2]; // Set the combobox cell

            for (Int32 i = 0; i < filePathArray.Length; i++)
            {
                revisionComboBoxCell.Items.Add(filePathArray[i]); // Add the file paths to the combobox
                System.Diagnostics.Debug.WriteLine(filePathArray[i], "Printed");
                revisionComboBoxCell.Value = revisionComboBoxCell.Items[0]; // Set the default value to the first item in the combobox

            }



        }

        private void LoadCsvFileData(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            bool DataFound = false;
            string clipboardContent = "";
            var records = csv.GetRecords<CsvRecords>();
            ;

            foreach (var record in records)
            {
                if (records != null)
                {
                    int currentIndex = FileGridView.Rows.Add();
                    ValidatePartNumbers(record.PartNumber);

                    String[] fileNames = Directory.GetFiles(DefaultFolderPath, record.PartNumber + "*.*", SearchOption.AllDirectories);
                    String[] PathComponents = fileNames[0].Split(DefaultFolderPath); // Split the file path into the folder path and the part number
                    String[] filePathArray = FindAllFilePaths(record.PartNumber); // Find all file paths with the part number
                    PathComponents = PathComponents[1].Split(record.PartNumber); // Split the part number from the file path
                    PopulateGridView(currentIndex, record.PartNumber, PathComponents, filePathArray);
                    CopyToClipboard(record.ShopOrder, ref clipboardContent);

                }
            }

            ShowSuccessMessageBox("Shop Orders Copied to Clipboard");
            this.Activate();
        } // Load CSV File Data and Populate the DataGridView and Clipboard

        private void OpenCsvFileBrowserDialog()
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Select CSV File",
                InitialDirectory = "C:\\Users\\gedmsinv\\OneDrive - GED Integrated Solutions\\Desktop\\Excel\\Print Test.csv", //@"C:\",
                Filter = "CSV Files|*.csv"
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadCsvFileData(filePath);
            }
            else
            {
                ShowErrorMessageBox("File Not Found", null);
            }
        } // Open File Dialog and Load CSV File

        private void CopyToClipboard(string shopOrder, ref string clipboardContent)
        {
            if (!string.IsNullOrEmpty(clipboardContent))
            {
                clipboardContent += ClipboardSeparator; // Set Your value here
            }

            // add the shop order to the combined shop order
            clipboardContent += shopOrder;

            try
            {
                Clipboard.SetText(clipboardContent);
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("Clipboard Error", ex);
            }
        }

        private void ShowErrorMessageBox(string errorMessage, Exception ex)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } // Show Error Message Box

        private void ShowSuccessMessageBox(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        } // Show Success Message Box


        private string[] FindAllFilePaths(string PartNumber)
        {
            String[] fileNames = Directory.GetFiles(DefaultFolderPath, PartNumber + "*.*", SearchOption.AllDirectories);

            if (fileNames.Length > 0)
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(fileNames[i]))
                    {
                        fileNames[i] = fileNames[i].Substring(fileNames[i].IndexOf(PartNumber) + PartNumber.Length);
                        System.Diagnostics.Debug.WriteLine(PartNumber, "Printed");
                    }
                    else
                    {
                        fileNames = new string[1];
                        fileNames[i] = "No Files Found";
                    }
                }
            }


            return fileNames;

        } // Find All File Paths

        
        
    }


}