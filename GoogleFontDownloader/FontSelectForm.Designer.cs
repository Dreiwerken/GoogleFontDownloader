namespace GoogleFontDownloader
{
    partial class FontSelectForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontSelectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.selectFont = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.fontSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loadTimeLabel = new System.Windows.Forms.Label();
            this.resetSelect = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fontTree = new GoogleFontDownloader.TriStateTreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Font Search:";
            // 
            // selectFont
            // 
            this.selectFont.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFont.Location = new System.Drawing.Point(426, 288);
            this.selectFont.Name = "selectFont";
            this.selectFont.Size = new System.Drawing.Size(138, 34);
            this.selectFont.TabIndex = 3;
            this.selectFont.Text = "Select Font";
            this.selectFont.UseVisualStyleBackColor = true;
            this.selectFont.Click += new System.EventHandler(this.selectFont_Click);
            // 
            // fontSearch
            // 
            this.fontSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontSearch.Location = new System.Drawing.Point(98, 7);
            this.fontSearch.Name = "fontSearch";
            this.fontSearch.Size = new System.Drawing.Size(350, 25);
            this.fontSearch.TabIndex = 5;
            this.fontSearch.TextChanged += new System.EventHandler(this.fontSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Load Time:";
            // 
            // loadTimeLabel
            // 
            this.loadTimeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.loadTimeLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadTimeLabel.Location = new System.Drawing.Point(94, 295);
            this.loadTimeLabel.Name = "loadTimeLabel";
            this.loadTimeLabel.Size = new System.Drawing.Size(100, 20);
            this.loadTimeLabel.TabIndex = 8;
            this.loadTimeLabel.Text = "Fast";
            this.loadTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.loadTimeLabel, "Load times are estimates and can vary from user to user.");
            // 
            // resetSelect
            // 
            this.resetSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetSelect.Location = new System.Drawing.Point(454, 7);
            this.resetSelect.Name = "resetSelect";
            this.resetSelect.Size = new System.Drawing.Size(110, 25);
            this.resetSelect.TabIndex = 9;
            this.resetSelect.Text = "Reset selection";
            this.resetSelect.UseVisualStyleBackColor = true;
            this.resetSelect.Click += new System.EventHandler(this.resetSelect_Click);
            // 
            // fontTree
            // 
            this.fontTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fontTree.Enabled = false;
            this.fontTree.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontTree.Location = new System.Drawing.Point(12, 38);
            this.fontTree.Name = "fontTree";
            this.fontTree.Size = new System.Drawing.Size(552, 240);
            this.fontTree.TabIndex = 6;
            this.fontTree.TriStateStyleProperty = GoogleFontDownloader.TriStateTreeView.TriStateStyles.GoogleFontDownloader;
            // 
            // FontSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 330);
            this.Controls.Add(this.resetSelect);
            this.Controls.Add(this.loadTimeLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fontTree);
            this.Controls.Add(this.fontSearch);
            this.Controls.Add(this.selectFont);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FontSelectForm";
            this.Text = "GoogleFontDownloader: Font select";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectFont;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TextBox fontSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label loadTimeLabel;
        private TriStateTreeView fontTree;
        private System.Windows.Forms.Button resetSelect;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}