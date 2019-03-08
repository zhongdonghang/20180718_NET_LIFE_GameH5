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
        /// <summary>
        /// 游戏赢了，减去应得的Lb积分，换成LoveBird积分 1:1比例
        /// </summary>
        public bool LB2LoveBird(string gameNo, string uID,string LBCount)
        {
            string userId = uID;
            string gameName = "";
            string comeSum = LBCount;
            if (gameNo == "2") gameName = "H5-看你有多色";
            if (gameNo == "3") gameName = "H5-消消看";
            if (gameNo == "4") gameName = "H5-扫雷";
            if (gameNo == "5") gameName = "H5-美女拼图";
            if (gameNo == "6") gameName = "H5-疯狂算术";

            bool isTrue = false;

            if (comeSum.Trim() != "0")
            {
                //减积分
                if (CommonTools.GiveCoinToPlayer(userId, "-" + comeSum, "2", gameName))
                {
                    //增加积分
                    if (CommonTools.GiveCoinToPlayer(userId, comeSum, "1", gameName))
                    {
                        isTrue = true;
                    }
                }
            }
            else
            {
                isTrue = true;
            }

            return isTrue;
        }

        /// <summary>
        /// 扣除入场分操作
        /// </summary>
        /// <param name="gameNo"></param>
        /// <param name="uID"></param>
        public void PayScoreForBeginGame(string gameNo,string uID)
        {
            string userId = uID;
            string gameName = "";
            string comeSum = "";
            string type = "";
            TGameApp gameApp = new TGameApp();
            TGameEntity entity = gameApp.GetForm(gameNo);
            JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);
            comeSum = "-" + setting["PlayLBPay"].ToString().Trim();
            if (gameNo == "2") gameName = "H5-看你有多色-扣除入场分"+comeSum;
            if (gameNo == "3") gameName = "H5-消消看-扣除入场分" + comeSum;
            if (gameNo == "4") gameName = "H5-扫雷-扣除入场分" + comeSum;
            if (gameNo == "5") gameName = "H5-美女拼图-扣除入场分" + comeSum;
            if (gameNo == "6") gameName = "H5-疯狂算术-扣除入场分" + comeSum;
            type = "2";

            if (!CommonTools.GiveCoinToPlayer(userId, comeSum, type, gameName))
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('网络连接失败，或者您的积分不足，请检查！'); history.go(-1);</script></head><body></body></html>");
                Response.End();
            }
        }

        /// <summary>
        /// 判断是否有足够的积分来玩游戏
        /// </summary>
        /// <param name="gameNo"></param>
        public bool IsEnoughScoreToPlay(string gameNo,string userID)
        {
            TGameApp gameApp = new TGameApp();

            TGameEntity entity = gameApp.GetForm(gameNo);
            JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);

            bool isTrue = false;
            if (Session["LBOrLoveBird"].ToString() == "LB")//用LB进行游戏
            {
                isTrue = CommonTools.CheckPlayerCoinToGame(setting["LowestPlayLB"].ToString(), userID, "2");
                if (!isTrue)
                {
                    Response.Write("<html><head><title>系统提示</title><script>alert('您的" + Session["LBOrLoveBird"] + "积分余额不足，至少需要" + setting["LowestPlayLB"].ToString() + "个,请充值'); history.go(-1);</script></head><body></body></html>");
                    Response.End();
                }
            }
            else
            {
                isTrue = CommonTools.CheckPlayerCoinToGame(setting["LowestPlayLoveBird"].ToString(), userID, "1");
                if (!isTrue)
                {
                    Response.Write("<html><head><title>系统提示</title><script>alert('您的" + Session["LBOrLoveBird"] + "积分余额不足，至少需要" + setting["LowestPlayLoveBird"].ToString() + "个,请充值');history.go(-1);</script></head><body></body></html>");
                    Response.End();
                }
            }

            return isTrue;
        }

        /// <summary>
        /// 判断是否传入LB或者LoveBird参数
        /// </summary>
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

        /// <summary>
        /// 检查是否登录
        /// </summary>
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

        /// <summary>
        /// 检查登录状态
        /// </summary>
        public void CheckUserLoginState()
        {
            if (Session["loginID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            if (Session["userID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('登录超时，请重新登录进入游戏');</script></head><body></body></html>");
                Response.End();
            }
            if (Session["LBOrLoveBird"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('登录超时，请重新登录进入游戏');</script></head><body></body></html>");
                Response.End();
            }
        }
    }
}