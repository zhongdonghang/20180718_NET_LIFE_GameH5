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
        public ActionResult GetGridJson(string keyword)
        {
            var data = gameLogApp.GetList(keyword);
            return Content(data.ToJson());
        }

    }
}
