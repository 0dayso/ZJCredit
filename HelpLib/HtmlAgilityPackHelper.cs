using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;


namespace ZJCredit
{
    public class HtmlAgilityPackHelper
    {
        /// <summary>
        /// GetDocumentNodeByHtml
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlNode GetDocumentNodeByHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode;
        }


        void Test()
        {
            GetDocumentNodeByHtml("123");
        }


    }
}
