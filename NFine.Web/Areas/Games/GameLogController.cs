using NFine.Application.TGameLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Code;

namespace NFine.Web.Areas.Games
{
    public class GameLogController : Controller
    {
        //
        // GET: /Games/GameLog/
        TGameLogApp gameLogApp = new TGameLogApp();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword,string selGame,string selWinOrLost,string selTime)
        {
            
             var data = new
            {
                rows = gameLogApp.GetList(pagination, keyword,selGame,selWinOrLost,selTime),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
             


            //var data = gameLogApp.GetList(keyword);
            //return Content(data.ToJson());
        }

    }
}
