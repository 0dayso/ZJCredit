using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using ZJCredit;

namespace HelpLib
{
    public class ProxyHelper
    {
        private readonly HashSet<string> _proxyHashSetSet = new HashSet<string>();
        private readonly HashSet<string> _usingHashSet = new HashSet<string>();
        private readonly HashSet<string> _notUsedHashSet = new HashSet<string>();
        private readonly object _object = new object();
        public enum DaiLiType
        {
            KuaiDaiLi,
            XDaiLi,
            DaXiangDaiLi
        }



        public void InitProxyHashSet(string url,DaiLiType daiLiType,string postData = null)
        {
            var httpHelper = new HttpHelper();

            string html;
            switch (daiLiType)
            {
                case DaiLiType.DaXiangDaiLi:
                    html = httpHelper.GetHtmlByPost(url, postData);
                    var matchCollectionDaXiang = Regex.Matches(html, @"\d+\.\d+\.\d+\.\d+:\d+");
                    foreach (Match match in matchCollectionDaXiang)
                    {
                        var value = match.Value;
                        //Add时如果HashSet中存在，则不会Add进去
                        _proxyHashSetSet.Add(value);
                        _notUsedHashSet.Add(value);
                    }
                    break;
                case DaiLiType.KuaiDaiLi:
                    html = httpHelper.GetHtmlByGet(url);
                    var matchCollectionKuai = Regex.Matches(html, @"\d+\.\d+\.\d+\.\d+:\d+");
                    foreach (Match match in matchCollectionKuai)
                    {
                        var value = match.Value;
                        //Add时如果HashSet中存在，则不会Add进去
                        _proxyHashSetSet.Add(value);
                        _notUsedHashSet.Add(value);
                    }
                    break;
                case DaiLiType.XDaiLi:
                    html = httpHelper.GetHtmlByGet(url);
                    var jObject = JObject.Parse(html);
                    var jArray = JArray.Parse(jObject["RESULT"].ToString());
                    foreach (var jToken in jArray)
                    {
                        var proxy = $"{jToken["ip"]}:{jToken["port"]}";
                        //Add时如果HashSet中存在，则不会Add进去
                        _proxyHashSetSet.Add(proxy);
                        _notUsedHashSet.Add(proxy);
                    }
                    break;
            }
            

            ////浅复制 不是想要的结果
            //_notUsedHashSet = _proxyHashSetSet;
        }

        public string GetOneProxy()
        {
            string first;
            lock (_object)
            {
                first = _notUsedHashSet.FirstOrDefault();
                if (first != null)
                {
                    _notUsedHashSet.Remove(first);
                    _usingHashSet.Add(first);
                }
            }
            
            return first;
        }

        public bool ReleaseOneProxy(string proxy)
        {
            lock (_object)
            {
                if (_usingHashSet.Contains(proxy))
                {
                    _usingHashSet.Remove(proxy);
                    _notUsedHashSet.Add(proxy);
                    return true;
                }
                return false;
            }
        }


        public void AuthenticationProxy()
        {
            var usefulCount = 0;
            var uselessCount = 0;

            foreach (var proxy in _proxyHashSetSet)
            {
                var url = "https://www.bing.com/";
                HttpHelper httpHelper = new HttpHelper();
                try
                {
                    httpHelper.GetHtmlByGet(url, new WebProxy(proxy));
                    Console.WriteLine($"{proxy} useful");
                    usefulCount++;
                }
                catch(Exception exception) 
                {
                    Console.WriteLine($"{proxy} useless : {exception.Message} {exception.StackTrace}");
                    uselessCount++;
                }

            }

            Console.WriteLine($"usefulCount:{usefulCount},uselessCount:{uselessCount}");
        }

        private void Test()
        {
            ////kuaidaili
            //var url = "http://dev.kuaidaili.com/api/getproxy/?orderid=909664274113782&num=100&b_pcchrome=1&b_pcie=1&b_pcff=1&protocol=2&method=2&an_ha=1&sp1=1&quality=1&sep=1";
            //InitProxyHashSet(url, DaiLiType.KuaiDaiLi);

            //var url = "http://www.xdaili.cn/ipagent/greatRecharge/getGreatIp?spiderId=91b5d33c75d14893bb68728a6f65d389&orderno=YZ2017678995ODR2Wu&returnType=2&count=18";
            //InitProxyHashSet(url, DaiLiType.XDaiLi);

            var url = "http://www.daxiangdaili.com/pick/";
            var postData = "tid=555405481678560&num=100&area=&foreign=all&operator=%E7%94%B5%E4%BF%A1&operator=%E8%81%94%E9%80%9A&operator=%E7%A7%BB%E5%8A%A8&ports=&exclude_ports=8090%2C8123&category=2&protocol=&filter=on&download=";
            InitProxyHashSet(url, DaiLiType.DaXiangDaiLi,postData);

            //var proxy = GetOneProxy();
            //var result = ReleaseOneProxy(proxy);
            AuthenticationProxy();
        }
    }
}
