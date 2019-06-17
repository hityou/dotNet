using System;
using System.Xml;
namespace ConsoleApp1
{
    class Format
    {
        public string path = "format.xml";
    }
    class Program
    {
        string dataXml = "data.xml";
        static void Main(string[] args)
        {
            Format f = new Format();
            XmlDocument doc = new XmlDocument();
            doc.Load(f.path);
            XmlElement data = doc.DocumentElement;
            
            Console.WriteLine("Hello World!");
        }
    }
}
