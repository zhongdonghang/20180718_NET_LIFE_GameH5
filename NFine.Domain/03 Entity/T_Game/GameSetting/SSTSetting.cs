using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.GameSetting
{

    public class SSTWinLevel
    {
        public int WinCount { get; set; }

        public int WinScore { get; set; }
    }

    public class SSTLostsLevel
    {
        public int LostCount { get; set; }

        public int LostScore { get; set; }
    }

    /// <summary>
    /// 愚人节算术题配置
    /// </summary>
    public  class SSTSetting:SettingBase
    {
        public SSTWinLevel WinLevel1 { get; set; }

        public SSTWinLevel WinLevel2 { get; set; }

        public SSTLostsLevel LostLevel1 { get; set; }

        public SSTLostsLevel LostLevel2 { get; set; }

    }
}
