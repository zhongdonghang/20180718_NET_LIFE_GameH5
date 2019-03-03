using NFine.Application.TGame;
using NFine.Code;
using NFine.Domain._03_Entity.T_Game.GameDescEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.Games
{



    public class H5GameInterFaceController: Controller
    {
        public string GetGameDesc(string gameName,string LBAccount)
        {

            return NFine.Code.Json.ToJson(new TGameApp().GetGameInfo(gameName,LBAccount)) ;
        }


    }
}