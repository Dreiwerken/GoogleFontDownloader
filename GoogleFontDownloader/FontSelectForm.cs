using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GoogleFontDownloader
{
    public partial class FontSelectForm : Form
    {
        TextBox cssURLBox;
        Dictionary<string, bool> fonts = new Dictionary<string, bool>();
        IOrderedEnumerable<GoogleFontEntryModel> familyMetadataList;

        public FontSelectForm(TextBox cssURL)
        {
            InitializeComponent();
            cssURLBox = cssURL;

            fontList.ItemCheck += FontList_ItemCheck;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Download font json
            using (var webClient = new WebClient())
            {
                GoogleFontsModel parsedfonts;
                webClient.Headers.Add("user-agent", Properties.Settings.Default.UserAgent);
                string rawFonts = webClient.DownloadString(Properties.Settings.Default.FontsURL);
                try
                {
                    parsedfonts = JsonConvert.DeserializeObject<GoogleFontsModel>(rawFonts);
                }
                catch
                {
                    string[] rawFontsArray = rawFonts.Split('\n');
                    rawFontsArray[0] = "";
                    rawFonts = string.Join("\n", rawFontsArray);
                    parsedfonts = JsonConvert.DeserializeObject<GoogleFontsModel>(rawFonts);
                }

                familyMetadataList = parsedfonts.familyMetadataList.OrderBy(f => f.defaultSort);
                foreach (var font in familyMetadataList)
                {
                    fonts.Add(font.family, false);
                }
            } 
            #endregion

            #region Read fonts from string
            Match match = Regex.Match(cssURLBox.Text, @"fonts\.googleapis\.com\/css\?family=(.+)$");
            if (match.Groups.Count == 2)
            {
                string[] fontStrings = match.Groups[1].ToString().Split('|');
                foreach (string item in fontStrings)
                {
                    string fontStr = item;
                    if (fontStr.Contains(":"))
                    {
                        fontStr = fontStr.Split(':').First();
                    }

                    fontStr = fontStr.Replace('+', ' ');

                    if (fonts.ContainsKey(fontStr))
                    {
                        fonts[fontStr] = true;
                    }
                }
            }
            #endregion

            #region Add fonts to font list
            fontList.Invoke((MethodInvoker)delegate
                {
                    fontList.Columns.Add("Fonts", fontList.Width - 22);
                    fontList.CheckBoxes = true;
                    int itemsCount = 0;
                    ListViewItem[] items = fonts
                                            .Where(i => string.IsNullOrEmpty(fontSearch.Text) || i.Key.StartsWith(fontSearch.Text))
                                            .Select(c => new ListViewItem(c.Key))
                                            .ToArray();

                    foreach (ListViewItem item in items)
                    {
                        item.Checked = fonts[item.Text];
                        items[itemsCount++] = item;
                    }

                    fontList.Items.AddRange(items);
                }); 
            #endregion
        }

        private void FontList_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            ListViewItem item = fontList.Items[e.Index];
            fonts[item.Text] = !item.Checked;
        }

        private void selectFont_Click(object sender, EventArgs e)
        {
            string url = Properties.Settings.Default.FontBaseURL;

            if(fontList.CheckedItems.Count <= 0)
            {
                MessageBox.Show("You must select at least one font", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach(ListViewItem font in fontList.CheckedItems)
            {
                url += font.Text.Replace(" ", "+") + "|";
            }

            cssURLBox.Text = url.Remove(url.Length - 1);
            this.Close();
        }

        private void fontSearch_TextChanged(object sender, EventArgs e)
        {
            fontList.Clear();
            fontList.Columns.Add("Fonts", fontList.Width - 22);
            fontList.CheckBoxes = true;
            int itemsCount = 0;
            ListViewItem[] items = fonts
                                    .Where(i => string.IsNullOrEmpty(fontSearch.Text) || i.Key.StartsWith(fontSearch.Text))
                                    .Select(c => new ListViewItem(c.Key))
                                    .ToArray();

            foreach(ListViewItem item in items)
            {
                item.Checked = fonts[item.Text];
                items[itemsCount++] = item;
            }

            fontList.Items.AddRange(items);
        }
    }
}
