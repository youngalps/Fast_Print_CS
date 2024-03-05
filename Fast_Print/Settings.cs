using System;
using System.Windows.Forms;

namespace Fast_Print;

public partial class Settings : Form
{
    // initialize Main form
    private readonly Main _mainform;

    public Settings(Main mainForm)
    {
        InitializeComponent();
        _mainform = mainForm;

        // Apply the loaded settings to the form's properties
        excelPath.Text = Properties.Settings.Default.SettingExcelPath;
        drawingPath.Text = Properties.Settings.Default.SettingDrawingPath;
        acrobatPath.Text = Properties.Settings.Default.SettingAcrobatPath;
        printerPath.Text = Properties.Settings.Default.SettingPrinterPath;

        // Now that we have loadedSettings, we can pass it to PdfPrinter
        // Assuming Main form has a public property or method to access PublicFileGridView
    }


    private void Settings_Load(object sender, EventArgs e)
    {
        // Left empty intentionally
    }

    private void Excel_Browse_Btn(object sender, EventArgs e)
    {
        using var folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.Description = "Select a Folder for Excel Files";
        folderBrowserDialog.ShowNewFolderButton = true; // Allows users to create a new folder

        // Attempt to set an initial directory
        folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        var result = folderBrowserDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            excelPath.Text = folderBrowserDialog.SelectedPath;
            Properties.Settings.Default.SettingExcelPath = excelPath.Text;
            excelPath.ReadOnly = true;
        }
    }


    private void Printer_Browse_Btn(object sender, EventArgs e)
    {
        try
        {
            var printDialog = new PrintDialog();

            // Send the printer name to the text box if a printer is selected
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printerPath.Text = printDialog.PrinterSettings.PrinterName;
                Properties.Settings.Default.SettingPrinterPath = printerPath.Text;
            }
        }
        // Beyond this point, printDialog is disposed.
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }


    private void Acrobat_Browse_Btn(object sender, EventArgs e)
    {
        using var folderBrowserDialog = new OpenFileDialog();
        folderBrowserDialog.Title = "Select Acrobat Executable";
        folderBrowserDialog.Filter = "Executable Files|*.exe";
        folderBrowserDialog.InitialDirectory =
            @"C:\Program Files\Adobe\Acrobat DC\Acrobat"; // Set the initial directory to the Acrobat folder
        var result = folderBrowserDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            var folderPath = folderBrowserDialog.FileName;
            acrobatPath.Text = folderPath;
            Properties.Settings.Default.SettingAcrobatPath = folderPath;
            acrobatPath.ReadOnly = true;
        }
    }


    private void Drawing_Browse_Btn(object sender, EventArgs e)
    {
        // Using the 'using' statement to ensure the FolderBrowserDialog is properly disposed of
        using var folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.Description = "Select a Folder for Drawings";
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer; // Start browsing from 'My Computer'
        folderBrowserDialog.ShowNewFolderButton = true; // Allow users to create a new folder if necessary

        var result = folderBrowserDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            // Set the selected folder's path to the drawingPath text box and update MainForm settings
            drawingPath.Text = folderBrowserDialog.SelectedPath;
            Properties.Settings.Default.SettingDrawingPath = drawingPath.Text;
            drawingPath.ReadOnly = true;
        }

        // FolderBrowserDialog is automatically disposed here
    }


    // save settings
    private void Save_Btn_Click(object sender, EventArgs e)
    {
        // Assign the updated settings from your form controls to the appSettings instance
        Save_Settings();
        Close();
    }

    private void Save_Settings()

    {
        Properties.Settings.Default.SettingExcelPath = excelPath.Text;
        Properties.Settings.Default.SettingDrawingPath = drawingPath.Text;
        Properties.Settings.Default.SettingAcrobatPath = acrobatPath.Text;
        Properties.Settings.Default.SettingPrinterPath = printerPath.Text;
        Properties.Settings.Default.Save();
        // Apply the loaded settings to the form's properties
        _mainform.RefreshFswLocation();
    }
}