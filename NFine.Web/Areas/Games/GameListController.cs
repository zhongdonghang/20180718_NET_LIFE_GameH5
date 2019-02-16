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
            //加载看看你有多色的设置显示
            TGameEntity entity =  gameApp.GetForm("2");
            JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);
            ViewBag.GameName = setting["GameName"].ToString();
            ViewBag.IsWinWithHighest = setting["IsWinWithHighest"].ToString();
            ViewBag.WinLevelScore = setting["WinLevelScore"].ToString();
            ViewBag.LBRatio = setting["LBRatio"].ToString();
            ViewBag.LoveBirdRatio = setting["LoveBirdRatio"].ToString();
            ViewBag.LowestPlayLoveBird = setting["LowestPlayLoveBird"].ToString();
            ViewBag.LowestPlayLB = setting["LowestPlayLB"].ToString();
            ViewBag.SeTax = double.Parse(setting["Tax"].ToString())*100;

            //加载消消看的设置显示
            TGameEntity XXK = gameApp.GetForm("3");
            JObject XXKSetting = NFine.Code.Json.ToJObject(XXK.F_Setting);
            ViewBag.XXKGameName = XXKSetting["GameName"].ToString();
            ViewBag.XXKIsWinWithHighest = XXKSetting["IsWinWithHighest"].ToString();
            ViewBag.XXKWinLevelScore = XXKSetting["WinLevelScore"].ToString();
            ViewBag.XXKLBRatio = XXKSetting["LBRatio"].ToString();
            ViewBag.XXKLoveBirdRatio = XXKSetting["LoveBirdRatio"].ToString();
            ViewBag.XXKLowestPlayLoveBird = XXKSetting["LowestPlayLoveBird"].ToString();
            ViewBag.XXKLowestPlayLB = XXKSetting["LowestPlayLB"].ToString();
            ViewBag.XXKTax = double.Parse(XXKSetting["Tax"].ToString())*100;

            //加载扫雷的设置显示
            TGameEntity SL = gameApp.GetForm("4");
            JObject SLSetting = NFine.Code.Json.ToJObject(SL.F_Setting);

            ViewBag.txtGameSaoLeiName = SLSetting["GameName"].ToString();
            ViewBag.txtSaoLeiLostScore = SLSetting["LostScore"].ToString();
            ViewBag.txtSaoleiRuleTimes1 = SLSetting["Rule1"]["Times"].ToString();
            ViewBag.txtSaoleiRuleScore1 = SLSetting["Rule1"]["Score"].ToString();
            ViewBag.txtSaoleiRuleTimes2 = SLSetting["Rule2"]["Times"].ToString();
            ViewBag.txtSaoleiRuleScore2 = SLSetting["Rule2"]["Score"].ToString();
            ViewBag.txtSaoleiRuleTimes3 = SLSetting["Rule3"]["Times"].ToString();
            ViewBag.txtSaoleiRuleScore3 = SLSetting["Rule3"]["Score"].ToString();

            ViewBag.txtLowestPlayLBForSL = SLSetting["LowestPlayLB"].ToString();
            ViewBag.txtLowestPlayLoveBirdForSL = SLSetting["LowestPlayLoveBird"].ToString();
            ViewBag.txtGameSLTax = double.Parse(SLSetting["Tax"].ToString()) * 100;
            return View();
        }

        
        /// <summary>
        /// 保存看看你有多色的设置
        /// </summary>
        /// <returns></returns>
        public string SaveSeSetting()
        {
            string txtGameSeName = Request["txtGameSeName"].Trim();
            string txtGameSeScoreForLB = Request["txtGameSeScoreForLB"].Trim();
            string txtGameSeScoreForLoveBird = Request["txtGameSeScoreForLoveBird"].Trim();
            string txtLowestPlayLBForSe = Request["txtLowestPlayLBForSe"].Trim();
            string txtLowestPlayLoveBirdForSe = Request["txtLowestPlayLoveBirdForSe"].Trim();
            string txtTax = Request["txtGameSeTax"].Trim();
            SeSetting setting = new SeSetting();
            setting.GameName = txtGameSeName;
            setting.LBRatio = double.Parse(txtGameSeScoreForLB);
            setting.LoveBirdRatio = double.Parse(txtGameSeScoreForLoveBird);
            setting.LowestPlayLB = double.Parse(txtLowestPlayLBForSe);
            setting.LowestPlayLoveBird = double.Parse(txtLowestPlayLoveBirdForSe);
            setting.IsWinWithHighest = true;
            setting.WinLevelScore = 0;

            setting.Tax =double.Parse(txtTax) / 100;

            string jsonStr =  NFine.Code.Json.ToJson(setting);
            TGameEntity entity = gameApp.GetForm("2");
            entity.F_Setting = jsonStr;
            gameApp.SubmitForm(entity, "2");
            return "yes";
        }

        /// <summary>
        /// 保存消消看的设置
        /// </summary>
        /// <returns></returns>
        public string SaveXXKSetting()
        {
            string txtGameXXKName = Request["txtGameXXKName"].Trim();
            string txtGameXXKScoreForLB = Request["txtGameXXKScoreForLB"].Trim();
            string txtGameXXKScoreForLoveBird = Request["txtGameXXKScoreForLoveBird"].Trim();
            string txtLowestPlayLBForXXK = Request["txtLowestPlayLBForXXK"].Trim();
            string txtLowestPlayLoveBirdForXXK = Request["txtLowestPlayLoveBirdForXXK"].Trim();
            string txtTax = Request["txtGameXXKTax"].Trim();
            XXKSetting setting = new XXKSetting();
            setting.GameName = txtGameXXKName;
            setting.LBRatio = double.Parse(txtGameXXKScoreForLB);
            setting.LoveBirdRatio = double.Parse(txtGameXXKScoreForLoveBird);
            setting.LowestPlayLB = double.Parse(txtLowestPlayLBForXXK);
            setting.LowestPlayLoveBird = double.Parse(txtLowestPlayLoveBirdForXXK);
            setting.IsWinWithHighest = true;
            setting.WinLevelScore = 0;
            setting.Tax = double.Parse(txtTax) / 100;

            string jsonStr = NFine.Code.Json.ToJson(setting);
            TGameEntity entity = gameApp.GetForm("3");
            entity.F_Setting = jsonStr;
            gameApp.SubmitForm(entity, "3");
            return "yes";
        }

        /// <summary>
        /// 保存扫雷设置
        /// </summary>
        /// <returns></returns>
        public string SaveSLSetting()
        {
            string txtGameSaoLeiName = Request["txtGameSaoLeiName"].Trim();
            string txtSaoLeiLostScore = Request["txtSaoLeiLostScore"].Trim();
            string txtSaoleiRuleTimes1 = Request["txtSaoleiRuleTimes1"].Trim();
            string txtSaoleiRuleScore1 = Request["txtSaoleiRuleScore1"].Trim();
            string txtSaoleiRuleTimes2 = Request["txtSaoleiRuleTimes2"].Trim();
            string txtSaoleiRuleScore2 = Request["txtSaoleiRuleScore2"].Trim();
            string txtSaoleiRuleTimes3 = Request["txtSaoleiRuleTimes3"].Trim();
            string txtSaoleiRuleScore3 = Request["txtSaoleiRuleScore3"].Trim();
            string txtLowestPlayLBForSL = Request["txtLowestPlayLBForSL"].Trim();
            string txtLowestPlayLoveBirdForSL = Request["txtLowestPlayLoveBirdForSL"].Trim();
            string txtGameSLTax = Request["txtGameSLTax"].Trim();

            SaoLeiSetting setting = new SaoLeiSetting();

            setting.GameName = txtGameSaoLeiName;
            setting.LostScore = int.Parse(txtSaoLeiLostScore);

            setting.Rule1 = new SaoLeiRule();
            setting.Rule1.Times = double.Parse(txtSaoleiRuleTimes1);
            setting.Rule1.Score = int.Parse(txtSaoleiRuleScore1);

            setting.Rule2 = new SaoLeiRule();
            setting.Rule2.Times = double.Parse(txtSaoleiRuleTimes2);
            setting.Rule2.Score = int.Parse(txtSaoleiRuleScore2);

            setting.Rule3 = new SaoLeiRule();
            setting.Rule3.Times = double.Parse(txtSaoleiRuleTimes3);
            setting.Rule3.Score = int.Parse(txtSaoleiRuleScore3);

            setting.LowestPlayLB = double.Parse(txtLowestPlayLBForSL);
            setting.LowestPlayLoveBird = double.Parse(txtLowestPlayLoveBirdForSL);

            setting.Tax = double.Parse(txtGameSLTax) / 100;

            string jsonStr = NFine.Code.Json.ToJson(setting);
            TGameEntity entity = gameApp.GetForm("4");
            entity.F_Setting = jsonStr;
            gameApp.SubmitForm(entity, "4");

            return "yes";
        }


    }
}
