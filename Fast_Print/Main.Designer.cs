using Fast_Print.Classes;

namespace Fast_Print
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            button1 = new System.Windows.Forms.Button();
            colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRevisions = new System.Windows.Forms.DataGridViewComboBoxColumn();
            colPrinterStatus = new System.Windows.Forms.DataGridViewImageColumn();
            button2 = new System.Windows.Forms.Button();
            printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ClearGrid = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            fsw = new System.IO.FileSystemWatcher();
            FileGridView = new System.Windows.Forms.DataGridView();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Shop_Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Revision = new System.Windows.Forms.DataGridViewComboBoxColumn();
            PrintStatus = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)fsw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FileGridView).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            button1.Location = new System.Drawing.Point(24, 403);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(125, 52);
            button1.TabIndex = 0;
            button1.Text = "Load Csv";
            button1.UseVisualStyleBackColor = false;
            button1.Click += Btn_SelectFile_Click;
            // 
            // colPath
            // 
            colPath.HeaderText = "Path";
            colPath.Name = "colPath";
            colPath.Visible = false;
            // 
            // colName
            // 
            colName.HeaderText = "Name";
            colName.Name = "colName";
            // 
            // colRevisions
            // 
            colRevisions.HeaderText = "Revisions";
            colRevisions.Name = "colRevisions";
            // 
            // colPrinterStatus
            // 
            colPrinterStatus.HeaderText = "Printer Status";
            colPrinterStatus.Name = "colPrinterStatus";
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            button2.Location = new System.Drawing.Point(409, 403);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(125, 52);
            button2.TabIndex = 0;
            button2.Text = "Print Preview";
            button2.UseVisualStyleBackColor = false;
            button2.Click += Btn_PrintFiles_Click;
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (System.Drawing.Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // ClearGrid
            // 
            ClearGrid.Location = new System.Drawing.Point(169, 417);
            ClearGrid.Name = "ClearGrid";
            ClearGrid.Size = new System.Drawing.Size(81, 28);
            ClearGrid.TabIndex = 3;
            ClearGrid.Text = "Clear";
            ClearGrid.UseVisualStyleBackColor = true;
            ClearGrid.Click += ClearGrid_Btn;
            // 
            // button3
            // 
            button3.Image = Properties.Resources.Icon_Settings;
            button3.Location = new System.Drawing.Point(-1, 0);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(303, 45);
            button3.TabIndex = 4;
            button3.UseVisualStyleBackColor = true;
            button3.Click += Settings_Click_Btn;
            // 
            // fsw
            // 
            fsw.EnableRaisingEvents = true;
            fsw.Filter = "*PDF";
            fsw.IncludeSubdirectories = true;
            fsw.SynchronizingObject = this;
            fsw.Changed += fsw_Changed;
            // 
            // FileGridView
            // 
            FileGridView.AllowUserToAddRows = false;
            FileGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            FileGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FileGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { FilePath, Shop_Order, PartNumber, Revision, PrintStatus });
            FileGridView.Location = new System.Drawing.Point(-1, 43);
            FileGridView.Name = "FileGridView";
            FileGridView.Size = new System.Drawing.Size(572, 354);
            FileGridView.TabIndex = 5;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(269, 417);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(84, 28);
            button4.TabIndex = 6;
            button4.Text = "Clipboard";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Btn_CopyClipBoard;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.Color.Red;
            button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            button5.Location = new System.Drawing.Point(299, 0);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(272, 45);
            button5.TabIndex = 7;
            button5.Text = "STOP PRINTING";
            button5.UseVisualStyleBackColor = false;
            button5.Click += Btn_StopPrinting;
            // 
            // FilePath
            // 
            FilePath.HeaderText = "FilePath";
            FilePath.Name = "FilePath";
            FilePath.Visible = false;
            // 
            // Shop_Order
            // 
            Shop_Order.HeaderText = "SO#";
            Shop_Order.Name = "Shop_Order";
            // 
            // PartNumber
            // 
            PartNumber.HeaderText = "Part No";
            PartNumber.Name = "PartNumber";
            // 
            // Revision
            // 
            Revision.HeaderText = "Revision";
            Revision.Name = "Revision";
            Revision.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            Revision.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PrintStatus
            // 
            PrintStatus.HeaderText = "Print Status";
            PrintStatus.Name = "PrintStatus";
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.SystemColors.GrayText;
            ClientSize = new System.Drawing.Size(571, 466);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(FileGridView);
            Controls.Add(button3);
            Controls.Add(ClearGrid);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Main";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "GED Fast Print";
            Load += Main_Load_;
            ((System.ComponentModel.ISupportInitialize)fsw).EndInit();
            ((System.ComponentModel.ISupportInitialize)FileGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button ClearGrid;
        private System.Windows.Forms.Button button3;
        private System.IO.FileSystemWatcher fsw;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colRevisions;
        private System.Windows.Forms.DataGridViewImageColumn colPrinterStatus;
        private System.Windows.Forms.DataGridView FileGridView;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shop_Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn Revision;
        private System.Windows.Forms.DataGridViewImageColumn PrintStatus;
    }
}

