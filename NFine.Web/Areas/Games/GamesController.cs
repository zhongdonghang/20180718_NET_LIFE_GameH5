﻿using NFine.Application;
using NFine.Application.TGameLog;
using NFine.Domain._03_Entity.T_Game.GameSetting;
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

        #region 围住神经猫
        //loginID=LB33255558&userID=LB332555588888888&LBOrLoveBird=LB
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

            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];
            return new RedirectResult("/GameContent/shenjingmao2/index.html");
        }

        public void Shenjingmao2ResultHandle()
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


            string currentScore = Request.Params["score"];//本次游戏的分数
            string LBAccount = Session["loginID"].ToString();
            string userID = Session["userID"].ToString();

            //先判断玩家是否是第一次玩
            TGameLogApp app = new TGameLogApp();

            ///从数据库获取游戏设置
            Shenjingmao2Setting setting = new Shenjingmao2Setting();
            setting.GameName = "围住神经猫";
            setting.IsWinWithHighest = true;
            setting.LBRatio = 1;
            setting.LoveBirdRatio = 1;

            int F_CoinType = 2;
            if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
            if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;

            if (app.GetGameLogByAccount(LBAccount, "sjm2"))//不是第一次玩
            {
                //取出历史最高分的记录
                double maxScore = app.GetMaxScoreByAccount(LBAccount, F_CoinType,"sjm2");
                //跟当前分数比，大于等于最高分，视为赢，赠送相应的积分,否则视为输
                if (double.Parse(currentScore) <= maxScore)//大于等于历史最高分，win
                {
                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "sjm2";
                    log.F_Score = int.Parse(currentScore);
                    log.F_GameScore = int.Parse(currentScore);
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 1;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "玩游戏得分" + log.F_GameScore;
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
                else //Lost
                {
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "sjm2";
                    log.F_Score = int.Parse(currentScore);
                    log.F_GameScore = int.Parse(currentScore);
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 2;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    double tmp = double.Parse(currentScore) - maxScore;
                    log.F_Remark = "玩游戏得分" + log.F_GameScore + ",游戏输了,和最高分相差" + tmp;
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
            else//第一次玩
            {
                //写入记录，肯定是赢，赠送积分
                TGameLogEntity log = new TGameLogEntity();
                log.F_Id = Guid.NewGuid().ToString();
                log.F_LBAccount = LBAccount;
                log.F_LogNo = userID;
                log.F_GameNo = "sjm2";
                log.F_Score = int.Parse(currentScore);
                log.F_GameScore = int.Parse(currentScore);
                log.F_CoinType = F_CoinType;
                log.F_WinOrLost = 1;
                log.F_LogState = 0;
                log.F_LogTime = DateTime.Now;
                log.F_LogType = 0;
                log.F_LogFlag = 0;
                log.F_Remark = "第一次玩游戏，赢得积分" + log.F_GameScore;
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
        #endregion

        #region 看看你有多色
        //loginID=LB33255558&userID=556f3452-5eb0-42e1-b1de-e5b5daa544b1&LBOrLoveBird=LB
        public ActionResult Se()
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




            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];

            //判断是不是有足够币来进行游戏
            SeSetting setting = new SeSetting();
            setting.LowestPlayLoveBird = 1000;
            string coinType = "";
            if (Session["LBOrLoveBird"].ToString() == "LB")
            {
                coinType = "2";
            }else
            {
                coinType = "1";
            }

            bool isTrue = CommonTools.CheckPlayerCoinToGame(setting.LowestPlayLoveBird+"", Request.Params["userID"], coinType);
            if (!isTrue)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('您的"+ Session["LBOrLoveBird"] + "余额不足，至少需要"+ setting.LowestPlayLoveBird + "个,请充值');</script></head><body></body></html>");
                Response.End();
            }

            return new RedirectResult("/GameContent/se/index.html");
        }

        public string SeResultHandle()
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
            string ret = "";

            string currentScore = Request.Params["score"];//本次游戏的分数
            string LBAccount = Session["loginID"].ToString();
            string userID = Session["userID"].ToString();

            //先判断玩家是否是第一次玩
            TGameLogApp app = new TGameLogApp();
            ///从数据库获取游戏设置
            SeSetting setting = new SeSetting();
            setting.GameName = "看看你有多色";
            setting.IsWinWithHighest = true;
            setting.LBRatio = 1;
            setting.LoveBirdRatio = 1;

            int F_CoinType = 2;

            if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
            if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;

           

            if (app.GetGameLogByAccount(LBAccount, "se"))//不是第一次玩
            {
                //取出历史最高分的记录
                double maxScore = app.GetMaxScoreByAccount(LBAccount, F_CoinType,"se");
                //跟当前分数比，大于等于最高分，视为赢，赠送相应的积分,否则视为输
                if (double.Parse(currentScore) >= maxScore)//大于等于历史最高分，win
                {
                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "se";
                    log.F_Score = int.Parse(currentScore);
                    log.F_GameScore = int.Parse(currentScore);
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 1;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "玩游戏得分" + log.F_GameScore;
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);



                    //积分操作
                    if (CommonTools.GiveCoinToPlayer(userID, log.F_Score.ToString(), F_CoinType.ToString(), setting.GameName))
                    {
                        ret = "你赢了！恭喜你获得" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                    }
                    else
                    {
                        ret = "网络错误，赠送积分失败";
                    }


                }
                else //Lost
                {
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "se";
                    log.F_Score = int.Parse(currentScore);
                    log.F_GameScore = int.Parse(currentScore);
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 2;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    double tmp = double.Parse(currentScore) - maxScore;
                    log.F_Remark = "玩游戏得分" + log.F_GameScore + ",游戏输了,和最高分相差" + tmp;
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);
                    

                    if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting.GameName))
                    {
                        ret = "你输了！扣除" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个，你和历史最高分相差了" + tmp + "个";
                    }
                    else
                    {
                        ret = "网络错误，扣除积分失败";
                    }

                }
            }
            else//第一次玩
            {
                //写入记录，肯定是赢，赠送积分
                TGameLogEntity log = new TGameLogEntity();
                log.F_Id = Guid.NewGuid().ToString();
                log.F_LBAccount = LBAccount;
                log.F_LogNo = userID;
                log.F_GameNo = "se";
                log.F_Score = int.Parse(currentScore);
                log.F_GameScore = int.Parse(currentScore);
                log.F_CoinType = F_CoinType;
                log.F_WinOrLost = 1;
                log.F_LogState = 0;
                log.F_LogTime = DateTime.Now;
                log.F_LogType = 0;
                log.F_LogFlag = 0;
                log.F_Remark = "第一次玩游戏，赢得积分" + log.F_GameScore;
                log.F_MarkTime = DateTime.Now;
                log.F_CreatorUserId = "system";
                log.F_CreatorTime = DateTime.Now;
                log.F_DeleteMark = false;
                log.F_DeleteUserId = "";
                log.F_DeleteTime = null;
                log.F_LastModifyUserId = "";
                log.F_LastModifyTime = null;
                app.SubmitForm(log, string.Empty);


                if (CommonTools.GiveCoinToPlayer(userID, log.F_Score.ToString(), F_CoinType.ToString(), setting.GameName))
                {
                    ret = "你赢了！恭喜你获得" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                }
                else
                {
                    ret = "网络错误，赠送积分失败";
                }
            }
            return ret;
        }

        #endregion

        #region 消消看

        //loginID=LB33255558&userID=556f3452-5eb0-42e1-b1de-e5b5daa544b1&LBOrLoveBird=LB
        public ActionResult XXK()
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
            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];

            //判断是不是有足够币来进行游戏
            XXKSetting setting = new XXKSetting();
            setting.LowestPlayLoveBird = 1000;
            string coinType = "";
            if (Session["LBOrLoveBird"].ToString() == "LB")
            {
                coinType = "2";
            }
            else
            {
                coinType = "1";
            }

            bool isTrue = CommonTools.CheckPlayerCoinToGame(setting.LowestPlayLoveBird + "", Request.Params["userID"], coinType);
            if (!isTrue)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('您的" + Session["LBOrLoveBird"] + "余额不足，至少需要" + setting.LowestPlayLoveBird + "个,请充值');</script></head><body></body></html>");
                Response.End();
            }

            return new RedirectResult("/GameContent/xxk1000/index.html");
        }

        public string XXKResultHandle()
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
            string ret = "";

            string currentScore = Request.Params["score"];//本次游戏的分数
            string LBAccount = Session["loginID"].ToString();
            string userID = Session["userID"].ToString();

            //先判断玩家是否是第一次玩
            TGameLogApp app = new TGameLogApp();
            ///从数据库获取游戏设置
            SeSetting setting = new SeSetting();
            setting.GameName = "看看你有多色";
            setting.IsWinWithHighest = true;
            setting.LBRatio = 1;
            setting.LoveBirdRatio = 1;

            int F_CoinType = 2;

            if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
            if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;

            if (app.GetGameLogByAccount(LBAccount, "XXK"))//不是第一次玩
            {
                //取出历史最高分的记录
                double maxScore = app.GetMaxScoreByAccount(LBAccount, F_CoinType, "XXK");
                //跟当前分数比，大于等于最高分，视为赢，赠送相应的积分,否则视为输
                if (double.Parse(currentScore) >= maxScore)//大于等于历史最高分，win
                {
                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "XXK";
                    log.F_Score = int.Parse(currentScore);
                    log.F_GameScore = int.Parse(currentScore);
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 1;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "玩游戏得分" + log.F_GameScore;
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);

                    //积分操作
                    if (CommonTools.GiveCoinToPlayer(userID, log.F_Score.ToString(), F_CoinType.ToString(), setting.GameName))
                    {
                        ret = "你赢了！恭喜你获得" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                    }
                    else
                    {
                        ret = "网络错误，赠送积分失败";
                    }
                }
                else //Lost
                {
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "XXK";
                    log.F_Score = int.Parse(currentScore);
                    log.F_GameScore = int.Parse(currentScore);
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 2;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    double tmp = double.Parse(currentScore) - maxScore;
                    log.F_Remark = "玩游戏得分" + log.F_GameScore + ",游戏输了,和最高分相差" + tmp;
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);


                    if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting.GameName))
                    {
                        ret = "你输了！扣除" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个，你和历史最高分相差了" + tmp + "个";
                    }
                    else
                    {
                        ret = "网络错误，扣除积分失败";
                    }

                }
            }
            else//第一次玩
            {
                //写入记录，肯定是赢，赠送积分
                TGameLogEntity log = new TGameLogEntity();
                log.F_Id = Guid.NewGuid().ToString();
                log.F_LBAccount = LBAccount;
                log.F_LogNo = userID;
                log.F_GameNo = "XXK";
                log.F_Score = int.Parse(currentScore);
                log.F_GameScore = int.Parse(currentScore);
                log.F_CoinType = F_CoinType;
                log.F_WinOrLost = 1;
                log.F_LogState = 0;
                log.F_LogTime = DateTime.Now;
                log.F_LogType = 0;
                log.F_LogFlag = 0;
                log.F_Remark = "第一次玩游戏，赢得积分" + log.F_GameScore;
                log.F_MarkTime = DateTime.Now;
                log.F_CreatorUserId = "system";
                log.F_CreatorTime = DateTime.Now;
                log.F_DeleteMark = false;
                log.F_DeleteUserId = "";
                log.F_DeleteTime = null;
                log.F_LastModifyUserId = "";
                log.F_LastModifyTime = null;
                app.SubmitForm(log, string.Empty);


                if (CommonTools.GiveCoinToPlayer(userID, log.F_Score.ToString(), F_CoinType.ToString(), setting.GameName))
                {
                    ret = "你赢了！恭喜你获得" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                }
                else
                {
                    ret = "网络错误，赠送积分失败";
                }
            }
            return ret;
        }

        #endregion
    }
}
