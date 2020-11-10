using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

namespace CHB
{
    class Program
    {
        public Form m_f;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Program p = new Program();

            Application.Run(p.m_f);

            Console.Write("helloooooooo");
        }

        Program()
        {
            m_f = new Form();
            InitializeComponent(m_f);

            WebBrowser wb = m_f.Controls["webBrowser1"] as WebBrowser;

        /*string docText = "<html><body>Please enter your name:<br/>" +
        "<input type='text' name='userName'/><br/>" +
        "<a href='http://www.microsoft.com'>continue</a>" +
        "<table ID=\"dgv\" width=\"100%\">holder</table>" +
        "</body></html>";
*/
            /*string docText = wb.Document;
            string cells = "<tr><td>12345678</td><td>12345678</td></tr>";
            cells += "<tr><td>abc</td><td>def</td></tr>";*/

//docText = docText.Replace("holder", cells);

//wb.DocumentText =docText;
//wb.Refresh();


                //wb.DocumentText = docText.Replace("holder", cells);
            /*DataSet ds = new DataSet();
            ds.ReadXml("xml/CHK.xml");
            DataTable dt = ds.Tables[0];

            DataGridView dgv = m_f.Controls["dataGridView1"] as DataGridView;
            dgv.DataSource = dt;*/
        }

