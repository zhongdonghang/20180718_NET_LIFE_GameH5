//-----------------------------------------------------------------------
// <copyright file=" TGame.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: TGame.cs
// * history : Created by T4 01/22/2019 15:08:22 
// </copyright>
//-----------------------------------------------------------------------

using Maticsoft.DBUtility;
using Newtonsoft.Json.Linq;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.GameDescEntity;
using NFine.Domain.Entity.TGame;
using NFine.Domain.IRepository.TGame;
using NFine.Repository.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.TGame
{
    public class TGameApp
    {
		private ITGameRepository service = new TGameRepository();
        string PersonGameMaxLoveBird = System.Configuration.ConfigurationManager.AppSettings["PersonGameMaxLoveBird"].ToString();

        #region 游戏描述

        private const string GAME_SE_DESC = "大众类游戏，色盲者慎入！过一关游戏得一分，考验你眼力的时候来了，请不要眨眼睛哦，来，请开始你的表演！积分玩法，以你的最好成绩为准，超过你的最高分，视为赢得比赛，将按既定规则奖励LoveBird积分，否则扣除LB积分.";
        private const string GAME_XXK_DESC = "消除类游戏，消消乐游戏同款，挑战你的最高分吧！积分玩法，以你的最好成绩为准，超过你的最高分，视为赢得比赛，将按既定规则奖励LoveBird积分，否则扣除LB积分";
        private const string GAME_SAOLEI_DESC = "经典扫雷游戏，跟windows系统自带扫雷稍有区别，考验你逻辑能力的时刻来了，方块里数字1代表，周边有一个雷，数字2代表有两个雷，如此类推，请展示你真正的技术！积分规则，限时99秒，在这个时间内能扫清雷的，将获得LoveBird积分，踩到雷的将扣除积分，时间越快，获得LoveBird积分越多哦";
        private const string GAME_MSPT_DESC = "拼图雷游戏，总共有10关，每一个关卡都对成绩进行结算，每过一关将获得相应LoveBird积分，失败了则扣除LB积分，准备好了吗，Ready?。。。。。GO!";
        private const string GAME_SST_DESC = "快速算数题，考的是你的反映能力，每道题的答题时间是1秒钟，最后得分按通关量来算，通关越多，得分越高，最后将兑换成LoveBird积分，如果没达到积分关数要求，将扣除LB积分哦";

        public DataEntity GetGameInfo(string gameName, string LBAccount)
        {
                DataEntity e = new DataEntity();
                string sqlAllPlayerBestScore = "select max(F_GameScore) from [dbo].[T_GameLog] where F_GameNo='" + gameName + "'";
                string sqlMeBestScore = "select max(F_GameScore) from [dbo].[T_GameLog] where F_GameNo='" + gameName + "' and F_LBAccount='" + LBAccount + "'";
                string sqlAllPlayerCount = "select COUNT(distinct F_LBAccount) as playerCount from dbo.T_GameLog where F_GameNo='" + gameName + "'  Group by F_GameNo";

                object objAllPlayerBestScore = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlAllPlayerBestScore, null);
                e.AllPlayerBestScore = objAllPlayerBestScore == null ? "暂无" : objAllPlayerBestScore.ToString();

                object objMeBestScore = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlMeBestScore, null);
                e.MeBestScore = objMeBestScore == null ? "暂无" : objMeBestScore.ToString();

                object objAllPlayerCount = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlAllPlayerCount, null);
                e.AllPlayerCount = objAllPlayerCount == null ? "暂无" : (int.Parse( objAllPlayerCount.ToString())*100).ToString();

             //   e.AllPlayerCount = (int.Parse(e.AllPlayerCount) * 100).ToString();

                string sqlGame = "select F_Setting from T_Game where F_EName='" + gameName + "'";
                string setting = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sqlGame, null).ToString();
                JObject json = Json.ToJObject(setting);
                double tax = 0;
                tax = double.Parse(json["Tax"].ToString()) * 100;
                switch (gameName)
                {
                    case "se":
                    e.GameDesc = GAME_SE_DESC+",当前游戏每日赢得LoveBird积分限额"+ PersonGameMaxLoveBird + "个.";
                    e.ScoreRuleDesc = string.Format("看看你有多色游戏，至少要有{0}LB积分才能玩哦！一个LB积分等于{1}游戏积分，赢家将被按百分之{2}的税率收取税金.开始游戏需要扣除{3}LB积分作为进场费用。"+
                        " 游戏得分规则:如果你的分数比你的历史最高分高，那就多出那一部分分数按照跟LB积分的比例兑换后，扣除税率然后换成等比的LoveBird积分充值到你钱包账户。"+
                        " 游戏扣分规则：如果你的分数比你的历史最高分低，那分差部分按照LB积分的兑换比例兑换后，直接扣除相应的LB积分数量。", json["LowestPlayLB"], json["LBRatio"], tax,json["PlayLBPay"]);
                        break;
                    case "XXK":
                    e.GameDesc = GAME_XXK_DESC + ",当前游戏每日赢得LoveBird积分限额" + PersonGameMaxLoveBird + "个."; 
                    e.ScoreRuleDesc = string.Format("消消看游戏，最低的进场LB积分门槛是{0}积分,一个LB积分等于{1}游戏积分，赢家将被按百分之{2}的税率收取税金.开始此游戏需要扣除{3}LB积分作为进场费用。"+
                        " 游戏得分规则:如果你的分数比你的历史最高分高，那就多出那一部分分数按照跟LB积分的比例兑换后，扣除税率然后换成等比的LoveBird积分充值到你钱包账户。"+
                        "游戏扣分规则：如果你的分数比你的历史最高分低，那分差部分按照LB积分的兑换比例兑换后，直接扣除相应的LB积分数量。 ", json["LowestPlayLB"], json["LBRatio"], tax, json["PlayLBPay"]);
                        break;
                    case "saolei":
                    e.GameDesc = GAME_SAOLEI_DESC + ",当前游戏每日赢得LoveBird积分限额" + PersonGameMaxLoveBird + "个.";
                    e.ScoreRuleDesc = string.Format("扫雷游戏，最低的进场LB积分门槛是{0}积分,赢家将被按百分之{1}的税率收取税金，输了将被扣除{2}个LB积分,{3}秒内扫雷成功，"+
                        " 将获得{4}个LoveBird积分，{5}秒内扫雷成功，将获得{6}个LoveBird积分，{7}秒内扫雷成功，将获得{8}个LoveBird积分。开始此游戏需要扣除{9}LB积分作为进场费用。"+
                        " ", json["LowestPlayLB"], tax, json["LostScore"], json["Rule1"]["Times"], json["Rule1"]["Score"], json["Rule2"]["Times"], json["Rule2"]["Score"], json["Rule3"]["Times"], json["Rule3"]["Score"], json["PlayLBPay"]);
                        break;
                    case "mspt":
                    e.GameDesc = GAME_MSPT_DESC + ",当前游戏每日赢得LoveBird积分限额" + PersonGameMaxLoveBird + "个.";
                    e.ScoreRuleDesc = string.Format("美女拼图游戏,最低入场的LB积分门槛是{0}积分，赢家将被按百分之{1}的税率收取税金," +
                            "全通关将获取{2}个LB积分," +
                            " 通{3}关获得{4}个LoveBird积分,输了扣除{5}个LB积分," +
                            " 通{6}关获得{7}个LoveBird积分,输了扣除{8}个LB积分," +
                            " 通{9}关获得{10}个LoveBird积分,输了扣除{11}个LB积分," +
                            " 通{12}关获得{13}个LoveBird积分,输了扣除{14}个LB积分," +
                            " 通{15}关获得{16}个LoveBird积分,输了扣除{17}个LB积分," +
                            " 通{18}关获得{19}个LoveBird积分,输了扣除{20}个LB积分," +
                            " 通{21}关获得{22}个LoveBird积分,输了扣除{23}个LB积分," +
                            " 通{24}关获得{25}个LoveBird积分,输了扣除{26}个LB积分," +
                            " 通{27}关获得{28}个LoveBird积分,输了扣除{29}个LB积分," +
                            " 通{30}关获得{31}个LoveBird积分,输了扣除{32}个LB积分。开始游戏需要扣除{33}LB积分作为进场费用。",
                            json["LowestPlayLB"], tax, json["ALlInScore"],
                           "1", json["Rule1"]["WinScore"], json["Rule1"]["LostScore"],
                           "2", json["Rule2"]["WinScore"], json["Rule2"]["LostScore"],
                           "3", json["Rule3"]["WinScore"], json["Rule3"]["LostScore"],
                           "4", json["Rule4"]["WinScore"], json["Rule4"]["LostScore"],
                           "5", json["Rule5"]["WinScore"], json["Rule5"]["LostScore"],
                           "6", json["Rule6"]["WinScore"], json["Rule6"]["LostScore"],
                           "7", json["Rule7"]["WinScore"], json["Rule7"]["LostScore"],
                           "8", json["Rule8"]["WinScore"], json["Rule8"]["LostScore"],
                           "9", json["Rule9"]["WinScore"], json["Rule9"]["LostScore"],
                           "10", json["Rule10"]["WinScore"], json["Rule10"]["LostScore"],
                           json["PlayLBPay"]
                            );
                        break;
                    case "sst":
                    e.GameDesc = GAME_SST_DESC + ",当前游戏每日赢得LoveBird积分限额" + PersonGameMaxLoveBird + "个.";
                    e.ScoreRuleDesc = string.Format("疯狂算术题游戏,最低入场的LB积分门槛是{0}积分，赢家将被按百分之{1}的税率收取税金," +
                            "做对{2}题，奖励LoveBird积分{3}个，" +
                            "做对{4}题，奖励LoveBird积分{5}个，" +
                            "做不到{6}题，扣除LB积分{7}个，" +
                            "做不到{8}题，扣除LB积分{9}。开始游戏需要扣除{10}LB积分作为进场费用" +
                            " ",
                            json["LowestPlayLB"], tax,
                            json["WinLevel1"]["WinCount"], json["WinLevel1"]["WinScore"],
                            json["WinLevel2"]["WinCount"], json["WinLevel2"]["WinScore"],
                            json["LostLevel1"]["LostCount"], json["LostLevel1"]["LostScore"],
                            json["LostLevel2"]["LostCount"], json["LostLevel2"]["LostScore"], json["PlayLBPay"]
                            );
                        break;
                }
            return e;
        }

        #endregion



        public List<TGameEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<TGameEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_CName.Contains(keyword));
                expression = expression.Or(t => t.F_EName.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Category == 1);
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<TGameEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<TGameEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
              //  expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

	    public TGameEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(TGameEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(TGameEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }
    }
}