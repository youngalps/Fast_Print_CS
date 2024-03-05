namespace Fast_Print
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            browseExcel = new System.Windows.Forms.Button();
            excelPath = new System.Windows.Forms.TextBox();
            excelText = new System.Windows.Forms.Label();
            printerText = new System.Windows.Forms.Label();
            printerPath = new System.Windows.Forms.TextBox();
            browsePrinter = new System.Windows.Forms.Button();
            acrobatText = new System.Windows.Forms.Label();
            acrobatPath = new System.Windows.Forms.TextBox();
            browseAcrobat = new System.Windows.Forms.Button();
            drawingText = new System.Windows.Forms.Label();
            drawingPath = new System.Windows.Forms.TextBox();
            browseDrawing = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // browseExcel
            // 
            browseExcel.Location = new System.Drawing.Point(380, 38);
            browseExcel.Name = "browseExcel";
            browseExcel.Size = new System.Drawing.Size(97, 23);
            browseExcel.TabIndex = 0;
            browseExcel.Text = "Browse";
            browseExcel.UseVisualStyleBackColor = true;
            browseExcel.Click += Excel_Browse_Btn;
            // 
            // excelPath
            // 
            excelPath.Location = new System.Drawing.Point(23, 38);
            excelPath.Name = "excelPath";
            excelPath.Size = new System.Drawing.Size(334, 23);
            excelPath.TabIndex = 2;
            // 
            // excelText
            // 
            excelText.AutoSize = true;
            excelText.Location = new System.Drawing.Point(23, 20);
            excelText.Name = "excelText";
            excelText.Size = new System.Drawing.Size(70, 15);
            excelText.TabIndex = 3;
            excelText.Text = "Excel Folder";
            excelText.Click += Excel_Browse_Btn;
            // 
            // printerText
            // 
            printerText.AutoSize = true;
            printerText.Location = new System.Drawing.Point(23, 78);
            printerText.Name = "printerText";
            printerText.Size = new System.Drawing.Size(42, 15);
            printerText.TabIndex = 6;
            printerText.Text = "Printer";
            // 
            // printerPath
            // 
            printerPath.Location = new System.Drawing.Point(23, 96);
            printerPath.Name = "printerPath";
            printerPath.ReadOnly = true;
            printerPath.Size = new System.Drawing.Size(334, 23);
            printerPath.TabIndex = 5;
            // 
            // browsePrinter
            // 
            browsePrinter.Location = new System.Drawing.Point(380, 96);
            browsePrinter.Name = "browsePrinter";
            browsePrinter.Size = new System.Drawing.Size(97, 23);
            browsePrinter.TabIndex = 4;
            browsePrinter.Text = "Browse";
            browsePrinter.UseVisualStyleBackColor = true;
            browsePrinter.Click += Printer_Browse_Btn;
            // 
            // acrobatText
            // 
            acrobatText.AutoSize = true;
            acrobatText.Location = new System.Drawing.Point(23, 132);
            acrobatText.Name = "acrobatText";
            acrobatText.Size = new System.Drawing.Size(87, 15);
            acrobatText.TabIndex = 9;
            acrobatText.Text = "Adobe Acrobat";
            // 
            // acrobatPath
            // 
            acrobatPath.Location = new System.Drawing.Point(23, 151);
            acrobatPath.Name = "acrobatPath";
            acrobatPath.Size = new System.Drawing.Size(334, 23);
            acrobatPath.TabIndex = 8;
            // 
            // browseAcrobat
            // 
            browseAcrobat.Location = new System.Drawing.Point(380, 151);
            browseAcrobat.Name = "browseAcrobat";
            browseAcrobat.Size = new System.Drawing.Size(97, 23);
            browseAcrobat.TabIndex = 7;
            browseAcrobat.Text = "Browse";
            browseAcrobat.UseVisualStyleBackColor = true;
            browseAcrobat.Click += Acrobat_Browse_Btn;
            // 
            // drawingText
            // 
            drawingText.AutoSize = true;
            drawingText.Location = new System.Drawing.Point(23, 187);
            drawingText.Name = "drawingText";
            drawingText.Size = new System.Drawing.Size(87, 15);
            drawingText.TabIndex = 12;
            drawingText.Text = "Drawing Folder";
            // 
            // drawingPath
            // 
            drawingPath.Location = new System.Drawing.Point(23, 206);
            drawingPath.Name = "drawingPath";
            drawingPath.Size = new System.Drawing.Size(334, 23);
            drawingPath.TabIndex = 11;
            // 
            // browseDrawing
            // 
            browseDrawing.Location = new System.Drawing.Point(380, 206);
            browseDrawing.Name = "browseDrawing";
            browseDrawing.Size = new System.Drawing.Size(97, 23);
            browseDrawing.TabIndex = 10;
            browseDrawing.Text = "Browse";
            browseDrawing.UseVisualStyleBackColor = true;
            browseDrawing.Click += Drawing_Browse_Btn;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(557, 230);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(93, 39);
            button1.TabIndex = 13;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Save_Btn_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(662, 281);
            Controls.Add(button1);
            Controls.Add(drawingText);
            Controls.Add(drawingPath);
            Controls.Add(browseDrawing);
            Controls.Add(acrobatText);
            Controls.Add(acrobatPath);
            Controls.Add(browseAcrobat);
            Controls.Add(printerText);
            Controls.Add(printerPath);
            Controls.Add(browsePrinter);
            Controls.Add(excelText);
            Controls.Add(excelPath);
            Controls.Add(browseExcel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            Text = "Form1";
            TopMost = true;
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Excel Folder
        private System.Windows.Forms.Button browseExcel;
        private System.Windows.Forms.TextBox excelPath;
        private System.Windows.Forms.Label excelText;
        // Printer
        private System.Windows.Forms.Label printerText;
        private System.Windows.Forms.TextBox printerPath;
        private System.Windows.Forms.Button browsePrinter;
        // Adobe Acrobat
        private System.Windows.Forms.Label acrobatText;
        private System.Windows.Forms.TextBox acrobatPath;
        private System.Windows.Forms.Button browseAcrobat;

        // Drawing Folder
        private System.Windows.Forms.Label drawingText;
        private System.Windows.Forms.TextBox drawingPath;
        private System.Windows.Forms.Button browseDrawing;
        private System.Windows.Forms.Button button1;
    }
}