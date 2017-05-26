using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZJCredit
{
    class HMDInfo
    {

        public HMDInfo()
        {
        }

        public HMDInfo(string pubOrganName,string projectName,string naturalName,string identityNumber,string organName,
                       string organCode,string pubTime,string pubDeadline,string punishmentNumber,string punishmentTime,
                       string punishmentFact,string punishmentBasis,string punishmentResult)
        {
            _pubOrganName = pubOrganName;
            _projectName = projectName;
            _naturalName = naturalName;
            _identityNumber = identityNumber;
            _organName = organName;
            _organCode = organCode;
            _pubTime = pubTime;
            _pubDeadline = pubDeadline;
            _punishmentNumber = punishmentNumber;
            _punishmentTime = punishmentTime;
            _punishmentFact = punishmentFact;
            _punishmentBasis = punishmentBasis;
            _punishmentResult = punishmentResult;
    }

        public string PubOrganName {
            get { return _pubOrganName; }
            set { _pubOrganName = value; }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        public string NatrualName
        {
            get { return _naturalName;}
            set { _naturalName = value; }
        }

        public string IdentityNumber
        {
            get { return _identityNumber; }
            set { _identityNumber = value; }
        }

        public string OrganName
        {
            get { return _organName; }
            set { _organName = value; }
        }

        public string OrganCode
        {
            get { return _organCode; }
            set { _organCode = value; }
        }

        public string PubTime
        {
            get { return _pubTime; }
            set { _pubTime = value; }
        }

        public string PubDeadline
        {
            get { return _pubDeadline; }
            set { _pubDeadline = value; }
        }

        public string PunishmentNumber
        {
            get { return _punishmentNumber; }
            set { _punishmentNumber = value; }
        }

        public string PunishmentTime
        {
            get { return _punishmentTime; }
            set { _punishmentTime = value; }
        }

        public string PunishmentFact
        {
            get { return _punishmentFact; }
            set { _punishmentFact = value; }
        }

        public string PunishmentBasis
        {
            get { return _punishmentBasis; }
            set { _punishmentBasis = value; }
        }

        public string PunishmentResult
        {
            get { return _punishmentResult; }
            set { _punishmentResult = value; }
        }

        //发布机关名称
        private string _pubOrganName;
        //失信黑名单项目名称
        private string _projectName;
        //自然人姓名
        private string _naturalName;
        //自然人身份证号码
        private string _identityNumber;
        //所属单位名称
        private string _organName;
        //所属单位组织机构代码
        private string _organCode;
        //发布时间
        private string _pubTime;
        //发布期限
        private string _pubDeadline;
        //处罚文号
        private string _punishmentNumber;
        //处罚时间
        private string _punishmentTime;
        //处罚事实
        private string _punishmentFact;
        //处罚依据
        private string _punishmentBasis;
        //处罚结果
        private string _punishmentResult;
    }
}
