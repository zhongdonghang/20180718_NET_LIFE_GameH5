using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.Report
{
    /// <summary>
    /// 后台首页顶部数据实体
    /// </summary>
   public class IndexTopReport
    {
        /// <summary>
        /// 游戏玩家总数量
        /// </summary>
        public int PlayerAllCount { get; set; }

        /// <summary>
        /// 当天玩家数量
        /// </summary>
        public int PlayerCountToday { get; set; }

        /// <summary>
        /// 累计消耗的LB数量
        /// </summary>
        public int TotalConsumeLbCount { get; set; }

        /// <summary>
        /// 累计消耗的LoveBird数量
        /// </summary>
        public int TotalConsumeLoveBirdCount { get; set; }
    }
}
