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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontSelectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.selectFont = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.fontSearch = new System.Windows.Forms.TextBox();
            this.fontList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Font Search:";
            // 
            // selectFont
            // 
            this.selectFont.Location = new System.Drawing.Point(426, 284);
            this.selectFont.Name = "selectFont";
            this.selectFont.Size = new System.Drawing.Size(138, 23);
            this.selectFont.TabIndex = 3;
            this.selectFont.Text = "Select Font";
            this.selectFont.UseVisualStyleBackColor = true;
            this.selectFont.Click += new System.EventHandler(this.selectFont_Click);
            // 
            // fontSearch
            // 
            this.fontSearch.Location = new System.Drawing.Point(83, 7);
            this.fontSearch.Name = "fontSearch";
            this.fontSearch.Size = new System.Drawing.Size(481, 20);
            this.fontSearch.TabIndex = 5;
            this.fontSearch.TextChanged += new System.EventHandler(this.fontSearch_TextChanged);
            // 
            // fontList
            // 
            this.fontList.FullRowSelect = true;
            this.fontList.GridLines = true;
            this.fontList.LabelEdit = true;
            this.fontList.Location = new System.Drawing.Point(12, 33);
            this.fontList.Name = "fontList";
            this.fontList.Size = new System.Drawing.Size(552, 230);
            this.fontList.TabIndex = 0;
            this.fontList.UseCompatibleStateImageBehavior = false;
            this.fontList.View = System.Windows.Forms.View.Details;
            // 
            // FontSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 319);
            this.Controls.Add(this.fontSearch);
            this.Controls.Add(this.selectFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fontList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.ListView fontList;
    }
}