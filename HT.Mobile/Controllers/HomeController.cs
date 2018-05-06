using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class HomeController : BaseController
    {
        //首页
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            

            return View();
        }
    }
}