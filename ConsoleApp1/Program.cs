using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Http;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
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
            /*string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=true; AttachDbFileName=C:\\REPOS\\.NET\\CORESQL.MDF;";
            SqlConnection conn = new SqlConnection(connStr);
conn.Open();
string qStr = "select * from paylog";
SqlCommand cmd = new SqlCommand(qStr, conn);
SqlDataReader reader = cmd.ExecuteReader();
while(reader.Read())
Console.WriteLine(reader[1].ToString());
reader.Close();
conn.Close();*/
        }

        static void GetPlayers()
        {
            /*var url = "https://localhost:5001/Logs/GetPlayer";
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
            }*/
            Console.WriteLine("Hello World!");
            string queryStr = "Select * from pay";
/*string connStr = "Data Source=(local)\\SQLEXPRESS;AttachDbFilename=D:\\dev\\.Net\\ConsoleApp1\\mssql.mdf;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            
            SqlCommand sqlcmd = new SqlCommand(queryStr, conn);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            Console.WriteLine(reader[0].ToString());
            sqlcmd.Clone();
            conn.Close();*/

            SqliteConnection sqliteconn = new SqliteConnection("Data Source=sqlite.db");
            sqliteconn.Open();
            SqliteCommand sqliteCmd = new SqliteCommand(queryStr, sqliteconn);
            SqliteDataReader sqliteReader = sqliteCmd.ExecuteReader();
            while (sqliteReader.Read())
            Console.WriteLine("sqlite "+sqliteReader[0].ToString()+sqliteReader[1].ToString());
            sqliteReader.Close();
            sqliteconn.Close();
            /*string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=true; AttachDbFileName=C:\\REPOS\\.NET\\CORESQL.MDF;";
            SqlConnection conn = new SqlConnection(connStr);
conn.Open();
string qStr = "select * from paylog";
SqlCommand cmd = new SqlCommand(qStr, conn);
//SqlDataReader reader = cmd.ExecuteReader();
while(reader.Read())
Console.WriteLine(reader[1].ToString());
reader.Close();
conn.Close();*/
        }

    }
}
