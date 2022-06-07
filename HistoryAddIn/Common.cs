using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HistoryAddIn
{
    public static class Common
    {
        internal static readonly string logFileName = "MS_Word_History_Addin_Logs.txt";

        internal static readonly string directoryFolder = @"C:\ProgramData\MS_Word_History_Addin_Logs";

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

        internal static void ScrapeWeb(string URL)
        {
            try
            {
                //btnSearch.Visible = false;
                //progressBar1.Visible = true;

                var response = CallUrl(URL).Result;

                var linkList = ParseHTML(response);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<string> ParseHTML(string html)
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
