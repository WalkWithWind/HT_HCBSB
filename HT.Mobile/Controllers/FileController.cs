using HT.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HT.Mobile.Controllers
{
    public class FileController : BaseController
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {

            var file = Request.Files[0];
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string webDir = Server.MapPath("/");
            string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + new Random().Next(999999) + fileExtension;
            string filePath = string.Format("{0}\\Upload\\{1}", webDir, filename);
            file.SaveAs(filePath);
            return JsonResult(APIErrCode.Success, "OK", string.Format("/Upload/{0}", filename));

        }
    }
}