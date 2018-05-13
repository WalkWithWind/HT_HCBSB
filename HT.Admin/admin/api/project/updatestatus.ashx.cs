using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HT.Admin.admin.api.project
{
	/// <summary>
	/// 更新审核状态
	/// </summary>
	public class updatestatus : BaseHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			int id = int.Parse(context.Request["id"]);
			int status = int.Parse(context.Request["status"]);
			apiResp.status = HT.BLL.Admin.BLLProject.UpdateStatus(id,status);
			if (apiResp.status)
			{
				apiResp.msg = "ok";
			}
			else
			{
				apiResp.msg = "操作失败";
			}
			context.Response.Write(HT.Utility.JSONHelper.ObjectToJson(apiResp));

		}

		
	}
}