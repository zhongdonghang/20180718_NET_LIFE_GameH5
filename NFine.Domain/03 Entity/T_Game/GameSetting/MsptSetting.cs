using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.GameSetting
{
    public class MsptRule
    {
        /// <summary>
        /// 关卡
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public int WinScore { get; set; }

        /// <summary>
        /// 失分
        /// </summary>
        public int LostScore { get; set; }
    }


    /// <summary>
    /// 美女拼图游戏设置
    /// </summary>
    public class MsptSetting:SettingBase
    {
        public MsptRule Rule1 { get; set; }
        public MsptRule Rule2 { get; set; }
        public MsptRule Rule3 { get; set; }
        public MsptRule Rule4 { get; set; }
        public MsptRule Rule5 { get; set; }
        public MsptRule Rule6 { get; set; }
        public MsptRule Rule7 { get; set; }
        public MsptRule Rule8 { get; set; }
        public MsptRule Rule9 { get; set; }
        public MsptRule Rule10 { get; set; } 

        public int ALlInScore { get; set; }
    }
}
