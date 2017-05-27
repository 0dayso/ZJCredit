using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZJCredit;

namespace AdministrativePenalty
{
    class AdministrativePenaltyCrawler
    {
        private readonly MySqlHelper mySqlHelper;
        private readonly HttpHelper httpHelper;
        private readonly Queue<string> _urlQueue;
        private int _totalPage;
        private int _curPage;

        public AdministrativePenaltyCrawler()
        {
            mySqlHelper = new MySqlHelper();
            httpHelper = new HttpHelper();
            _urlQueue = new Queue<string>();
            _totalPage = _curPage = 0;
        }

        public void Run()
        {
            var url = "http://www.zjcredit.gov.cn/html/sgsList02.htm";
            InitHttpEncoding(url);
            InitTotalPage(url);
            
            //while (_curPage<_totalPage)
            //{
            //    var html = httpHelper.GetHtmlByGet(url);

            //}
        }


        private void InitHttpEncoding(string url)
        {
            var html = httpHelper.GetHtmlByGet(url);
            httpHelper.HttpEncoding = HttpHelper.GetHtmlEncoding(html);
        }

        private void InitTotalPage(string url)
        {
            var html = httpHelper.GetHtmlByGet(url);
            var totalRecord = int.Parse(Regex.Match(html, @"(?<=totalRecord[\s]*:[\s]*)\d+").Value);
            _totalPage = totalRecord%8 == 0 ? totalRecord : totalRecord + 1;
        }


    }

    class MainClass
    {
        public static void Main()
        {
            //var administrativePenaltyCrawler = new AdministrativePenaltyCrawler();
            //administrativePenaltyCrawler.Run();
        }
    }
}
