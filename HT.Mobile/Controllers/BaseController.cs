using HT.Model.Enum;
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

		/// <summary>
		/// 返回Json
		/// </summary>
		/// <param name="apiCode">ApiCode</param>
		/// <param name="msg">消息</param>
		/// <param name="data">业务数据</param>
		/// <returns></returns>
		public ActionResult JsonResult(APIErrCode apiCode,string msg="",dynamic data=null) {

			apiResp.code = (int)apiCode;
			if (apiCode== APIErrCode.Success)
			{
				apiResp.status = true;
			}
			apiResp.msg = msg;
			apiResp.result = data;
			return Json(apiResp, JsonRequestBehavior.AllowGet);

		}

	}
}