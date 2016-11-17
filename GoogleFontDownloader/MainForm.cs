using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace GoogleFontDownloader
{
    public partial class MainForm : Form
    {
        private string[] userAgents = new string[] {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36", // woff2
            "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"                                               // woff
        };

        public MainForm()
        {
            InitializeComponent();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
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

                    css = css.Replace(font[1], "fonts/" + saveName);
                    webClient.DownloadFile(font[1], fontPath + saveName);


                    progressBar.Invoke((MethodInvoker)delegate
                    {
                        progressBar.Value += 1;
                    });
                }
            }

            File.WriteAllText(folderPath.Text + "/fonts.css", css);
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
    }
}
