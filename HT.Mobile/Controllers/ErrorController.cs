using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index(string msg = "您的权限不足")
        {
            ViewBag.msg = msg;
            return View();
        }
    }
}