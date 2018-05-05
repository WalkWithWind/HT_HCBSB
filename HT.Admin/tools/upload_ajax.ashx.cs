using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using HT.Admin.Models;
using HT.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HT.Admin.tools
{
    /// <summary>
    /// upload_ajax 的摘要说明
    /// </summary>
    public class UploadAjax : IHttpHandler, IRequiresSessionState
    {
        private readonly SystemConfig _config = new SystemConfig();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //取得操作类型
            string action = HTRequest.GetQueryString("action");
            switch (action)
            {
                case "EditorFile":
                    EditorFile(context);
                    break;
                case "ManagerFile":
                    ManageFile(context);
                    break;
                default:
                    UpLoadFile(context);
                    break;
            }
        }

        /// <summary>
        /// 普通上传
        /// </summary>
        /// <param name="context"></param>
        private void UpLoadFile(HttpContext context)
        {
            string delfile = HTRequest.GetString("DelFilePath");
            HttpPostedFile upfile = context.Request.Files["Filedata"];
            bool iswater = false; //默认不打水印
            bool isthumbnail = false; //默认不生成缩略图

            if (HTRequest.GetQueryString("IsWater") == "1")
                iswater = true;
            if (HTRequest.GetQueryString("IsThumbnail") == "1")
                isthumbnail = true;
            if (upfile == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            FileUploader uploader = new FileUploader();
            string msg = uploader.FileSaveAs(upfile, isthumbnail, iswater);
            string webpath = _config["webpath"];
            string filepath = _config["filepath"];
            if (!string.IsNullOrEmpty(delfile) && delfile.ToLower().StartsWith(webpath.ToLower() + filepath.ToLower()))
            {
                Utils.DeleteUpFile(delfile);
            }
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }

        /// <summary>
        /// 管理文件
        /// </summary>
        private void ManageFile(HttpContext context)
        {

            //根目录路径，相对路径
            String rootPath = _config["webpath"] + _config["filepath"] + "/"; //站点目录+上传目录
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = _config["webpath"] + _config["filepath"] + "/";
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath;
            String currentUrl;
            String currentDirPath;
            String moveupDirPath;

            String dirPath = Utils.GetMapPath(rootPath);
            //根据path参数，设置各路径和URL
            String path = context.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = context.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("Access is not allowed.");
                context.Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("Parameter is not valid.");
                context.Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                context.Response.Write("Directory does not exist.");
                context.Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            foreach (string item in dirList)
            {
                DirectoryInfo dir = new DirectoryInfo(item);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            foreach (string item in fileList)
            {
                FileInfo file = new FileInfo(item);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonConvert.SerializeObject(result));
            context.Response.End();
        }

        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return String.Compare(xInfo.FullName, yInfo.FullName, StringComparison.Ordinal);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return String.Compare(xInfo.Extension, yInfo.Extension, StringComparison.Ordinal);
            }
        }

        /// <summary>
        /// 编辑文件
        /// </summary>
        private void EditorFile(HttpContext context)
        {
            bool iswater = false;
            string water = HTRequest.GetQueryString("IsWater");
            if (water == "1")
            {
                iswater = true;
            }
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                context.Response.Write(BackInfo(1, "请选择要上传文件！"));
                return;
            }
            FileUploader file = new FileUploader();
            string remsg = file.FileSaveAs(imgFile, false, iswater);
            JObject json = JObject.Parse(remsg);
            string status = json["status"].ToString();
            string msg = json["msg"].ToString();
            if (status == "0")
            {
                context.Response.Write(BackInfo(1, msg));
                return;
            }
            string filepath = json["path"].ToString();
            context.Response.Write(BackInfo(0, "", filepath));
        }

        /// <summary>
        /// 通用返回消息son串
        /// </summary>
        /// <param name="error">状态</param>
        /// <param name="msg">错误消息</param>
        /// <param name="url">跳转地址</param>
        private string BackInfo(int error, string msg, string url = "")
        {
            var info = new
            {
                error,
                msg,
                url
            };
            return JsonConvert.SerializeObject(info);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}