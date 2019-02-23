using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.Report
{

    public class RealTimeActiveItem
    {
        public string GameName { get; set; }
        public string EGameName { get; set; }

        public string HourString { get; set; }

        public string PlayerCounts { get; set; }
    }

    /// <summary>
    /// 实时玩家数据统计
    /// </summary>
    public class RealTimeActivePlayerStatisticsReport
    {
        public List<RealTimeActiveItem> items { get; set; }
    }
}
