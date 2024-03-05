using System;
using System.IO;
using System.Text.Json;

namespace Fast_Print.Classes
{
    public class AppSettings
    {
        public string SettingExcelPath { get; set; }
        public string SettingDrawingPath { get; set; }
        public string SettingAcrobatPath { get; set; }
        public string SettingPrinterPath { get; set; }

        // Singleton instance
        public static AppSettings instance; // what does this do?
        public static AppSettings Instance => instance ??= LoadSettings(); // Using null-coalescing assignment to ensure instance is loaded
        

        // Internal constructor to allow JsonSerializer to work and maintain singleton pattern
        public AppSettings() { }

        public static AppSettings LoadSettings(string jsonFilePath = "appsettings.json")
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    instance = new AppSettings(); // Using null-coalescing operator to ensure instance is not null
                    instance = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings(); // Using null-coalescing operator to ensure instance is not null
                   
                }
                else
                {
                    instance = new AppSettings();
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                throw new InvalidOperationException($"Error loading settings: {ex.Message}", ex);
                instance = new AppSettings();

            }

            return instance;
        }

              
        public void SaveSettings(string jsonFilePath = "appsettings.json")  
        {
            
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);
            System.Diagnostics.Debug.WriteLine(jsonString); // Write the JSON string to the file
            // Write the JSON string to the file

            File.WriteAllText(jsonFilePath, jsonString);

            
            
        }

    }
}