        private void htmCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = m_f.Controls["webBrowser1"] as WebBrowser;
 
wb.Document.All["btn-query"].Click += new HtmlElementEventHandler(queryClick);
            //wb.Document.All["dgv"].SetAttribute("class", "table table-bordered");
            {
                HtmlElement tableRow = null;
DataSet ds = new DataSet();
            ds.ReadXml("xml/CHK.xml");
            DataTable dt = ds.Tables[0];
        
        HtmlElement tableColumn = null;
        HtmlElement tableSelect = null;
        /*HtmlElement fn = wb.Document.CreateElement("TH");      
        fn.InnerText = "Select";
        wb.Document.All["dgvHead"].AppendChild(fn);*/
wb.Document.All["dgvHead"].InnerHtml = "";
wb.Document.All["dgvBody"].InnerHtml = "";
wb.Document.All["btn-search"].InnerHtml="";
        foreach (DataColumn dc in dt.Columns)
        {
            tableColumn = wb.Document.CreateElement("TH");
            wb.Document.All["dgvHead"].AppendChild(tableColumn);
            tableColumn.SetAttribute("nowrap", "nowarp");
            tableColumn.SetAttribute("bgcolor", "#f0ad4e");
            tableColumn.InnerText=dc.ColumnName;

            tableSelect = wb.Document.CreateElement("li");
            wb.Document.All["btn-search"].AppendChild(tableSelect);
            tableSelect.InnerText = dc.ColumnName;
        }
        int trcount=0;
        //Console.WriteLine(wb.Document.All["dgvHead"].InnerHtml);
        foreach (DataRow dr in dt.Rows)
        {
            tableRow = wb.Document.CreateElement("TR");
            tableRow.SetAttribute("id", "tr"+trcount);
            tableRow.Click += new HtmlElementEventHandler(trClick);
            wb.Document.All["dgvBody"].AppendChild(tableRow);

++trcount;

            /*HtmlElement bs = wb.Document.CreateElement("TD");
            HtmlElement b = wb.Document.CreateElement("input");
            b.SetAttribute("type", "checkbox");
            //b.InnerText = "...";
            bs.AppendChild(b);
            tableRow.AppendChild(bs);*/

            foreach (DataColumn col in dt.Columns)
            {
                Object dbCell = dr[col];
                HtmlElement tableCell = wb.Document.CreateElement("TD");
                //tableCell.SetAttribute("nowrap", "nowarp");
                if (!(dbCell is DBNull))
                {
                    tableCell.InnerText = dbCell.ToString();
                }
                tableRow.AppendChild(tableCell);
            }
        }

                /*string cells = "<tr><td>12345678</td><td>12345678</td></tr>";
                cells += "<tr><td>abc</td><td>def</td></tr>";*/
                //he.InnerHtml = "<tr><td>test</td><td>test</td></tr>";
                //he.InnerText = "test";

                //Console.WriteLine(wb.Document.All["dgvBody"].InnerHtml);
                
                //foreach (HtmlElement tr in wb.Document.All["dgv"].Children)
                {
                    //Console.WriteLine(tr.InnerHtml);
                    //Console.WriteLine(tr.InnerHtml);
                }
                //wb.Document.All["dgvv"].InnerHtml = wb.Document.All["dgvv"].InnerHtml.Replace("holder",cells);
                //Console.Write(wb.Document.All["dgv"].InnerHtml);

                //foreach (HtmlElement td in wb.Document.All["dgv"].Children)
                {
                    //Console.Write(td.InnerHtml+"\n");
                }
                //wb.Document.All["check"].Click += new HtmlElementEventHandler(loginClick);
            }
        }
        private string trActive = "";
        private void trClick(object sender, HtmlElementEventArgs e)
        {
            HtmlElement he = sender as HtmlElement;
            he.SetAttribute("bgcolor", "#ffcf00");

            //Console.WriteLine(he.OuterHtml);
            if (!string.IsNullOrEmpty(trActive))
            {
                WebBrowser wb = m_f.Controls["webBrowser1"] as WebBrowser;
                wb.Document.All[trActive].SetAttribute("bgcolor", "");
            }
            trActive = he.GetAttribute("id");
        }
        private void queryClick(object sender, HtmlElementEventArgs e)
        {
            htmCompleted(m_f.Controls["webBrowser1"], null);
        }
        private void loginClick(object sender, HtmlElementEventArgs e)
        {
            WebBrowser wb = m_f.Controls["webBrowser1"] as WebBrowser;
            HtmlElement tbUserid = wb.Document.All["inputEmail"];
            HtmlElement tbPasswd = wb.Document.All["inputPassword"];

            string user = tbUserid.GetAttribute("value").Trim();
            string word = tbPasswd.GetAttribute("value").Trim();

            XmlDocument doc = new XmlDocument();
            doc.Load("xml/Account.xml");
            XmlNodeList xnl = doc.DocumentElement.GetElementsByTagName("Row");

            IEnumerable<XElement> rows =
            from r in XElement.Load("xml/account.xml").Elements("Row")
            where user == r.Element("代號").Value &&  word == r.Element("密碼").Value
            select r;
            //foreach(var r in rows)
            if (rows.Count() > 0)
            {
                MessageBox.Show(rows.First().Element("名稱").Value+"登入成功");
                //Console.Write(r.Element("名稱").Value+","+r.Element("代號").Value);
            }
            else
            {
                MessageBox.Show(rows.First().Element("名稱").Value+"登入失敗");
            }

            //Console.Write(user+","+word);
        }

        void InitializeComponent(Form a_f)
        {
            a_f.SuspendLayout();
            // 
            // webBrowser1
            // 
            WebBrowser webBrowser1 = new System.Windows.Forms.WebBrowser();
            webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            //webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.Location = new System.Drawing.Point(0, 0);
            webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            webBrowser1.Name = "webBrowser1";
            webBrowser1.Size = new System.Drawing.Size(1091, 727);
            webBrowser1.TabIndex = 0;
            webBrowser1.Url = new System.Uri(System.IO.Path.GetFullPath("UI/Test.htm"));
            webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.htmCompleted);
            a_f.Controls.Add(webBrowser1);
            // 
            // dataGridView1
            // 
            /*DataGridView dataGridView1 = new System.Windows.Forms.DataGridView();
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(26, 272);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new System.Drawing.Size(826, 245);
            dataGridView1.TabIndex = 2;
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            a_f.Controls.Add(dataGridView1);*/
            // 
            // Form1
            // 
            a_f.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            a_f.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            a_f.ClientSize = new System.Drawing.Size(889, 539);
            a_f.Name = "Form1";
            a_f.Text = "Form1";
            a_f.ResumeLayout(false);

        }
    }
}
