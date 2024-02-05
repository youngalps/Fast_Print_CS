using System;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Pdf
using CsvHelper;

namespace Fast_Print
{
    public partial class Form1 : Form
    {
        // Contants


        private const string FolderPath = @"C:\Users\gedmsinv\OneDrive - GED Integrated Solutions\Desktop\DWG"; // TODO: set this to the network drive
        private const string Separator = ";";

        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenCsvFileDialog();
        }


        private void Btn_PrintFiles_Click(object sender, EventArgs e)
        {
            PrintSelectedPDF();
        }

        // Lets see what comes out 
        private void PrintSelectedPDF()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                // Get the selected part number and revision
                string partNumber = row.Cells[0].Value.ToString();
                string revision = row.Cells[1].Value.ToString();

                // Construct the full PDF path
                string pdfPath = Path.Combine(FolderPath, partNumber + revision + ".PDF");

                // Print the selected PDF
                PrintPDF(pdfPath);
            }


        }

        private void PrintPDF(string pdfPath)
        {

            try
            {
                using (PdfDocument document = PdfDocument.Load(pdfPath))
                {
                    using (PrintDocument printDocument = new PrintDocument())
                    {
                        printDocument.PrintPage += (sender, e) =>
                        {
                            document.Render(e.Graphics, e.PageBounds, 0);
                            document.
                        };

                        printDocument.Print();
                    }
                }

                ShowSuccessMessageBox("PDF Printed Successfully");
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("Error Printing PDF", ex);
            }
        }






        private void Form1_Load(object sender, EventArgs e)
        {
            // Method intentionally left empty.

        }

        private void LoadCsvData(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            string clipboardContent = "";

            var records = csv.GetRecords<CsvRecords>();
            foreach (var record in records)
            {
                if (records != null)
                {
                    ProcessShopOrder(record.ShopOrder, ref clipboardContent); // goes to clipboard
                    int currentIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[currentIndex].Cells[0].Value = record.PartNumber;
                    DataGridViewComboBoxCell placeholder = (DataGridViewComboBoxCell)dataGridView1.Rows[currentIndex].Cells[1];

                    String[] tempStr = FindAllFile(record.PartNumber);
                    for (Int32 i = 0; i < tempStr.Length; i++)
                    {
                        placeholder.Items.Add(tempStr[i]);
                    }



                }
            }
            ShowSuccessMessageBox("Shop Orders Copied to Clipboard");

        }

        private void OpenCsvFileDialog()
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Select CSV File",
                InitialDirectory = @"C:\",
                Filter = "CSV Files|*.csv"
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadCsvData(filePath);
            }
            else
            {
                ShowErrorMessageBox("File Not Found", null);
            }
        }

        private void ProcessShopOrder(string shopOrder, ref string clipboardContent)
        {
            if (!string.IsNullOrEmpty(clipboardContent))
            {
                clipboardContent += Separator; // Set Your value here
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
        }

        private void ShowSuccessMessageBox(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Method intentionally left empty.
        }

        private string[] FindAllFile(string PartNumber)
        {
            String[] TempString = Directory.GetFiles(FolderPath, PartNumber + "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < TempString.Length; i++)
            {
                TempString[i] = TempString[i].Substring(TempString[i].IndexOf(PartNumber) + PartNumber.Length);
            }

            return TempString;
        }
    }
}




