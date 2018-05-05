using System;

namespace HT.Admin.Models
{
    [Serializable]
    public class Menu
    {
        public int id { get; set; }
        public int parentid { get; set; }
        public int classlayer { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string iconurl { get; set; }
        public string linkurl { get; set; }
        public int isshow { get; set; }
        public int sortid { get; set; }
        public string actiontype { get; set; }
        public string remark { get; set; }
    }
}