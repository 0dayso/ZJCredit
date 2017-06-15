using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HelpLib;
using ZJCredit;

namespace Sincerity
{
    public class SincerityCrawler
    {
        private string _tableId;
        private readonly Queue<string> _urlQueue = new Queue<string>();
        private int _totalRecord = 0;
        private const int _interval = 70;
        private readonly MySqlHelper _mySqlHelper = new MySqlHelper();
        private Encoding _httpEncoding;
        private readonly object _object = new object();
        private event EventHandler<ExceptionEventArgs> EventHandler;

        public void Run()
        {
            var url = "http://www.zjcredit.gov.cn/info/sincerityList.do?id=F39D7C0EB37CACD0B40A66E03FB83887EE45DD73051FEBE9";
            InitTableId(url);
            var httpHelper = new HttpHelper
            {
                Timeout = 5 * 60 * 1000
            };
            var html = httpHelper.GetHtmlByGet(url);
            InitHttpEncoding(html);
            InitTotalRecord(html);
            InitUrlQueue();
            GetInfoAndInsertDb(_urlQueue.Dequeue());

            while (_urlQueue.Count!=0)
            {
                var ThreadNum = _urlQueue.Count < 5 ? _urlQueue.Count : 5;
                var Tasks = new Task[ThreadNum];
                for (var i = 0; i < ThreadNum; i++)
                {
                    string curUrl;
                    lock (_object)
                    {
                        curUrl = _urlQueue.Dequeue();
                    }
                    Tasks[i] = new Task(()=>GetInfoAndInsertDb(curUrl));
                    //Tasks[i] = new Task(delegate() { GetInfoAndInsertDb(curUrl); });
                    //Tasks[i] = new Task(GetInfoAndInsertDb,curUrl);
                    Tasks[i].Start();
                }
                for (var i = 0; i < ThreadNum; i++)
                    Tasks[i].Wait();

            }
            

        }

        private void GetInfoAndInsertDb(object o)
        {
            try
            {
                //var listDic = GetInfo(o.ToString());
                //InsertDb(listDic, "SincerityInfo");
                GetInfoAndInsertDb(o.ToString(), InsertDb, "SincerityInfo");
            }
            catch (Exception e)
            {
                EventHandler += WriteLog;
                var tempEventHandler = EventHandler;
                tempEventHandler?.Invoke(this, new ExceptionEventArgs(e));
            }
        }



        private void WriteLog(object sender, ExceptionEventArgs exceptionEventArgs)
        {

            //var exceptionEventArgs = eventArgs as ExceptionEventArgs;
            var log4NetHelper = new Log4NetHelper(typeof(SincerityCrawler));
            log4NetHelper.WriteLog(this, new ExceptionEventArgs(exceptionEventArgs?.Exception));
        }

        private void InitTableId(string url)
        {
            _tableId = Regex.Match(url, @"(?<=id=)\w+").Value;
        }

        private void InitTotalRecord(string html)
        {
            int.TryParse(Regex.Match(html, @"(?<=totalRecord:)\d+").Value, out _totalRecord);
        }

        private void InitHttpEncoding(string html)
        {
            _httpEncoding = HttpHelper.GetHtmlEncoding(html);
        }

        private void InitUrlQueue()
        {
            const int initRecord = 99961;
            var curRecord = initRecord;
            var endRecord = initRecord;
            while (endRecord < _totalRecord)
            {
                endRecord = curRecord + _interval - 1 > _totalRecord ? _totalRecord : curRecord + _interval - 1;
                var url = $"http://www.zjcredit.gov.cn/info/promptsProxy.do?startrecord={curRecord}&endrecord={endRecord}&perpage=10&totalRecord={_totalRecord}";
                Console.WriteLine(url);
                _urlQueue.Enqueue(url);
                curRecord = endRecord + 1;
            }
        }


        private void InsertDb(IEnumerable<Dictionary<string, string>> listDic,string tableName)
        {
            foreach (var dic in listDic)
            {
                _mySqlHelper.InsertTable(dic, tableName);
            }
        }

        private void InsertDb(Dictionary<string, string> dic, string tableName)
        {
            _mySqlHelper.InsertTable(dic,tableName);
        }

