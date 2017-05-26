namespace FirefoxProfilePasswordUnlocker
{
    partial class MainScreen
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbDirectoryDefault = new System.Windows.Forms.RadioButton();
            this.rbDirectoryChoose = new System.Windows.Forms.RadioButton();
            this.gbDirectory = new System.Windows.Forms.GroupBox();
            this.lbProfiles = new System.Windows.Forms.ListBox();
            this.bBrowseFolder = new System.Windows.Forms.Button();
            this.tbDirectoryPath = new System.Windows.Forms.TextBox();
            this.dgUserCredentials = new System.Windows.Forms.DataGridView();
            this.gbDirectory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserCredentials)).BeginInit();
            this.SuspendLayout();
            // 
            // rbDirectoryDefault
            // 
            this.rbDirectoryDefault.AutoSize = true;
            this.rbDirectoryDefault.Location = new System.Drawing.Point(25, 34);
            this.rbDirectoryDefault.Name = "rbDirectoryDefault";
            this.rbDirectoryDefault.Size = new System.Drawing.Size(102, 17);
            this.rbDirectoryDefault.TabIndex = 1;
            this.rbDirectoryDefault.TabStop = true;
            this.rbDirectoryDefault.Text = "Default directory";
            this.rbDirectoryDefault.UseVisualStyleBackColor = true;
            this.rbDirectoryDefault.CheckedChanged += new System.EventHandler(this.RadioDirectory_CheckedChanged);
            // 
            // rbDirectoryChoose
            // 
            this.rbDirectoryChoose.AutoSize = true;
            this.rbDirectoryChoose.Location = new System.Drawing.Point(133, 34);
            this.rbDirectoryChoose.Name = "rbDirectoryChoose";
            this.rbDirectoryChoose.Size = new System.Drawing.Size(104, 17);
            this.rbDirectoryChoose.TabIndex = 2;
            this.rbDirectoryChoose.TabStop = true;
            this.rbDirectoryChoose.Text = "Choose directory";
            this.rbDirectoryChoose.UseVisualStyleBackColor = true;
            this.rbDirectoryChoose.CheckedChanged += new System.EventHandler(this.RadioDirectory_CheckedChanged);
            // 
            // gbDirectory
            // 
            this.gbDirectory.Controls.Add(this.lbProfiles);
            this.gbDirectory.Controls.Add(this.bBrowseFolder);
            this.gbDirectory.Controls.Add(this.tbDirectoryPath);
            this.gbDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDirectory.Location = new System.Drawing.Point(12, 9);
            this.gbDirectory.Name = "gbDirectory";
            this.gbDirectory.Size = new System.Drawing.Size(600, 180);
            this.gbDirectory.TabIndex = 3;
            this.gbDirectory.TabStop = false;
            this.gbDirectory.Text = "Firefox Profile Directory";
            // 
            // lbProfiles
            // 
            this.lbProfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProfiles.FormattingEnabled = true;
            this.lbProfiles.ItemHeight = 16;
            this.lbProfiles.Location = new System.Drawing.Point(13, 79);
            this.lbProfiles.Name = "lbProfiles";
            this.lbProfiles.Size = new System.Drawing.Size(568, 84);
            this.lbProfiles.TabIndex = 5;
            this.lbProfiles.SelectedIndexChanged += new System.EventHandler(this.LbProfiles_SelectedIndexChanged);
            // 
            // bBrowseFolder
            // 
            this.bBrowseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bBrowseFolder.Location = new System.Drawing.Point(481, 49);
            this.bBrowseFolder.Name = "bBrowseFolder";
            this.bBrowseFolder.Size = new System.Drawing.Size(100, 23);
            this.bBrowseFolder.TabIndex = 4;
            this.bBrowseFolder.Text = "Browse...";
            this.bBrowseFolder.UseVisualStyleBackColor = true;
            this.bBrowseFolder.Click += new System.EventHandler(this.BBrowseFolder_Click);
            // 
            // tbDirectoryPath
            // 
            this.tbDirectoryPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDirectoryPath.Location = new System.Drawing.Point(13, 49);
            this.tbDirectoryPath.Name = "tbDirectoryPath";
            this.tbDirectoryPath.Size = new System.Drawing.Size(468, 23);
            this.tbDirectoryPath.TabIndex = 0;
            this.tbDirectoryPath.TextChanged += new System.EventHandler(this.TbDirectoryPath_Changed);
            // 
            // dgUserCredentials
            // 
            this.dgUserCredentials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUserCredentials.Location = new System.Drawing.Point(12, 196);
            this.dgUserCredentials.Name = "dgUserCredentials";
            this.dgUserCredentials.Size = new System.Drawing.Size(600, 230);
            this.dgUserCredentials.TabIndex = 4;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.dgUserCredentials);
            this.Controls.Add(this.rbDirectoryChoose);
            this.Controls.Add(this.rbDirectoryDefault);
            this.Controls.Add(this.gbDirectory);
            this.Name = "MainScreen";
            this.Text = "Firefox Profile Password Unlocker";
            this.gbDirectory.ResumeLayout(false);
            this.gbDirectory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserCredentials)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton rbDirectoryDefault;
        private System.Windows.Forms.RadioButton rbDirectoryChoose;
        private System.Windows.Forms.GroupBox gbDirectory;
        private System.Windows.Forms.TextBox tbDirectoryPath;
        private System.Windows.Forms.Button bBrowseFolder;
        private System.Windows.Forms.ListBox lbProfiles;
        private System.Windows.Forms.DataGridView dgUserCredentials;
    }
}