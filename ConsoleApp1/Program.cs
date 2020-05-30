using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Http;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GetPlayers();

            string formatXml = "format.xml";
            string dataXml = "data.xml";
            string dataTxt = "data.txt";

            XmlDocument formatDoc = new XmlDocument();
            formatDoc.Load(formatXml);
            XmlNodeList cols = formatDoc.DocumentElement.GetElementsByTagName("Daily");

            XmlDocument dataDoc = new XmlDocument();
            dataDoc.Load(dataXml);
            XmlNodeList rows = dataDoc.DocumentElement.GetElementsByTagName("Row");

            List<string> lines = new List<string>(rows.Count);
            foreach(XmlNode row in rows)
            {
                string line = "";
                
                foreach (XmlNode col in cols[0].ChildNodes)
                {
                    string text = row[col["xml"].InnerText].InnerText;
                    int length = Convert.ToInt32(col["length"].InnerText);

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding big5 = Encoding.GetEncoding(950);
                    byte[] bytes = big5.GetBytes(text);

                    int diff = bytes.Length - text.Length;

                    string alignText = text.PadLeft(length - diff);
                    line += alignText.Substring(0, length - diff);
                    
                }
                lines.Add(line);
            }          
            
            using (StreamWriter sw = new StreamWriter(dataTxt))
            {
                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                    Console.WriteLine(line);
                }
            }
            
        }

        static void GetPlayers()
        {
            var url = "https://localhost:5001/Logs/GetPlayer";
            using (HttpClient hc = new HttpClient())
            {
                try
                {
                    hc.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage hrm = hc.GetAsync(url).Result;
                    string body = hrm.Content.ReadAsStringAsync().Result;
Console.WriteLine(body);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("exception "+e.Message);
                }
            }
        }
    }
}