        private void GetInfoAndInsertDb(string url,Action<Dictionary<string, string>,string> insertDb,string tableName)
        {
            Func<string, string> removeSpace = s => s.Replace("&nbsp;", "");


            var httpHelper = new HttpHelper
            {
                HttpEncoding = _httpEncoding,
                Timeout = 5 * 60 * 1000
            };

            var html = httpHelper.GetHtmlByPost(url, $"id={_tableId}&inTime={DateTime.Now}");

            var matchCollection = Regex.Matches(Regex.Match(html, @"dataStore[\s]*=[\s]*\[.*?\]").Value, "(?<=\")[^,]+?(?=\")");

            foreach (Match match in matchCollection)
            {
                var cId = Regex.Match(Regex.Match(match.Value, @"[E][\$][^\$]*[\$]").Value, @"(?<=E[\$])[\S]*(?=[\$])").Value;
                var content = httpHelper.GetHtmlByPost("http://www.zjcredit.gov.cn/info/promptsDetail.do", $"tableId={_tableId}&cId={cId}&inTime={DateTime.Now}");
                var htmlNode = HtmlAgilityPackHelper.GetDocumentNodeByHtml(content);
                var sincerityInfo = new SincerityInfo();
                sincerityInfo.InfoName = Regex.Match(content, "(?<=信息名称：).*?(?=')").Value;
                sincerityInfo.DataSources = Regex.Match(content, "(?<=数据来源：).*?(?=')").Value;
                sincerityInfo.UpdateTime = Regex.Match(content, "(?<=更新时间：).*?(?=')").Value;
                var htmlNodeList = htmlNode.SelectNodes("//td[@class='xyml_t2']").ToList();
                sincerityInfo.OrganizationCode = removeSpace(htmlNodeList[0].InnerText);
                sincerityInfo.LegalRepresentative = removeSpace(htmlNodeList[1].InnerText);
                sincerityInfo.WorkAddress = removeSpace(htmlNodeList[2].InnerText);
                sincerityInfo.Court = removeSpace(htmlNodeList[3].InnerText);
                sincerityInfo.CaseNo = removeSpace(htmlNodeList[4].InnerText);
                sincerityInfo.ExecutiveBasis = removeSpace(htmlNodeList[5].InnerText);
                sincerityInfo.ExecutiveReason = removeSpace(htmlNodeList[6].InnerText);
                sincerityInfo.ExecutiveTime = removeSpace(htmlNodeList[7].InnerText);
                sincerityInfo.ExecutiveMoney = removeSpace(htmlNodeList[8].InnerText);
                sincerityInfo.NotExecutiveMoney = removeSpace(htmlNodeList[9].InnerText);
                sincerityInfo.ExposureTime = removeSpace(htmlNodeList[10].InnerText);

                var dic = new Dictionary<string, string>
                {
                    ["InfoName"] = sincerityInfo.InfoName,
                    ["DataSources"] = sincerityInfo.DataSources,
                    ["UpdateTime"] = sincerityInfo.UpdateTime,
                    ["OrganizationCode"] = sincerityInfo.OrganizationCode,
                    ["LegalRepresentative"] = sincerityInfo.LegalRepresentative,
                    ["WorkAddress"] = sincerityInfo.WorkAddress,
                    ["Court"] = sincerityInfo.Court,
                    ["CaseNo"] = sincerityInfo.CaseNo,
                    ["ExecutiveBasis"] = sincerityInfo.ExecutiveBasis,
                    ["ExecutiveReason"] = sincerityInfo.ExecutiveReason,
                    ["ExecutiveTime"] = sincerityInfo.ExecutiveTime,
                    ["ExecutiveMoney"] = sincerityInfo.ExecutiveMoney,
                    ["NotExecutiveMoney"] = sincerityInfo.NotExecutiveMoney,
                    ["ExposureTime"] = sincerityInfo.ExposureTime,
                    ["PageUrl"] = url,
                    ["CId"] = cId,
                    ["ThreadId"] = Thread.CurrentThread.ManagedThreadId.ToString()
                };

                foreach (var keyValue in dic)
                {
                    Console.WriteLine($"{keyValue.Key}:{keyValue.Value}");
                }
                //一条一条插入
                InsertDb(dic, tableName);
            }


        }

