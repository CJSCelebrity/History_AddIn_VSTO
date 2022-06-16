using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using Word = Microsoft.Office.Interop.Word;

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

        internal static void ScrapeWorldHistoryWeb(string URL)
        {
            try
            {
                //btnSearch.Visible = false;
                //progressBar1.Visible = true;

                ParseWorldHistoryHTML(URL);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void ParseWorldHistoryHTML(string url)
        {
            const string xPath = "//a[@class='font-weight-bold font-18']";
            const string childXPath = "//section[@data-level='1']";
            const string imgXPath = "//img";

            HtmlDocument parentDocument = CustomLoadHTML(url);

            //Load first initial page and scrape its data
            var htmlNodes = parentDocument.DocumentNode.SelectSingleNode(xPath);

            foreach (var node in parentDocument.DocumentNode.SelectNodes(xPath))
            {
                HtmlAttribute attribute = node.Attributes["href"];
                string newUrl = worldSearchHistorySite + attribute.Value;

                //Load second page and scrape its data.
                HtmlDocument childDocument = CustomLoadHTML(newUrl);

                #region Display text from HTML file onto the Word Document
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
                    childNode.InnerText.Replace("â€”", " ").Replace("Ã", " ").Replace("â€", " ").Replace("â€œ", " ");
                    rng.Text = childNode.InnerText;
                    rng.Select();
                }
                #endregion

                #region Add the images from the HTML page
                foreach (HtmlNode childNode in childDocument.DocumentNode.SelectNodes(imgXPath))
                {
                    HtmlAttribute imgAttribute = childNode.Attributes["src"];
                    Globals.ThisAddIn.Application.Selection.InlineShapes.AddPicture(imgAttribute.Value);
                }
                #endregion
            }
        }

        private static HtmlDocument CustomLoadHTML(string url) 
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
