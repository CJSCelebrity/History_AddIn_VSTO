using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Linq;

namespace HistoryAddIn
{
    public partial class SearchForTopics : Form,IDisposable
    {
        internal static readonly string logFileName = "MS_Word_History_Addin_Logs.txt";

        internal static readonly string directoryFolder = @"C:\ProgramData\MS_Word_History_Addin_Logs";

        public SearchForTopics()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtbxTopic_TextChanged(object sender, EventArgs e)
        {
            WriteToLog("Search Topic/Phrase:" + txtbxTopic.Text);
        }

        private void numSearchCounter_ValueChanged(object sender, EventArgs e)
        {
            WriteToLog("Search Counter:" + numSearchCounter.Value.ToString());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CreateLogDirectory();
            try
            { /*
               * CREATE TWO ALTERNATIVES TO SEARCH FOR WOLRD HISTORY AND SOUTH AFRICAN HISTORY
               */
                if (Convert.ToInt32(numSearchCounter.Value) < 1)
                {
                    MessageBox.Show("Please specify a value greater than 1 in the 'Search how many websites'", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteToLog("Search stopped due to invalid search numbers");
                    return;
                }
                else if (txtbxTopic.Text.Length == 0)
                {
                    MessageBox.Show("Please specify a topic to search for", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteToLog("Search stopped due to no search topic/phrase being specified");
                    return;
                }
                else 
                {
                    try
                    {
                        int counterValue = Convert.ToInt32(numSearchCounter.Value);

                        for (int i = 0; i < counterValue; i++)
                        {
                            if (checkBoxSouthAfrica.Checked)
                            {
                                Task.Run(() => ScrapeWeb($"https://www.sahistory.org.za/search?s={txtbxTopic.Text}#gsc.tab=0&gsc.q={txtbxTopic.Text}&gsc.page={counterValue}"));
                            }
                            else
                            {
                                Task.Run(() => ScrapeWeb($"https://www.britannica.com/search?query={txtbxTopic.Text}"));
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                   
                }

            }
            catch(Exception ex)
            {
                WriteToLog(ex.Message);
            }
            finally 
            {
                Dispose();
            }
        }

        internal void CreateLogDirectory() 
        {
            if (!Directory.Exists(directoryFolder)) 
            {
                Directory.CreateDirectory(directoryFolder);
            }
            
        }

        internal void WriteToLog(string text) 
        {
            File.WriteAllText(directoryFolder + logFileName, text);
        }

        private void ScrapeWeb(string URL)
        {
            try
            {
                btnSearch.Visible = false;
                progressBar1.Visible = true;

                var response = CallUrl(URL).Result;

                var linkList = ParseHTML(response);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<string> ParseHTML(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var programmerLinks = htmlDoc.DocumentNode.Descendants("li")
                    .Where(node => !node.GetAttributeValue("class", "").Contains("tocsection")).ToList();

            List<string> wikiLink = new List<string>();

            foreach (var link in programmerLinks)
            {
                if (link.FirstChild.Attributes.Count > 0)
                    wikiLink.Add("https://en.wikipedia.org/" + link.FirstChild.Attributes[0].Value);
            }

            return wikiLink;

        }
        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }
    }
}
