using NFine.Code;
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
    }
}
