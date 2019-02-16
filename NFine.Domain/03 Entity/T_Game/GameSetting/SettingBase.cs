using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.GameSetting
{
    /// <summary>
    /// 游戏配置基类
    /// </summary>
    public class SettingBase
    {
        /// <summary>
        /// 游戏名字
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// LB积分和游戏分数比例，即1个LB积分等于游戏的多少分数
        /// </summary>
        public double LBRatio { get; set; }

        /// <summary>
        /// LoveBird积分和游戏分数比例，即1个LoveBird积分等于游戏的多少分数
        /// </summary>
        public double LoveBirdRatio { get; set; }

        /// <summary>
        /// 最低入場的LB積分數量
        /// </summary>
        public double LowestPlayLB { get; set; }

        /// <summary>
        /// 最低入场的LoveBird积分数量
        /// </summary>
        public double LowestPlayLoveBird { get; set; }

        /// <summary>
        /// 税收比例，这里是一个数字
        /// </summary>
        public double Tax { get; set; }
    }
}
