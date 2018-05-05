using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Utility
{
    /// <summary>
    /// 日志通用类
    /// </summary>
    public sealed class LogUtils
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content">内容</param>
        public static void Write(string content)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\App_Log\" + DateTime.Now.ToString("yyyyMM") + @"\" + DateTime.Now.ToString("dd") + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string name = path + DateTime.Now.ToString("yyyyMMdd") + ".log";
                FileStream file = new FileStream(name, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(file, Encoding.UTF8);
                writer.WriteLine(content);
                writer.WriteLine();
                writer.Flush();
                writer.Close();
            }
            catch (Exception)
            {
                Write(content);
            }
        }
    }
}
