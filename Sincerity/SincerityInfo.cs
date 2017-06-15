using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sincerity
{
    public class SincerityInfo
    {
        public SincerityInfo()
        {
        }

        public SincerityInfo(string infoName,string dataSources,string updateTime,string organizationCode,
                             string legalRepresentative,string workAddress,string court,string caseNo,
                             string executiveBasis,string executiveReason,string executiveTime,string executiveMoney,
                             string notExecutiveMoney,string exposureTime)
        {
            InfoName = infoName;
            DataSources = dataSources;
            UpdateTime = updateTime;
            OrganizationCode = organizationCode;
            LegalRepresentative = legalRepresentative;
            WorkAddress = workAddress;
            Court = court;
            CaseNo = caseNo;
            ExecutiveBasis = executiveBasis;
            ExecutiveReason = executiveReason;
            ExecutiveTime = executiveTime;
            ExecutiveMoney = executiveMoney;
            NotExecutiveMoney = notExecutiveMoney;
            ExposureTime = executiveMoney;
        }

        /// <summary>
        /// 信息名称
        /// </summary>
        public string InfoName { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSources { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 法人代表
        /// </summary>
        public string LegalRepresentative { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string WorkAddress { get; set; }
        
        /// <summary>
        /// 执行法院
        /// </summary>
        public string Court { get; set; }

        /// <summary>
        /// 案号
        /// </summary>
        public string CaseNo { get; set; }

        /// <summary>
        /// 执行依据
        /// </summary>
        public string ExecutiveBasis { get; set; }

        /// <summary>
        /// 执行案由
        /// </summary>
        public string ExecutiveReason { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string ExecutiveTime { get; set; }

        /// <summary>
        /// 执行金额（元）
        /// </summary>
        public string ExecutiveMoney { get; set; }
        
        /// <summary>
        /// 未执行金额（元）
        /// </summary>
        public string NotExecutiveMoney { get; set; }

        /// <summary>
        /// 曝光时间
        /// </summary>
        public string ExposureTime { get; set; }
    }
}
