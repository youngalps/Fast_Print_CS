﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fast_Print.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\gedmsinv\\OneDrive - GED Integrated Solutions\\Desktop\\Excel")]
        public string SettingExcelPath {
            get {
                return ((string)(this["SettingExcelPath"]));
            }
            set {
                this["SettingExcelPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\gedmsinv\\OneDrive - GED Integrated Solutions\\Desktop\\DWG")]
        public string SettingDrawingPath {
            get {
                return ((string)(this["SettingDrawingPath"]));
            }
            set {
                this["SettingDrawingPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Adobe\\Acrobat DC\\Acrobat\\Acrobat.exe")]
        public string SettingAcrobatPath {
            get {
                return ((string)(this["SettingAcrobatPath"]));
            }
            set {
                this["SettingAcrobatPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\gedps\\MaterialsKonica")]
        public string SettingPrinterPath {
            get {
                return ((string)(this["SettingPrinterPath"]));
            }
            set {
                this["SettingPrinterPath"] = value;
            }
        }
    }
}
