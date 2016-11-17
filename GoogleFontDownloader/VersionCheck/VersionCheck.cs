using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;
using System.Windows.Forms;

namespace GoogleFontDownloader
{
    class VersionCheck
    {
        public static void CheckForUpdate()
        {
            using (var client = new WebClient())
            {
                string AppName = "GoogleFontDownloader";
                string realaseURL = Properties.Settings.Default["ReleaseURL"].ToString();
                string realaseAPI = Properties.Settings.Default["ReleaseAPI"].ToString();
                string userAgent = Properties.Settings.Default["UserAgent"].ToString();
                string AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                client.Headers.Add("user-agent", userAgent);

                try
                {
                    string json = client.DownloadString(realaseAPI);
                    IList<GithubReleaseModel> model = JsonConvert.DeserializeObject<IList<GithubReleaseModel>>(json);
                    if (model.Count > 0 && model[0].tag_name != AppVersion)
                    {
                        string text = $"Your {AppName} version is outdated.";

                        if (model[0].assets.Count > 0)
                        {
                            text += $" Click on \"Yes\" to download the new version {model[0].tag_name}.";
                            realaseURL = model[0].assets[0].browser_download_url;
                        }
                        else
                        {
                            text += " Click on \"Yes\" to open the github release page.";
                        }

                        DialogResult dialogResult = MessageBox.Show(text, AppName, MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(realaseURL);
                        }
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Version check failed:\n" + ex.ToString());
                }
            }
        }
    }
}
