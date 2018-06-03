﻿using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.user
{
    /// <summary>
    /// detail 的摘要说明
    /// </summary>
    public class detail : BaseHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request["id"];

            ht_user model = HT.BLL.Admin.BLLUser.GetUser(int.Parse(id));

            apiResp.msg = "查询完成";
            apiResp.status = true;
            apiResp.result = model;
            
            context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));

        }

    }
}