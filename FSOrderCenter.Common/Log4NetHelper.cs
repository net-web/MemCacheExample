using FSOrderCenter.Model.Log4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSOrderCenter.Common
{
    /// <summary>  
    /// 日志记录器  
    /// </summary> 
    public class Log4NetHelper
    {
        private static readonly log4net.ILog log = null;
        static Log4NetHelper()
        {
            log = log4net.LogManager.GetLogger("Logger");
        }
        /// <summary>  
        /// 记录调试信息  
        /// </summary>  
        /// <param name="message"></param>  
        public static void Debug(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("Logger");
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        /// <summary>  
        /// 记录错误信息  
        /// </summary>  
        /// <param name="message"></param>  
        public static void Error(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("Logger");
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        /// <summary>  
        /// 记录致命错误  
        /// </summary>  
        /// <param name="message"></param>  
        public static void Fatal(string message)
        {

            //log4net.ILog log = log4net.LogManager.GetLogger("Logger");
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }

        /// <summary>  
        /// 记录一般信息  
        /// </summary>  
        /// <param name="message"></param>  
        public static void Info(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("Logger");
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        /// <summary>  
        /// 记录警告信息  
        /// </summary>  
        /// <param name="message"></param>  
        public static void Warn(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("Logger");
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        /// <summary>
        /// 写文本日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteFileLog(LogMessage message)
        {
            string logContent = string.Format("---- systemID:{0}；mobile:{1}；requestIP:{2}；requestDate:{3}；requestMethod:{4}；content:{5}；Remark:{6}；", 
                message.SystemID, message.Mobile, message.RequestIP, message.RequestDate, message.MethodCode + " " + message.MethodName, message.Content, message.Remark);
            Info(logContent);
        }
        /// <summary>
        /// 写Windows事件日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteEventLog(LogMessage message)
        {
            string logContent = string.Format("systemID:{0}；requestIP:{1}；requestDate:{2}；requestMethod:{3}；content:{4}；Remark:{5}",
                    message.SystemID, message.RequestIP, message.RequestDate, message.MethodCode + " " + message.MethodName, message.Content, message.Remark);
            Error(logContent);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="methodType">枚举值</param>
        /// <param name="message">异常信息</param>
        public static void LogFour(LogFourType methodType, string message)
        {
            switch (Convert.ToInt32(methodType))
            {
                case 0:
                    log.Debug(message);
                    break;
                case 1:
                    log.Error(message);
                    break;
                case 2:
                    log.Fatal(message);
                    break;
                case 3:
                    log.Info(message);
                    break;
                default:
                    log.Warn(message);
                    break;
            }
        }
    }
}
