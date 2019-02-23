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
            ///顶部矩形框数据
            IndexTopReport data = DataReportApp.ToIndexTopReport();
            ViewBag.PlayerAllCount = data.PlayerAllCount;
            ViewBag.PlayerCountToday = data.PlayerCountToday;
            ViewBag.TotalConsumeLbCount = data.TotalConsumeLbCount;
            ViewBag.TotalConsumeLoveBirdCount = data.TotalConsumeLoveBirdCount;

            //人员实时情况统计

            RealTimeActivePlayerStatisticsReport rept = DataReportApp.ToIndexRealTimeActivePlayerStatisticsReport();

          //  rept.items

            var data1 = "{ "+
                " labels: [\"0\", \"1\", \"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"11\", \"12\", \"13\", \"14\", \"15\", \"16\", \"17\", \"18\", \"19\", \"20\", \"21\", \"22\", \"23\"], " +
                "  datasets: [ " +
                  "  { " +
            "label: \"My First dataset\", " +
             "           fillColor: \"rgba(255,0,0,0.2)\", " +
              "          strokeColor: \"rgba(255,0,0,1)\", " +
               "         pointColor: \"rgba(255,0,0,1)\", " +
                "        pointStrokeColor: \"#fff\", "+
                 "       pointHighlightFill: \"#fff\", "+
                  "      pointHighlightStroke: \"rgba(255,0,0,1)\", " +
                   "     data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()] " +
                "    }, " +
                "    { " +
           " label: \"My Second dataset\", " +
            "            fillColor: \"rgba(0,255,0,0.2)\", " +
            "            strokeColor: \"rgba(0,255,0,1)\", " +
            "            pointColor: \"rgba(0,255,0,1)\", " +
            "            pointStrokeColor: \"#fff\", "+
            "            pointHighlightFill: \"#fff\", "+
            "            pointHighlightStroke: \"rgba(0,255,0,1)\", " +
            "            data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()] " +
            "        }, " +
           "     { " +
          "  label: \"My Second dataset\", " +
          "          fillColor: \"rgba(255,255,0,0.2)\", " +
          "          strokeColor: \"rgba(255,255,0,1)\", " +
          "          pointColor: \"rgba(255,255,0,1)\", " +
          "          pointStrokeColor: \"#fff\", "+
          "          pointHighlightFill: \"#fff\", "+
          "          pointHighlightStroke: \"rgba(255,255,0,1)\", " +
          "          data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()] " +
         "       }, " +
         "       { " +
         "   label: \"My Second dataset\", " +
         "           fillColor: \"rgba(151,187,205,0.2)\", " +
         "           strokeColor: \"rgba(151,187,205,1)\", " +
         "           pointColor: \"rgba(151,187,205,1)\", " +
         "           pointStrokeColor: \"#fff\", "+
         "           pointHighlightFill: \"#fff\", "+
         "           pointHighlightStroke: \"rgba(151,187,205,1)\", " +
         "           data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()] " +
         "       }, " +
         "       { " +
         "   label: \"My Second dataset\", " +
       "             fillColor: \"rgba(151,187,205,0.2)\", " +
       "             strokeColor: \"rgba(151,187,205,1)\", " +
       "             pointColor: \"rgba(151,187,205,1)\", " +
       "             pointStrokeColor: \"#fff\", "+
       "             pointHighlightFill: \"#fff\", "+
       "             pointHighlightStroke: \"rgba(151,187,205,1)\", " +
       "             data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()] " +
       "         } " +
       "         ] " +
       "     }";

            ViewBag.data1 = data1;

            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
