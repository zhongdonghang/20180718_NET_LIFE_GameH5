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

            //加载显示美女拼图规则
            TGameEntity mn = gameApp.GetForm("5");
            if (mn.F_Setting != string.Empty)
            {
                JObject mnSetting = NFine.Code.Json.ToJObject(mn.F_Setting);

                ViewBag.txtGameMNName = mnSetting["GameName"].ToString();

                ViewBag.txtGameMNWinScore1 = mnSetting["Rule1"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore1 = mnSetting["Rule1"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore2 = mnSetting["Rule2"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore2 = mnSetting["Rule2"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore3 = mnSetting["Rule3"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore3 = mnSetting["Rule3"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore4 = mnSetting["Rule4"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore4 = mnSetting["Rule4"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore5 = mnSetting["Rule5"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore5 = mnSetting["Rule5"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore6 = mnSetting["Rule6"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore6 = mnSetting["Rule6"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore7 = mnSetting["Rule7"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore7 = mnSetting["Rule7"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore8 = mnSetting["Rule8"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore8 = mnSetting["Rule8"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore9 = mnSetting["Rule9"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore9 = mnSetting["Rule9"]["LostScore"].ToString();

                ViewBag.txtGameMNWinScore10 = mnSetting["Rule10"]["WinScore"].ToString();
                ViewBag.txtGameMNLostScore10 = mnSetting["Rule10"]["LostScore"].ToString();

                ViewBag.txtGameMNWinAllScore = mnSetting["ALlInScore"].ToString();
                ViewBag.txtLowestPlayLBForMN = mnSetting["LowestPlayLB"].ToString();
                ViewBag.txtLowestPlayLoveBirdForMN = mnSetting["LowestPlayLoveBird"].ToString();

                ViewBag.txtGameMNTax = double.Parse(mnSetting["Tax"].ToString()) * 100;
            }
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

        /// <summary>
        /// 保存疯狂算术题设置
        /// </summary>
        /// <returns></returns>
        public string SaveSSTSetting()
        {
            string txtGameSSTName = Request["txtGameSSTName"].Trim();
            string txtGameSSTWinCount1 = Request["txtGameSSTWinCount1"].Trim();
            string txtGameSSTWinScore1 = Request["txtGameSSTWinScore1"].Trim();

            string txtGameSSTWinCount2 = Request["txtGameSSTWinCount2"].Trim();
            string txtGameSSTWinScore2 = Request["txtGameSSTWinScore2"].Trim();

            string txtGameSSTLostCount1 = Request["txtGameSSTLostCount1"].Trim();
            string txtGameSSTLostScore1 = Request["txtGameSSTLostScore1"].Trim();

            string txtGameSSTLostCount2 = Request["txtGameSSTLostCount2"].Trim();
            string txtGameSSTLostScore2 = Request["txtGameSSTLostScore2"].Trim();

            string txtLowestPlayLBForSST = Request["txtLowestPlayLBForSST"].Trim();
            string txtLowestPlayLoveBirdForSST = Request["txtLowestPlayLoveBirdForSST"].Trim();

            string txtGameSSTTax = Request["txtGameSSTTax"].Trim();

            SSTSetting setting = new SSTSetting();
            setting.GameName = txtGameSSTName;
            setting.LowestPlayLB = double.Parse(txtLowestPlayLBForSST);
            setting.LowestPlayLoveBird = double.Parse(txtLowestPlayLoveBirdForSST);
            setting.Tax = double.Parse(txtGameSSTTax) / 100;

            setting.WinLevel1 = new SSTWinLevel();
            setting.WinLevel1.WinCount = int.Parse(txtGameSSTWinCount1);
            setting.WinLevel1.WinScore = int.Parse(txtGameSSTWinScore1);

            setting.WinLevel2 = new SSTWinLevel();
            setting.WinLevel2.WinCount = int.Parse(txtGameSSTWinCount2);
            setting.WinLevel2.WinScore = int.Parse(txtGameSSTWinScore2);

            setting.LostLevel1 = new SSTLostsLevel();
            setting.LostLevel1.LostCount = int.Parse(txtGameSSTLostCount1);
            setting.LostLevel1.LostScore = int.Parse(txtGameSSTLostScore1);

            setting.LostLevel2 = new SSTLostsLevel();
            setting.LostLevel2.LostCount = int.Parse(txtGameSSTLostCount2);
            setting.LostLevel2.LostScore = int.Parse(txtGameSSTLostScore2);

            string jsonStr = NFine.Code.Json.ToJson(setting);
            TGameEntity entity = gameApp.GetForm("6");
            entity.F_Setting = jsonStr;
            gameApp.SubmitForm(entity, "6");

            return "yes";
        }

        /// <summary>
        /// 保存美女拼图设置
        /// </summary>
        /// <returns></returns>
        public string SaveMNSetting()
        {
            string txtGameMNName = Request["txtGameMNName"].Trim();

            string txtGameMNWinScore1 = Request["txtGameMNWinScore1"].Trim();
            string txtGameMNLostScore1 = Request["txtGameMNLostScore1"].Trim();

            string txtGameMNWinScore2 = Request["txtGameMNWinScore2"].Trim();
            string txtGameMNLostScore2 = Request["txtGameMNLostScore2"].Trim();

            string txtGameMNWinScore3 = Request["txtGameMNWinScore3"].Trim();
            string txtGameMNLostScore3 = Request["txtGameMNLostScore3"].Trim();

            string txtGameMNWinScore4 = Request["txtGameMNWinScore4"].Trim();
            string txtGameMNLostScore4 = Request["txtGameMNLostScore4"].Trim();

            string txtGameMNWinScore5 = Request["txtGameMNWinScore5"].Trim();
            string txtGameMNLostScore5 = Request["txtGameMNLostScore5"].Trim();

            string txtGameMNWinScore6 = Request["txtGameMNWinScore6"].Trim();
            string txtGameMNLostScore6 = Request["txtGameMNLostScore6"].Trim();

            string txtGameMNWinScore7 = Request["txtGameMNWinScore7"].Trim();
            string txtGameMNLostScore7 = Request["txtGameMNLostScore7"].Trim();

            string txtGameMNWinScore8 = Request["txtGameMNWinScore8"].Trim();
            string txtGameMNLostScore8 = Request["txtGameMNLostScore8"].Trim();

            string txtGameMNWinScore9 = Request["txtGameMNWinScore9"].Trim();
            string txtGameMNLostScore9 = Request["txtGameMNLostScore9"].Trim();

            string txtGameMNWinScore10 = Request["txtGameMNWinScore10"].Trim();
            string txtGameMNLostScore10 = Request["txtGameMNLostScore10"].Trim();

            string txtGameMNWinAllScore = Request["txtGameMNWinAllScore"].Trim();

            string txtLowestPlayLBForMN = Request["txtLowestPlayLBForMN"].Trim();
            string txtLowestPlayLoveBirdForMN = Request["txtLowestPlayLoveBirdForMN"].Trim();
            string txtGameMNTax = Request["txtGameMNTax"].Trim();

            MsptSetting setting = new MsptSetting();
            setting.GameName = txtGameMNName;
            setting.LowestPlayLB = double.Parse(txtLowestPlayLBForMN);
            setting.LowestPlayLoveBird = double.Parse(txtLowestPlayLoveBirdForMN);
            setting.Tax = double.Parse(txtGameMNTax) / 100;

            setting.Rule1 = new MsptRule();
            setting.Rule1.Level = 1;
            setting.Rule1.WinScore = int.Parse(txtGameMNWinScore1);
            setting.Rule1.LostScore = int.Parse(txtGameMNLostScore1);

            setting.Rule2 = new MsptRule();
            setting.Rule2.Level = 2;
            setting.Rule2.WinScore = int.Parse(txtGameMNWinScore2);
            setting.Rule2.LostScore = int.Parse(txtGameMNLostScore2);

            setting.Rule3 = new MsptRule();
            setting.Rule3.Level = 3;
            setting.Rule3.WinScore = int.Parse(txtGameMNWinScore3);
            setting.Rule3.LostScore = int.Parse(txtGameMNLostScore3);

            setting.Rule4 = new MsptRule();
            setting.Rule4.Level = 4;
            setting.Rule4.WinScore = int.Parse(txtGameMNWinScore4);
            setting.Rule4.LostScore = int.Parse(txtGameMNLostScore4);

            setting.Rule5 = new MsptRule();
            setting.Rule5.Level = 5;
            setting.Rule5.WinScore = int.Parse(txtGameMNWinScore5);
            setting.Rule5.LostScore = int.Parse(txtGameMNLostScore5);

            setting.Rule6 = new MsptRule();
            setting.Rule6.Level = 6;
            setting.Rule6.WinScore = int.Parse(txtGameMNWinScore6);
            setting.Rule6.LostScore = int.Parse(txtGameMNLostScore6);

            setting.Rule7 = new MsptRule();
            setting.Rule7.Level = 7;
            setting.Rule7.WinScore = int.Parse(txtGameMNWinScore7);
            setting.Rule7.LostScore = int.Parse(txtGameMNLostScore7);

            setting.Rule8 = new MsptRule();
            setting.Rule8.Level = 8;
            setting.Rule8.WinScore = int.Parse(txtGameMNWinScore8);
            setting.Rule8.LostScore = int.Parse(txtGameMNLostScore8);

            setting.Rule9 = new MsptRule();
            setting.Rule9.Level = 9;
            setting.Rule9.WinScore = int.Parse(txtGameMNWinScore9);
            setting.Rule9.LostScore = int.Parse(txtGameMNLostScore9);

            setting.Rule10 = new MsptRule();
            setting.Rule10.Level = 10;
            setting.Rule10.WinScore = int.Parse(txtGameMNWinScore10);
            setting.Rule10.LostScore = int.Parse(txtGameMNLostScore10);

            setting.ALlInScore = int.Parse(txtGameMNWinAllScore);

            string jsonStr = NFine.Code.Json.ToJson(setting);
            TGameEntity entity = gameApp.GetForm("5");
            entity.F_Setting = jsonStr;
            gameApp.SubmitForm(entity, "5");

            return "yes";
        }


    }
}
