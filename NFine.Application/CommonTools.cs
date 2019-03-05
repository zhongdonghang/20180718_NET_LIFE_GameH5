using Maticsoft.DBUtility;
using Newtonsoft.Json.Linq;
using NFine.Application.TGame;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.GameSetting;
using NFine.Domain.Entity.TGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application
{
    public class CommonTools
    {
        /// <summary>
        /// 查询当日账户限额
        /// </summary>
        /// <param name="LBAccount"></param>
        /// <returns></returns>
        public static bool CalcPersonGameMaxLoveBird(string LBAccount)
        {
            bool isTrue = false;
            string beginTime = DateTime.Now.ToShortDateString() + " 00:00:00";
            string endTime = DateTime.Now.ToShortDateString() + " 23:59:59";
            string sql = "select SUM(F_Score) from dbo.T_GameLog where F_WinOrLost=1 and F_LBAccount='"+ LBAccount + "' "+
                            " and F_LogTime> '"+ beginTime + "' and F_LogTime< '"+ endTime + "'";

            object resultObj = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql, null);
            double result = resultObj == null ? 0 : (double)resultObj;

            double PersonGameMaxLoveBird = double.Parse(System.Configuration.ConfigurationManager.AppSettings["PersonGameMaxLoveBird"].ToString());
            if (result > PersonGameMaxLoveBird)//如果超额了，就返回真
            {
                isTrue = true;
            }
            return isTrue;
        }

       



        /// <summary>
        /// 判断玩家是否持有足够的币来玩游戏
        /// </summary>
        /// <param name="bets">预计要消耗的数额</param>
        /// <param name="UserID">账户ID</param>
        /// <param name="type">1：LoveBird积分 2:LB积分</param>
        /// <returns></returns>
        public static bool CheckPlayerCoinToGame(string bets, string UserID,string type)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["CheckRemainingIntegral"].ToString();
            string parm = "userId="+UserID+ "&bets="+ bets + "&type="+ type + "";
            string responseString = HttpMethods.HttpPost(url, parm);
            return responseString.Contains("0");
        }

        /// <summary>
        /// 加减积分
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="comeSum">输赢的金额，赢为正数，如：“5”，输为负数，如：“-5”</param>
        /// <param name="type">积分类型 LoveBird积分：1 或 LB积分 ： 2</param>
        /// <param name="gameName"></param>
        /// <returns>游戏名称，如：“王者荣耀”</returns>
        public static bool GiveCoinToPlayer(string userId,string comeSum,string type,string gameName)
        {


            string url = System.Configuration.ConfigurationManager.AppSettings["ModifyRemainingIntegral"].ToString();
            string parm = "userId=" + userId + "&comeSum=" + comeSum + "&type=" + type + "&gameName="+gameName;
            string responseString = HttpMethods.HttpPost(url, parm);
            return responseString.Contains("0");
        }

        /// <summary>
        /// 游戏积分转换成钱包积分
        /// </summary>
        /// <param name="gameScore">游戏得分</param>
        /// <param name="coinType">消耗积分类型 1:LoveBird积分 2:LB积分</param>
        /// <param name="setting">游戏表里设置字段的json对象</param>
        /// <returns></returns>
        public static double GameScore2LifeScore(int gameScore,int coinType, JObject setting)
        {
            double LifeScore = 0;
            if (coinType == 2)
            {
                double LBRatio = double.Parse(setting["LBRatio"].ToString());
                //LB积分和游戏分数比例，即1个LB积分等于游戏的多少分数
                LifeScore = gameScore / LBRatio;
            }
            else if (coinType == 1)
            {
                double LoveBirdRatio = double.Parse(setting["LoveBirdRatio"].ToString());
                LifeScore = gameScore / LoveBirdRatio;
            }
            return LifeScore;
        }

    }
}
