using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GoogleFontDownloader
{
    public partial class MainForm : Form
    {
        Timer rotationTimer = new Timer();
        private string[] userAgents = new string[] {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36", // woff2
            "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"                                               // woff
        };

        public MainForm()
        {
            InitializeComponent();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            cssURL.MouseDoubleClick += CssURL_MouseDoubleClick;
            folderPath.MouseDoubleClick += FolderPath_MouseDoubleClick;
            rotationTimer.Interval = 150;
            rotationTimer.Tick += rotationTimer_Tick;

            if (Properties.Settings.Default.lastCSSFolderPath != "")
                cssFolderPath.Text = Properties.Settings.Default.lastCSSFolderPath;
        }

        private void FolderPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(folderPath.Text.Trim() != "")
                Process.Start(folderPath.Text);
        }

        private void CssURL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cssURL.Text.Contains(Properties.Settings.Default.FontBaseURL))
                Process.Start(cssURL.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            folderPath.Text = (Properties.Settings.Default.lastFolder != "" ? Properties.Settings.Default.lastFolder : Application.StartupPath);
        }

        private void selectFolder_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowser.ShowDialog();

            if(res == DialogResult.OK)
            {
                folderPath.Text = folderBrowser.SelectedPath;
                Properties.Settings.Default.lastFolder = folderBrowser.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void download_Click(object sender, EventArgs e)
        {
            cssURL.Text = cssURL.Text.Trim();
            folderPath.Text = folderPath.Text.Trim();
     
            if (cssURL.Text == "")
            {
                MessageBox.Show("CSS url must not be empty!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(!cssURL.Text.Contains("fonts.googleapis.com/css?family="))
            {
                MessageBox.Show("The specified URL is invalid", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (folderPath.Text == "")
            {
                MessageBox.Show("Folder path must not be empty!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!Directory.Exists(folderPath.Text))
            {
                MessageBox.Show("Folder must be exist!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            progressBar.Invoke((MethodInvoker)delegate
            {
                progressBar.Value = 0;
            });

            Properties.Settings.Default.lastCSSFolderPath = cssFolderPath.Text;
            Properties.Settings.Default.Save();

            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string css = String.Empty;

            using (var webClient = new WebClient())
            {
                foreach (string ua in userAgents)
                {
                    webClient.Headers.Add("user-agent", ua);
                    css += webClient.DownloadString(cssURL.Text);
                }

                webClient.Headers.Add("user-agent", userAgents[0]);

                var matchs = Regex.Matches(css, @"local\('([a-zA-Z0-9_-]+)'\), url\((.+)\) format")
                    .Cast<Match>()
                    .Select(m => new List<string>() { m.Groups[1].Value, m.Groups[2].Value })
                    .ToArray();

                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Maximum = matchs.Count();
                    progressBar.Value = 0;
                });

                try
                {
                    string fontPath = folderPath.Text + "/fonts/";
                    if (Directory.Exists(fontPath))
                    {
                        Directory.Delete(fontPath, true);
                    }

                    Directory.CreateDirectory(fontPath);
                    File.Delete(folderPath.Text + "/fonts.css");

                    foreach (var font in matchs)
                    {
                        string fontExt = Path.GetExtension(font[1]);
                        string fontName = font[0].Replace(" ", "").Replace("-", "");
                        string saveName = fontName + fontExt;
                        int count = 0;

                        while (File.Exists(fontPath + saveName))
                        {
                            saveName = fontName + count++ + fontExt;
                        }

                        css = css.Replace(font[1], cssFolderPath + saveName);

                        if (File.Exists(fontPath + saveName))
                            File.Delete(fontPath + saveName);

                        webClient.DownloadFile(font[1], fontPath + saveName);

                        progressBar.Invoke((MethodInvoker)delegate
                        {
                            progressBar.Value += 1;
                        });
                    }

                    File.WriteAllText(folderPath.Text + "/fonts.css", css);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Download complete!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void selectCSS_Click(object sender, EventArgs e)
        {
            Form selectFontForm = new FontSelectForm(cssURL);
            selectFontForm.Show();
        }

        private void defaultCSSFolderPath_Click(object sender, EventArgs e)
        {
            cssFolderPath.Text = "fonts/";
            Properties.Settings.Default.lastCSSFolderPath = "";
            Properties.Settings.Default.Save();
        }

        #region EasterEgg
        int logoClickCount = 0;
        int moveStep = 2;
        Image orginalImage;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            copyright.Text += " . ";
            if (++logoClickCount == 3)
            {
                orginalImage = logo.Image;
                rotationTimer.Start();
            }
        }

        private void rotationTimer_Tick(object sender, EventArgs e)
        {
            Image flipImage = logo.Image;
            flipImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
            logo.Image = flipImage;

            if (rotationTimer.Interval > 1)
            {
                rotationTimer.Interval -= 1;
            }
            else
            {
                if (this.logo.Location.X > this.Width)
                {
                    moveStep = -6;
                }
                else if(logo.Location.X < logo.Width * -1)
                {
                    moveStep = 1;
                }
                else if(logo.Location.X == 12 && moveStep == 1)
                {
                    rotationTimer.Stop();
                    flipImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    logo.Image = flipImage;
                    moveStep = 2;
                }

                this.logo.Location = new System.Drawing.Point(this.logo.Location.X + moveStep, 10);
            }
        }
        #endregion

        private void copyright_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dreiwerken.de/");
        }
    }
}
