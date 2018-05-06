using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace HT.Utility
{
    public class JSONHelper
    {
       
        /// <summary>
        /// 对象序列化成json数据
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <returns>json数据</returns>
        public static string ObjectToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        
        /// <summary>
        /// 将json数据反序列化成对象
        /// </summary>
        /// <param name="json">json数据</param>
        /// <returns>对象</returns>
        public static object JsonToObject(string json)
        {
            JavaScriptSerializer jscvt = new JavaScriptSerializer();
            return jscvt.DeserializeObject(json);
        }
    }
}
