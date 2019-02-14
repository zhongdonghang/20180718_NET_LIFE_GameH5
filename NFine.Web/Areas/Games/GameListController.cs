using NFine.Application.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.GameSetting;
using NFine.Domain.Entity.TGame;
using Newtonsoft.Json.Linq;

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
            TGameEntity entity =  gameApp.GetForm("2");
            JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);
            ViewBag.GameName = setting["GameName"].ToString();
            ViewBag.IsWinWithHighest = setting["IsWinWithHighest"].ToString();
            ViewBag.WinLevelScore = setting["WinLevelScore"].ToString();
            ViewBag.LBRatio = setting["LBRatio"].ToString();
            ViewBag.LoveBirdRatio = setting["LoveBirdRatio"].ToString();
            ViewBag.LowestPlayLoveBird = setting["LowestPlayLoveBird"].ToString();
            ViewBag.LowestPlayLB = setting["LowestPlayLB"].ToString();
            return View();
        }

        

        public string SaveSeSetting()
        {
            string txtGameSeName = Request["txtGameSeName"].Trim();
            string txtGameSeScoreForLB = Request["txtGameSeScoreForLB"].Trim();
            string txtGameSeScoreForLoveBird = Request["txtGameSeScoreForLoveBird"].Trim();
            string txtLowestPlayLBForSe = Request["txtLowestPlayLBForSe"].Trim();
            string txtLowestPlayLoveBirdForSe = Request["txtLowestPlayLoveBirdForSe"].Trim();

            SeSetting setting = new SeSetting();
            setting.GameName = txtGameSeName;
            setting.LBRatio = double.Parse(txtGameSeScoreForLB);
            setting.LoveBirdRatio = double.Parse(txtGameSeScoreForLoveBird);
            setting.LowestPlayLB = double.Parse(txtLowestPlayLBForSe);
            setting.LowestPlayLoveBird = double.Parse(txtLowestPlayLoveBirdForSe);
            setting.IsWinWithHighest = true;
            setting.WinLevelScore = 0;

           string jsonStr =  NFine.Code.Json.ToJson(setting);

         
            TGameEntity entity = gameApp.GetForm("2");
            entity.F_Setting = jsonStr;
            gameApp.SubmitForm(entity, "2");
            return "yes";
        }


    }
}
