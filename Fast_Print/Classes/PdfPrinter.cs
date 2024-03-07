using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
        private readonly DataGridView _fileGridView;
        private bool _isPrinting = false;
        private CancellationTokenSource _cancellationTokenSource = new();

        public PdfPrinter(DataGridView fileGridView)
        {
            _fileGridView = fileGridView;
        }

        public async Task PrintSelectedPdfAsync()
        {
            if (_fileGridView.Rows.Count <= 1) return;

            foreach (DataGridViewRow row in _fileGridView.Rows)
            {
                var fullPath = row.Cells[0].Value?.ToString();
                var partNumber = row.Cells[2].Value?.ToString();
                var revision = row.Cells[3].Value?.ToString();

                if (!string.IsNullOrEmpty(fullPath) && !string.IsNullOrEmpty(partNumber) && !string.IsNullOrEmpty(revision))
                {
                    var folderRelativePath = fullPath[..(fullPath.LastIndexOf('\\') + 1)];
                    var pdfFilePath = $"{folderRelativePath}{partNumber}-{revision}";

                    AddToPrintQueue(pdfFilePath, row.Index);
                }
            }
        }

        public void AddToPrintQueue(string pdfFilePath, int index)
        {
            lock (_queueLock)
            {
                _printQueue.Enqueue((pdfFilePath, index));
                if (!_isPrinting)
                {
                    _isPrinting = true;
                    _cancellationTokenSource = new CancellationTokenSource();
                    Task.Run(() => ProcessPrintQueue(_cancellationTokenSource.Token));
                }
            }
        }

        private async Task ProcessPrintQueue(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                (string pdfFilePath, int index) job;
                lock (_queueLock)
                {
                    if (_printQueue.Count == 0)
                    {
                        _isPrinting = false;
                        break;
                    }
                    job = _printQueue.Dequeue();
                }

                await PrintPdfAsync(job.pdfFilePath, job.index, cancellationToken);
            }
        }

        public async Task PrintPdfAsync(string pdfFilePath, int index, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            try
            {
                UpdateDataGridView(index, Resources.Icon_Load);

                var args = $"/t \"{pdfFilePath}\" \"{_printerName}\"";
                using (var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _acrobatExecutable,
                        Arguments = args,
                        CreateNoWindow = true,
                        UseShellExecute = true
                    }
                })
                {
                    try
                    {
                        process.Start();
                        await Task.Run(() => process.WaitForExit(10000), cancellationToken);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, @"Error here", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                   
                }

                UpdateDataGridView(index, Resources.Icon_Check);
            }
            catch (Exception)
            {
                // Log the exception
                //UpdateDataGridView(index, Resources.Icon_Error);
                MessageBox.Show(@"Error occurred while printing the PDF file.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGridView(int index, object value)
        {
            if (index >= 0 && index < _fileGridView.Rows.Count)
            {
                // Define the update action to be performed on the UI thread.
                Action updateAction = () => _fileGridView.Rows[index].Cells[4].Value = value;

                // Check if invoking on the UI thread is required.
                if (_fileGridView.InvokeRequired)
                {
                    // Perform the update on the UI thread.
                    _fileGridView.BeginInvoke(updateAction);
                }
                else
                {
                    // Directly perform the update if already on the UI thread.
                    updateAction();
                }
            }
        }

        public void CancelPrinting()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
