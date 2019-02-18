using Maticsoft.DBUtility;
using NFine.Domain._03_Entity.T_Game.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.TGame
{
    /// <summary>
    /// 数据汇总
    /// </summary>
   public class DataReportApp
    {
        public IndexTopReport ToIndexTopReport()
        {
            IndexTopReport obj = new IndexTopReport();

            //   SqlHelper
            string sql1 = "select count(distinct F_LogNo) from [T_GameLog]";
            string sql2 = "select count(distinct F_LogNo) from [T_GameLog]  where  DateDiff(dd,F_LogTime,getdate())=0  ";
            string sql3 = "select sum(F_Score) from [dbo].[T_GameLog]  where F_CoinType=2 and F_WinOrLost=2";
            string sql4 = "select sum(F_Score) from [dbo].[T_GameLog]  where F_CoinType=1 and F_WinOrLost=2";

            obj.PlayerAllCount = (int)SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql1, null);
           obj.PlayerCountToday = (int) SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql2, null);
            SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql3, null);
            SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql4, null);


            return obj;
        }
    }
}
