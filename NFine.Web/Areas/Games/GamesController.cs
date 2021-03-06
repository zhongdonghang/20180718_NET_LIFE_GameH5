﻿using Newtonsoft.Json.Linq;
using NFine.Application;
using NFine.Application.TGame;
using NFine.Application.TGameLog;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.GameSetting;
using NFine.Domain.Entity.TGame;
using NFine.Domain.Entity.TGameLog;
using NFine.Web.App_Start._01_Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.Games
{
    public class GamesController : GameBaseController
    {
        //
        // GET: /Games/Games/

        TGameApp gameApp = new TGameApp();
      string PersonGameMaxLoveBird =  System.Configuration.ConfigurationManager.AppSettings["PersonGameMaxLoveBird"].ToString();

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
            base.CheckUserLogin();
            base.CheckLBOrLoveBird();
            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];
            //判断是不是有足够币来进行游戏
            base.IsEnoughScoreToPlay("2", Session["userID"].ToString());

            //扣除入场分
            PayScoreForBeginGame("2", Session["userID"].ToString());
            return new RedirectResult("/GameContent/se/index.html");
        }

        public string SeResultHandle()
        {
            string ret = "";

            try
            {
                CheckUserLoginState();
                string currentScore = Request.Params["score"];//本次游戏的分数
                string LBAccount = Session["loginID"].ToString();
                string userID = Session["userID"].ToString();

                //先判断玩家是否是第一次玩
                TGameLogApp app = new TGameLogApp();

                ///从数据库获取游戏设置
                TGameEntity entity = gameApp.GetForm("2");
                JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);
                int F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;
                double maxScore = app.GetMaxScoreByAccount(LBAccount, F_CoinType, "se");

                if (app.GetGameLogByAccount(LBAccount, "se"))//不是第一次玩
                {
                    //取出历史最高分的记录

                    //跟当前分数比，大于等于最高分，视为赢，赠送相应的积分,否则视为输
                    if (double.Parse(currentScore) > maxScore)//大于等于历史最高分，win
                    {
                        //写入记录，赠送积分
                        TGameLogEntity log = new TGameLogEntity();
                        log.F_Id = Guid.NewGuid().ToString();
                        log.F_LBAccount = LBAccount;
                        log.F_LogNo = userID;
                        log.F_GameNo = "se";

                        double resultScore = double.Parse(currentScore) - maxScore; //计算实际得的游戏分
                        double Tax = double.Parse(setting["Tax"].ToString());//取出税率
                                                                             //按兑换比例计算实际得分（LB或者LoveBird）
                        double tmpLifeScore = CommonTools.GameScore2LifeScore((int)resultScore, F_CoinType, setting);//原始得分
                        log.F_Score = (int)(tmpLifeScore - ((double)tmpLifeScore * Tax)); //扣税得分
                        log.F_Tax = ((double)tmpLifeScore * Tax); //税金
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

                        if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                        {
                            Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                            Response.End();
                        }
                        else
                        {
                            //积分操作
                            if (LB2LoveBird("2", userID, log.F_Score.ToString()))
                            {
                                ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                            }
                            else
                            {
                                ret = "网络错误，赠送积分失败";
                            }
                        }
                    }
                    else //Lost
                    {
                        TGameLogEntity log = new TGameLogEntity();
                        log.F_Id = Guid.NewGuid().ToString();
                        log.F_LBAccount = LBAccount;
                        log.F_LogNo = userID;
                        log.F_GameNo = "se";
                        double resultScore = maxScore - double.Parse(currentScore);
                        log.F_Score = (int)CommonTools.GameScore2LifeScore((int)resultScore, F_CoinType, setting);
                        log.F_GameScore = int.Parse(currentScore);
                        log.F_CoinType = F_CoinType;
                        log.F_WinOrLost = 2;
                        log.F_LogState = 0;
                        log.F_LogTime = DateTime.Now;
                        log.F_LogType = 0;
                        log.F_LogFlag = 0;
                        log.F_Tax = 0;
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
                        if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting["GameName"].ToString()))
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

                    //计算税收
                    double Tax = double.Parse(setting["Tax"].ToString());
                    //按兑换比例计算实际得分（LB或者LoveBird）
                    int tmpLifeScore = (int)CommonTools.GameScore2LifeScore(int.Parse(currentScore), F_CoinType, setting);//原始得分
                    log.F_Score = tmpLifeScore - (int)((double)tmpLifeScore * Tax); //扣税得分
                    log.F_Tax = ((double)tmpLifeScore * Tax); //税金

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

                    if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                    {
                        Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                        Response.End();
                    }
                    else
                    {
                        if (LB2LoveBird("2", userID, log.F_Score.ToString()))
                        {
                            ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                        }
                        else
                        {
                            ret = "网络错误，赠送积分失败";
                        }
                    }
                }
                base.IsEnoughScoreToPlay("2", userID);
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.ToString());
                ret = "网络出现问题，请刷新再玩！";
            }
           
            return ret;
        }

        #endregion

        #region 消消看

        //loginID=LB33255558&userID=556f3452-5eb0-42e1-b1de-e5b5daa544b1&LBOrLoveBird=LB
        public ActionResult XXK()
        {
            base.CheckUserLogin();
            base.CheckLBOrLoveBird();

            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];

            //判断是不是有足够币来进行游戏
            base.IsEnoughScoreToPlay("3", Session["userID"].ToString());
            //扣除入场分
            PayScoreForBeginGame("3", Session["userID"].ToString());
            return new RedirectResult("/GameContent/xxk1000/index.html");
        }

        public string XXKResultHandle()
        {
            string ret = "";
            try
            {
                CheckUserLoginState();
                string currentScore = Request.Params["score"];//本次游戏的分数
                string LBAccount = Session["loginID"].ToString();
                string userID = Session["userID"].ToString();

                //先判断玩家是否是第一次玩
                TGameLogApp app = new TGameLogApp();
                ///从数据库获取游戏设置
                TGameEntity entity = gameApp.GetForm("3");
                JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);

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

                        //计算税收
                        double Tax = double.Parse(setting["Tax"].ToString());

                        double resultScore = double.Parse(currentScore) - maxScore;

                        int tmpLifeScore = (int)CommonTools.GameScore2LifeScore((int)resultScore, F_CoinType, setting);//原始得分

                        log.F_Score = tmpLifeScore - (int)((double)tmpLifeScore * Tax); //扣税得分
                        log.F_Tax = ((double)tmpLifeScore * Tax); //税金
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
                        if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                        {
                            Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                            Response.End();
                        }
                        else
                        {
                            if (LB2LoveBird("3", userID, log.F_Score.ToString()))
                            {
                                ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                            }
                            else
                            {
                                ret = "网络错误，赠送积分失败";
                            }
                        }
                    }
                    else //Lost
                    {
                        TGameLogEntity log = new TGameLogEntity();
                        log.F_Id = Guid.NewGuid().ToString();
                        log.F_LBAccount = LBAccount;
                        log.F_LogNo = userID;
                        log.F_GameNo = "XXK";

                        double resultScore = maxScore - double.Parse(currentScore);
                        log.F_Score = (int)CommonTools.GameScore2LifeScore((int)resultScore, F_CoinType, setting); //int.Parse(currentScore);
                        log.F_GameScore = int.Parse(currentScore);
                        log.F_CoinType = F_CoinType;
                        log.F_WinOrLost = 2;
                        log.F_LogState = 0;
                        log.F_LogTime = DateTime.Now;
                        log.F_LogType = 0;
                        log.F_LogFlag = 0;
                        log.F_Tax = 0;
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


                        if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting["GameName"].ToString()))
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

                    //计算税收
                    double Tax = double.Parse(setting["Tax"].ToString());
                    //按兑换比例计算实际得分（LB或者LoveBird）
                    int tmpLifeScore = (int)CommonTools.GameScore2LifeScore(int.Parse(currentScore), F_CoinType, setting);//原始得分
                    log.F_Score = tmpLifeScore - (int)((double)tmpLifeScore * Tax); //扣税得分
                    log.F_Tax = ((double)tmpLifeScore * Tax); //税金
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


                    //积分操作
                    if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                    {
                        Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                        Response.End();
                    }
                    else
                    {
                        if (LB2LoveBird("3", userID, log.F_Score.ToString()))
                        {
                            ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                        }
                        else
                        {
                            ret = "网络错误，赠送积分失败";
                        }
                    }
                }
                base.IsEnoughScoreToPlay("3",userID);
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.ToString());
                ret = "网络出现问题，请刷新再玩！";
            }
          
            return ret;
        }

        #endregion

        #region 扫雷

        //loginID=LB33255558&userID=556f3452-5eb0-42e1-b1de-e5b5daa544b1&LBOrLoveBird=LB
        public ActionResult SaoLei()
        {
            base.CheckUserLogin();
            base.CheckLBOrLoveBird();

            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];

            //判断是不是有足够币来进行游戏
            base.IsEnoughScoreToPlay("4", Session["userID"].ToString());
            //扣除入场分
            PayScoreForBeginGame("4", Session["userID"].ToString());
            return new RedirectResult("/GameContent/saolei/index.html");
        }

        public string SaoLeiResultHandle()
        {
            string ret = "";

            try
            {
                CheckUserLoginState();

                string LBAccount = Session["loginID"].ToString();
                string userID = Session["userID"].ToString();

                string result = Request["result"].Trim().ToLower();
                string times = "0";

                TGameLogApp app = new TGameLogApp();

                ///从数据库获取游戏设置
                TGameEntity entity = gameApp.GetForm("4");
                JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);

                //积分类型
                int F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;

                if (result == "win")//赢了
                {
                    times = Request["times"].Trim();
                    double timeSeconds = double.Parse(times);
                    timeSeconds /= 1000;
                    int RuleScore = 0;
                    if (timeSeconds <= double.Parse(setting["Rule1"]["Times"].ToString()))//第一级别
                    {
                        RuleScore = int.Parse(setting["Rule1"]["Score"].ToString());
                    }
                    else if (timeSeconds <= double.Parse(setting["Rule2"]["Times"].ToString()))//第二级别
                    {
                        RuleScore = int.Parse(setting["Rule2"]["Score"].ToString());
                    }
                    else if (timeSeconds <= double.Parse(setting["Rule3"]["Times"].ToString()))//第三级别
                    {
                        RuleScore = int.Parse(setting["Rule3"]["Score"].ToString());
                    }
                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "saolei";

                    //计算税收
                    double Tax = double.Parse(setting["Tax"].ToString());

                    //按兑换比例计算实际得分（LB或者LoveBird）
                    log.F_Score = RuleScore - (int)((double)RuleScore * Tax); //扣税得分
                    log.F_Tax = ((double)RuleScore * Tax); //税金
                    log.F_GameScore = RuleScore;
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 1;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "扫雷赢了" + log.F_Score + "积分,用时" + timeSeconds + "秒";
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
                    if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                    {
                        Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                        Response.End();
                    }
                    else
                    {
                        if (LB2LoveBird("4", userID, log.F_Score.ToString()))
                        {
                            ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                        }
                        else
                        {
                            ret = "网络错误，赠送积分失败";
                        }
                    }
                }
                else if (result == "lost")//输了
                {

                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "saolei";
                    //按兑换比例计算实际得分（LB或者LoveBird）
                    log.F_Score = int.Parse(setting["LostScore"].ToString());
                    log.F_Tax = 0; //税金
                    log.F_GameScore = int.Parse(setting["LostScore"].ToString());
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 2;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "扫雷输了扣除" + log.F_Score + "积分";
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);

                    if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting["GameName"].ToString()))
                    {
                        ret = "你输了！扣除" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                    }
                    else
                    {
                        ret = "网络错误，扣除积分失败";
                    }
                }
                base.IsEnoughScoreToPlay("4",userID);
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.ToString());
                ret = "网络出现问题，请刷新再玩！";
            }
          
            return ret;
        }

        #endregion

        #region 美女拼图

        //loginID=LB33255558&userID=556f3452-5eb0-42e1-b1de-e5b5daa544b1&LBOrLoveBird=LB
        public ActionResult MN()
        {
            base.CheckUserLogin();
            base.CheckLBOrLoveBird();

            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];

            //判断是不是有足够币来进行游戏
            base.IsEnoughScoreToPlay("5", Session["userID"].ToString());
            //扣除入场分
            PayScoreForBeginGame("5", Session["userID"].ToString());
            return new RedirectResult("/GameContent/mspt/index.html");
        }

        public string MNResultHandle()
        {
            string ret = "";
            try
            {

                CheckUserLoginState();

                string LBAccount = Session["loginID"].ToString();
                string userID = Session["userID"].ToString();

                string result = Request["result"].Trim().ToLower();

                TGameLogApp app = new TGameLogApp();
                ///从数据库获取游戏设置
                TGameEntity entity = gameApp.GetForm("5");
                JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);
                string level = Request["level"].Trim();
                //积分类型
                int F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;

                if (result == "win")//赢了
                {
                    int gameLevel = int.Parse(level);
                    int score = GetMNGameScore(gameLevel, true, setting);

                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "mspt";

                    //计算税收
                    double Tax = double.Parse(setting["Tax"].ToString());

                    //按兑换比例计算实际得分（LB或者LoveBird）
                    log.F_Score = score - (int)((double)score * Tax); //扣税得分
                    log.F_Tax = ((double)score * Tax); //税金
                    log.F_GameScore = score;
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 1;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "美女拼图赢了" + log.F_Score + "积分,关数为第" + gameLevel + "关";
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
                    if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                    {
                        Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                        Response.End();
                    }
                    else
                    {
                        if (LB2LoveBird("5", userID, log.F_Score.ToString()))
                        {
                            ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                        }
                        else
                        {
                            ret = "网络错误，赠送积分失败";
                        }
                    }
                }
                else
                {
                    int gameLevel = int.Parse(level);
                    int score = GetMNGameScore(gameLevel, false, setting);
                    //写入记录
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "mspt";
                    //按兑换比例计算实际得分（LB或者LoveBird）
                    log.F_Score = score;
                    log.F_Tax = 0; //税金
                    log.F_GameScore = score;
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 2;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "美女拼图在第" + gameLevel + "关输了,扣除" + log.F_Score + "积分";
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);

                    if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting["GameName"].ToString()))
                    {
                        ret = "你输了！扣除" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                    }
                    else
                    {
                        ret = "网络错误，扣除积分失败";
                    }
                }
                base.IsEnoughScoreToPlay("5",userID);
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.ToString());
                ret = "网络出现问题，请刷新再玩！";
            }
         
            return ret;
        }

        public int GetMNGameScore(int level,bool isWin, JObject setting)
        {
            int result = 0;
            switch (level)
            {
                case 1:
                    result = isWin ? int.Parse(setting["Rule1"]["WinScore"].ToString()) : int.Parse(setting["Rule1"]["LostScore"].ToString());
                    break;
                case 2:
                    result = isWin ? int.Parse(setting["Rule2"]["WinScore"].ToString()) : int.Parse(setting["Rule2"]["LostScore"].ToString());
                    break;
                case 3:
                    result = isWin ? int.Parse(setting["Rule3"]["WinScore"].ToString()) : int.Parse(setting["Rule3"]["LostScore"].ToString());
                    break;
                case 4:
                    result = isWin ? int.Parse(setting["Rule4"]["WinScore"].ToString()) : int.Parse(setting["Rule4"]["LostScore"].ToString());
                    break;
                case 5:
                    result = isWin ? int.Parse(setting["Rule5"]["WinScore"].ToString()) : int.Parse(setting["Rule5"]["LostScore"].ToString());
                    break;
                case 6:
                    result = isWin ? int.Parse(setting["Rule6"]["WinScore"].ToString()) : int.Parse(setting["Rule6"]["LostScore"].ToString());
                    break;
                case 7:
                    result = isWin ? int.Parse(setting["Rule7"]["WinScore"].ToString()) : int.Parse(setting["Rule7"]["LostScore"].ToString());
                    break;
                case 8:
                    result = isWin ? int.Parse(setting["Rule8"]["WinScore"].ToString()) : int.Parse(setting["Rule8"]["LostScore"].ToString());
                    break;
                case 9:
                    result = isWin ? int.Parse(setting["Rule9"]["WinScore"].ToString()) : int.Parse(setting["Rule9"]["LostScore"].ToString());
                    break;
                case 10:
                    result = isWin ? int.Parse(setting["Rule10"]["WinScore"].ToString()) : int.Parse(setting["Rule10"]["LostScore"].ToString());
                    break;
                default:
                    break;
            }

            return result;
        }

        #endregion

        #region 疯狂算术

        //loginID=LB33255558&userID=556f3452-5eb0-42e1-b1de-e5b5daa544b1&LBOrLoveBird=LB
        public ActionResult SST()
        {
            base.CheckUserLogin();
            base.CheckLBOrLoveBird();
            Session["loginID"] = Request.Params["loginID"];
            Session["userID"] = Request.Params["userID"];
            Session["LBOrLoveBird"] = Request.Params["LBOrLoveBird"];
            //判断是不是有足够币来进行游戏
            base.IsEnoughScoreToPlay("6", Session["userID"].ToString());
            //扣除入场分
            PayScoreForBeginGame("6", Session["userID"].ToString());
            return new RedirectResult("/GameContent/sst/index.html");
        }

        public string SSTResultHandle()
        {
            string ret = "";

            try
            {
                CheckUserLoginState();

                string LBAccount = Session["loginID"].ToString();
                string userID = Session["userID"].ToString();
                string result = Request["gameResult"].Trim().ToLower();
                TGameLogApp app = new TGameLogApp();
                ///从数据库获取游戏设置
                TGameEntity entity = gameApp.GetForm("6");
                JObject setting = NFine.Code.Json.ToJObject(entity.F_Setting);
                //积分类型
                int F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LB") F_CoinType = 2;
                if (Session["LBOrLoveBird"].ToString() == "LoveBird") F_CoinType = 1;
                //计算游戏应得分数
                int count = int.Parse(result);
                int winLevel1 = int.Parse(setting["WinLevel1"]["WinCount"].ToString());
                int winLevel2 = int.Parse(setting["WinLevel2"]["WinCount"].ToString());
                int LostLevel1 = int.Parse(setting["LostLevel1"]["LostCount"].ToString());
                int LostLevel2 = int.Parse(setting["LostLevel2"]["LostCount"].ToString());

                bool isWin = false;
                int score = 0;
                if (count >= winLevel1)
                {
                    isWin = true;
                    score = int.Parse(setting["WinLevel1"]["WinScore"].ToString());
                }
                else if (count >= winLevel2 && count < winLevel1)
                {
                    isWin = true;
                    score = int.Parse(setting["WinLevel2"]["WinScore"].ToString());
                }

                if (count < LostLevel1 && count >= LostLevel2)
                {
                    isWin = false;
                    score = int.Parse(setting["LostLevel1"]["LostScore"].ToString());
                }
                else if (count < LostLevel2)
                {
                    isWin = false;
                    score = int.Parse(setting["LostLevel2"]["LostScore"].ToString());
                }

                //计算税收

                if (isWin)
                {
                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "sst";

                    double Tax = double.Parse(setting["Tax"].ToString());
                    //按兑换比例计算实际得分（LB或者LoveBird）
                    log.F_Score = score - (int)((double)score * Tax); //扣税得分
                    log.F_Tax = ((double)score * Tax); //税金
                    log.F_GameScore = count;
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 1;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "疯狂算术题赢了" + log.F_Score + "积分,答对了" + count + "道题,扣除税金" + log.F_Tax + "个";
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
                    if (CommonTools.CalcPersonGameMaxLoveBird(LBAccount))//超额
                    {
                        Response.Write("<html><head><title>系统提示</title><script>alert('您当日赢的LoveBird积分数量已经超过系统限制额度[" + PersonGameMaxLoveBird + "]LoveBird积分，请明日再来!');</script></head><body></body></html>");
                        Response.End();
                    }
                    else
                    {
                        if (LB2LoveBird("6", userID, log.F_Score.ToString()))
                        {
                            ret = "你赢了，你的LoveBird积分增加了" + log.F_Score.ToString() + "个！";
                        }
                        else
                        {
                            ret = "网络错误，赠送积分失败";
                        }
                    }
                }
                else //输了
                {

                    //写入记录，赠送积分
                    TGameLogEntity log = new TGameLogEntity();
                    log.F_Id = Guid.NewGuid().ToString();
                    log.F_LBAccount = LBAccount;
                    log.F_LogNo = userID;
                    log.F_GameNo = "sst";
                    //按兑换比例计算实际得分（LB或者LoveBird）
                    log.F_Score = score;
                    log.F_Tax = 0; //税金
                    log.F_GameScore = count;
                    log.F_CoinType = F_CoinType;
                    log.F_WinOrLost = 2;
                    log.F_LogState = 0;
                    log.F_LogTime = DateTime.Now;
                    log.F_LogType = 0;
                    log.F_LogFlag = 0;
                    log.F_Remark = "疯狂算术题输了扣除" + log.F_Score + "积分,共答对了" + count + "道题";
                    log.F_MarkTime = DateTime.Now;
                    log.F_CreatorUserId = "system";
                    log.F_CreatorTime = DateTime.Now;
                    log.F_DeleteMark = false;
                    log.F_DeleteUserId = "";
                    log.F_DeleteTime = null;
                    log.F_LastModifyUserId = "";
                    log.F_LastModifyTime = null;
                    app.SubmitForm(log, string.Empty);

                    if (CommonTools.GiveCoinToPlayer(userID, "-" + log.F_Score.ToString(), F_CoinType.ToString(), setting["GameName"].ToString()))
                    {
                        ret = "你输了！扣除" + Session["LBOrLoveBird"].ToString() + "积分" + log.F_Score + "个";
                    }
                    else
                    {
                        ret = "网络错误，扣除积分失败";
                    }
                }
                base.IsEnoughScoreToPlay("6",userID);
            }
            catch (Exception ex)
            {
                WriteLog.Write(ex.ToString());
                ret = "网络出现问题，请刷新再玩！";
            }
            
            return ret;
        }

        #endregion
    }
}
