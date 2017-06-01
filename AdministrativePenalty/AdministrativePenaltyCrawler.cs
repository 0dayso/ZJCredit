using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HelpLib;
using ZJCredit;

namespace AdministrativePenalty
{
    class AdministrativePenaltyCrawler
    {
        private readonly MySqlHelper _mySqlHelper;
        private readonly HttpHelper _httpHelper;
        private readonly Queue<string> _urlQueue;
        private int _totalRecord;
        private int _curRecord;
        private readonly int _interval;
        private EventHandler<ExceptionEventArgs> eventHandler;

        public AdministrativePenaltyCrawler()
        {
            _mySqlHelper = new MySqlHelper();
            _httpHelper = new HttpHelper {Timeout = 5*60*1000};
            _urlQueue = new Queue<string>();
            _totalRecord = 0;
            _curRecord = 1;
            _interval = 80;
        }

        public void Run()
        {
            var url = "http://www.zjcredit.gov.cn/html/sgsList02.htm";
            InitHttpEncoding(url);
            InitTotalRecord(url);
            InitUrlQueue();
            var threadNum = 5;
            var tasks = new Task[threadNum];
            
            while (_urlQueue.Count!=0)
            {
                for (var i = 0; i < threadNum; i++)
                {
                    if (_urlQueue.Count == 0)
                        break;
                    url = _urlQueue.Dequeue();
                    tasks[i] = new Task(GetInfoAndInsertDb,url);
                    tasks[i].Start();
                }

                //等待线程执行完成
                for (var i = 0; i < threadNum; i++)
                {
                    tasks[i].Wait();
                }
            }
            

        }





        private void GetInfoAndInsertDb(object o)
        {
            try
            {
                var listDic = GetInfo(o.ToString(), "id2=");
                foreach (var dic in listDic)
                {
                    _mySqlHelper.InsertTable(dic, "AdministrativePenaltyInfo");
                }
            }
            catch (Exception e)
            {
                eventHandler += WriteLog;
                eventHandler(this,new ExceptionEventArgs(e));
            }
        }

        private void Test()
        {
            var e = new Exception("测试日志");
            eventHandler += WriteLog;
            eventHandler(this, new ExceptionEventArgs(e));
        }


        private List<Dictionary<string,string>> GetInfo(string url,string postDataString)
        {
            Func<string, string> removeSpace = s => s.Replace("&nbsp;", "");

            List<Dictionary<string,string>> listDic = new List<Dictionary<string, string>>();
            var html = _httpHelper.GetHtmlByPost(url, postDataString);
            var urlCollection = Regex.Matches(Regex.Match(Regex.Match(html, @"dataStore[\s]*=[\s]*\[.*?\]").Value, "(?<=\")[^,]+?(?=\")").Value,@"(?<=\$)[^\$]*$");
            foreach (Match caseUrl in urlCollection)
            {

                html = _httpHelper.GetHtmlByGet($"http://www.zjcredit.gov.cn{caseUrl.Value}");
                var htmlNode = HtmlAgilityPackHelper.GetDocumentNodeByHtml(html);
                var administrativePenaltyInfo = new AdministrativePenaltyInfo();

                administrativePenaltyInfo.CaseName = htmlNode.SelectSingleNode("//td[@class='listf2']").InnerText;
                administrativePenaltyInfo.CaseId = htmlNode.SelectSingleNode("//table[2]//tr[1]/td[@class='xzcf_xx']").InnerText;
                administrativePenaltyInfo.PenaltyObject = removeSpace(htmlNode.SelectSingleNode("//table[2]//tr[2]/td[@class='xzcf_xx']/text()").InnerText);
                administrativePenaltyInfo.LegalRepresentative = Regex.Match(htmlNode.SelectSingleNode("//table[2]//span[@class='xzcf_mc']").InnerText, "(?<=：).*").Value.Trim();
                administrativePenaltyInfo.Department = htmlNode.SelectSingleNode("//table[2]//tr[3]/td[@class='xzcf_xx']").InnerText;
                administrativePenaltyInfo.PenaltyDate = htmlNode.SelectSingleNode("//table[2]//tr[4]/td[@class='xzcf_xx']").InnerText;
                administrativePenaltyInfo.PenalyText = htmlNode.SelectSingleNode("//table[4]//td[@class='xzcf_jds']").InnerText;

                var dic = new Dictionary<string,string>
                {
                    ["CaseName"] = administrativePenaltyInfo.CaseName,
                    ["CaseId"] = administrativePenaltyInfo.CaseId,
                    ["PenaltyObject"] = administrativePenaltyInfo.PenaltyObject,
                    ["LegalRepresentative"] = administrativePenaltyInfo.LegalRepresentative,
                    ["Department"] = administrativePenaltyInfo.Department,
                    ["PenaltyDate"] = administrativePenaltyInfo.PenaltyDate,
                    ["PenalyText"] = administrativePenaltyInfo.PenalyText,
                    ["PostUrl"] = url,
                    ["Url"] = caseUrl.Value,
                    ["ThreadId"] = Thread.CurrentThread.ManagedThreadId.ToString()
                };

                foreach (var info in dic)
                    Console.WriteLine($"{info.Key}:{info.Value}");

                listDic.Add(dic);
            }

            return listDic;
        }


