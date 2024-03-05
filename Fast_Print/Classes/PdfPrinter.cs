using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fast_Print.Properties;

namespace Fast_Print.Classes
{
    public class PdfPrinter
    {
        private readonly string _printerName = Properties.Settings.Default.SettingPrinterPath;
        private readonly string _acrobatExecutable = Properties.Settings.Default.SettingAcrobatPath;
        private readonly Queue<(string PdfFilePath, int Index)> _printQueue = new();
        private readonly object _queueLock = new();
        private static DataGridView _fileGridView;
        private bool _isPrinting = false;
        

        
        public PdfPrinter(DataGridView fileGridView)
        {
            _fileGridView = fileGridView;
        }

       

        public void AddToPrintQueue(string pdfFilePath, int index)
        {
            lock (_queueLock)
            {
                _printQueue.Enqueue((pdfFilePath, index));
                if (!_isPrinting) // If the queue is not being processed, start processing it
                {
                    _isPrinting = true;
                    Task.Run(() => ProcessPrintQueue());
                }
            }
        }

        private async Task ProcessPrintQueue()
        {
            while (true) // Keep processing the queue until it's empty
            {
                (string pdfFilePath, int index) job; // Dequeue the next job

                lock (_queueLock) // Lock the queue while we dequeue
                {
                    if (_printQueue.Count == 0) // If the queue is empty, stop processing
                    {
                        _isPrinting = true; 
                        break;
                    }
                
                    job = _printQueue.Dequeue(); // Dequeue the next job
                }
                //Print the PDF
                await PrintPdfAsync(job.pdfFilePath, job.index); // Print the PDF
                
          
            }
        }

        public async Task PrintPdfAsync(string pdfFilePath, int index)
        {
            try
            {
                UpdateDataGridView(index, Resources.Icon_Load); // Assuming Icon_Load is the loading icon

                var args = $"/t \"{pdfFilePath}\" \"{_printerName}\"";
                using (var process = new Process 
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _acrobatExecutable,
                        Arguments = args,
                        CreateNoWindow = true,
                        UseShellExecute = true // For GUI applications
                    }
                })
                {
                    process.Start();
                    await process.WaitForExitAsync();
                }

                UpdateDataGridView(index, Resources.Icon_Check); // Assuming Icon_Check is the success icon
            }
            catch (Exception ex)
            {
                
                UpdateDataGridView(index, Resources.Icon_Error); // Assuming Icon_Error is the error icon
            }
        }

        private void UpdateDataGridView(int index, object value)
        {
            if (_fileGridView.InvokeRequired)
            {
                _fileGridView.BeginInvoke(new Action(() => UpdateDataGridView(index, value)));
            }
            else
            {
                _fileGridView.Rows[index].Cells[3].Value = value;
            }
        }
    }

 
}

