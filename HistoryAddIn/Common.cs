using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;

namespace HistoryAddIn
{
    public static class Common
    {
        internal static readonly string logFileName = "MS_Word_History_Addin_Logs.txt";

        internal static readonly string directoryFolder = @"C:\ProgramData\MS_Word_History_Addin_Logs";

        internal static readonly string worldSearchHistorySite = "https://www.britannica.com/";

        internal static readonly string southAfricanHistorySite = "https://www.sahistory.org.za/";

        internal static void CreateLogDirectory()
        {
            if (!Directory.Exists(directoryFolder))
            {
                Directory.CreateDirectory(directoryFolder);
            }

        }

        internal static void WriteToLog(string text)
        {
            File.WriteAllText(directoryFolder + logFileName, text);
        }

        internal static void ScrapeSouthAfricanHistoryWeb(string URL)
        {
            try
            {
                //btnSearch.Visible = false;
                //progressBar1.Visible = true;

                var response = CallUrl(URL).Result;

                var linkList = ParseSouthAfricanHistoryHTML(response);

            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static void ScrapeWorldHistoryWeb(string URL)
        {
            try
            {
                //btnSearch.Visible = false;
                //progressBar1.Visible = true;

                //var response = CallUrl(URL).Result;

                ParseWorldHistoryHTML(URL);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static List<string> ParseSouthAfricanHistoryHTML(string html)
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

        private static void ParseWorldHistoryHTML(string url)
        {
            const string xPath = "//a[@class='font-weight-bold font-18']";
            const string childXPath = "//section[@data-level='1']";

            HtmlDocument parentDocument = LoadHtml(url);

            //Load first initial page and scrape its data
            var htmlNodes = parentDocument.DocumentNode.SelectSingleNode(xPath);

            foreach (var node in parentDocument.DocumentNode.SelectNodes(xPath))
            {
                HtmlAttribute attribute = node.Attributes["href"];
                string newUrl = worldSearchHistorySite + attribute.Value;

                //Load second page and scrape its data.
                HtmlDocument childDocument = LoadHtml(newUrl);

                foreach (HtmlNode childNode in childDocument.DocumentNode.SelectNodes(childXPath))
                {
                    var fileName = "HTML_For_" + attribute.Value + ".html";
                    string directory = @"C:\History_Addin_HTML_Data\";

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (!File.Exists(fileName))
                    {
                        //using (StreamWriter streamWriter = File.CreateText(fileName)) 
                        //{
                        //    streamWriter.WriteLine()
                        //}
                    }
                    //Specifies a range of the word document so the text can be populated
                    Word.Range rng = Globals.ThisAddIn.Application.ActiveDocument.Range(0, 0);
                    rng.Text = childNode.InnerText;
                    rng.Select();
                }
            }
        }

        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }
        private static HtmlDocument LoadHtml(string url) 
        {
            try
            {
                WebClient webClient = new WebClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                string html = webClient.DownloadString(url);

                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                return document;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
