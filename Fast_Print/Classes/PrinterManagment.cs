using System;
using System.Management;

namespace Fast_Print.Classes;

public class PrinterHelper
{
    private enum PrinterStatus
    {
        Idle = 3,
        Printing = 4,
        Warmup = 5,
        StoppedPrinting = 6,
        Offline = 7,
        Paused = 8,
        Error = 9,
        Busy = 10,
        NotAvailable = 11,
        Waiting = 12,
        Processing = 13,
        Initialization = 14
    }


    public static string GetPrinterStatus(string printerName)
    {
        var path = $"Win32_Printer.DeviceID='{printerName}'";


        using var printer = new ManagementObject(path);
        printer.Get();
        var printerStatus = (PrinterStatus)Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
        string status;
        switch (printerStatus)
        {
            case PrinterStatus.Idle: // Idle  // 3
                status = "The printer is idle.";
                break;
            case PrinterStatus.Printing: // Printing // 4
                status = "The printer is printing.";
                break;
            case PrinterStatus.Warmup: // Warming Up // 5
                status = "The printer is warming up.";
                break;
            case PrinterStatus.StoppedPrinting: // Stopped Printing // 6
                status = "The printer has stopped printing.";
                break;
            case PrinterStatus.Offline: // Offline // 7
                status = "The printer is offline.";
                break;
            case PrinterStatus.Paused: // Paused // 8
                status = "The printer is paused.";
                break;
            case PrinterStatus.Error: // Error // 9
                status = "The printer is in an error state.";
                break;
            case PrinterStatus.Busy: // Busy // 10
                status = "The printer is busy.";
                break;
            case PrinterStatus.NotAvailable: // Not Available // 11
                status = "The printer is not available.";
                break;
            case PrinterStatus.Waiting: // Waiting // 12
                status = "The printer is waiting.";
                break;
            case PrinterStatus.Processing: // Processing // 13
                status = "The printer is processing a print job.";
                break;
            case PrinterStatus.Initialization: // Initializing // 14
                status = "The printer is initializing.";
                break;
            default:
                status = "The printer is in an unknown status.";
                break;
        }

        return status;
    }


    public static int GetPrintJobCount(string printerName)
    {
        var path = $"Win32_Printer.DeviceID='{printerName}'";
        int jobCount;

        try
        {
            using var printer = new ManagementObject(path);
            printer.Get();
            jobCount = Convert.ToInt32(printer.Properties["JobCountSinceLastReset"].Value);
        }
        catch (Exception )
        {
            // Handle exception or return a specific value indicating an error
            jobCount = -1; // Example error value
        }

        return jobCount;
    }
}