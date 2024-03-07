using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fast_Print.Classes
{

    public class UIComponets
    {
        public static string OpenCsvFileBrowserDialog()
        {


            using var openFileDialog = new OpenFileDialog();
            {
                openFileDialog.Title = @"Select CSV File";
                openFileDialog.InitialDirectory = Properties.Settings.Default.SettingExcelPath;
                openFileDialog.Filter = @"CSV Files|*.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show(@"File Not Selected", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }


        }
    }
}
