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
            this.SuspendLayout();
            // 
            // download
            // 
            this.download.Location = new System.Drawing.Point(403, 138);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(78, 23);
            this.download.TabIndex = 0;
            this.download.Text = "Download";
            this.download.UseVisualStyleBackColor = true;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // cssURL
            // 
            this.cssURL.Location = new System.Drawing.Point(59, 77);
            this.cssURL.Name = "cssURL";
            this.cssURL.Size = new System.Drawing.Size(422, 20);
            this.cssURL.TabIndex = 1;
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(18, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(464, 51);
            this.title.TabIndex = 2;
            this.title.Text = "Google Font Downloader";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(59, 103);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(364, 20);
            this.folderPath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "CSS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder:";
            // 
            // selectFolder
            // 
            this.selectFolder.Location = new System.Drawing.Point(429, 103);
            this.selectFolder.Name = "selectFolder";
            this.selectFolder.Size = new System.Drawing.Size(52, 23);
            this.selectFolder.TabIndex = 6;
            this.selectFolder.Text = "Select";
            this.selectFolder.UseVisualStyleBackColor = true;
            this.selectFolder.Click += new System.EventHandler(this.selectFolder_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(17, 138);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(380, 23);
            this.progressBar.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 176);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.selectFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.title);
            this.Controls.Add(this.cssURL);
            this.Controls.Add(this.download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Google Font Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button download;
        private System.Windows.Forms.TextBox cssURL;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

