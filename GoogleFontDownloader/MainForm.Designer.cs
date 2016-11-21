namespace GoogleFontDownloader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.download = new System.Windows.Forms.Button();
            this.cssURL = new System.Windows.Forms.TextBox();
            this.title = new System.Windows.Forms.Label();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.selectFolder = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.selectCSS = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cssFolderPath = new System.Windows.Forms.TextBox();
            this.defaultCSSFolderPath = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.clickCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // download
            // 
            this.download.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.download.Location = new System.Drawing.Point(413, 186);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(78, 23);
            this.download.TabIndex = 0;
            this.download.Text = "Download";
            this.download.UseVisualStyleBackColor = true;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // cssURL
            // 
            this.cssURL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cssURL.Location = new System.Drawing.Point(62, 91);
            this.cssURL.Name = "cssURL";
            this.cssURL.Size = new System.Drawing.Size(364, 23);
            this.cssURL.TabIndex = 1;
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(77, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(402, 65);
            this.title.TabIndex = 2;
            this.title.Text = "Google Font Downloader";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // folderPath
            // 
            this.folderPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderPath.Location = new System.Drawing.Point(62, 117);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(364, 23);
            this.folderPath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "CSS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder:";
            // 
            // selectFolder
            // 
            this.selectFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFolder.Location = new System.Drawing.Point(432, 117);
            this.selectFolder.Name = "selectFolder";
            this.selectFolder.Size = new System.Drawing.Size(59, 23);
            this.selectFolder.TabIndex = 6;
            this.selectFolder.Text = "Select";
            this.selectFolder.UseVisualStyleBackColor = true;
            this.selectFolder.Click += new System.EventHandler(this.selectFolder_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 186);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(387, 23);
            this.progressBar.TabIndex = 8;
            // 
            // selectCSS
            // 
            this.selectCSS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectCSS.Location = new System.Drawing.Point(432, 89);
            this.selectCSS.Name = "selectCSS";
            this.selectCSS.Size = new System.Drawing.Size(59, 23);
            this.selectCSS.TabIndex = 9;
            this.selectCSS.Text = "Select";
            this.selectCSS.UseVisualStyleBackColor = true;
            this.selectCSS.Click += new System.EventHandler(this.selectCSS_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Font folder path in CSS:";
            // 
            // cssFolderPath
            // 
            this.cssFolderPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cssFolderPath.Location = new System.Drawing.Point(154, 148);
            this.cssFolderPath.Name = "cssFolderPath";
            this.cssFolderPath.Size = new System.Drawing.Size(272, 23);
            this.cssFolderPath.TabIndex = 10;
            this.cssFolderPath.Text = "fonts/";
            // 
            // defaultCSSFolderPath
            // 
            this.defaultCSSFolderPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultCSSFolderPath.Location = new System.Drawing.Point(432, 146);
            this.defaultCSSFolderPath.Name = "defaultCSSFolderPath";
            this.defaultCSSFolderPath.Size = new System.Drawing.Size(59, 23);
            this.defaultCSSFolderPath.TabIndex = 12;
            this.defaultCSSFolderPath.Text = "Default";
            this.defaultCSSFolderPath.UseVisualStyleBackColor = true;
            this.defaultCSSFolderPath.Click += new System.EventHandler(this.defaultCSSFolderPath_Click);
            // 
            // logo
            // 
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Image = global::GoogleFontDownloader.Properties.Resources.gfd;
            this.logo.Location = new System.Drawing.Point(12, 10);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(64, 64);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 13;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // clickCountLabel
            // 
            this.clickCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clickCountLabel.Location = new System.Drawing.Point(-2, 205);
            this.clickCountLabel.Name = "clickCountLabel";
            this.clickCountLabel.Size = new System.Drawing.Size(508, 18);
            this.clickCountLabel.TabIndex = 14;
            this.clickCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 221);
            this.Controls.Add(this.download);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.clickCountLabel);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.defaultCSSFolderPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cssFolderPath);
            this.Controls.Add(this.selectCSS);
            this.Controls.Add(this.selectFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.title);
            this.Controls.Add(this.cssURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Google Font Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button download;
        private System.Windows.Forms.TextBox cssURL;
        private System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button selectCSS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cssFolderPath;
        private System.Windows.Forms.Button defaultCSSFolderPath;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label clickCountLabel;
    }
}

