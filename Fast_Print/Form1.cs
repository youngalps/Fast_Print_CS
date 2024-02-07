

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using CsvHelper;
using Patagames.Pdf;
using Patagames.Pdf.Enums;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.WinForms;





namespace Fast_Print
{

    // Declare the event
    //public event EventHandler<CellClickEventArgs> CellClicked;

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
            //PrintPDF("C:\\Users\\gedmsinv\\OneDrive - GED Integrated Solutions\\Desktop\\DWG\\3-\\" + "3-1004-A.pdf"); Debug
            PrintSelectedPDF();
        }

        // Lets see what comes out
        private void PrintSelectedPDF()//
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //Get the selected part number and revision
                if (!String.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    if (dataGridView1.RowCount > 1)
                    {
                        //if (!String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            string folderPathEx = row.Cells[0].Value.ToString();
                            string partNumber = row.Cells[1].Value.ToString();
                            string revision = row.Cells[2].Value.ToString();

                            string pdfPath = FolderPath + folderPathEx + partNumber + revision;//Path.Combine(

                            PrintPDF(pdfPath);
                        }
                    }
            }
        }

        private void PrintPDF(string pdfPath)
        {
            try
            {
                var doc = PdfDocument.Load(pdfPath);

                using (var printDoc = new PdfPrintDocument(doc))
                {
                    // Set up print settings if needed, e.g., printDoc.PrinterSettings

                    int currentPage = 0; // Track the current page number

                    printDoc.PrintPage += (sender, e) =>
                    {
                        var page = doc.Pages[currentPage];
                        int width = (int)(page.Width / 72.0 * 96);
                        int height = (int)(page.Height / 72.0 * 96);

                        using (var bitmap = new PdfBitmap(width, height, true))
                        {
                            bitmap.FillRect(0, 0, width, height, FS_COLOR.White);
                            page.Render(bitmap, 0, 0, width, height, PageRotate.Normal, RenderFlags.FPDF_LCD_TEXT);

                            e.Graphics.DrawImage(bitmap.GetImage(), e.MarginBounds);
                        }

                        currentPage++;

                        e.HasMorePages = currentPage < doc.Pages.Count;
                    };

                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("Error", ex);
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
                    
                    String[] TempString = Directory.GetFiles(FolderPath, record.PartNumber + "*.*", SearchOption.AllDirectories);
                   
                    String[] SplitString = TempString[0].Split(FolderPath);
                    SplitString = SplitString[1].Split(record.PartNumber);
                    dataGridView1.Rows[currentIndex].Cells[0].Value = SplitString[0];

                    dataGridView1.Rows[currentIndex].Cells[1].Value = record.PartNumber;
                    DataGridViewComboBoxCell placeholder = (DataGridViewComboBoxCell)dataGridView1.Rows[currentIndex].Cells[2];
                    String[] tempStr = FindAllFile(record.PartNumber);
                    for (Int32 i = 0; i < tempStr.Length; i++)
                    {
                        placeholder.Items.Add(tempStr[i]);
                        placeholder.Value = placeholder.Items[0];
                        //dataGridView1.Rows[currentIndex].Cells[3].Value = DataGridView1_CellContentClick(); 
                    }
                }
            }
            ShowSuccessMessageBox("Shop Orders Copied to Clipboard");
            this.Activate();
        }

        private void OpenCsvFileDialog()
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

            if (TempString.Length > 0)
            {
                for (int i = 0; i < TempString.Length; i++)
                {
                    if (!string.IsNullOrEmpty(TempString[i]))
                    {
                        TempString[i] = TempString[i].Substring(TempString[i].IndexOf(PartNumber) + PartNumber.Length);
                        System.Diagnostics.Debug.WriteLine(PartNumber, "Printed");
                    }
                    else
                    {
                        TempString = new string[1];
                        TempString[i] = "No Files Found";
                    }
                }
            }
           

            return TempString;

        }

    }

    /*
    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if(e.ColumnIndex == 3)
        {
            String FilePath = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dataGridView1.Rows[e.RowIndex].Cells[3].Value = File.GetLastWriteTime(FolderPath + FilePath).ToString();
            //dataGridView1.Rows[e.RowIndex].Cells[3].Value = UpdateLastModified();

            // Raise the event
            OnCellClicked(new CellClickEventArgs(e.RowIndex, FilePath));

        }
    }


    // Method to raise the event
    protected virtual void OnCellClicked(CellClickEventArgs e)
    {
        CellClicked?.Invoke(this, e);
    }
    */
}
