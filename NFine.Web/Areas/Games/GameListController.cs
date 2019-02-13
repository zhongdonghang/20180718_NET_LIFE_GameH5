using NFine.Application.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Code;

namespace NFine.Web.Areas.Games
{
    public class GameListController : ControllerBase
    {
        //
        // GET: /Games/GameList/

        TGameApp gameApp = new TGameApp();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = gameApp.GetList(keyword);
            return Content(data.ToJson());
        }

        public ActionResult GameSetting()
        {
            return View();
        }

    }
}
