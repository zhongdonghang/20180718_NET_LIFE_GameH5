using Maticsoft.DBUtility;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// <summary>
        /// 首页顶部
        /// </summary>
        /// <returns></returns>
        public static IndexTopReport ToIndexTopReport()
        {
            IndexTopReport obj = null;

            ICache objCache = CacheFactory.Cache();
            obj = objCache.GetCache<IndexTopReport>("IndexTopReport");
            if(obj==null)
            {
                obj = new IndexTopReport();
                string sql1 = "select count(distinct F_LogNo) from [T_GameLog]";
                string sql2 = "select count(distinct F_LogNo) from [T_GameLog]  where  DateDiff(dd,F_LogTime,getdate())=0  ";
                string sql3 = "select sum(F_Score) from [dbo].[T_GameLog]  where F_CoinType=2 and F_WinOrLost=2";
                string sql4 = "select sum(F_Score) from [dbo].[T_GameLog]  where F_CoinType=1 and F_WinOrLost=2";

                Object objPlayerAllCount = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql1, null).ToString();
                int PlayerAllCount = 0;
                if (int.TryParse(objPlayerAllCount.ToString(), out PlayerAllCount))
                {
                    obj.PlayerAllCount = PlayerAllCount;
                }

                Object objPlayerCountToday = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql2, null);
                int PlayerCountToday = 0;
                if (int.TryParse(objPlayerCountToday.ToString(), out PlayerCountToday))
                {
                    obj.PlayerCountToday = PlayerCountToday;
                }

                Object objTotalConsumeLbCount = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql3, null);
                int TotalConsumeLbCount = 0;
                if (int.TryParse(objTotalConsumeLbCount.ToString(), out TotalConsumeLbCount))
                {
                    obj.TotalConsumeLbCount = TotalConsumeLbCount;
                }

                Object objTotalConsumeLoveBirdCount = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, sql4, null);
                int TotalConsumeLoveBirdCount = 0;
                if (int.TryParse(objTotalConsumeLoveBirdCount.ToString(), out TotalConsumeLoveBirdCount))
                {
                    obj.TotalConsumeLoveBirdCount = TotalConsumeLoveBirdCount;
                }
                objCache.WriteCache<IndexTopReport>(obj, "IndexTopReport");
            }
            return obj;
        }

        public static RealTimeActivePlayerStatisticsReport ToIndexRealTimeActivePlayerStatisticsReport()
        {
            string time = DateTime.Now.ToShortDateString();
            RealTimeActivePlayerStatisticsReport report = new RealTimeActivePlayerStatisticsReport();
            report.items = new List<RealTimeActiveItem>();
            for (int i = 0; i <= 23; i++)
            {
                int beginTime = i;
                int endTime = i + 1;
                if (endTime == 24) endTime = 0;
                string sql = "select F_GameNo,COUNT(F_LBAccount) as playerCount from dbo.T_GameLog where F_LogTime >= '" + time + " "+beginTime+":00:00' and F_LogTime< '"+ time + " "+endTime+":00:00'  group by F_GameNo";
                using (SqlDataReader reader = SqlHelper.ExecuteReader(System.Data.CommandType.Text, sql, null))
                {
                    while (reader.Read())
                    {
                        RealTimeActiveItem item = new RealTimeActiveItem();
                        item.EGameName = reader["F_GameNo"].ToString();
                        item.PlayerCounts = reader["playerCount"].ToString();
                        item.HourString = endTime.ToString();
                        report.items.Add(item);
                    }
                }
            }
            return report;
        }
    }
}
