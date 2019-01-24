using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.T_Game
{
    /// <summary>
    /// 神经猫游戏规则
    /// </summary>
    public class SjmGameRegulation
    {
        public int FirstEnterGameLeastLB { get; set; }

        public int FirstEnterGameLeastLoveBird { get; set; }

        public int LoveBirdToGameScore { get; set; }

        public int LBToGameScore { get; set; }


    }
}
