using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;

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
            catch (Exception)
            {

                throw;
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
            

            object missing = System.Reflection.Missing.Value;

            //Create a new word document object
            //Word.Document doc = app.Documents.Add();

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
                    SelectionInsertText();
                    //Specifies a range of the word document so the text can be populated
                    //Word.Range rng = app.ActiveDocument.Range(0, 0);
                    //rng.Text = "New Text";
                    //rng.Select();
                    //range.Text
                    //wordDocument.Content.SetRange(0, 0);
                    //wordDocument.Content.Text = childNode.InnerHtml + Environment.NewLine;
                    //Console.WriteLine(childNode);
                }
            }
        }

        private static void SelectionInsertText()
        {
            Document document = new Document();
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph();

            TextBox textBox = paragraph.AppendTextBox(180, 30);
            textBox.Format.VerticalOrigin = VerticalOrigin.Margin;
            textBox.Format.VerticalPosition = 100;
            textBox.Format.HorizontalOrigin = HorizontalOrigin.Margin;
            textBox.Format.HorizontalPosition = 50;
            textBox.Format.NoLine = true;

            CharacterFormat format = new CharacterFormat(document);
            format.FontName = "Calibri";
            format.FontSize = 15;
            format.Bold = true;

            Paragraph paragraph1 = textBox.Body.AddParagraph();
            paragraph1.AppendText("This is my new document").ApplyCharacterFormat(format);
            document.SaveToFile("result.docx", FileFormat.Docx);
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