        private void InitHttpEncoding(string url)
        {
            var html = _httpHelper.GetHtmlByGet(url);
            _httpHelper.HttpEncoding = HttpHelper.GetHtmlEncoding(html);
        }

        
        private void InitTotalRecord(string url)
        {
            var html = _httpHelper.GetHtmlByGet(url);
            _totalRecord = int.Parse(Regex.Match(html, @"(?<=totalRecord[\s]*:[\s]*)\d+").Value);
        }

        
        

        private void InitUrlQueue()
        {
            while (_curRecord < _totalRecord)
            {
                string url; 
                if (_curRecord+_interval-1 < _totalRecord)
                {
                    url = $"http://www.zjcredit.gov.cn/page/sgs/sgsProxy.jsp?startrecord={_curRecord}&endrecord={_curRecord + _interval - 1}&perpage=8&totalRecord={_totalRecord}";
                    _urlQueue.Enqueue(url);
                    _curRecord += _interval;
                }
                else
                {
                    url = $"http://www.zjcredit.gov.cn/page/sgs/sgsProxy.jsp?startrecord={_curRecord}&endrecord={_totalRecord}&perpage=8&totalRecord={_totalRecord}";
                    _urlQueue.Enqueue(url);
                    _curRecord = _totalRecord;
                }

                Console.WriteLine(url);
            }
        }


        private void WriteLog(object sender, ExceptionEventArgs exceptionEventArgs)
        {
            
            //var exceptionEventArgs = eventArgs as ExceptionEventArgs;
            var log4NetHelper = new Log4NetHelper(typeof(AdministrativePenaltyCrawler));
            log4NetHelper.WriteLog(this, new ExceptionEventArgs(exceptionEventArgs?.Exception));
        }

    }

    class MainClass
    {
        public static void Main()
        {
            var administrativePenaltyCrawler = new AdministrativePenaltyCrawler();
            administrativePenaltyCrawler.Run();
        }
    }
}



/*
    CREATE TABLE AdministrativePenaltyInfo
    (
	    CaseName VARCHAR(128) COMMENT '案件名称',
	    CaseId VARCHAR(128) COMMENT '行政处罚决定书文号',
	    Department VARCHAR(128) COMMENT '执法部门',
	    PenaltyObject VARCHAR(128) COMMENT '被处罚单位（被处罚人）',
	    LegalRepresentative VARCHAR(128) COMMENT '法定代表人',
	    PenaltyDate VARCHAR(128) COMMENT '作出行政处罚的日期',
	    PenalyText Text COMMENT '行政处罚决定书（全文或摘要）',
        PostUrl VARCHAR(128) COMMENT 'Post链接',
        Url VARCHAR(128) COMMENT '链接',
	    ThreadId VARCHAR(128) COMMENT '线程号'
    )
    
*/
