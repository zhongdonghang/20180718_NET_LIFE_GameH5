using Newtonsoft.Json.Linq;
using NFine.Application;
using NFine.Application.TGame;
using NFine.Domain.Entity.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.App_Start._01_Handler
{
    public class GameBaseController: Controller
    {
        public void IsEnoughScoreToPlay(string gameNo)
        {
            TGameApp gameApp = new TGameApp();

            TGameEntity entity = gameApp.GetForm(gameNo);
            JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);

            bool isTrue = false;
            if (Session["LBOrLoveBird"].ToString() == "LB")//用LB进行游戏
            {
                isTrue = CommonTools.CheckPlayerCoinToGame(setting["LowestPlayLB"].ToString(), Request.Params["userID"], "2");
                if (!isTrue)
                {
                    Response.Write("<html><head><title>系统提示</title><script>alert('您的" + Session["LBOrLoveBird"] + "积分余额不足，至少需要" + setting["LowestPlayLB"].ToString() + "个,请充值'); history.go(-1);</script></head><body></body></html>");
                    Response.End();
                }
            }
            else
            {
                isTrue = CommonTools.CheckPlayerCoinToGame(setting["LowestPlayLoveBird"].ToString(), Request.Params["userID"], "1");
                if (!isTrue)
                {
                    Response.Write("<html><head><title>系统提示</title><script>alert('您的" + Session["LBOrLoveBird"] + "积分余额不足，至少需要" + setting["LowestPlayLoveBird"].ToString() + "个,请充值');history.go(-1);</script></head><body></body></html>");
                    Response.End();
                }
            }

        }

        public void CheckLBOrLoveBird()
        {
            if (Request.Params["LBOrLoveBird"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('参数不对，请传入是消耗LB积分还是LoveBird积分');</script></head><body></body></html>");
                Response.End();
            }
            else if (string.IsNullOrEmpty(Request.Params["LBOrLoveBird"]))
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('参数不对，请传入是消耗LB积分还是LoveBird积分');</script></head><body></body></html>");
                Response.End();
            }
        }

        public void CheckUserLogin()
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
            if (Request.Params["userID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            else if (string.IsNullOrEmpty(Request.Params["userID"]))
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
        }
    }
}