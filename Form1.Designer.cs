
namespace WinFormsPaczkomat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonPackSelectFiles = new System.Windows.Forms.Button();
            this.buttonPackPack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPackDestinationLocation = new System.Windows.Forms.Button();
            this.textBoxPackDestination = new System.Windows.Forms.TextBox();
            this.textBoxPackName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUnpackSourceLocation = new System.Windows.Forms.TextBox();
            this.buttonUnpackSelectToExtract = new System.Windows.Forms.Button();
            this.buttonUnpackUnpack = new System.Windows.Forms.Button();
            this.buttonUnpackDestinationLocation = new System.Windows.Forms.Button();
            this.textBoxUnpackTargetLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBarPack = new System.Windows.Forms.ProgressBar();
            this.progressBarUnpack = new System.Windows.Forms.ProgressBar();
            this.listOfFilesToPack = new System.Windows.Forms.ListBox();
            this.buttonPackDeleteAllItems = new System.Windows.Forms.Button();
            this.buttonPackDeleteSelectedItem = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxCompressionLevel = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonPackSelectFolders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPackSelectFiles
            // 
            this.buttonPackSelectFiles.Location = new System.Drawing.Point(15, 308);
            this.buttonPackSelectFiles.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.buttonPackSelectFiles.Name = "buttonPackSelectFiles";
            this.buttonPackSelectFiles.Size = new System.Drawing.Size(90, 40);
            this.buttonPackSelectFiles.TabIndex = 4;
            this.buttonPackSelectFiles.Text = "Dodaj pliki";
            this.buttonPackSelectFiles.UseVisualStyleBackColor = true;
            this.buttonPackSelectFiles.Click += new System.EventHandler(this.buttonPackSelectFiles_Click);
            // 
            // buttonPackPack
            // 
            this.buttonPackPack.Location = new System.Drawing.Point(317, 363);
            this.buttonPackPack.Name = "buttonPackPack";
            this.buttonPackPack.Size = new System.Drawing.Size(100, 30);
            this.buttonPackPack.TabIndex = 8;
            this.buttonPackPack.Text = "Pakuj";
            this.buttonPackPack.UseVisualStyleBackColor = true;
            this.buttonPackPack.Click += new System.EventHandler(this.buttonPackPack_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(430, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 388);
            this.label1.TabIndex = 108;
            this.label1.Text = "label1";
            // 
            // buttonPackDestinationLocation
            // 
            this.buttonPackDestinationLocation.Location = new System.Drawing.Point(390, 45);
            this.buttonPackDestinationLocation.Name = "buttonPackDestinationLocation";
            this.buttonPackDestinationLocation.Size = new System.Drawing.Size(27, 23);
            this.buttonPackDestinationLocation.TabIndex = 1;
            this.buttonPackDestinationLocation.Text = "...";
            this.buttonPackDestinationLocation.UseVisualStyleBackColor = true;
            this.buttonPackDestinationLocation.Click += new System.EventHandler(this.buttonPackDestinationLocation_Click);
            // 
            // textBoxPackDestination
            // 
            this.textBoxPackDestination.Location = new System.Drawing.Point(154, 45);
            this.textBoxPackDestination.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.textBoxPackDestination.Name = "textBoxPackDestination";
            this.textBoxPackDestination.ReadOnly = true;
            this.textBoxPackDestination.Size = new System.Drawing.Size(230, 23);
            this.textBoxPackDestination.TabIndex = 112;
            // 
            // textBoxPackName
            // 
            this.textBoxPackName.Location = new System.Drawing.Point(154, 77);
            this.textBoxPackName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.textBoxPackName.Name = "textBoxPackName";
            this.textBoxPackName.Size = new System.Drawing.Size(263, 23);
            this.textBoxPackName.TabIndex = 2;
            this.textBoxPackName.Text = "Archiwum";
            this.textBoxPackName.TextChanged += new System.EventHandler(this.textBoxPackName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 103;
            this.label2.Text = "Nazwa archiwum ";
            // 
            // textBoxUnpackSourceLocation
            // 
            this.textBoxUnpackSourceLocation.Location = new System.Drawing.Point(589, 45);
            this.textBoxUnpackSourceLocation.Name = "textBoxUnpackSourceLocation";
            this.textBoxUnpackSourceLocation.ReadOnly = true;
            this.textBoxUnpackSourceLocation.Size = new System.Drawing.Size(230, 23);
            this.textBoxUnpackSourceLocation.TabIndex = 113;
            // 
            // buttonUnpackSelectToExtract
            // 
            this.buttonUnpackSelectToExtract.Location = new System.Drawing.Point(825, 44);
            this.buttonUnpackSelectToExtract.Name = "buttonUnpackSelectToExtract";
            this.buttonUnpackSelectToExtract.Size = new System.Drawing.Size(27, 23);
            this.buttonUnpackSelectToExtract.TabIndex = 9;
            this.buttonUnpackSelectToExtract.Text = "...";
            this.buttonUnpackSelectToExtract.UseVisualStyleBackColor = true;
            this.buttonUnpackSelectToExtract.Click += new System.EventHandler(this.buttonUnpackSelectToExtract_Click);
            // 
            // buttonUnpackUnpack
            // 
            this.buttonUnpackUnpack.Location = new System.Drawing.Point(752, 363);
            this.buttonUnpackUnpack.Name = "buttonUnpackUnpack";
            this.buttonUnpackUnpack.Size = new System.Drawing.Size(100, 30);
            this.buttonUnpackUnpack.TabIndex = 11;
            this.buttonUnpackUnpack.Text = "Wypakuj";
            this.buttonUnpackUnpack.UseVisualStyleBackColor = true;
            this.buttonUnpackUnpack.Click += new System.EventHandler(this.buttonUnpackUnpack_Click);
            // 
            // buttonUnpackDestinationLocation
            // 
            this.buttonUnpackDestinationLocation.Location = new System.Drawing.Point(825, 77);
            this.buttonUnpackDestinationLocation.Name = "buttonUnpackDestinationLocation";
            this.buttonUnpackDestinationLocation.Size = new System.Drawing.Size(27, 23);
            this.buttonUnpackDestinationLocation.TabIndex = 10;
            this.buttonUnpackDestinationLocation.Text = "...";
            this.buttonUnpackDestinationLocation.UseVisualStyleBackColor = true;
            this.buttonUnpackDestinationLocation.Click += new System.EventHandler(this.buttonUnpackTargetLocation_Click);
            // 
            // textBoxUnpackTargetLocation
            // 
            this.textBoxUnpackTargetLocation.Location = new System.Drawing.Point(589, 77);
            this.textBoxUnpackTargetLocation.Name = "textBoxUnpackTargetLocation";
            this.textBoxUnpackTargetLocation.ReadOnly = true;
            this.textBoxUnpackTargetLocation.Size = new System.Drawing.Size(230, 23);
            this.textBoxUnpackTargetLocation.TabIndex = 114;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(447, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 19);
            this.label3.TabIndex = 109;
            this.label3.Text = "Rozpakowanie archiwum zip";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(410, 1);
            this.label4.TabIndex = 101;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(15, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 19);
            this.label5.TabIndex = 100;
            this.label5.Text = "Tworzenie archiwum zip";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(442, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(410, 1);
            this.label6.TabIndex = 110;
            this.label6.Text = "label6";
            // 
            // progressBarPack
            // 
            this.progressBarPack.Location = new System.Drawing.Point(15, 363);
            this.progressBarPack.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.progressBarPack.Name = "progressBarPack";
            this.progressBarPack.Size = new System.Drawing.Size(280, 30);
            this.progressBarPack.TabIndex = 107;
            // 
            // progressBarUnpack
            // 
            this.progressBarUnpack.Location = new System.Drawing.Point(447, 363);
            this.progressBarUnpack.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.progressBarUnpack.Name = "progressBarUnpack";
            this.progressBarUnpack.Size = new System.Drawing.Size(280, 30);
            this.progressBarUnpack.TabIndex = 115;
            // 
            // listOfFilesToPack
            // 
            this.listOfFilesToPack.FormattingEnabled = true;
            this.listOfFilesToPack.ItemHeight = 15;
            this.listOfFilesToPack.Location = new System.Drawing.Point(15, 163);
            this.listOfFilesToPack.Name = "listOfFilesToPack";
            this.listOfFilesToPack.Size = new System.Drawing.Size(402, 139);
            this.listOfFilesToPack.TabIndex = 106;
            // 
            // buttonPackDeleteAllItems
            // 
            this.buttonPackDeleteAllItems.Location = new System.Drawing.Point(327, 308);
            this.buttonPackDeleteAllItems.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.buttonPackDeleteAllItems.Name = "buttonPackDeleteAllItems";
            this.buttonPackDeleteAllItems.Size = new System.Drawing.Size(90, 40);
            this.buttonPackDeleteAllItems.TabIndex = 7;
            this.buttonPackDeleteAllItems.Text = "Usuń wszystkie";
            this.buttonPackDeleteAllItems.UseVisualStyleBackColor = true;
            this.buttonPackDeleteAllItems.Click += new System.EventHandler(this.buttonPackDeleteAllItems_Click);
            // 
            // buttonPackDeleteSelectedItem
            // 
            this.buttonPackDeleteSelectedItem.Location = new System.Drawing.Point(224, 308);
            this.buttonPackDeleteSelectedItem.Name = "buttonPackDeleteSelectedItem";
            this.buttonPackDeleteSelectedItem.Size = new System.Drawing.Size(90, 40);
            this.buttonPackDeleteSelectedItem.TabIndex = 6;
            this.buttonPackDeleteSelectedItem.Text = "Usuń";
            this.buttonPackDeleteSelectedItem.UseVisualStyleBackColor = true;
            this.buttonPackDeleteSelectedItem.Click += new System.EventHandler(this.buttonPackDeleteSelectedItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(447, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 15);
            this.label7.TabIndex = 111;
            this.label7.Text = "Lokalizacja źródłowa:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 15);
            this.label8.TabIndex = 102;
            this.label8.Text = "Lokalizacja archiwum ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 112);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 15);
            this.label10.TabIndex = 104;
            this.label10.Text = "Poziom kompresji";
            // 
            // comboBoxCompressionLevel
            // 
            this.comboBoxCompressionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompressionLevel.FormattingEnabled = true;
            this.comboBoxCompressionLevel.Items.AddRange(new object[] {
            "brak kompresji",
            "średni",
            "najwyższy"});
            this.comboBoxCompressionLevel.Location = new System.Drawing.Point(154, 109);
            this.comboBoxCompressionLevel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.comboBoxCompressionLevel.Name = "comboBoxCompressionLevel";
            this.comboBoxCompressionLevel.Size = new System.Drawing.Size(263, 23);
            this.comboBoxCompressionLevel.TabIndex = 3;
            this.comboBoxCompressionLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompressionLevel_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 142);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 15);
            this.label11.TabIndex = 105;
            this.label11.Text = "Pliki do spakowania";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(447, 80);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 15);
            this.label9.TabIndex = 112;
            this.label9.Text = "Lokalizacja docelowa: ";
            // 
            // buttonPackSelectFolders
            // 
            this.buttonPackSelectFolders.Location = new System.Drawing.Point(118, 308);
            this.buttonPackSelectFolders.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.buttonPackSelectFolders.Name = "buttonPackSelectFolders";
            this.buttonPackSelectFolders.Size = new System.Drawing.Size(90, 40);
            this.buttonPackSelectFolders.TabIndex = 5;
            this.buttonPackSelectFolders.Text = "Dodaj foldery";
            this.buttonPackSelectFolders.UseVisualStyleBackColor = true;
            this.buttonPackSelectFolders.Click += new System.EventHandler(this.buttonPackSelectFolders_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 405);
            this.Controls.Add(this.buttonPackSelectFolders);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBoxCompressionLevel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonPackDeleteSelectedItem);
            this.Controls.Add(this.buttonPackDeleteAllItems);
            this.Controls.Add(this.listOfFilesToPack);
            this.Controls.Add(this.progressBarUnpack);
            this.Controls.Add(this.progressBarPack);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxUnpackTargetLocation);
            this.Controls.Add(this.buttonUnpackDestinationLocation);
            this.Controls.Add(this.buttonUnpackUnpack);
            this.Controls.Add(this.buttonUnpackSelectToExtract);
            this.Controls.Add(this.textBoxUnpackSourceLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPackName);
            this.Controls.Add(this.textBoxPackDestination);
            this.Controls.Add(this.buttonPackDestinationLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPackPack);
            this.Controls.Add(this.buttonPackSelectFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OxiZip";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPackSelectFiles;
        private System.Windows.Forms.Button buttonPackPack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPackDestinationLocation;
        private System.Windows.Forms.TextBox textBoxPackDestination;
        private System.Windows.Forms.TextBox textBoxPackName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUnpackSourceLocation;
        private System.Windows.Forms.Button buttonUnpackSelectToExtract;
        private System.Windows.Forms.Button buttonUnpackUnpack;
        private System.Windows.Forms.Button buttonUnpackDestinationLocation;
        private System.Windows.Forms.TextBox textBoxUnpackTargetLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBarPack;
        private System.Windows.Forms.ProgressBar progressBarUnpack;
        private System.Windows.Forms.ListBox listOfFilesToPack;
        private System.Windows.Forms.Button buttonPackDeleteAllItems;
        private System.Windows.Forms.Button buttonPackDeleteSelectedItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxCompressionLevel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonPackSelectFolders;
    }
}

