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
        bool initComplete = false;
        int fontTileCounter = 0;
        Color colorGreen = Color.FromArgb(76, 175, 80);
        Color colorYellow = Color.FromArgb(255, 160, 0);
        Color colorRed = Color.FromArgb(249, 86, 88);

        TextBox cssURLBox;
        Dictionary<string, GoogleFontEntryModel> fonts = new Dictionary<string, GoogleFontEntryModel>();

        public FontSelectForm(TextBox cssURL)
        {
            InitializeComponent();
            cssURLBox = cssURL;

            fontTree.CheckBoxes = true;
            fontTree.AfterCheck += FontTree_AfterCheck;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            initComplete = true;
        }

        private void FontTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!initComplete)
                return;

            CheckChildeNodeWhenParentChecked(e.Node, e.Node.Checked);

            if (e.Node.Name.Contains("/tile|"))
                CalcLoadTime(e.Node.Checked);

            CheckFontsInFontList(e.Node);
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

                foreach (GoogleFontEntryModel font in parsedfonts.familyMetadataList.OrderBy(f => f.defaultSort))
                {

                    foreach(string subset in font.subsets)
                    {
                        font.parsedSubsets.Add(subset, false);
                    }

                    fonts.Add(font.family, font);
                }


            } 
            #endregion

            #region Read fonts from string
            Match match = Regex.Match(cssURLBox.Text, @"fonts\.googleapis\.com\/css\?family=([a-zA-Z\|\+\:\,0-9]+)($|(&|&amp;)subset=([a-zA-Z\,\-]+))");
            if (match.Groups.Count >= 2)
            {
                string[] fontStrings = match.Groups[1].ToString().Split('|');
                string[] fontSubsets = (match.Groups.Count >= 4 ? match.Groups[4].ToString().Split(',') : null);

                foreach (string fontFullName in fontStrings)
                {
                    string fontName = "";
                    string[] fontTiles = null;
                
                    if (fontFullName.Contains(":"))
                    {
                        string[] fontParts = fontFullName.Split(':');
                        fontName = fontParts[0].Replace('+', ' ');
                        fontTiles = fontParts[1].Split(',');

                    }

                    if (fonts.ContainsKey(fontName))
                    {
                        GoogleFontEntryModel font = fonts[fontName];
                        font.selected = true;

                        if(fontTiles != null)
                        {
                            foreach(string fontTile in fontTiles)
                            {
                                if (font.fonts.ContainsKey(fontTile))
                                    font.fonts[fontTile].selected = true;
                            }
                        }

                        if(fontSubsets != null)
                        {
                            foreach (string fontSubset in fontSubsets)
                            {
                                if (font.parsedSubsets.ContainsKey(fontSubset))
                                    font.parsedSubsets[fontSubset] = true;
                            }
                        }

                        fonts[fontName] = font;
                    }
                }
            }
            #endregion
            
            foreach (var font in fonts)
            {
                string key = font.Key.Replace(' ', '+');
                fontTree.Invoke((MethodInvoker)delegate
                {
                    TreeNode node = fontTree.Nodes.Add(key, font.Key);
                    
                    //check
                    if(font.Value.selected)
                    {
                        node.Checked = true;
                    }

                    if (font.Value.fonts.Count > 1)
                    {
                        TreeNode subNode = node.Nodes.Add(key + "/selectAllTile", "Types");
                        foreach (var fontTile in font.Value.fonts)
                        {
                            if(fontTile.Value != null && fontTile.Value.selected)
                            {
                                CalcLoadTime(true);
                            }

                            subNode.Nodes.Add(key + "/tile|" + fontTile.Key, fixFontTileName(fontTile.Key)).Checked = (fontTile.Value == null ? false : fontTile.Value.selected);
                        }
                    }

                    if (font.Value.subsets.Count > 1)
                    {
                        TreeNode subNode = node.Nodes.Add(key + "/selectAllSubset", "Languages");
                        foreach (var subset in font.Value.parsedSubsets)
                        {
                            if (subset.Key == "menu")
                                continue;

                            if (subset.Value)
                            {
                                CalcLoadTime(true);
                            }
                            subNode.Nodes.Add(key + "/subset|" + subset.Key, fixSubsetName(subset.Key)).Checked = subset.Value;
                        }
                    }
                });
            }
        }

        public string fixSubsetName(string subset)
        {
            subset = ucfirst(subset);
            subset = subset.Replace("-ext", " Extended");
            return subset;
        }

        public string fixFontTileName(string fontTile)
        {
            var type = (fontTile.Contains("i") ? fontTile.Remove(fontTile.Length - 1) : fontTile);

            if (fontTile.Contains("i"))
                fontTile = fontTile.Replace("i", " Italic");

            switch (type)
            {
                case "100":
                    fontTile = "Thin " + fontTile;
                    break;
                case "200":
                    fontTile = "Extra light" + fontTile;
                    break;
                case "300":
                    fontTile = "Light " + fontTile;
                    break;
                case "400":
                    fontTile = "Regular " + fontTile;
                    break;
                case "500":
                    fontTile = "Medium " + fontTile;
                    break;
                case "600":
                    fontTile = "Semi bold " + fontTile;
                    break;
                case "700":
                    fontTile = "Bold " + fontTile;
                    break;
                case "800":
                    fontTile = "Extra bold " + fontTile;
                    break;
                case "900":
                    fontTile = "Black " + fontTile;
                    break;
            }

            

            return fontTile;
        }

        public string ucfirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void CheckChildeNodeWhenParentChecked(TreeNode node, Boolean isChecked)
        {
            if (node.Name != "" || !node.Name.EndsWith("/selectAllTile") || !node.Name.EndsWith("/selectAllSubset"))
                return; 

            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    this.CheckChildeNodeWhenParentChecked(item, isChecked);
                }
            }
        }

        private void CheckFontsInFontList(TreeNode node)
        {
            if (node.Name == "" || node.Name.EndsWith("/selectAllTile") || node.Name.EndsWith("/selectAllSubset"))
                return;

            string[] fontNameParts = node.Name.Split('/');
            string fontName = fontNameParts[0].Replace('+', ' ');
            string fontExtra = (fontNameParts.Count() == 2 ? fontNameParts[1] : null);

            if(fontExtra == null)
            {
                string fontCleanName = fontName.Replace(' ', '+');
                fonts[fontName].selected = node.Checked;
                if (fonts[fontName].fonts.Count() > 1 && fonts[fontName].fonts.ContainsKey("400"))
                {
                    fonts[fontName].fonts["400"].selected = true;
                    node.Nodes[fontCleanName + "/selectAllTile"].Nodes[fontCleanName + "/tile|400"].Checked = true;
                }

                if (fonts[fontName].parsedSubsets.Count() > 0 && fonts[fontName].parsedSubsets.ContainsKey("latin"))
                {
                    fonts[fontName].parsedSubsets["latin"] = true;
                    node.Nodes[fontCleanName + "/selectAllSubset"].Nodes[fontCleanName + "/subset|latin"].Checked = true;
                }
            }
            else
            {
                string[] fontExtraParts = fontExtra.Split('|');
                switch(fontExtraParts[0])
                {
                    case "tile":
                        fonts[fontName].fonts[fontExtraParts[1]].selected = node.Checked;
                        break;
                    case "subset":
                        fonts[fontName].parsedSubsets[fontExtraParts[1]] = node.Checked;
                        break;
                }
            }
        }

        private void CalcLoadTime(bool check)
        {
            if (check)
                fontTileCounter++;
            else
                fontTileCounter--;

            if(fontTileCounter >= 6)
            {
                loadTimeLabel.BackColor = colorRed;
                loadTimeLabel.Text = "Slow";
            }
            else if (fontTileCounter >= 3)
            {
                loadTimeLabel.BackColor = colorYellow;
                loadTimeLabel.Text = "Moderate";
            }
            else if (fontTileCounter >= 0)
            {
                loadTimeLabel.BackColor = colorGreen;
                loadTimeLabel.Text = "Fast";
            }
        }

        private void selectFont_Click(object sender, EventArgs e)
        {
            List<string> fontNames = new List<string>();
            List<string> subsets = new List<string>();

            foreach(var font in fonts)
            {
                if (!font.Value.selected)
                    continue;

                string fontName = font.Key.Replace(' ', '+');
                string fontTile = "";

                #region Read font tile
                foreach (var tile in font.Value.fonts)
                {
                    if (tile.Value.selected)
                    {
                        fontTile += tile.Key + ',';
                    }
                }

                if (fontTile != "")
                {
                    fontTile = fontTile.Remove(fontTile.Length - 1);
                }
                #endregion

                #region Read subset
                foreach (var subset in font.Value.parsedSubsets)
                {
                    if (!subsets.Contains(subset.Key) && subset.Value)
                        subsets.Add(subset.Key);
                }
                #endregion

                fontNames.Add(fontName + (fontTile != "" ? ":" + fontTile : ""));
            }

            cssURLBox.Text = Properties.Settings.Default.FontBaseURL + String.Join("|", fontNames) + (subsets.Count > 0 ? "&subset=" + String.Join(",", subsets) : "") ;
            this.Close();
        }
    }
}
