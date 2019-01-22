using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.Games
{
    public class GamesController : Controller
    {
        //
        // GET: /Games/Games/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shenjingmao2()
        {
            if (Request.Params["loginID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            else if (string.IsNullOrEmpty(Request.Params["loginID"]))
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }

            Session["loginID"] = Request.Params["loginID"];
            return new RedirectResult("/GameContent/shenjingmao2/index.html");
        }

        public void Shenjingmao2ResultHandle()
        {
            if (Session["loginID"] == null)
            {
                Response.Write("<html><head><title>系统提示</title><script>alert('请先登录');</script></head><body></body></html>");
                Response.End();
            }
            string scorc = Request.Params["score"];
            string LBAccount = Session["loginID"].ToString();

            //先判断玩家是否是第一次玩



        }
    }
}
