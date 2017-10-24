using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FSOrderCenter.Common;
using FSOrderCenter.Model.Log4Net;
using System.Text;

namespace FSOrderCenter.WebApp.Controllers
{
    public class Log4NetController : Controller
    {
        //
        // GET: /Log4Net/
        /// <summary>
        /// 日志 等级记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //ILog logger = LogManager.GetLogger("error");
            Log4NetHelper.Info("请选择认证方式Info");
            Log4NetHelper.Warn("请选择认证方式Warn");
            Log4NetHelper.Fatal("请选择认证方式Fatal");
            Log4NetHelper.Error("请选择认证方式Error");
            Log4NetHelper.Debug("请选择认证方式Debug");  
            return View();
        }

        /// <summary>
        /// Log4Net 测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Log4Net()
        {
            //None>Fatal>ERROR>WARN>DEBUG>INFO>ALL
            try
            {
                int a = 5;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                //ILog logger = LogManager.GetLogger("error");
                Log4NetHelper.Error("发生错误:" + ex);
                //logger.Debug("发生错误:" + ex);
            }
            return View();
        }
        /// <summary>
        /// 滚动日志，  批量写入日志中
        /// </summary>
        /// <returns></returns>
        public ActionResult LogWhile()
        {
            while (true)
            {
                try
                {
                    int a = 5;
                    int b = 0;
                    int c = a / b;
                }
                catch (Exception ex)
                {
                    //ILog logger = LogManager.GetLogger("error");
                    Log4NetHelper.Error("发生错误:" + ex);
                    //logger.Debug("发生错误:" + ex);
                }
            }
            return View();
        }
        /// <summary>
        /// windows时间日志写入文本
        /// </summary>
        /// <returns></returns>
        public ActionResult LogWindows()
        {
            //初始化日志信息
            LogMessage message = new LogMessage();
            message.SystemID = "LJ-12334";
            message.RequestIP = "127.0.0.1";
            message.RequestDate = DateTime.Now;
            message.MethodCode = "NGBOSS接口命令字编码";
            message.MethodName = "Log4Net控制器下LogWindows方法";
            message.Content = "测试一下 ====写Windows事件日志==== 这个方法";
            message.Remark = "这只是一个测试。";
            //调 Log4NetHelper 写入windows日志到文本
            Log4NetHelper.WriteEventLog(message);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<li>外围系统编号:{0}</li><li>客户端请求的IP地址:{1}</li><li>客户端请求的时间:{2}</li><li>NGBOSS:{3}</li><li>客户端发起请求的操作内容:{4}</li><li>备注：{5}</li>", message.SystemID, message.RequestIP, message.RequestDate, message.MethodCode + " " + message.MethodName, message.Content, message.Remark);
            ViewData["EventList"] = sb.ToString();
            return View();
        }
	}
}