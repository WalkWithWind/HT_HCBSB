using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class HomeController : Controller
    {

        //首页
        [AllowAnonymous]
        [Authorize]
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