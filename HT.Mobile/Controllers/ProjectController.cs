using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        //货源列表
        public ActionResult GoodsSource()
        {
            return View();
        }
        //货源详情
        public ActionResult GoodsSourceDetails()
        {
            return View();
        }

    }
}