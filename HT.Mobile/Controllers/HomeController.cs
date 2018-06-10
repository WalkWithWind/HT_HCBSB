using HT.BLL;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;

namespace HT.Mobile.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var token = BLL.BLLWeixin.AccessToken;
            ViewBag.FootActive = 0;
            return View();
        }
        /// <summary>
        /// 登陆页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 广搞列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult AdList(string code)
        {
            List<ht_ad> model = BLLAd.GetAdList(code);

            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(apiResp);
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult CateList(int cid)
        {
            List<ht_category> model = BLLCategory.GetCateList(cid);
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(apiResp);
        }

        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PinpaiList()
        {
            List<ht_pinpai> model = BLLPinpai.GetPinpaiList();
            if (Request.IsAjaxRequest())
            {
                apiResp.status = true;
                apiResp.result = model;
                return Json(apiResp);
            }
            return View(apiResp);
        }

        //生成二维码
        public ActionResult GetQrCode(string redirect, int size= 10)
        {
            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            Bitmap image = qrCodeEncoder.Encode(redirect);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            // 设置当前流的位置为流的开始    
            ms.Seek(0, SeekOrigin.Begin);
            string contentType = "image/jpeg";
            return File(ms, contentType);
        }

    }
}