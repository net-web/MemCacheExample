using FSOrderCenter.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FSOrderCenter.WebApp.Controllers
{
    public class MemCacheController : Controller
    {
        //
        // GET: /MemCache/
        public ActionResult Index()
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //  开始监视代码运行时间
            if (MemCacheHelper.Get("CustomerTime") != null)
            {
                ViewBag.listSource = MemCacheHelper.Get("CustomerTime");
            }
            else
            {
                ViewBag.listSource = "请先测试 直接查询数据库所用时间";
            }
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            MemCacheHelper.Delete("CustomerTime");//清空 CustomerTime 缓存
            ViewData["MemCacheTime"] = milliseconds;
            return View();
        }
	}
}