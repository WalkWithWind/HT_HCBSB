using HT.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HT.Mobile.Controllers
{
    [Filter.OAuthFilter]
    public class BaseController : Controller
    {
        public ApiResponse apiResp = new ApiResponse();

    }
}