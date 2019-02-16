using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game.GameSetting
{

    public class SaoLeiRule
    {
        public double Times { get; set; }
        public int Score { get; set; }
    }

   public class SaoLeiSetting:SettingBase
    {
        public int LostScore { get; set; }

        public SaoLeiRule Rule1 { get; set; }
        public SaoLeiRule Rule2 { get; set; }
        public SaoLeiRule Rule3 { get; set; }
    }
}
