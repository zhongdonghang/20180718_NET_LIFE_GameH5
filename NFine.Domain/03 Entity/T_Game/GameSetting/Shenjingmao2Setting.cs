using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.GameSetting
{
   public class Shenjingmao2Setting:GameSetting.SettingBase
    {
        /// <summary>
        /// 游戏模式：是否根据最高分来定输赢
        /// </summary>
        public bool IsWinWithHighest { get; set; }

        /// <summary>
        /// 游戏模式：如果不是根据最高分来定输赢，那根据这个分数来定输赢。
        /// </summary>
        public double WinLevelScore { get; set; }

    }
}
