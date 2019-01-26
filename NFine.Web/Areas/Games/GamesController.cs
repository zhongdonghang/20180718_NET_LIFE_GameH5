using NFine.Application.TGameLog;
using NFine.Domain.Entity.TGameLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.Games
{
    public class GamesController : Controller
    {
        //
        // GET: /Games/Games/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shenjingmao2()
        {
            if (Request.Params["loginID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            else if (string.IsNullOrEmpty(Request.Params["loginID"]))
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            Session["loginID"] = Request.Params["loginID"];
            return new RedirectResult("/GameContent/shenjingmao2/index.html");
        }

        public void Shenjingmao2ResultHandle()
        {
            if (Session["loginID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            string scorc = Request.Params["score"];
            string LBAccount = Session["loginID"].ToString();

            //先判断玩家是否是第一次玩
            TGameLogApp app = new TGameLogApp();
            if (app.GetGameLogByAccount(LBAccount))//不是第一次玩
            {
                //取出历史最高分的记录

                
                //跟当前分数比，大于等于最高分，视为赢，赠送相应的积分,否则视为输

            }
            else//第一次玩
            {
                //写入记录，肯定是赢，赠送积分
                TGameLogEntity log = new TGameLogEntity();
                log.F_Id = Guid.NewGuid().ToString();
                log.F_LBAccount = LBAccount;
                log.F_LogNo = "";
                log.F_GameNo = 1;
                log.F_Score = int.Parse(scorc);
                log.F_GameScore = int.Parse(scorc);
                log.F_CoinType = 1;
                log.F_WinOrLost = 1;
                log.F_LogState = 0;
                log.F_LogTime = DateTime.Now;
                log.F_LogType = 0;
                log.F_LogFlag = 0;
                log.F_Remark = "第一次玩游戏，赢得积分"+ log.F_GameScore;
                log.F_MarkTime = DateTime.Now;
                log.F_CreatorUserId = "system";
                log.F_CreatorTime = DateTime.Now;
                log.F_DeleteMark = false;
                log.F_DeleteUserId = "";
                log.F_DeleteTime = null;
                log.F_LastModifyUserId = "";
                log.F_LastModifyTime = null;
                app.SubmitForm(log, string.Empty);
            }


        }
    }
}
