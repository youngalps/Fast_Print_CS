namespace Fast_Print
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            button1 = new System.Windows.Forms.Button();
            FileGridView = new System.Windows.Forms.DataGridView();
            FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Part_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Revisions = new System.Windows.Forms.DataGridViewComboBoxColumn();
            LastModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            button2 = new System.Windows.Forms.Button();
            printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)FileGridView).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(44, 372);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(161, 46);
            button1.TabIndex = 0;
            button1.Text = "Load Csv";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Btn_SelectFile_Click;
            // 
            // FileGridView
            // 
            FileGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FileGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { FilePath, Part_Number, Revisions, LastModified });
            FileGridView.Location = new System.Drawing.Point(12, 12);
            FileGridView.Name = "FileGridView";
            FileGridView.Size = new System.Drawing.Size(496, 323);
            FileGridView.TabIndex = 1;
           
           
            // 
            // FilePath
            // 
            FilePath.HeaderText = "FilePath";
            FilePath.Name = "FilePath";
            FilePath.Visible = false;
            // 
            // Part_Number
            // 
            Part_Number.HeaderText = "Part Number";
            Part_Number.Name = "Part_Number";
            // 
            // Revisions
            // 
            Revisions.HeaderText = "Revisions";
            Revisions.Name = "Revisions";
            // 
            // LastModified
            // 
            LastModified.HeaderText = "LastModified";
            LastModified.Name = "LastModified";
            LastModified.ReadOnly = true;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(302, 372);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(161, 46);
            button2.TabIndex = 2;
            button2.Text = "Print Preview";
            button2.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(520, 430);
            Controls.Add(button2);
            Controls.Add(FileGridView);
            Controls.Add(button1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "GED Fast Print";
            //Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)FileGridView).EndInit();
            ResumeLayout(false);
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView FileGridView;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_Number;
        private System.Windows.Forms.DataGridViewComboBoxColumn Revisions;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastModified;
    }
}

