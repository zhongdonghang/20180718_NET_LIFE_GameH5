/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Application.TGame;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.Report;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace NFine.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Default()
        {
            IndexTopReport data = DataReportApp.ToIndexTopReport();
            ViewBag.PlayerAllCount = data.PlayerAllCount;
            ViewBag.PlayerCountToday = data.PlayerCountToday;
            ViewBag.TotalConsumeLbCount = data.TotalConsumeLbCount;
            ViewBag.TotalConsumeLoveBirdCount = data.TotalConsumeLoveBirdCount;
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
