using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSOrderCenter.Model.Log4Net
{
    public class LogMessage
    {
        public LogMessage() { }

        #region 初始化日志属性
        private string _systemID = string.Empty;
        /// <summary>
        /// 外围系统编号
        /// </summary>
        public String SystemID
        {
            get
            {
                return _systemID;
            }
            set
            {
                _systemID = value;
            }
        }

        private string _accountID = string.Empty;
        /// <summary>
        /// 外围系统帐号
        /// </summary>
        public String AccountID
        {
            get
            {
                return _accountID;
            }
            set
            {
                _accountID = value;
            }
        }

        private string _mobile = string.Empty;
        /// <summary>
        /// 办理业务的当前手机号码,2013-02-28 Add by HeKai.
        /// </summary>
        public String Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
            }
        }

        private string _methodCode = string.Empty;
        /// <summary>
        /// NGBOSS接口命令字编码
        /// </summary>
        public String MethodCode
        {
            get
            {
                return _methodCode;
            }
            set
            {
                _methodCode = value;
            }
        }

        private string _remark = string.Empty;
        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }

        private string _requestIP = string.Empty;
        /// <summary>
        /// 客户端请求的IP地址
        /// </summary>  
        public String RequestIP
        {
            get
            {
                return _requestIP;
            }
            set
            {
                _requestIP = value;
            }
        }

        private DateTime _requestDate = DateTime.Now;
        /// <summary>
        /// 客户端请求的时间
        /// </summary>
        public DateTime RequestDate
        {
            get
            {
                return _requestDate;
            }
            set
            {
                if (value != null)
                    _requestDate = value;
            }
        }

        private string _methodName = string.Empty;
        /// <summary>
        /// 客户端请求的NGBOSS方法名称
        /// </summary>
        public String MethodName
        {
            get
            {
                return _methodName;
            }
            set
            {
                _methodName = value;
            }
        }

        private string _content = string.Empty;
        /// <summary>
        /// 客户端发起请求的操作内容
        /// </summary>
        public String Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        #endregion
    }
}
