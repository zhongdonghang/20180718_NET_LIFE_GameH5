﻿using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.Games
{

    public class DataEntity
    {
        public string AllPlayerBestScore { get; set; }

        public string MeBestScore { get; set; }

        public string AllPlayerCount { get; set; }

        public string GameDesc { get; set; }

        public string ScoreRuleDesc { get; set; }
    }

    public class H5GameInterFaceController: Controller
    {
        public string GetGameDesc(string gameName,string LBAccount)
        {
            DataEntity o = new DataEntity();
            o.AllPlayerBestScore = "10000";
            o.MeBestScore = "9000";
            o.AllPlayerCount = "6666";
            o.GameDesc = "游戏介绍";
            o.ScoreRuleDesc = "积分规则介绍";
            return NFine.Code.Json.ToJson(o) ;
        }


    }
}