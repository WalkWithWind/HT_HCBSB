using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace HT.Utility
{
     public class FileUploader
    {
        readonly SystemConfig _config;
        private readonly int watermarktype;
        private readonly int watermarkposition;
        private readonly int watermarkimgquality;
        private readonly int watermarkfontsize;
        private readonly int imgmaxwidth;
        private readonly int imgmaxheight;
        private readonly int thumbnailwidth;
        private readonly int thumbnailheight;
        private readonly int watermarktransparency;
        public FileUploader()
        {
            _config = new SystemConfig();
            watermarktype = Convert.ToInt32(_config["watermarktype"]);
            watermarkposition = Convert.ToInt32(_config["watermarkposition"]);
            watermarkimgquality = Convert.ToInt32(_config["watermarkimgquality"]);
            watermarkfontsize = Convert.ToInt32(_config["watermarkfontsize"]);
            imgmaxwidth = Convert.ToInt32(_config["imgmaxwidth"]);
            imgmaxheight = Convert.ToInt32(_config["imgmaxheight"]);
            thumbnailwidth = Convert.ToInt32(_config["thumbnailwidth"]);
            thumbnailheight = Convert.ToInt32(_config["thumbnailheight"]);
            watermarktransparency = Convert.ToInt32(_config["watermarktransparency"]);
        }
        /// <summary>
        /// 裁剪图片并保存
        /// </summary>
        public bool CropSaveAs(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int x, int y)
        {
            string fileExt = Utils.GetFileExt(fileName); //文件扩展名，不含“.”
            if (!IsImage(fileExt))
            {
                return false;
            }
            string newFileDir = Utils.GetMapPath(newFileName.Substring(0, newFileName.LastIndexOf(@"/", StringComparison.Ordinal) + 1));
            //检查是否有该路径，没有则创建
            if (!Directory.Exists(newFileDir))
            {
                Directory.CreateDirectory(newFileDir);
            }
            try
            {
                string fileFullPath = Utils.GetMapPath(fileName);
                string toFileFullPath = Utils.GetMapPath(newFileName);
                return Thumbnail.MakeThumbnailImage(fileFullPath, toFileFullPath, 180, 180, cropWidth, cropHeight, x, y);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <returns>上传后文件信息</returns>
        public string FileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater)
        {
            try
            {
                string fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                int fileSize = postedFile.ContentLength; //获得文件大小，以字节为单位
                string fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1); //取得原文件名
                string newFileName = Utils.GetGuidString() + "." + fileExt; //随机生成新的文件名
                string newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名
                string upLoadPath = GetUpLoadPath(); //上传目录相对路径
                string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径
                string newFilePath = upLoadPath + newFileName; //上传后的路径
                string newThumbnailPath = upLoadPath + newThumbnailFileName; //上传后的缩略图路径
                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    return "{\"status\": 0, \"msg\": \"不允许上传" + fileExt + "类型的文件！\"}";
                }
                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, fileSize))
                {
                    return "{\"status\": 0, \"msg\": \"文件超过限制的大小！\"}";
                }
                //检查上传的物理路径是否存在，不存在则创建
                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }

                //保存文件
                postedFile.SaveAs(fullUpLoadPath + newFileName);
                //如果是图片，检查图片是否超出最大尺寸，是则裁剪
                if (IsImage(fileExt))
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newFileName,
                        imgmaxwidth, imgmaxheight);
                }
                //如果是图片，检查是否需要生成缩略图，是则生成
                if (IsImage(fileExt) && isThumbnail && thumbnailwidth > 0 && thumbnailheight > 0)
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newThumbnailFileName,
                        thumbnailwidth, thumbnailheight, "Cut");
                }
                else
                {
                    newThumbnailPath = newFilePath; //不生成缩略图则返回原图
                }
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt) && isWater)
                {
                    switch (watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                _config["watermarktext"], watermarkposition,
                                watermarkimgquality, _config["watermarkfont"], watermarkfontsize);
                            break;
                        case 2:
                            WaterMark.AddImageSignPic(newFilePath, newFilePath,
                                 _config["watermarkpic"], watermarkposition,
                                watermarkimgquality, watermarktransparency);
                            break;
                    }
                }
                //处理完毕，返回JOSN格式的文件信息
                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + fileName + "\", \"path\": \"" + newFilePath + "\", \"thumb\": \""
                    + newThumbnailPath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}";
            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }

        /// <summary>
        /// 保存远程文件到本地
        /// </summary>
        /// <param name="fileUri">URI地址</param>
        /// <returns>上传后的路径</returns>
        public string RemoteSaveAs(string fileUri)
        {
            WebClient client = new WebClient();
            string fileExt = fileUri.LastIndexOf(".", StringComparison.Ordinal) == -1 ? "gif" : Utils.GetFileExt(fileUri);
            string newFileName = Utils.GetGuidString() + "." + fileExt; //随机生成新的文件名
            string upLoadPath = GetUpLoadPath(); //上传目录相对路径
            string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径
            string newFilePath = upLoadPath + newFileName; //上传后的路径
            //检查上传的物理路径是否存在，不存在则创建
            if (!Directory.Exists(fullUpLoadPath))
            {
                Directory.CreateDirectory(fullUpLoadPath);
            }

            try
            {
                client.DownloadFile(fileUri, fullUpLoadPath + newFileName);
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt))
                {
                    switch (watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                _config["watermarktext"], watermarkposition,
                                watermarkimgquality, _config["watermarkfont"], watermarkfontsize);
                            break;
                        case 2:
                            WaterMark.AddImageSignPic(newFilePath, newFilePath, _config["watermarkpic"], watermarkposition,
                                watermarkimgquality, watermarktransparency);
                            break;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            client.Dispose();
            return newFilePath;
        }

        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        private string GetUpLoadPath()
        {
            string path = _config["webpath"] + _config["filepath"] + "/"; //站点目录+上传目录
            string filesave = _config["filesave"];
            switch (filesave)
            {
                case "1": //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;
                default: //按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string fileExt)
        {
            //判断是否开启水印
            if (watermarktype > 0)
            {
                //判断是否可以打水印的图片类型
                ArrayList al = new ArrayList { "bmp", "jpeg", "jpg", "png" };
                if (al.Contains(fileExt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string fileExt)
        {
            ArrayList al = new ArrayList { "bmp", "jpeg", "jpg", "gif", "png" };
            return al.Contains(fileExt.ToLower());
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        private bool CheckFileExt(string fileExt)
        {
            //检查危险文件
            string[] excExt = { "asp", "aspx", "ashx", "asa", "asmx", "asax", "php", "jsp", "htm", "html" };
            if (excExt.Any(t => String.Equals(t, fileExt, StringComparison.CurrentCultureIgnoreCase)))
            {
                return false;
            }
            JArray list = JArray.Parse(_config["fileextensions"].ToString());
            return list.Any(token => token.ToString().Equals(fileExt));
        }

        /// <summary>
        /// 检查文件大小是否合法
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        /// <param name="fileSize">文件大小(B)</param>
        private bool CheckFileSize(string fileExt, int fileSize)
        {
            //判断是否为图片文件
            if (IsImage(fileExt))
            {
                int size = Convert.ToInt32(_config["imgsize"]);
                if (size > 0 && fileSize > size * 1024)
                {
                    return false;
                }
            }
            else
            {
                int size = Convert.ToInt32(_config["attachsize"]);
                if (size > 0 && fileSize > size * 1024)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