        private IEnumerable<Dictionary<string, string>> GetInfo(string url)
        {
            Func<string, string> removeSpace = s => s.Replace("&nbsp;", "");

            var listDic = new List<Dictionary<string, string>>();

            var httpHelper = new HttpHelper
            {
                HttpEncoding = _httpEncoding,
                Timeout = 5 * 60 * 1000
            };

            var html = httpHelper.GetHtmlByPost(url, $"id={_tableId}&inTime={DateTime.Now}");

            var matchCollection = Regex.Matches(Regex.Match(html, @"dataStore[\s]*=[\s]*\[.*?\]").Value, "(?<=\")[^,]+?(?=\")");

            foreach (Match match in matchCollection)
            {
                var cId = Regex.Match(Regex.Match(match.Value, @"[E][\$][^\$]*[\$]").Value, @"(?<=E[\$])[\S]*(?=[\$])").Value;
                var content = httpHelper.GetHtmlByPost("http://www.zjcredit.gov.cn/info/promptsDetail.do",$"tableId={_tableId}&cId={cId}&inTime={DateTime.Now}");
                var htmlNode = HtmlAgilityPackHelper.GetDocumentNodeByHtml(content);
                var sincerityInfo = new SincerityInfo();
                sincerityInfo.InfoName = Regex.Match(content, "(?<=信息名称：).*?(?=')").Value;
                sincerityInfo.DataSources = Regex.Match(content, "(?<=数据来源：).*?(?=')").Value;
                sincerityInfo.UpdateTime = Regex.Match(content, "(?<=更新时间：).*?(?=')").Value;
                var htmlNodeList = htmlNode.SelectNodes("//td[@class='xyml_t2']").ToList();
                sincerityInfo.OrganizationCode = removeSpace(htmlNodeList[0].InnerText);
                sincerityInfo.LegalRepresentative = removeSpace(htmlNodeList[1].InnerText);
                sincerityInfo.WorkAddress = removeSpace(htmlNodeList[2].InnerText);
                sincerityInfo.Court = removeSpace(htmlNodeList[3].InnerText);
                sincerityInfo.CaseNo = removeSpace(htmlNodeList[4].InnerText);
                sincerityInfo.ExecutiveBasis = removeSpace(htmlNodeList[5].InnerText);
                sincerityInfo.ExecutiveReason = removeSpace(htmlNodeList[6].InnerText);
                sincerityInfo.ExecutiveTime = removeSpace(htmlNodeList[7].InnerText);
                sincerityInfo.ExecutiveMoney = removeSpace(htmlNodeList[8].InnerText);
                sincerityInfo.NotExecutiveMoney = removeSpace(htmlNodeList[9].InnerText);
                sincerityInfo.ExposureTime = removeSpace(htmlNodeList[10].InnerText);

                var dic = new Dictionary<string,string>
                {
                    ["InfoName"] = sincerityInfo.InfoName,
                    ["DataSources"] = sincerityInfo.DataSources,
                    ["UpdateTime"] = sincerityInfo.UpdateTime,
                    ["OrganizationCode"] = sincerityInfo.OrganizationCode,
                    ["LegalRepresentative"] = sincerityInfo.LegalRepresentative,
                    ["WorkAddress"] = sincerityInfo.WorkAddress,
                    ["Court"] = sincerityInfo.Court,
                    ["CaseNo"] = sincerityInfo.CaseNo,
                    ["ExecutiveBasis"] = sincerityInfo.ExecutiveBasis,
                    ["ExecutiveReason"] = sincerityInfo.ExecutiveReason,
                    ["ExecutiveTime"] = sincerityInfo.ExecutiveTime,
                    ["ExecutiveMoney"] = sincerityInfo.ExecutiveMoney,
                    ["NotExecutiveMoney"] = sincerityInfo.NotExecutiveMoney,
                    ["ExposureTime"] = sincerityInfo.ExposureTime,
                    ["PageUrl"] = url,
                    ["CId"] = cId,
                    ["ThreadId"] = Thread.CurrentThread.ManagedThreadId.ToString()
                };

                listDic.Add(dic);
                foreach (var keyValue in dic)
                {
                    Console.WriteLine($"{keyValue.Key}:{keyValue.Value}");
                }
            }

            

            return listDic;
        }


    }

    public class MainClass
    {
        public static void Main(string[] args)
        {
            var sincerityCrawler = new SincerityCrawler();
            sincerityCrawler.Run();
        }
    }


/*
    CREATE TABLE SincerityInfo
    (
        InfoName VARCHAR(256),
		DataSources VARCHAR(256),
		UpdateTime VARCHAR(256),
		OrganizationCode VARCHAR(256),
		LegalRepresentative VARCHAR(256),
		WorkAddress VARCHAR(256),
		Court VARCHAR(256),
		CaseNo VARCHAR(256),
		ExecutiveBasis VARCHAR(256),
		ExecutiveReason VARCHAR(256),
		ExecutiveTime VARCHAR(256),
		ExecutiveMoney VARCHAR(256),
		NotExecutiveMoney VARCHAR(256),
		ExposureTime VARCHAR(256),
		PageUrl VARCHAR(256),
		CId VARCHAR(256),
		ThreadId VARCHAR(266)
    )
*/

}
