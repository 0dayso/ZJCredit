using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ZJCredit
{
    class HMDCrawler
    {
        private readonly HttpHelper _httpHelper;
        private readonly MySqlHelper _mySqlHelper;
        private readonly Queue<string> _urlQueue;
        public HMDCrawler()
        {
            _httpHelper = new HttpHelper();
            _mySqlHelper = new MySqlHelper();
            _urlQueue = new Queue<string>();
        }

        public void Run()
        {
            
            var html = _httpHelper.GetHtmlByGet("http://www.zjcredit.gov.cn/hmd/hmd.do");
            //得到网页编码
            _httpHelper.HttpEncoding = HttpHelper.GetHtmlEncoding(html);
            html = _httpHelper.GetHtmlByGet("http://www.zjcredit.gov.cn/hmd/hmd.do");

            var url = $"http://www.zjcredit.gov.cn/hmd/{Regex.Match(Regex.Match(Regex.Match(html, "initData.*?]").Value, "\".*?\"").Value, @"(?<=\$)[^\$]*(?="")").Value}";
            html = _httpHelper.GetHtmlByGet(url);

            var htmlNode = HtmlAgilityPackHelper.GetDocumentNodeByHtml(html);
 
            var htmlNodeCollection = htmlNode.SelectNodes("//table[2]//a");
            foreach (var node in htmlNodeCollection)
            {
                _urlQueue.Enqueue($"http://www.zjcredit.gov.cn{node.Attributes["href"].Value}");
            }


            while (_urlQueue.Count!=0)
            {
                var threadTotalNum = 5;
                var taskArray = new Task[threadTotalNum];
                for (var i = 0; i < threadTotalNum; i++)
                {
                    //判断队列是否已经取完 若取完则退出循环
                    if(_urlQueue.Count==0)
                        break;
                    url = _urlQueue.Dequeue();
                    taskArray[i] = new Task(GetInfoInsertDb, url);
                    taskArray[i].Start();
                }

                //等待这几个线程结束
                for (var i = 0; i < threadTotalNum; i++)
                {
                    taskArray[i].Wait();
                }

            }

        }



        private HMDInfo GetInfo(string url)
        {
            Func<string,string> replaceSpace = s=> s.Trim().Replace("&nbsp;", "");
            
            var hmdInfo = new HMDInfo();
            var html = _httpHelper.GetHtmlByGet(url);
            var htmlNode = HtmlAgilityPackHelper.GetDocumentNodeByHtml(html);
            hmdInfo.PubOrganName = replaceSpace(htmlNode.SelectSingleNode("//table[3]//tr[1]/td[2]").InnerText);
            hmdInfo.ProjectName = replaceSpace(htmlNode.SelectSingleNode("//table[3]//tr[2]/td[2]/nobr").InnerText);
            hmdInfo.NatrualName = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[1]/td[2]").InnerText);
            hmdInfo.IdentityNumber = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[1]/td[4]").InnerText);
            hmdInfo.OrganName = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[2]/td[2]").InnerText);
            hmdInfo.OrganCode = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[3]/td[2]").InnerText);
            hmdInfo.PubTime = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[4]/td[2]").InnerText);
            hmdInfo.PubDeadline = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[4]/td[4]").InnerText);
            hmdInfo.PunishmentNumber = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[5]/td[2]").InnerText);
            hmdInfo.PunishmentTime = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[5]/td[4]").InnerText);
            hmdInfo.PunishmentFact = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[6]/td[2]").InnerText);
            hmdInfo.PunishmentBasis = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[7]/td[2]").InnerText);
            hmdInfo.PunishmentResult = replaceSpace(htmlNode.SelectSingleNode("//table[5]//tr[8]/td[2]").InnerText);          
            return hmdInfo;
        }

        private void InsertDb(HMDInfo hmdInfo)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                ["PubOrganName"] = hmdInfo.PubOrganName,
                ["ProjectName"] = hmdInfo.ProjectName,
                ["NatrualName"] = hmdInfo.NatrualName,
                ["IdentityNumber"] = hmdInfo.IdentityNumber,
                ["OrganName"] = hmdInfo.OrganName,
                ["OrganCode"] = hmdInfo.OrganCode,
                ["PubTime"] = hmdInfo.PubTime,
                ["PubDeadline"] = hmdInfo.PubDeadline,
                ["PunishmentNumber"] = hmdInfo.PunishmentNumber,
                ["PunishmentTime"] = hmdInfo.PunishmentTime,
                ["PunishmentFact"] = hmdInfo.PunishmentFact,
                ["PunishmentBasis"] = hmdInfo.PunishmentBasis,
                ["PunishmentResult"] = hmdInfo.PunishmentResult,
                ["ThreadNumber"] = Thread.CurrentThread.ManagedThreadId.ToString()
            };

            foreach (var info in dic)
            {
                Console.WriteLine($"{info.Key}:{info.Value}");
            }

            _mySqlHelper.InsertTable(dic, "HMDInfo");
        }


        private void GetInfoInsertDb(object o)
        {
            var info = GetInfo((string) o);
            InsertDb(info);
        }


    }



    class MainClass
    {
        public static void Main()
        {
            var hMDCrawler = new HMDCrawler();
            hMDCrawler.Run();
        }
    }

    /*
    CREATE TABLE HMDInfo
    (
	    PubOrganName VARCHAR(128) COMMENT '发布机关名称',
	    ProjectName VARCHAR(128) COMMENT '失信黑名单项目名称',
	    NatrualName VARCHAR(128) COMMENT '自然人姓名',
	    IdentityNumber VARCHAR(128) COMMENT '自然人身份证号码',
	    OrganName VARCHAR(128) COMMENT '所属单位名称',
	    OrganCode VARCHAR(128) COMMENT '所属单位组织机构代码',
	    PubTime VARCHAR(128) COMMENT '发布时间',
	    PubDeadline VARCHAR(128) COMMENT '发布期限',
	    PunishmentNumber VARCHAR(128) COMMENT '处罚文号',
	    PunishmentTime VARCHAR(128) COMMENT '处罚时间',
	    PunishmentFact VARCHAR(128) COMMENT '处罚事实',
	    PunishmentBasis VARCHAR(128) COMMENT '处罚依据',
	    PunishmentResult VARCHAR(128) COMMENT '处罚结果',
        ThreadNumber VARCHAR(128) COMMENT '线程号'
    )
    */
}
