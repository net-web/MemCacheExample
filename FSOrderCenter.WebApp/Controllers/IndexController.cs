using FSOrderCenter.Common;
using FSOrderCenter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FSOrderCenter.WebApp.Controllers
{
    public class IndexController : Controller
    {
        // 存储形式  key-value
        // 服务器重启，内存数据丢失
        // 没有主从复制
        // MemCache 安全（1、内网访问 2、防火墙）
        // 惰性删除
        // GET: /Index/
        FSOrderCenterEntities oa = new FSOrderCenterEntities();
        /// <summary>
        /// 测 从数据库取数据时间
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            #region 查询Id小于8999的所有记录数
            var list = oa.UserInfo.Where(u => u.Id < 8999);
            StringBuilder sb = new StringBuilder();
            foreach (var userinfo in list)
            {
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", userinfo.Id, userinfo.Name, userinfo.PhoneNum, userinfo.AgainCode);
            }

            ViewData["userinfolist"] = sb.ToString();
            #endregion

            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            ViewData["CustomerTime"] = milliseconds;
            //将查询结果集写入缓存
            MemCacheHelper.Set("CustomerTime", sb.ToString());
            return View();
        }
        /// <summary>
        /// 测 内存取数据 时间
        /// </summary>
        /// <returns></returns>
        public ActionResult MemCache()
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //如果内存中有 key 对应的数据，则取出； 没有则提示先进行数据库查询
            #region 从内存中取数据
            if (MemCacheHelper.Get("CustomerTime") != null)
            {
                ViewBag.listSource = MemCacheHelper.Get("CustomerTime");
            }
            else
            {
                ViewBag.listSource = "请先测试 直接查询数据库所用时间";
            }
            #endregion
            stopwatch.Stop(); 
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            MemCacheHelper.Delete("CustomerTime");//清空 CustomerTime 缓存
            ViewData["MemCacheTime"] = milliseconds;
            return View();
        }
        /// <summary>
        /// 指定时间，缓存过期
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCacheTime()
        {
            //查询 号码为：44566789976  的用户姓名，10s钟后缓存过期
            var user = oa.UserInfo.Where(u => u.PhoneNum == "44566789976").FirstOrDefault();
            
            //var userList = from u in oa.UserInfo
            //               where u.PhoneNum == "44566789976"
            //               select u;
            //var user = userList.FirstOrDefault();
            
            ViewData["userName"] = user.Name;
            //缓存起来，设定过期时间为10s
            MemCacheHelper.Set("userTime",user.Name, DateTime.Now.AddSeconds(10));
            return View();
        }
        /// <summary>
        /// 检测 内存数据是否还存在
        /// </summary>
        /// <returns></returns>
        public ActionResult CacheOutTime()
        {
            //获取 key ： userTime 的内存，  判断是否存在
            if (MemCacheHelper.Get("userTime") == null)
            {//不存在
                ViewBag.isExist = "号码为：44566789976 的用户真的过期了...";
            }
            else
            {//存在
                ViewBag.isExist = "内存数据没有过期";
            }
            return View();
        }
        /// <summary>
        /// .Net自带缓存与MemCache简单对比
        /// </summary>
        /// <returns></returns>
        public ActionResult CompareCache()
        {
            return View();
        }
        /// <summary>
        /// 中间页 ， 展示信息
        /// </summary>
        /// <returns></returns>
        public ActionResult showMessage()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CompareMemCacheWithDictionary()
        {
            var list = oa.UserInfo.Where(u => true);
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int num=0;
            foreach (UserInfo userinfo in list)
            {

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add(num.ToString(), userinfo.Name);
                num++;
            }
            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            ViewData["dicTime"] = milliseconds;//存入 dictionary 总共用时

            System.Diagnostics.Stopwatch stopwatchMem = new Stopwatch();
            stopwatchMem.Start();
            int Mem = 0;
            foreach (UserInfo userinfo in list)
            {
                MemCacheHelper.Set(Mem.ToString(), userinfo.Name);
                num++;
            }
            stopwatchMem.Stop();
            TimeSpan timespanMem = stopwatchMem.Elapsed; //  获取当前实例测量得出的总时间
            double millisecondsMem = timespanMem.TotalMilliseconds;  //  总毫秒数
            ViewData["memTime"] = millisecondsMem; //存入MemCache 总共用时

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
            catch(Exception ex)
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
        
	}
}