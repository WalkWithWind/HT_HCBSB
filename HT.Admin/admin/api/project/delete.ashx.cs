using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HT.Model;

namespace HT.Admin.admin.api.project
{
	/// <summary>
	/// 删除项目
	/// </summary>
	public class delete : BaseHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			string ids = context.Request["ids"];
			apiResp.status = HT.BLL.Admin.BLLProject.Delete(ids);
			if (apiResp.status)
			{
				apiResp.msg = "ok";
			}
			else
			{
				apiResp.msg = "删除失败";
			}
			context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));

		}

	}
}