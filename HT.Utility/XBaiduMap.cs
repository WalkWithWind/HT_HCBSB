using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HT.Utility
{
    /// <summary>
    /// 百度地图
    /// </summary>
    public class XBaiduMap
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// 应用密钥
        /// </summary>
        private readonly string _ak;

        /// <summary>
        /// 加密私钥
        /// </summary>
        private readonly string _sk;

        /// <summary>
        /// 源坐标类型
        /// </summary>
        private readonly string _from;

        /// <summary>
        /// 转换坐标类型
        /// </summary>
        private readonly string _to;

        public XBaiduMap(string url, string ak, string sk) : this(url, ak, sk, "1", "5")
        {
        }

        public XBaiduMap(string url, string ak, string sk, string fromtype, string totype)
        {
            _url = url;
            _ak = ak;
            _sk = sk;
            _from = fromtype;
            _to = totype;
        }

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="longitude">原始经度</param>
        /// <param name="latitude">原始纬度</param>
        /// <param name="xlongitude">转化经度</param>
        /// <param name="xlatitude">转化纬度</param>
        /// <returns></returns>
        public void ConvertCoord(decimal longitude, decimal latitude, out decimal xlongitude, out decimal xlatitude)
        {
            xlongitude = 0;
            xlatitude = 0;
            string coord = longitude + "," + latitude;
            string url = string.Format("{0}?&coords={1}&ak={2}&from={3}&to={4}&output={5}", _url, coord, _ak, _from, _to,"json");
            string result = RequestUtil.Get(url);
            if (!string.IsNullOrEmpty(result))
            {
                dynamic resdata = JsonConvert.DeserializeObject<dynamic>(result);
                if (resdata != null)
                {
                    int status = resdata.status ?? -1;
                    if (status == 0)
                    {
                        JArray jarray = JArray.FromObject(resdata.result) ?? new JArray();
                        if (jarray.Count>0)
                        {
                            dynamic jitem = jarray.FirstOrDefault();
                            if (jitem!=null)
                            {
                                xlongitude = jitem.x ?? 0;
                                xlatitude = jitem.y ?? 0;
                            }
                        }
                    }
                }
            }
        }
    }
}
