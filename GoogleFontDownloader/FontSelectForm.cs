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
            fontTree.Enabled = true;
            loading.Visible = false;
        }

        private void FontTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!initComplete)
                return;

            CheckParent(e.Node);
            CheckFontsInFontList(e.Node);
            CalcLoadTime();
        }

        private void CheckParent(TreeNode node)
        {
            if(node.Parent != null)
            {
                if(node.Checked)
                {
                    if(!node.Parent.Checked)
                        node.Parent.Checked = true;
                }

                // Uncheck parent if all childs uncheckd
                foreach (TreeNode child in node.Parent.Nodes)
                {
                    if (child.Checked)
                        return;
                }

                if (node.Parent.Checked)
                    node.Parent.Checked = false;
            }
            else if(node.Parent == null && !node.Checked)
            {
                foreach (TreeNode child in node.Nodes)
                    child.Checked = false;
            }
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

                    // parse fontTiles 
                    foreach (var fontTile in font.fonts)
                        font.fontTiles[fontTile.Key] = false;

                    fonts.Add(font.family, font);
                }
            } 
            #endregion

            #region Read fonts from string
            Match match = Regex.Match(cssURLBox.Text, @"fonts\.googleapis\.com\/css\?family=([a-zA-Z\|\+\:\,0-9]+)");
            if (match.Groups.Count >= 2)
            {
                string[] fontStrings = match.Groups[1].ToString().Split('|');

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
                    else
                    {
                        fontName = fontFullName.Replace('+', ' ');
                    }

                    if (fonts.ContainsKey(fontName))
                    {
                        GoogleFontEntryModel font = fonts[fontName];
                        font.selected = true;

                        if(fontTiles != null)
                        {
                            foreach(string fontTile in fontTiles)
                            {
                                if (font.fontTiles.ContainsKey(fontTile))
                                    font.fontTiles[fontTile] = true;
                            }
                        }

                        fonts[fontName] = font;
                    }
                }
            }
            #endregion

            buildFontTree();
            CalcLoadTime();
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

        private void CheckFontsInFontList(TreeNode node)
        {
            if (node != null && node.Name == "")
                return;

            string[] fontNameParts = node.Name.Split('/');
            string fontName = fontNameParts[0].Replace('+', ' ');
            string fontExtra = (fontNameParts.Count() == 2 ? fontNameParts[1] : null);

            if (fontExtra == null)
            {
                string fontCleanName = fontName.Replace(' ', '+');

                if (fonts[fontName].selected != node.Checked)
                    fonts[fontName].selected = node.Checked;

                if (fonts[fontName].fontTiles.Count() > 1 && fonts[fontName].fontTiles.ContainsKey("400"))
                {
                    if (fonts[fontName].fontTiles["400"] != node.Checked)
                        fonts[fontName].fontTiles["400"] = true;

                    if (node.Nodes[fontCleanName + "/tile|400"] != null && node.Nodes[fontCleanName + "/tile|400"].Checked != node.Checked)
                        node.Nodes[fontCleanName + "/tile|400"].Checked = true;
                }
            }
            else
            {
                string[] fontExtraParts = fontExtra.Split('|');
                switch (fontExtraParts[0])
                {
                    case "tile":
                        if(fonts[fontName].fontTiles[fontExtraParts[1]] != node.Checked)
                            fonts[fontName].fontTiles[fontExtraParts[1]] = node.Checked;
                        break;
                }
            }
        }

        private void CalcLoadTime(bool check = true)
        {
            if(check)
            {
                fontTileCounter = 0;
                foreach (var font in fonts)
                {
                    bool fontTileFound = false;
                    foreach (var fontTile in font.Value.fontTiles)
                    {
                        if (fontTile.Value)
                        {
                            fontTileCounter++;
                            fontTileFound = true;
                        }
                    }
                
                    if(!fontTileFound && font.Value.selected)
                    {
                        fontTileCounter++;
                    }
                }
            }

            if (loadTimeLabel.InvokeRequired)
            {
                loadTimeLabel.Invoke(((MethodInvoker)delegate
                {
                    if (fontTileCounter >= 6)
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
                }));
                return;
            }

            if (fontTileCounter >= 6)
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
                foreach (var tile in font.Value.fontTiles)
                {
                    if (tile.Value)
                    {
                        fontTile += tile.Key + ',';
                    }
                }

                if (fontTile != "")
                {
                    fontTile = fontTile.Remove(fontTile.Length - 1);
                }
                #endregion
                
                fontNames.Add(fontName + (fontTile != "" ? ":" + fontTile : ""));
            }

            if (fontNames.Count > 0)
                cssURLBox.Text = Properties.Settings.Default.FontBaseURL + String.Join("|", fontNames) + (subsets.Count > 0 ? "&subset=" + String.Join(",", subsets) : "");
            else
                cssURLBox.Text = "";

            this.Close();
        }

        private void fontSearch_TextChanged(object sender, EventArgs e)
        {
            buildFontTree(fontSearch.Text);
        }

        private void buildFontTree(string filter = "")
        {
            fontTree.Nodes.Clear();
            foreach (var font in fonts)
            {
                string key = font.Key.Replace(' ', '+');

                if (filter != "" && !font.Value.family.ToLower().Contains(filter.ToLower()))
                    continue;

                if(fontTree.InvokeRequired)
                {
                    fontTree.Invoke((MethodInvoker)delegate
                    {
                        TreeNode node = fontTree.Nodes.Add(key, font.Key);

                        //check
                        if (font.Value.selected)
                        {
                            node.Checked = true;
                        }

                        fontTree.UpdateParentState(node);

                        if (font.Value.fontTiles.Count > 1)
                        {
                            foreach (var fontTile in font.Value.fontTiles)
                            {
                                TreeNode subNode = node.Nodes.Add(key + "/tile|" + fontTile.Key, fixFontTileName(fontTile.Key));
                                fontTree.UpdateParentState(subNode);

                                if (subNode.Checked != fontTile.Value)
                                    subNode.Checked = fontTile.Value;
                            }
                        }
                    });
                }
                else
                {
                    TreeNode node = fontTree.Nodes.Add(key, font.Key);

                    //check
                    if (font.Value.selected)
                    {
                        node.Checked = true;
                    }

                    fontTree.UpdateParentState(node);

                    if (font.Value.fontTiles.Count > 1)
                    {
                        foreach (var fontTile in font.Value.fontTiles)
                        {
                            TreeNode subNode = node.Nodes.Add(key + "/tile|" + fontTile.Key, fixFontTileName(fontTile.Key));
                            fontTree.UpdateParentState(subNode);

                            if (subNode.Checked != fontTile.Value)
                                subNode.Checked = fontTile.Value;
                        }
                    }
                }            
            }

        }

        private void resetSelect_Click(object sender, EventArgs e)
        {
            foreach (var font in fonts)
            {
                if (font.Value.fontTiles.Count > 1)
                    foreach (var fontTile in font.Value.fontTiles)
                    {
                        font.Value.fontTiles[fontTile.Key] = false;
                    }

                fonts[font.Key].selected = false;
            }

            if(fontSearch.Text != "")
            {
                fontSearch.Text = "";
            }
            else
            {
                buildFontTree();
            }

            fontTileCounter = 0;
            CalcLoadTime(false);
        }
    }
}
