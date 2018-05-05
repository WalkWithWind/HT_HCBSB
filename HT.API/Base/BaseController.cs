using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using HT.Model;
using HT.Utility;

namespace HT.API.Base
{
    /// <summary>
    /// API控制器基类
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// DB Entity
        /// </summary>
        public Entities db { get; private set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public SystemConfig sysconfig { get; private set; }

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public BaseController()
        {
            db = new Entities();
            sysconfig = new SystemConfig();
        }
    }
}
