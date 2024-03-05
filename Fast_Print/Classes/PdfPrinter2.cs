using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fast_Print.Properties;
using Spire.Pdf;



namespace Fast_Print.Classes
{
    public class PdfPrinter
    {

        // Settings Interface 
        public interface ISettings
        {
            string SettingPrinterPath { get; set; }
            string SettingAcrobatPath { get; set; }
        }
        // Object to hold the DataGridView
        private DataGridView FileGridView;
        private Queue<string> printQueue = new Queue<string>();
        private object queueLock = new object();
        //private AppSettings appSettings; // Declare appSettings here without initializing
       
        
        string printerName;
        string acrobatExecutable;




        // Constructor to initialize the DataGridView and AppSettings
        public PdfPrinter(DataGridView fileGridView)
        {
            
            FileGridView = fileGridView;
            
            // Load settings from the AppSettings class
           // appSettings = AppSettings.LoadSettings(); // Assuming LoadSettings is a static method now
            printerName = Properties.Settings.Default.SettingPrinterPath;
            acrobatExecutable = Properties.Settings.Default.SettingAcrobatPath;

        }
      







        //public PdfPrinter(DataGridView fileGridView, AppSettings appSettings)
        //{
        //    FileGridView = fileGridView;
        //    loadedSettings = appSettings;
        //}
        public void AddToPrintQueue(string pdfFilePath, int index)
        {
            lock (queueLock)
            {
                printQueue.Enqueue(pdfFilePath);
                if (printQueue.Count == 1)
                {
                    // Set print status to Icon Loa

                    //FileGridView.Rows[index].Cells[3].Value = Properties.Resources.Icon_Load;
                    Task.Run(() => ProcessPrintQueue(index));

                }
            }
        }

        private void ProcessPrintQueue(int index)
        {
            while (true)
            {
                string pdfFilePath;

                lock (queueLock)
                {
                    if (printQueue.Count == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Queue is empty");
                        break;
                    }

                    pdfFilePath = printQueue.Dequeue();
                    System.Diagnostics.Debug.WriteLine($"Printing PDF: {pdfFilePath}");
                }

                try
                {
                    PrintPDFAsync(pdfFilePath,index);
                    FileGridView.Rows[index].Cells[3].Value = Properties.Resources.Icon_Check;
                    //System.Diagnostics.Debug.WriteLine($"Printed successfully: {pdfFilePath}");

                }
                catch (Exception ex)
                {
                    //System.Diagnostics.Debug.WriteLine($"Error printing {pdfFilePath}: {ex.Message}");
                    FileGridView.Rows[index].Cells[3].Value = Properties.Resources.Icon_Error;
                }



            }
        }

        public async Task PrintPDFAsync(string pdfFilePath,int index)
        {
            
            try
            {
                // Corrected the path to include the executable name
                string acrobatExecutable = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";
               string args = $"/t \"{pdfFilePath}\" \"{printerName}\"";

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = acrobatExecutable;
                    process.StartInfo.Arguments = args;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = true; // Changed to true for GUI applications

                    // Inform about printing start.

                    FileGridView.Rows[index].Cells[3].Value = Properties.Resources.Icon_Load;

                    process.Start();
                    await process.WaitForExitAsync();

                    // Depending on Acrobat's behavior, you might need additional logic to ensure it closes as expected.

                    // Inform about successful printing.
                    
                    FileGridView.Rows[index].Cells[3].Value = Properties.Resources.Icon_Check;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the printing process.
                //StatusUpdated?.Invoke($"Error printing {pdfFilePath}: {ex.Message}");
                FileGridView.Rows[index].Cells[3].Value = Properties.Resources.Icon_Error;
            }
        }
    }
}
