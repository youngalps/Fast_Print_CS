using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spire.Pdf;

// TODAYS DATE: 10/12/2021
// TODAYS GOAL: 1. Get rid of old dependencies
//              - Remove PdfSharp
//              - Remove GhostScript
//              - Remove PDFIUM
//              --------------------
//              2. Add Options Menu
//              - Set Default Folder Path
//              - Set Printer
//              - Set Paper Size
//              --------------------
//              3. Clear Button
//              - Clear the grid
//              --------------------
//              4. Need to GIT without accidentally replacing the entire project
             




// Code Clean Up
//TODO  Get rid of old dependencies
//TODO: Redo String splits




// Code Robustness
//TODO: Add more error handling
//TODO: Add more logging
//TODO : Add more comments
//TODO: Add more unit tests
//TODO: PARSE: Add more parsing methods







// UI
//TODO:  BUTTON:Make a clear for the grid
//TODO : BUTTON:Allow user to set the default folder path // In Progress
//TODO:  DATAGRID: Sort by chosen column
//TODO : BUTTON:Allow user to set the default folder path // In Progress

// Printing
//TODO: Add a way to select the printer
//TODO: Add a way to select the paper size
//TODO: Add a way to select the number of copies to print
//TODO: Add a Printing Progress Bar
//TODO: Add a Print Preview
//TODO: Show Whats has been printed and what has not been printed


namespace Fast_Print
{
    public class PdfPrinter
    {
        private Queue<string> printQueue = new Queue<string>();
        private object queueLock = new object();

        public void AddToPrintQueue(string pdfFilePath)
        {
            lock (queueLock)
            {
                printQueue.Enqueue(pdfFilePath);
                if (printQueue.Count == 1)
                {
                    Task.Run(() => ProcessPrintQueue());
                    System.Diagnostics.Debug.WriteLine($"Added to print queue: {pdfFilePath}");
                }
            }
        }

        private void ProcessPrintQueue()
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
                    PrintPDF(pdfFilePath);
                    System.Diagnostics.Debug.WriteLine($"Printed successfully: {pdfFilePath}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error printing {pdfFilePath}: {ex.Message}");
                }

                // Introduce a delay between processing each item (adjust as needed)
                Task.Delay(3000).Wait(); 
            }
        }

        public void PrintPDF(string pdfFilePath)
        {
            // Simulate the printing process by waiting for a short duration (adjust as needed)
            Task.Delay(5000).Wait(); 

            // Load a PDF document
            PdfDocument doc = new PdfDocument(pdfFilePath);
            
            

           

            // Print the document
            doc.Print();
        }
        

       
    }
}
