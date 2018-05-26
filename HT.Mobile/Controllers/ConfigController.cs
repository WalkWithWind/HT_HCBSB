using HT.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    /// <summary>
    /// 获取配置
    /// </summary>
    public class ConfigController : BaseController
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <returns></returns>
        public ActionResult Get(string configName)
        {
            return JsonResult(APIErrCode.Success, "OK",BLL.BLLConfig.Get(configName));
        }
    }
}