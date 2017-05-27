using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdministrativePenalty
{
    class AdministrativePenaltyInfo
    {

        /// <summary>
        /// CaseName
        /// </summary>
        public string CaseName
        {
            get { return _caseName; }
            set { _caseName = value; }
        }

        public string CaseId
        {
            get { return _caseId; }
            set { _caseId = value; }
        }

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        public string PenaltyObject
        {
            get { return _penaltyObject; }
            set { _penaltyObject = value; }
        }

        public string LegalRepresentative
        {
            get { return _legalRepresentative; }
            set { _legalRepresentative = value; }
        }

        public string PenaltyDate
        {
            get { return _penaltyDate; }
            set { _penaltyDate = value; }
        }

        public string PenalyText
        {
            get { return _penalyText; }
            set { _penalyText = value; }
        }

        //案件名称
        private string _caseName;
        //行政处罚决定书文号
        private string _caseId;
        //执法部门
        private string _department;
        //被处罚单位（被处罚人）
        private string _penaltyObject;
        //法定代表人
        private string _legalRepresentative;
        //作出行政处罚的日期
        private string _penaltyDate;
        //行政处罚决定书（全文或摘要）
        private string _penalyText;

    }
}
